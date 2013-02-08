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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.AgencyDashboard
{
    /// <summary>
    /// Data transfer object for Program class.
    /// </summary>
    [DataContract]
    public class ProgramDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private LookupValueDto _ageGroup;
        private long _agencyKey;

        private LookupValueDto _capacityType;
        private string _displayName;
        private DateTime? _endDate;

        private LookupValueDto _genderSpecification;
        private string _name;
        private LookupValueDto _programCategory;
        private DateTime? _startDate;
        private LookupValueDto _treatmentApproach;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="ProgramDto"/> is active.
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
        /// Gets or sets the age group.
        /// </summary>
        /// <value>The age group.</value>
        [DataMember]
        public LookupValueDto AgeGroup
        {
            get { return _ageGroup; }
            set { ApplyPropertyChange ( ref _ageGroup, () => AgeGroup, value ); }
        }

        /// <summary>
        /// Gets or sets the agency key.
        /// </summary>
        /// <value>The agency key.</value>
        [DataMember]
        public long AgencyKey
        {
            get { return _agencyKey; }
            set { RaisePropertyChanged ( ref _agencyKey, () => AgencyKey, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the capacity.
        /// </summary>
        /// <value>The type of the capacity.</value>
        [DataMember]
        public virtual LookupValueDto CapacityType
        {
            get { return _capacityType; }
            set { ApplyPropertyChange ( ref _capacityType, () => CapacityType, value ); }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [DataMember]
        public virtual string DisplayName
        {
            get { return _displayName; }
            set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public virtual DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the gender specification.
        /// </summary>
        /// <value>The gender specification.</value>
        [DataMember]
        public LookupValueDto GenderSpecification
        {
            get { return _genderSpecification; }
            set { ApplyPropertyChange ( ref _genderSpecification, () => GenderSpecification, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the program.</value>
        [DataMember]
        public virtual string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the program category.
        /// </summary>
        /// <value>The program category.</value>
        [DataMember]
        public LookupValueDto ProgramCategory
        {
            get { return _programCategory; }
            set { ApplyPropertyChange ( ref _programCategory, () => ProgramCategory, value ); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public virtual DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the treatment approach.
        /// </summary>
        /// <value>The treatment approach.</value>
        [DataMember]
        public LookupValueDto TreatmentApproach
        {
            get { return _treatmentApproach; }
            set { ApplyPropertyChange ( ref _treatmentApproach, () => TreatmentApproach, value ); }
        }

        #endregion
    }
}
