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
using Pillar.Common.Collections;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Data transfer object for LocationProfile class.
    /// </summary>
    public class LocationProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private string _displayName;
        private SoftDeleteObservableCollection<LocationEmailAddressDto> _emailAddresses;
        private DateTime? _endDate;
        private LookupValueDto _geographicalRegion;
        private string _name;
        private DateTime? _startDate;
        private string _websiteUrlName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationProfileDto"/> class.
        /// </summary>
        public LocationProfileDto ()
        {
            _emailAddresses = new SoftDeleteObservableCollection<LocationEmailAddressDto> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        [DataMember]
        public string DisplayName
        {
            get { return _displayName; }
            set { ApplyPropertyChange ( ref _displayName, () => DisplayName, value ); }
        }

        /// <summary>
        /// Gets or sets the email addresses.
        /// </summary>
        /// <value>The email addresses.</value>
        [DataMember]
        public SoftDeleteObservableCollection<LocationEmailAddressDto> EmailAddresses
        {
            get { return _emailAddresses; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _emailAddresses, () => EmailAddresses, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        [DataMember]
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets or sets the geographical region.
        /// </summary>
        /// <value>The geographical region.</value>
        [DataMember]
        public LookupValueDto GeographicalRegion
        {
            get { return _geographicalRegion; }
            set { ApplyPropertyChange ( ref _geographicalRegion, () => GeographicalRegion, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the profile.</value>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [DataMember]
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the website URL.
        /// </summary>
        /// <value>The name of the website URL.</value>
        [DataMember]
        public string WebsiteUrlName
        {
            get { return _websiteUrlName; }
            set { ApplyPropertyChange ( ref _websiteUrlName, () => WebsiteUrlName, value ); }
        }

        #endregion
    }
}
