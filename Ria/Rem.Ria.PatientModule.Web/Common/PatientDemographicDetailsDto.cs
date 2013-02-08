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
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// Data transfer object for PatientDemographicDetails class.
    /// </summary>
    public class PatientDemographicDetailsDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _birthCityName;
        private LookupValueDto _birthCountyArea;
        private string _birthFirstName;
        private string _birthLastName;
        private LookupValueDto _birthStateProvince;
        private LookupValueDto _countyArea;
        private LookupValueDto _geographicalRegion;
        private string _motherFirstName;
        private string _motherMaidenName;
        private string _zipCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the birth city.
        /// </summary>
        /// <value>The name of the birth city.</value>
        [DataMember]
        public string BirthCityName
        {
            get { return _birthCityName; }
            set { ApplyPropertyChange ( ref _birthCityName, () => BirthCityName, value ); }
        }

        /// <summary>
        /// Gets or sets the birth county area.
        /// </summary>
        /// <value>The birth county area.</value>
        [DataMember]
        public virtual LookupValueDto BirthCountyArea
        {
            get { return _birthCountyArea; }
            set { ApplyPropertyChange ( ref _birthCountyArea, () => BirthCountyArea, value ); }
        }

        /// <summary>
        /// Gets or sets the first name of the birth.
        /// </summary>
        /// <value>The first name of the birth.</value>
        [DataMember]
        public string BirthFirstName
        {
            get { return _birthFirstName; }
            set { ApplyPropertyChange ( ref _birthFirstName, () => BirthFirstName, value ); }
        }

        /// <summary>
        /// Gets or sets the last name of the birth.
        /// </summary>
        /// <value>The last name of the birth.</value>
        [DataMember]
        public string BirthLastName
        {
            get { return _birthLastName; }
            set { ApplyPropertyChange ( ref _birthLastName, () => BirthLastName, value ); }
        }

        /// <summary>
        /// Gets or sets the birth state province.
        /// </summary>
        /// <value>The birth state province.</value>
        [DataMember]
        public LookupValueDto BirthStateProvince
        {
            get { return _birthStateProvince; }
            set { ApplyPropertyChange ( ref _birthStateProvince, () => BirthStateProvince, value ); }
        }

        /// <summary>
        /// Gets or sets the county area.
        /// </summary>
        /// <value>The county area.</value>
        [DataMember]
        public virtual LookupValueDto CountyArea
        {
            get { return _countyArea; }
            set { ApplyPropertyChange ( ref _countyArea, () => CountyArea, value ); }
        }

        /// <summary>
        /// Gets or sets the geographical region.
        /// </summary>
        /// <value>The geographical region.</value>
        [DataMember]
        public virtual LookupValueDto GeographicalRegion
        {
            get { return _geographicalRegion; }
            set { ApplyPropertyChange ( ref _geographicalRegion, () => GeographicalRegion, value ); }
        }

        /// <summary>
        /// Gets or sets the first name of the mother.
        /// </summary>
        /// <value>The first name of the mother.</value>
        [DataMember]
        public string MotherFirstName
        {
            get { return _motherFirstName; }
            set { ApplyPropertyChange ( ref _motherFirstName, () => MotherFirstName, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the mother maiden.
        /// </summary>
        /// <value>The name of the mother maiden.</value>
        [DataMember]
        public string MotherMaidenName
        {
            get { return _motherMaidenName; }
            set { ApplyPropertyChange ( ref _motherMaidenName, () => MotherMaidenName, value ); }
        }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>The zip code.</value>
        [DataMember]
        public string ZipCode
        {
            get { return _zipCode; }
            set { ApplyPropertyChange ( ref _zipCode, () => ZipCode, value ); }
        }

        #endregion
    }
}
