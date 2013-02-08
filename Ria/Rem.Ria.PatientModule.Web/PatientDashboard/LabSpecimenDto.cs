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
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Data transfer object for LabSpecimen class.
    /// </summary>
    [DataContract]
    public class LabSpecimenDto : ActivityDto
    {
        #region Constants and Fields

        private bool? _collectedHereIndicator;
        private LabFacilityDto _labFacility;
        private DateTime? _labReceivedDate;
        private SoftDeleteObservableCollection<LabResultDto> _labResults;
        private LookupValueDto _labSpecimenType;
        private DateTime? _labTestDate;
        private LookupValueDto _labTestName;
        private string _labTestNote;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LabSpecimenDto"/> class.
        /// </summary>
        public LabSpecimenDto ()
        {
            LabResults = new SoftDeleteObservableCollection<LabResultDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the collected here indicator.
        /// </summary>
        /// <value>The collected here indicator.</value>
        [DataMember]
        public bool? CollectedHereIndicator
        {
            get { return _collectedHereIndicator; }
            set { ApplyPropertyChange ( ref _collectedHereIndicator, () => CollectedHereIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the lab facility.
        /// </summary>
        /// <value>The lab facility.</value>
        [DataMember]
        public LabFacilityDto LabFacility
        {
            get { return _labFacility; }
            set { ApplyPropertyChange ( ref _labFacility, () => LabFacility, value ); }
        }

        /// <summary>
        /// Gets or sets the lab received date.
        /// </summary>
        /// <value>The lab received date.</value>
        [DataMember]
        public DateTime? LabReceivedDate
        {
            get { return _labReceivedDate; }
            set { ApplyPropertyChange ( ref _labReceivedDate, () => LabReceivedDate, value ); }
        }

        /// <summary>
        /// Gets or sets the lab results.
        /// </summary>
        /// <value>The lab results.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LabResultDto> LabResults
        {
            get { return _labResults; }
            set { ApplyPropertyChange ( ref _labResults, () => LabResults, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the lab specimen.
        /// </summary>
        /// <value>The type of the lab specimen.</value>
        [DataMember]
        public LookupValueDto LabSpecimenType
        {
            get { return _labSpecimenType; }
            set { ApplyPropertyChange ( ref _labSpecimenType, () => LabSpecimenType, value ); }
        }

        /// <summary>
        /// Gets or sets the lab test date.
        /// </summary>
        /// <value>The lab test date.</value>
        [DataMember]
        public DateTime? LabTestDate
        {
            get { return _labTestDate; }
            set { ApplyPropertyChange ( ref _labTestDate, () => LabTestDate, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the lab test.
        /// </summary>
        /// <value>The name of the lab test.</value>
        [DataMember]
        public LookupValueDto LabTestName
        {
            get { return _labTestName; }
            set { ApplyPropertyChange ( ref _labTestName, () => LabTestName, value ); }
        }

        /// <summary>
        /// Gets or sets the lab test note.
        /// </summary>
        /// <value>The lab test note.</value>
        [DataMember]
        public string LabTestNote
        {
            get { return _labTestNote; }
            set { ApplyPropertyChange ( ref _labTestNote, () => LabTestNote, value ); }
        }

        #endregion
    }
}
