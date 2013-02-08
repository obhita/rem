#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using NHibernate;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.CdsRuleService
{
    /// <summary>
    /// Class for handling check CDS rules request.
    /// </summary>
    public class CheckCdsRulesRequestHandler :
        NHibernateSessionRequestHandler<CheckCdsRulesRequest, CheckCdsRulesResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( CheckCdsRulesRequest request )
        {
            var response = CreateTypedResponse ();
            var alerts = new List<PatientAlertDto> ();

            var cdsRules = Session.QueryOver<CdsRule> ().List ();

            if ( cdsRules.Count > 0 )
            {
                var query = string.Empty;
                for ( var i = 0; i < cdsRules.Count; i++ )
                {
                    query +=
                        string.Format (
                            "select PatientModule.PatientAgeMedicationProblemLabExists(:PatientKey{0},:CutOffPatientBirthDate{0},:RootMedicationCode{0},:ProblemCodeCodedConceptCode{0},:LabTestTypeCodedConceptCode{0},:ValidLabOrderMonthCount{0})\n",
                            i );
                }
                var rulesQuery = Session.CreateSQLQuery ( query );
                for ( var i = 0; i < cdsRules.Count; i++ )
                {
                    var cdsRule = cdsRules[i];
                    var ageDateEnd = DateTime.Now.Date.AddYears ( -1 * cdsRule.Age.Value );
                    var ageDateStart = ageDateEnd.AddYears ( -1 );
                    var ageDate = ageDateStart;
                    rulesQuery.SetParameter ( "PatientKey" + i, request.PatientKey ).SetParameter ( "CutOffPatientBirthDate" + i, ageDate )
                        .SetParameter ( "RootMedicationCode" + i, cdsRule.MedicationCodedConcept.CodedConceptCode ).SetParameter (
                            "ProblemCodeCodedConceptCode" + i, cdsRule.ProblemCodedConcept.CodedConceptCode ).SetParameter (
                                "LabTestTypeCodedConceptCode" + i, cdsRule.LabTestName.CodedConceptCode ).SetParameter (
                                    "ValidLabOrderMonthCount" + i, cdsRule.ValidLabOrderMonthCount );
                }
                var patient = Session.Get<Patient> ( request.PatientKey );

                var results = rulesQuery.List ();

                if ( results.Count == cdsRules.Count )
                {
                    for ( var i = 0; i < cdsRules.Count; i++ )
                    {
                        var cdsRule = cdsRules[i];
                        var hasPatientAlert = patient.Alerts.FirstOrDefault ( a => a.CdsIdentifier == cdsRule.Key.ToString () ) != null;
                        var patientExists = ( bool )results[i];
                        if ( patientExists )
                        {
                            if ( !hasPatientAlert )
                            {
                                //PatientAlert patientAlert = patient.AddAlert ( "Fasting Blood Glucose Needed", "It is recommended that the clinician order a fasting blood glucose test.", "12345" );
                                var patientAlert = patient.AddAlert ( cdsRule.Name, cdsRule.RecommendationNote, cdsRule.Key.ToString () );
                                var patientAlertDto = Mapper.Map<PatientAlert, PatientAlertDto> ( patientAlert );
                                alerts.Add ( patientAlertDto );
                            }
                        }
                        else if ( hasPatientAlert )
                        {
                            patient.RemoveAlert ( patient.Alerts.FirstOrDefault ( a => a.CdsIdentifier == cdsRule.Key.ToString () ) );
                        }
                    }
                }

                //TODO: What do when number of results, not equal to number of cds rules ???
            }

            response.Alerts = alerts;
            return response;
        }

        #endregion
    }
}
