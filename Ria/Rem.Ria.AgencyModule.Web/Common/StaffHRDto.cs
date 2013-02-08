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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffHR class.
    /// </summary>
    public class StaffHRDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _confidentialNote;
        private LookupValueDto _employmentType;
        private SoftDeleteObservableCollection<StaffChecklistItemDto> _staffChecklist;
        private SoftDeleteObservableCollection<StaffEventDto> _staffEvents;
        private StaffNameDto _supervisorStaff;
        private string _titleName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the confidential note.
        /// </summary>
        /// <value>The confidential note.</value>
        [DataMember]
        public string ConfidentialNote
        {
            get { return _confidentialNote; }
            set { ApplyPropertyChange ( ref _confidentialNote, () => ConfidentialNote, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the employment.
        /// </summary>
        /// <value>The type of the employment.</value>
        [DataMember]
        public LookupValueDto EmploymentType
        {
            get { return _employmentType; }
            set { ApplyPropertyChange ( ref _employmentType, () => EmploymentType, value ); }
        }

        /// <summary>
        /// Gets or sets the staff checklist.
        /// </summary>
        /// <value>The staff checklist.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffChecklistItemDto> StaffChecklist
        {
            get { return _staffChecklist; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _staffChecklist, () => StaffChecklist, value ); }
        }

        /// <summary>
        /// Gets or sets the staff events.
        /// </summary>
        /// <value>The staff events.</value>
        [DataMember]
        public SoftDeleteObservableCollection<StaffEventDto> StaffEvents
        {
            get { return _staffEvents; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _staffEvents, () => StaffEvents, value ); }
        }

        /// <summary>
        /// Gets or sets the supervisor staff.
        /// </summary>
        /// <value>The supervisor staff.</value>
        [DataMember]
        public StaffNameDto SupervisorStaff
        {
            get { return _supervisorStaff; }
            set { ApplyPropertyChange ( ref _supervisorStaff, () => SupervisorStaff, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the title.
        /// </summary>
        /// <value>The name of the title.</value>
        [DataMember]
        public string TitleName
        {
            get { return _titleName; }
            set { ApplyPropertyChange ( ref _titleName, () => TitleName, value ); }
        }

        #endregion
    }
}
