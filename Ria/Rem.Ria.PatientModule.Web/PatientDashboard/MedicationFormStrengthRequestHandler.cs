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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Agatha.Common;
using Agatha.ServiceLayer;
using TerminologyService.WebService;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling medication form strength request.
    /// </summary>
    public class MedicationFormStrengthRequestHandler :
        RequestHandler<MedicationFormStrengthRequest, MedicationFormStrengthResponse>
    {
        #region Constants and Fields

        private readonly ITerminologyService _terminologyService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationFormStrengthRequestHandler"/> class.
        /// </summary>
        /// <param name="terminologyService">The terminology service.</param>
        public MedicationFormStrengthRequestHandler ( ITerminologyService terminologyService )
        {
            _terminologyService = terminologyService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Agatha.Common.Response"/></returns>
        public override Response Handle ( MedicationFormStrengthRequest request )
        {
            var concepts =
                _terminologyService.GetConceptByCodeSystemCodeWithAssociation ( request.SelectedConcept.CodedConceptCode, 1552, "has_ingredient" );
            TerminologyConcept mainConcept = null;
            var isIngredient = false;
            if ( concepts.Count () == 0 )
            {
                throw new Exception ( "Error drug not found." );
            }
            if ( concepts.ElementAt ( 0 ).Properties.FirstOrDefault ( p => p.Value == "BN" || p.Value == "IN" ) != null )
            {
                isIngredient = concepts.ElementAt ( 0 ).Properties.FirstOrDefault ( p => p.Value == "IN" ) != null;
                mainConcept = concepts.ElementAt ( 0 );
            }
            else if ( concepts.Count () > 1 )
            {
                mainConcept = concepts.ElementAt ( 1 );
            }
            else if ( concepts.ElementAt ( 0 ).Properties.FirstOrDefault ( p => p.Value == "SCD" ) != null )
            {
                concepts = _terminologyService.GetConceptByCodeSystemCodeWithAssociation ( concepts.ElementAt ( 0 ).Code, 1552, "isa" );
                if ( concepts.Count () < 2 )
                {
                    throw new Exception ( "Error drug not found." );
                }
                concepts = _terminologyService.GetConceptByCodeSystemCodeWithAssociation ( concepts.ElementAt ( 1 ).Code, 1552, "has_ingredient" );
                if ( concepts.Count () < 2 )
                {
                    throw new Exception ( "Error drug not found." );
                }
                mainConcept = concepts.ElementAt ( 1 );
                isIngredient = true;
            }
            else
            {
                throw new Exception ( "Error drug not found." );
            }

            var response = CreateTypedResponse ();
            response.DTSInfo = new MedicationDtsInfoDto ();
            if ( isIngredient )
            {
                var nameToSearch = mainConcept.DisplayName.Split ( ' ' )[0];
                IEnumerable<TerminologyConcept> allconcepts = _terminologyService.FindConceptsWithNameMatching (
                    nameToSearch,
                    mainConcept.VocabularyId,
                    "RxNorm Single Ing Drugs" );
                allconcepts = _terminologyService.FindConceptsByNames (
                    allconcepts.Select ( ac => ac.DisplayName ).ToArray (),
                    mainConcept.VocabularyId,
                    "constitutes",
                    "isa" );
                response.DTSInfo.Strengths =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SCDC" ) != null ) );
                response.DTSInfo.Drugs =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SCD" ) != null ) );

                response.DTSInfo.Forms =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SCDF" ) != null ) );
            }
            else
            {
                var allconcepts = _terminologyService.FindConceptAssociationsWithNameMatching (
                    mainConcept.DisplayName,
                    mainConcept.VocabularyId,
                    "has_ingredient",
                    "isa",
                    "constitutes" );

                response.DTSInfo.Strengths =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SBDC" ) != null ) );
                response.DTSInfo.Forms =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SBDF" ) != null ) );
                response.DTSInfo.Drugs =
                    new ObservableCollection<TerminologyConcept> (
                        allconcepts.Where ( concept => concept.Properties.FirstOrDefault ( p => p.Value == "SBD" ) != null ) );
            }

            response.MainCode = mainConcept;
            return response;
        }

        #endregion
    }
}
