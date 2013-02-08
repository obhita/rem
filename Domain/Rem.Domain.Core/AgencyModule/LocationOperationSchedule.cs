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

using System.Collections.Generic;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The LocationOperationSchedule defines a location's scheduled hours of operation.
    /// </summary>
    public class LocationOperationSchedule : LocationAggregateNodeBase
    {
        private readonly IList<LocationWorkHour> _locationWorkHours;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationOperationSchedule"/> class.
        /// </summary>
        protected internal LocationOperationSchedule()
        {
            _locationWorkHours = new List<LocationWorkHour> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationOperationSchedule"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected internal LocationOperationSchedule(string name)
            : this()
        {
            Check.IsNotNullOrWhitespace(name, () => Name);

            _name = name;
        }

        /// <summary>
        /// Gets the location Name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange(ref _name, () => Name, value); }
        }

        /// <summary>
        /// Gets LocationWorkHours.
        /// </summary>
        public virtual IEnumerable<LocationWorkHour> LocationWorkHours
        {
            get { return _locationWorkHours; }
            private set { }
        }

        /// <summary>
        /// The add work hour.
        /// </summary>
        /// <param name="locationWorkHour">
        /// The location work hour.
        /// </param>
        public virtual void AddWorkHour(LocationWorkHour locationWorkHour)
        {
            Check.IsNotNull ( locationWorkHour, "locationWorkHour is required." );

            DomainRuleEngine.CreateRuleEngine<LocationOperationSchedule, LocationWorkHour> ( this, () => AddWorkHour )
                .WithContext ( locationWorkHour )
                .Execute(() =>
                {
                    locationWorkHour.LocationOperationSchedule = this;
                    _locationWorkHours.Add(locationWorkHour);
                    NotifyItemAdded(() => LocationWorkHours, locationWorkHour);
                });
        }

        /// <summary>
        /// The remove work hour.
        /// </summary>
        /// <param name="locationWorkHour">
        /// The location work hour.
        /// </param>
        public virtual void RemoveWorkHour(LocationWorkHour locationWorkHour)
        {
            if (_locationWorkHours.Remove(locationWorkHour))
            {
                NotifyItemRemoved(() => LocationWorkHours, locationWorkHour);
            }
        }

        /// <summary>
        /// The revise name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public virtual void ReviseName(string name)
        {
            Check.IsNotNullOrWhitespace(name, () => Name);

            DomainRuleEngine.CreateRuleEngine<Location, string> ( Location, () => ReviseName )
                .WithContext ( name )
                .WithContext ( this )
                .Execute(() => { Name = name; });
        }
    }
}