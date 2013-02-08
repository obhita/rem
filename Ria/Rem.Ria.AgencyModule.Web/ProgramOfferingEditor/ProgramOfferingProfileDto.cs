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
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.AgencyDashboard;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.Web.ProgramOfferingEditor
{
    /// <summary>
    /// Data transfer object for ProgramOfferingProfile class.
    /// </summary>
    public class ProgramOfferingProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private int? _capacityCount;
        private DateTime? _endDate;
        private LocationDisplayNameDto _location;
        private ProgramDto _program;
        private DateTime? _startDate;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="ProgramOfferingProfileDto"/> is active.
        /// </summary>
        public virtual bool Active
        {
            get
            {
                bool active;

                // Null end date is considered as an all inclusive end date.
                if ( _endDate.HasValue )
                {
                    active = _startDate <= DateTime.Now.Date && ( _endDate.Value >= DateTime.Now.Date );
                }
                else
                {
                    active = _startDate <= DateTime.Now;
                }

                return active;
            }
        }

        /// <summary>
        /// Gets or sets the capacity count.
        /// </summary>
        /// <value>The capacity count.</value>
        public int? CapacityCount
        {
            get { return _capacityCount; }
            set { ApplyPropertyChange ( ref _capacityCount, () => CapacityCount, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>The location.</value>
        public LocationDisplayNameDto Location
        {
            get { return _location; }
            set { ApplyPropertyChange ( ref _location, () => Location, value ); }
        }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>The program.</value>
        public ProgramDto Program
        {
            get { return _program; }
            set { ApplyPropertyChange ( ref _program, () => Program, value ); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        #endregion
    }
}
