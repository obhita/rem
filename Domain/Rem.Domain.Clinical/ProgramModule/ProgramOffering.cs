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
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// ProgramOffering defines an offering of a program at a location.
    /// </summary>
    public class ProgramOffering : AuditableAggregateRootBase
    {
        private DateTime _startDate;
        private DateTime? _endDate;
        private int? _capacityCount;
        private Program _program;
        private Location _location;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOffering"/> class.
        /// </summary>
        protected internal ProgramOffering ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOffering"/> class.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="location">The location.</param>
        /// <param name="startDate">The start date.</param>
        protected internal ProgramOffering ( Program program, Location location, DateTime startDate )
            : this ()
        {
            Check.IsNotNull ( program, "Program is required." );
            Check.IsNotNull ( location, "Location is required." );
            Check.IsNotNull ( startDate, "Start Date is required." );
            _program = program;
            _location = location;
            _startDate = startDate;
        }

        /// <summary>
        /// Gets the program.
        /// </summary>
        [NotNull]
        public virtual Program Program
        {
            get { return _program; }
            private set { ApplyPropertyChange ( ref _program, () => Program, value ); }
        }

        /// <summary>
        /// Gets the start date.
        /// </summary>
        [NotNull]
        public virtual DateTime StartDate
        {
            get { return _startDate; }
            private set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            private set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets the capacity count.
        /// </summary>
        public virtual int? CapacityCount
        {
            get { return _capacityCount; }
            private set { ApplyPropertyChange ( ref _capacityCount, () => CapacityCount, value ); }
        }

        /// <summary>
        /// Gets the location.
        /// </summary>
        [NotNull]
        public virtual Location Location
        {
            get { return _location; }
            private set { ApplyPropertyChange ( ref _location, () => Location, value ); }
        }

        /// <summary>
        /// Revises the start date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        public virtual void ReviseStartDate ( DateTime startDate )
        {
            StartDate = startDate;
        }

        /// <summary>
        /// Revises the end date.
        /// </summary>
        /// <param name="endDate">The end date.</param>
        public virtual void ReviseEndDate ( DateTime? endDate )
        {
            EndDate = endDate;
        }

        /// <summary>
        /// Revises the capacity value.
        /// </summary>
        /// <param name="capacityValue">The capacity value.</param>
        public virtual void ReviseCapacityValue ( int? capacityValue )
        {
            CapacityCount = capacityValue;
        }

        /// <summary>
        /// Revises the location.
        /// </summary>
        /// <param name="location">The location.</param>
        public virtual void ReviseLocation ( Location location )
        {
            Location = location;
        }

        /// <summary>
        /// Revises the program.
        /// </summary>
        /// <param name="program">The program.</param>
        public virtual void ReviseProgram ( Program program )
        {
            Program = program;
        }
    }
}