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
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffEvent defines a work-related event that is associated to a staff.
    /// </summary>
    public class StaffEvent : StaffAggregateNodeBase
    {
        private DateTime? _eventDate;
        private StaffEventType _staffEventType;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffEvent"/> class.
        /// </summary>
        protected internal StaffEvent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffEvent"/> class.
        /// </summary>
        /// <param name="staffEventType">Type of the staff event.</param>
        protected internal StaffEvent(StaffEventType staffEventType)
        {
            Check.IsNotNull(staffEventType, "Staff Event Type cannot be null");

            _staffEventType = staffEventType;
        }

        /// <summary>
        /// Gets the type of the staff event.
        /// </summary>
        /// <value>
        /// The type of the staff event.
        /// </value>
        [NotNull]
        public virtual StaffEventType StaffEventType
        {
            get { return _staffEventType; }
            private set { ApplyPropertyChange(ref _staffEventType, () => StaffEventType, value); }
        }

        /// <summary>
        /// Gets the event date.
        /// </summary>
        public virtual DateTime? EventDate
        {
            get { return _eventDate; }
            private set { ApplyPropertyChange(ref _eventDate, () => EventDate, value); }
        }

        /// <summary>
        /// Revises the type of the event.
        /// </summary>
        /// <param name="staffEventType">Type of the staff event.</param>
        public virtual void ReviseEventType(StaffEventType staffEventType)
        {
            StaffEventType = staffEventType;
        }

        /// <summary>
        /// Revises the event date.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        public virtual void ReviseEventDate(DateTime? eventDate)
        {
            EventDate = eventDate;
        }
    }
}