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

using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.ClinicalCaseEditor
{
    /// <summary>
    /// Data transfer object for ClinicalCase class.
    /// </summary>
    [DataContract]
    public class ClinicalCaseDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private ClinicalCaseAdmissionDto _clinicalCaseAdmissionDto;
        private ClinicalCaseDischargeDto _clinicalCaseDischargeDto;
        private ClinicalCaseProfileDto _clinicalCaseProfileDto;
        private ClinicalCaseStatusDto _clinicalCaseStatusDto;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseDto"/> class.
        /// </summary>
        public ClinicalCaseDto ()
        {
            ClinicalCaseProfile = new ClinicalCaseProfileDto ();
            ClinicalCaseStatus = new ClinicalCaseStatusDto ();
            ClinicalCaseAdmission = new ClinicalCaseAdmissionDto ();
            ClinicalCaseDischarge = new ClinicalCaseDischargeDto ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the clinical case admission.
        /// </summary>
        /// <value>The clinical case admission.</value>
        [DataMember]
        public ClinicalCaseAdmissionDto ClinicalCaseAdmission
        {
            get { return _clinicalCaseAdmissionDto; }
            set
            {
                _clinicalCaseAdmissionDto = value;
                RaisePropertyChanged ( () => ClinicalCaseAdmission );
            }
        }

        /// <summary>
        /// Gets or sets the clinical case discharge.
        /// </summary>
        /// <value>The clinical case discharge.</value>
        [DataMember]
        public ClinicalCaseDischargeDto ClinicalCaseDischarge
        {
            get { return _clinicalCaseDischargeDto; }
            set
            {
                _clinicalCaseDischargeDto = value;
                RaisePropertyChanged ( () => ClinicalCaseDischarge );
            }
        }

        /// <summary>
        /// Gets or sets the clinical case profile.
        /// </summary>
        /// <value>The clinical case profile.</value>
        [DataMember]
        public ClinicalCaseProfileDto ClinicalCaseProfile
        {
            get { return _clinicalCaseProfileDto; }
            set
            {
                _clinicalCaseProfileDto = value;
                RaisePropertyChanged ( () => ClinicalCaseProfile );
            }
        }

        /// <summary>
        /// Gets or sets the clinical case status.
        /// </summary>
        /// <value>The clinical case status.</value>
        [DataMember]
        public ClinicalCaseStatusDto ClinicalCaseStatus
        {
            get { return _clinicalCaseStatusDto; }
            set
            {
                _clinicalCaseStatusDto = value;
                RaisePropertyChanged ( () => ClinicalCaseStatus );
            }
        }

        #endregion
    }
}
