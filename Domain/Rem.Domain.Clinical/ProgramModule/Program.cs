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
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// The Program defines a treatment agenda. 
    /// </summary>
    public class Program : AuditableAggregateRootBase
    {
        private readonly IList<ProgramOffering> _programOfferings;
        private CapacityType _capacityType;
        private string _displayName;
        private DateTime _startDate;
        private DateTime? _endDate;
        private string _name;
        private ProgramCharacteristics _programCharacteristics;

        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        protected internal Program ()
        {
            _programOfferings = new List<ProgramOffering>();
        }

        internal Program ( Agency agency, string name, DateTime startDate, ProgramCharacteristics programCharacteristics ) : this ()
        {
            Check.IsNotNull ( agency, "Agency is required." );
            Check.IsNotNullOrWhitespace ( name, "Display name is required." );
            Check.IsNotNull ( startDate, "StartDate is required." );
            Check.IsNotNull(programCharacteristics, "ProgramCharacteristics is required.");

            Agency = agency;
            _name = name;
            _startDate = startDate;
            _endDate = null;
            _programCharacteristics = programCharacteristics;
        }

        /// <summary>
        /// Gets the agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the program offerings.
        /// </summary>
        public virtual IEnumerable<ProgramOffering> ProgramOfferings
        {
            get { return _programOfferings.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public virtual string DisplayName
        {
            get { return _displayName; }
            private set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value ); }
        }

        /// <summary>
        /// Gets the type of the capacity.
        /// </summary>
        /// <value>
        /// The type of the capacity.
        /// </value>
        public virtual CapacityType CapacityType
        {
            get { return _capacityType; }
            private set { ApplyPropertyChange(ref _capacityType, () => CapacityType, value); }
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
            private set { ApplyPropertyChange(ref _endDate, () => EndDate, value); }
        }

        /// <summary>
        /// Gets the program characteristics.
        /// </summary>
        public virtual ProgramCharacteristics ProgramCharacteristics
        {
            get { return _programCharacteristics; }
            private set { ApplyPropertyChange(ref _programCharacteristics, () => ProgramCharacteristics, value); }
        }

        /// <summary>
        /// Revises the program characteristics.
        /// </summary>
        /// <param name="programCharacteristics">The program characteristics.</param>
        public virtual void ReviseProgramCharacteristics(ProgramCharacteristics programCharacteristics)
        {
            ProgramCharacteristics = programCharacteristics;
        }

        /// <summary>
        /// Renames the program.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void RenameProgram(string name)
        {
            Check.IsNotNullOrWhitespace(name, "name is required.");
          
            DomainRuleEngine.CreateRuleEngine<Program, string> ( this, () => RenameProgram )
                .WithContext ( name )
                .Execute ( () => Name = name );
        }

        /// <summary>
        /// Renames the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        public virtual void RenameDisplayName ( string displayName )
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Changes the type of the capacity.
        /// </summary>
        /// <param name="capacityType">Type of the capacity.</param>
        public virtual void ChangeCapacityType ( CapacityType capacityType )
        {
            CapacityType = capacityType;
        }

        /// <summary>
        /// Changes the start date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        public virtual void ChangeStartDate ( DateTime startDate )
        {
            StartDate = startDate;
        }

        /// <summary>
        /// Changes the end date.
        /// </summary>
        /// <param name="endtDate">The endt date.</param>
        public virtual void ChangeEndDate(DateTime? endtDate)
        {
            EndDate = endtDate;
        }
    }
}