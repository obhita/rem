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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Data transfer object for GpraSocialConnectedness class.
    /// </summary>
    public class GpraSocialConnectednessDto : GpraDtoBase
    {
        #region Constants and Fields

        private GpraNonResponseTypeDto<int?> _attendOtherGroupsCount;
        private GpraNonResponseTypeDto<bool?> _attendOtherGroupsIndicator;
        private GpraNonResponseTypeDto<int?> _attendReligiousGroupsCount;
        private GpraNonResponseTypeDto<bool?> _attendReligiousGroupsIndicator;
        private GpraNonResponseTypeDto<int?> _attendVoluntaryGroupsCount;
        private GpraNonResponseTypeDto<bool?> _attendVoluntaryGroupsIndicator;
        private GpraNonResponseTypeDto<LookupValueDto> _gpraTroubleContact;
        private string _gpraTroubleContactSpecificationNote;
        private GpraNonResponseTypeDto<bool?> _interactFamilyFriendsIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the attend other groups count.
        /// </summary>
        /// <value>The attend other groups count.</value>
        public GpraNonResponseTypeDto<int?> AttendOtherGroupsCount
        {
            get { return _attendOtherGroupsCount; }
            set { ApplyPropertyChange ( ref _attendOtherGroupsCount, () => AttendOtherGroupsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the attend other groups indicator.
        /// </summary>
        /// <value>The attend other groups indicator.</value>
        public GpraNonResponseTypeDto<bool?> AttendOtherGroupsIndicator
        {
            get { return _attendOtherGroupsIndicator; }
            set { ApplyPropertyChange ( ref _attendOtherGroupsIndicator, () => AttendOtherGroupsIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the attend religious groups count.
        /// </summary>
        /// <value>The attend religious groups count.</value>
        public GpraNonResponseTypeDto<int?> AttendReligiousGroupsCount
        {
            get { return _attendReligiousGroupsCount; }
            set { ApplyPropertyChange ( ref _attendReligiousGroupsCount, () => AttendReligiousGroupsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the attend religious groups indicator.
        /// </summary>
        /// <value>The attend religious groups indicator.</value>
        public GpraNonResponseTypeDto<bool?> AttendReligiousGroupsIndicator
        {
            get { return _attendReligiousGroupsIndicator; }
            set { ApplyPropertyChange ( ref _attendReligiousGroupsIndicator, () => AttendReligiousGroupsIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the attend voluntary groups count.
        /// </summary>
        /// <value>The attend voluntary groups count.</value>
        public GpraNonResponseTypeDto<int?> AttendVoluntaryGroupsCount
        {
            get { return _attendVoluntaryGroupsCount; }
            set { ApplyPropertyChange ( ref _attendVoluntaryGroupsCount, () => AttendVoluntaryGroupsCount, value ); }
        }

        /// <summary>
        /// Gets or sets the attend voluntary groups indicator.
        /// </summary>
        /// <value>The attend voluntary groups indicator.</value>
        public GpraNonResponseTypeDto<bool?> AttendVoluntaryGroupsIndicator
        {
            get { return _attendVoluntaryGroupsIndicator; }
            set { ApplyPropertyChange ( ref _attendVoluntaryGroupsIndicator, () => AttendVoluntaryGroupsIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra trouble contact.
        /// </summary>
        /// <value>The gpra trouble contact.</value>
        public GpraNonResponseTypeDto<LookupValueDto> GpraTroubleContact
        {
            get { return _gpraTroubleContact; }
            set { ApplyPropertyChange ( ref _gpraTroubleContact, () => GpraTroubleContact, value ); }
        }

        /// <summary>
        /// Gets or sets the gpra trouble contact specification note.
        /// </summary>
        /// <value>The gpra trouble contact specification note.</value>
        public string GpraTroubleContactSpecificationNote
        {
            get { return _gpraTroubleContactSpecificationNote; }
            set { ApplyPropertyChange ( ref _gpraTroubleContactSpecificationNote, () => GpraTroubleContactSpecificationNote, value ); }
        }

        /// <summary>
        /// Gets or sets the interact family friends indicator.
        /// </summary>
        /// <value>The interact family friends indicator.</value>
        public GpraNonResponseTypeDto<bool?> InteractFamilyFriendsIndicator
        {
            get { return _interactFamilyFriendsIndicator; }
            set { ApplyPropertyChange ( ref _interactFamilyFriendsIndicator, () => InteractFamilyFriendsIndicator, value ); }
        }

        #endregion
    }
}
