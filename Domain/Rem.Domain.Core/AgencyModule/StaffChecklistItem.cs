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

using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffChecklistItem defines a checklist of items to be completed.
    /// </summary>
    public class StaffChecklistItem : StaffAggregateNodeBase
    {
        private bool _checkedIndicator;
        private StaffChecklistItemType _staffChecklistItemType;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffChecklistItem"/> class.
        /// </summary>
        protected internal StaffChecklistItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffChecklistItem"/> class.
        /// </summary>
        /// <param name="staffChecklistItemType">Type of the staff checklist item.</param>
        protected internal StaffChecklistItem(StaffChecklistItemType staffChecklistItemType)
        {
            Check.IsNotNull(staffChecklistItemType, () => StaffChecklistItemType);

            _staffChecklistItemType = staffChecklistItemType;
            _checkedIndicator = false;
        }

        /// <summary>
        /// Gets the type of the staff checklist item.
        /// </summary>
        /// <value>
        /// The type of the staff checklist item.
        /// </value>
        [NotNull]
        public virtual StaffChecklistItemType StaffChecklistItemType
        {
            get { return _staffChecklistItemType; }
            private set { ApplyPropertyChange(ref _staffChecklistItemType, () => StaffChecklistItemType, value); }
        }

        /// <summary>
        /// Gets a value indicating whether [checked indicator] the item has been completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [checked indicator]; otherwise, <c>false</c>.
        /// </value>
        [NotNull]
        public virtual bool CheckedIndicator
        {
            get { return _checkedIndicator; }
            private set { ApplyPropertyChange(ref _checkedIndicator, () => CheckedIndicator, value); }
        }

        /// <summary>
        /// Revises the type of the checklist item.
        /// </summary>
        /// <param name="staffChecklistItemType">Type of the staff checklist item.</param>
        public virtual void ReviseChecklistItemType(StaffChecklistItemType staffChecklistItemType)
        {
            StaffChecklistItemType = staffChecklistItemType;
        }

        /// <summary>
        /// Revises the checked indicator.
        /// </summary>
        /// <param name="isChecked">If set to <c>true</c> [is checked].</param>
        public virtual void ReviseCheckedIndicator(bool isChecked)
        {
            CheckedIndicator = isChecked;
        }
    }
}