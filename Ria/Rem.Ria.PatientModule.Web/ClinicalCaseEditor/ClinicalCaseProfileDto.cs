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
using System.Runtime.Serialization;
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.ClinicalCaseEditor
{
    /// <summary>
    /// Data transfer object for ClinicalCaseProfile class.
    /// </summary>
    public class ClinicalCaseProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private long _clinicalCaseNumber;
        private DateTime? _clinicalCaseStartDate;
        private LookupValueDto _initialContactMethod;
        private LocationDisplayNameDto _initialLocation;
        private string _patientFullname;
        private long _patientKey;
        private string _patientPresentingProblemNote;
        private StaffNameDto _performedByStaff;
        private SoftDeleteObservableCollection<LookupValueDto> _priorityPopulations;
        private LookupValueDto _referralType;
        private SoftDeleteObservableCollection<ClinicalCaseSignedCommentDto> _signedComments;
        private SoftDeleteObservableCollection<LookupValueDto> _specialInitiatives;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseProfileDto"/> class.
        /// </summary>
        public ClinicalCaseProfileDto ()
        {
            _signedComments = new SoftDeleteObservableCollection<ClinicalCaseSignedCommentDto> ();
            _specialInitiatives = new SoftDeleteObservableCollection<LookupValueDto> ();
            _priorityPopulations = new SoftDeleteObservableCollection<LookupValueDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the clinical case number.
        /// </summary>
        /// <value>The clinical case number.</value>
        [DataMember]
        public long ClinicalCaseNumber
        {
            get { return _clinicalCaseNumber; }
            set { ApplyPropertyChange ( ref _clinicalCaseNumber, () => ClinicalCaseNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the clinical case start date.
        /// </summary>
        /// <value>The clinical case start date.</value>
        [DataMember]
        public DateTime? ClinicalCaseStartDate
        {
            get { return _clinicalCaseStartDate; }
            set { ApplyPropertyChange ( ref _clinicalCaseStartDate, () => ClinicalCaseStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the initial contact method.
        /// </summary>
        /// <value>The initial contact method.</value>
        [DataMember]
        public LookupValueDto InitialContactMethod
        {
            get { return _initialContactMethod; }
            set { ApplyPropertyChange ( ref _initialContactMethod, () => InitialContactMethod, value ); }
        }

        /// <summary>
        /// Gets or sets the initial location.
        /// </summary>
        /// <value>The initial location.</value>
        [DataMember]
        public LocationDisplayNameDto InitialLocation
        {
            get { return _initialLocation; }
            set { ApplyPropertyChange ( ref _initialLocation, () => InitialLocation, value ); }
        }

        /// <summary>
        /// Gets or sets the full name of the patient.
        /// </summary>
        /// <value>The full name of the patient.</value>
        [DataMember]
        public string PatientFullName
        {
            get { return _patientFullname; }
            set { ApplyPropertyChange ( ref _patientFullname, () => PatientFullName, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey
        {
            get { return _patientKey; }
            set { ApplyPropertyChange ( ref _patientKey, () => PatientKey, value ); }
        }

        /// <summary>
        /// Gets or sets the patient presenting problem note.
        /// </summary>
        /// <value>The patient presenting problem note.</value>
        [DataMember]
        public string PatientPresentingProblemNote
        {
            get { return _patientPresentingProblemNote; }
            set { ApplyPropertyChange ( ref _patientPresentingProblemNote, () => PatientPresentingProblemNote, value ); }
        }

        /// <summary>
        /// Gets or sets the performed by staff.
        /// </summary>
        /// <value>The performed by staff.</value>
        [DataMember]
        public StaffNameDto PerformedByStaff
        {
            get { return _performedByStaff; }
            set { ApplyPropertyChange ( ref _performedByStaff, () => PerformedByStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the priority populations.
        /// </summary>
        /// <value>The priority populations.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LookupValueDto> PriorityPopulations
        {
            get { return _priorityPopulations; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _priorityPopulations, () => PriorityPopulations, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the referral.
        /// </summary>
        /// <value>The type of the referral.</value>
        [DataMember]
        public LookupValueDto ReferralType
        {
            get { return _referralType; }
            set { ApplyPropertyChange ( ref _referralType, () => ReferralType, value ); }
        }

        /// <summary>
        /// Gets or sets the signed comments.
        /// </summary>
        /// <value>The signed comments.</value>
        [DataMember]
        public SoftDeleteObservableCollection<ClinicalCaseSignedCommentDto> SignedComments
        {
            get { return _signedComments; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _signedComments, () => SignedComments, value ); }
        }

        /// <summary>
        /// Gets or sets the special initiatives.
        /// </summary>
        /// <value>The special initiatives.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LookupValueDto> SpecialInitiatives
        {
            get { return _specialInitiatives; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _specialInitiatives, () => SpecialInitiatives, value ); }
        }

        #endregion
    }
}
