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
using System.Collections.ObjectModel;
using System.Linq;
using Agatha.Common;
using NHibernate;
using NHibernate.Criterion;
using Rem.Domain.Clinical.PatientModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using VisitStatus = Rem.WellKnownNames.VisitModule.VisitStatus;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling get growth information by patient key request.
    /// </summary>
    public class GetGrowthInformationByPatientKeyRequestHandler :
        NHibernateSessionRequestHandler<GetGrowthInformationByPatientKeyRequest, GetGrowthInformationByPatientKeyResponse>
    {
        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( GetGrowthInformationByPatientKeyRequest request )
        {
            var key = request.Key;

            var criteria = Session.CreateCriteria<VitalSign> ( "vs" )
                .CreateAlias ( "Visit", "v" )
                .CreateAlias ( "v.ClinicalCase", "cc" )
                .CreateAlias ( "cc.Patient", "p" )
                .CreateAlias ( "v.VisitStatus", "vst" )
                .Add ( Restrictions.Eq ( "p.Key", key ) )
                .Add ( Restrictions.Eq ( "vst.WellKnownName", VisitStatus.CheckedIn ) );

            var vitalSigns = criteria.List ();

            var growthInfoDto = new GrowthInfoDto ();
            if ( vitalSigns.Count > 0 )
            {
                var patient = ( ( VitalSign )vitalSigns[0] ).Visit.ClinicalCase.Patient;
                if ( patient.Profile.PatientGender == null )
                {
                    throw new Exception ( "Patient has no gender specified." );
                }
                if ( !patient.Profile.BirthDate.HasValue )
                {
                    throw new Exception ( "Patient has no birth date specified." );
                }
                var heights = new Dictionary<double, double> ();
                var weights = new Dictionary<double, double> ();
                foreach ( VitalSign vitalSign in vitalSigns )
                {
                    var ageAtVisit = CalculateAge ( patient.Profile.BirthDate.Value, vitalSign.Visit.CheckedInDateTime.Value );
                    if ( ageAtVisit <= 20 )
                    {
                        double heightValue = 0.0, widthValue = 0.0;
                        if ( vitalSign.Height != null )
                        {
                            if ( vitalSign.Height.FeetMeasure.HasValue && vitalSign.Height.InchesMeasure.HasValue )
                            {
                                heightValue = ( vitalSign.Height.FeetMeasure.Value * 12.0 ) + vitalSign.Height.InchesMeasure.Value;
                            }
                            else if ( vitalSign.Height.FeetMeasure.HasValue )
                            {
                                heightValue = ( vitalSign.Height.FeetMeasure.Value * 12.0 );
                            }
                            else if ( vitalSign.Height.InchesMeasure.HasValue )
                            {
                                heightValue = vitalSign.Height.InchesMeasure.Value;
                            }
                        }
                        if ( vitalSign.WeightLbsMeasure.HasValue )
                        {
                            widthValue = vitalSign.WeightLbsMeasure.Value;
                        }
                        if ( heightValue != 0.0 )
                        {
                            if ( heights.ContainsKey ( ageAtVisit ) )
                            {
                                heights[ageAtVisit] = heightValue;
                            }
                            else
                            {
                                heights.Add ( ageAtVisit, heightValue );
                            }
                        }
                        if ( widthValue != 0.0 )
                        {
                            if ( weights.ContainsKey ( ageAtVisit ) )
                            {
                                weights[ageAtVisit] = widthValue;
                            }
                            else
                            {
                                weights.Add ( ageAtVisit, widthValue );
                            }
                        }
                    }
                }

                growthInfoDto.Gender = new LookupValueDto
                    {
                        Key = patient.Profile.PatientGender.Key,
                        WellKnownName = patient.Profile.PatientGender.WellKnownName,
                        ShortName = patient.Profile.PatientGender.ShortName,
                        Name = patient.Profile.PatientGender.Name
                    };
                growthInfoDto.GrowthRateHeightDtos =
                    new ObservableCollection<GrowthRateHeightDto> (
                        heights.Select ( h => new GrowthRateHeightDto ( h.Key, h.Value ) ).OrderBy ( h => h.Age ) );
                growthInfoDto.GrowthRateWeightDtos =
                    new ObservableCollection<GrowthRateWeightDto> (
                        weights.Select ( h => new GrowthRateWeightDto ( h.Key, h.Value ) ).OrderBy ( h => h.Age ) );
            }
            else
            {
                var patientQuery = Session.CreateCriteria<Patient> ().Add ( Restrictions.Eq ( "Key", key ) );
                var patientList = patientQuery.List ();

                if ( patientList.Count > 0 )
                {
                    var patient = patientList[0] as Patient;
                    if ( patient != null )
                    {
                        if ( patient.Profile.PatientGender == null )
                        {
                            throw new Exception ( "Patient has no gender specified." );
                        }
                        growthInfoDto.Gender = new LookupValueDto
                            {
                                Key = patient.Profile.PatientGender.Key,
                                WellKnownName = patient.Profile.PatientGender.WellKnownName,
                                ShortName = patient.Profile.PatientGender.ShortName,
                                Name = patient.Profile.PatientGender.Name
                            };
                    }
                }
                else
                {
                    throw new ArgumentException ( string.Format ( "Patient with key: {0} does not exist.", key ) );
                }
                growthInfoDto.GrowthRateHeightDtos = new ObservableCollection<GrowthRateHeightDto> ();
                growthInfoDto.GrowthRateWeightDtos = new ObservableCollection<GrowthRateWeightDto> ();
            }
            var response = new GetGrowthInformationByPatientKeyResponse { GrowthInfoDto = growthInfoDto };

            return response;
        }

        #endregion

        #region Methods

        private static double CalculateAge ( DateTime birthDate, DateTime now )
        {
            var dif = now - birthDate;
            var age = dif.TotalDays / 365.242199;
            return age;
        }

        #endregion
    }
}
