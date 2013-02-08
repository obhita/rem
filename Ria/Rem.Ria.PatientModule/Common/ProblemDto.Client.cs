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
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for Problem class.
    /// </summary>
    public partial class ProblemDto : ICopyableDto, IActiveDto, IDtsSearchResultDto
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProblemDto"/> class.
        /// </summary>
        /// <param name="problemDto">The problem dto.</param>
        public ProblemDto ( ProblemDto problemDto )
            : base ( problemDto )
        {
            _clinicalCaseKey = problemDto._clinicalCaseKey;

            if ( problemDto._problemStatus != null )
            {
                _problemStatus = new LookupValueDto ();
                var diagnosisStatus = problemDto._problemStatus;
                _problemStatus.WellKnownName = diagnosisStatus.WellKnownName;
                _problemStatus.Key = diagnosisStatus.Key;
                _problemStatus.Name = diagnosisStatus.Name;
            }

            if ( problemDto._problemCodeCodedConcept != null )
            {
                _problemCodeCodedConcept = new CodedConceptDto ();
                var problemCode = problemDto._problemCodeCodedConcept;
                _problemCodeCodedConcept.CodedConceptCode = problemCode.CodedConceptCode;
                _problemCodeCodedConcept.Key = problemCode.Key;
                _problemCodeCodedConcept.CodeSystemIdentifier = problemCode.CodeSystemIdentifier;
                _problemCodeCodedConcept.CodeSystemName = problemCode.CodeSystemName;
                _problemCodeCodedConcept.CodeSystemVersionNumber = problemCode.CodeSystemVersionNumber;
                _problemCodeCodedConcept.DisplayName = problemCode.DisplayName;
                _problemCodeCodedConcept.NullFlavorIndicator = problemCode.NullFlavorIndicator;
            }

            if ( problemDto._problemType != null )
            {
                _problemType = new LookupValueDto ();
                var diagnosisType = problemDto._problemType;
                _problemType.WellKnownName = diagnosisType.WellKnownName;
                _problemType.Key = diagnosisType.Key;
                _problemType.Name = diagnosisType.Name;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the coded concept.
        /// </summary>
        public CodedConceptDto CodedConcept
        {
            get { return ProblemCodeCodedConcept; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                if ( ProblemStatus != null && ProblemStatus.WellKnownName == WellKnownNames.ClinicalCaseModule.ProblemStatus.Active )
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        public string SelectedText
        {
            get
            {
                if ( ProblemCodeCodedConcept != null )
                {
                    return ProblemCodeCodedConcept.DisplayName;
                }
                return null;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns>A <see cref="System.Object"/></returns>
        public object Copy ()
        {
            return new ProblemDto ( this );
        }

        #endregion
    }
}
