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

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for AgencyProfile class.
    /// </summary>
    [DataContract]
    public class AgencyProfileDto : EditableDataTransferObject
    {
        #region Constants and Fields

        private SoftDeleteObservableCollection<AgencyAliasDto> _agencyAliases;
        private LookupValueDto _agencyType;
        private string _displayName;
        private string _doingBusinessAsName;
        private SoftDeleteObservableCollection<AgencyEmailAddressDto> _emailAddresses;
        private DateTime? _endDate;
        private LookupValueDto _geographicalRegion;
        private string _legalName;
        private AgencyDisplayNameDto _parentAgency;
        private DateTime? _startDate;
        private string _websiteUrlName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the agency aliases.
        /// </summary>
        /// <value>The agency aliases.</value>
        [DataMember]
        public SoftDeleteObservableCollection<AgencyAliasDto> AgencyAliases
        {
            get { return _agencyAliases; }
            set { ApplySoftDeleteObservableCollectionChange ( ref _agencyAliases, () => AgencyAliases, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the agency.
        /// </summary>
        /// <value>The type of the agency.</value>
        [DataMember]
        public LookupValueDto AgencyType
        {
            get { return _agencyType; }
            set { ApplyPropertyChange ( ref _agencyType, () => AgencyType, value ); }
        }

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
        /// Gets or sets the name of the doing business as.
        /// </summary>
        /// <value>The name of the doing business as.</value>
        [DataMember]
        public string DoingBusinessAsName
        {
            get { return _doingBusinessAsName; }
            set { ApplyPropertyChange ( ref _doingBusinessAsName, () => DoingBusinessAsName, value ); }
        }

        /// <summary>
        /// Gets or sets the email addresses.
        /// </summary>
        /// <value>The email addresses.</value>
        [DataMember]
        public SoftDeleteObservableCollection<AgencyEmailAddressDto> EmailAddresses
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
        /// Gets or sets the name of the legal.
        /// </summary>
        /// <value>The name of the legal.</value>
        [DataMember]
        public string LegalName
        {
            get { return _legalName; }
            set { ApplyPropertyChange ( ref _legalName, () => LegalName, value ); }
        }

        /// <summary>
        /// Gets or sets the parent agency.
        /// </summary>
        /// <value>The parent agency.</value>
        [DataMember]
        public AgencyDisplayNameDto ParentAgency
        {
            get { return _parentAgency; }
            set { ApplyPropertyChange ( ref _parentAgency, () => ParentAgency, value ); }
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
