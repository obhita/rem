#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// LocationWorkHour defines the location work hour.
    /// </summary>
    public class LocationWorkHour : AuditableAggregateNodeBase, IAggregateNodeValueObject
    {
        private DayOfWeek _dayOfWeek;
        private LocationOperationSchedule _locationOperationSchedule;
        private TimeRange _workHourTimeRange;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationWorkHour"/> class.
        /// </summary>
        protected internal LocationWorkHour ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationWorkHour"/> class.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <param name="workHourTimeRange">The work hour time range.</param>
        public LocationWorkHour(DayOfWeek dayOfWeek, TimeRange workHourTimeRange)
        {
            Check.IsNotNull(dayOfWeek, () => DayOfWeek);

            _dayOfWeek = dayOfWeek;
            _workHourTimeRange = workHourTimeRange;
        }

        /// <summary>
        /// Gets or sets location operation schedule.
        /// </summary>
        [NotNull]
        public virtual LocationOperationSchedule LocationOperationSchedule
        {
            get { return _locationOperationSchedule; }
            protected internal set { ApplyPropertyChange(ref _locationOperationSchedule, () => LocationOperationSchedule, value); }
        }

        /// <summary>
        /// Gets the day of week.
        /// </summary>
        [NotNull]
        public virtual DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            private set { ApplyPropertyChange ( ref _dayOfWeek, () => DayOfWeek, value ); }
        }

        /// <summary>
        /// Gets work hour time range.
        /// </summary>
        public virtual TimeRange WorkHourTimeRange
        {
            get { return _workHourTimeRange; }
            private set { ApplyPropertyChange ( ref _workHourTimeRange, () => WorkHourTimeRange, value ); }
        }

        #region Overrides of AbstractAggregateNode

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public override IAggregateRoot AggregateRoot
        {
            get { return LocationOperationSchedule.Location; }
        }

        #endregion
    }
}