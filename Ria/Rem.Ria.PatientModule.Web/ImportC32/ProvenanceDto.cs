using System;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.ImportC32
{
    /// <summary>
    /// Defines data transfer object for provenance.
    /// </summary>
    public class ProvenanceDto : KeyedDataTransferObject
    {
        private string _extension;
        private string _assigningAuthority;
        private DateTimeOffset _signedTimestamp;
        private string _providerDirectoryEntry;
        private string _prefixName;
        private string _firstName;
        private string _lastName;
        private string _organizationName;
        private string _organizationExtension;
        private string _organizationAssigningAuthority;
        private string _phoneNumber;
        private string _phoneExtensionNumber;

        /// <summary>
        /// Gets or sets the extension.
        /// </summary>
        /// <value>
        /// The extension.
        /// </value>
        public string Extension
        {
            get { return _extension; }
            set { ApplyPropertyChange(ref _extension, () => Extension, value); }
        }

        /// <summary>
        /// Gets or sets the assigning authority.
        /// </summary>
        /// <value>
        /// The assigning authority.
        /// </value>
        public string AssigningAuthority
        {
            get { return _assigningAuthority; }
            set { ApplyPropertyChange(ref _assigningAuthority, () => AssigningAuthority, value); }
        }

        /// <summary>
        /// Gets or sets the signed timestamp.
        /// </summary>
        /// <value>
        /// The signed timestamp.
        /// </value>
        public DateTimeOffset SignedTimestamp
        {
            get { return _signedTimestamp; }
            set { ApplyPropertyChange(ref _signedTimestamp, () => SignedTimestamp, value); }
        }

        /// <summary>
        /// Gets or sets the provider directory entry.
        /// </summary>
        /// <value>
        /// The provider directory entry.
        /// </value>
        public string ProviderDirectoryEntry
        {
            get { return _providerDirectoryEntry; }
            set { ApplyPropertyChange(ref _providerDirectoryEntry, () => ProviderDirectoryEntry, value); }
        }

        /// <summary>
        /// Gets or sets the name of the prefix.
        /// </summary>
        /// <value>
        /// The name of the prefix.
        /// </value>
        public string PrefixName
        {
            get { return _prefixName; }
            set { ApplyPropertyChange(ref _prefixName, () => PrefixName, value); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName
        {
            get { return _firstName; }
            set { ApplyPropertyChange(ref _firstName, () => FirstName, value); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName
        {
            get { return _lastName; }
            set { ApplyPropertyChange(ref _lastName, () => LastName, value); }
        }

        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        /// <value>
        /// The name of the organization.
        /// </value>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { ApplyPropertyChange(ref _organizationName, () => OrganizationName, value); }
        }

        /// <summary>
        /// Gets or sets the organization extension.
        /// </summary>
        /// <value>
        /// The organization extension.
        /// </value>
        public string OrganizationExtension
        {
            get { return _organizationExtension; }
            set { ApplyPropertyChange(ref _organizationExtension, () => OrganizationExtension, value); }
        }

        /// <summary>
        /// Gets or sets the organization assigning authority.
        /// </summary>
        /// <value>
        /// The organization assigning authority.
        /// </value>
        public string OrganizationAssigningAuthority
        {
            get { return _organizationAssigningAuthority; }
            set { ApplyPropertyChange(ref _organizationAssigningAuthority, () => OrganizationAssigningAuthority, value); }
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { ApplyPropertyChange(ref _phoneNumber, () => PhoneNumber, value); }
        }

        /// <summary>
        /// Gets or sets the phone extension number.
        /// </summary>
        /// <value>
        /// The phone extension number.
        /// </value>
        public string PhoneExtensionNumber
        {
            get { return _phoneExtensionNumber; }
            set { ApplyPropertyChange(ref _phoneExtensionNumber, () => PhoneExtensionNumber, value); }
        }
    }
}
