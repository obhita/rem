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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.PatientEditor
{
    /// <summary>
    /// Data transfer object for Allergy class.
    /// </summary>
    [DataContract]
    public partial class AllergyDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private CodedConceptDto _allergenCodedConcept;
        private ObservableCollection<LookupValueDto> _allergyReactions;
        private LookupValueDto _allergySeverityType;
        private LookupValueDto _allergyStatus;
        private LookupValueDto _allergyType;
        private DateTime? _onsetEndDate;
        private DateTime? _onsetStartDate;
        private long _patientKey;
        private long _provenanceKey;


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AllergyDto"/> class.
        /// </summary>
        public AllergyDto ()
        {
            _allergyReactions = new ObservableCollection<LookupValueDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets all reaction descriptions.
        /// </summary>
        public string AllReactionDescriptions
        {
            get { return string.Join ( ", ", _allergyReactions.Select ( p => p.Name ) ); }
        }

        /// <summary>
        /// Gets or sets the allergen coded concept.
        /// </summary>
        /// <value>The allergen coded concept.</value>
        [DataMember]
        public CodedConceptDto AllergenCodedConcept
        {
            get { return _allergenCodedConcept; }
            set { ApplyPropertyChange ( ref _allergenCodedConcept, () => AllergenCodedConcept, value ); }
        }

        /// <summary>
        /// Gets or sets the allergy reactions.
        /// </summary>
        /// <value>The allergy reactions.</value>
        [DataMember]
        public ObservableCollection<LookupValueDto> AllergyReactions
        {
            get { return _allergyReactions; }
            set { ApplyCollectionChange ( ref _allergyReactions, () => AllergyReactions, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the allergy severity.
        /// </summary>
        /// <value>The type of the allergy severity.</value>
        [DataMember]
        public LookupValueDto AllergySeverityType
        {
            get { return _allergySeverityType; }
            set { ApplyPropertyChange ( ref _allergySeverityType, () => AllergySeverityType, value ); }
        }

        /// <summary>
        /// Gets or sets the allergy status.
        /// </summary>
        /// <value>The allergy status.</value>
        [DataMember]
        public LookupValueDto AllergyStatus
        {
            get { return _allergyStatus; }
            set { ApplyPropertyChange ( ref _allergyStatus, () => AllergyStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the allergy.
        /// </summary>
        /// <value>The type of the allergy.</value>
        [DataMember]
        public LookupValueDto AllergyType
        {
            get { return _allergyType; }
            set { ApplyPropertyChange ( ref _allergyType, () => AllergyType, value ); }
        }

        /// <summary>
        /// Gets or sets the onset end date.
        /// </summary>
        /// <value>The onset end date.</value>
        [DataMember]
        public DateTime? OnsetEndDate
        {
            get { return _onsetEndDate; }
            set { ApplyPropertyChange ( ref _onsetEndDate, () => OnsetEndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the onset start date.
        /// </summary>
        /// <value>The onset start date.</value>
        [DataMember]
        public DateTime? OnsetStartDate
        {
            get { return _onsetStartDate; }
            set { ApplyPropertyChange ( ref _onsetStartDate, () => OnsetStartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>The patient key.</value>
        [DataMember]
        public long PatientKey
        {
            get { return _patientKey; }
            set
            {
                _patientKey = value;
                RaisePropertyChanged ( () => PatientKey );
            }
        }

        /// <summary>
        /// Gets or sets the provenance key.
        /// </summary>
        /// <value>
        /// The provenance key.
        /// </value>
        [DataMember]
        public long ProvenanceKey
        {
            get { return _provenanceKey; }
            set { ApplyPropertyChange(ref _provenanceKey, () => ProvenanceKey, value); }
        }

        #endregion
    }
}
