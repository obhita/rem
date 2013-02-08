using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Represents the a Healthcare Provider Directory Entry
    /// </summary>
    public class HealthProviderEntry
    {
        private readonly IEnumerable<Tuple<string, string, byte[]>> _attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthProviderEntry"/> class.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        public HealthProviderEntry(IEnumerable<Tuple<string, string, byte[]>> attributes)
        {
            _attributes = attributes;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is a healthcare professional.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is healthcare professional; otherwise, <c>false</c>.
        /// </value>
        public bool IsHcProfessional
        {
            get { return !string.IsNullOrEmpty(FindAttributeByValue("HCProfessional")); }
        }

        /// <summary>
        /// Gets the common name.
        /// </summary>
        /// <value>
        /// The name of the common.
        /// </value>
        public string CommonName
        {
            get { return FindAttributeByName("cn"); }
        }

        /// <summary>
        /// Gets the name of the credential.
        /// </summary>
        /// <value>
        /// The name of the credential.
        /// </value>
        public string CredentialName
        {
            get { return FindAttributeByName("credentialName"); }
        }

        /// <summary>
        /// Gets the credential number.
        /// </summary>
        public string CredentialNumber
        {
            get { return FindAttributeByName("credentialNumber"); }
        }

        /// <summary>
        /// Gets the type of the credential.
        /// </summary>
        /// <value>
        /// The type of the credential.
        /// </value>
        public string CredentialType
        {
            get { return FindAttributeByName("credentialType"); }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public string Identifier
        {
            get { return FindAttributeByName("hcIdentifier"); }
        }

        /// <summary>
        /// Gets the profession.
        /// </summary>
        public string Profession
        {
            get { return FindAttributeByName("hcProfession"); }
        }

        /// <summary>
        /// Gets the surname.
        /// </summary>
        public string Surname
        {
            get { return FindAttributeByName("sn"); }
        }

        /// <summary>
        /// Gets the business category.
        /// </summary>
        public string BusinessCategory
        {
            get { return FindAttributeByName("businessCategory"); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public string DisplayName
        {
            get { return FindAttributeByName("displayName"); }
        }

        /// <summary>
        /// Gets the given name.
        /// </summary>
        /// <value>
        /// The given name.
        /// </value>
        public string GivenName
        {
            get { return FindAttributeByName("givenName"); }
        }

        /// <summary>
        /// Gets the specialization.
        /// </summary>
        public string Specialization
        {
            get { return FindAttributeByName("hcSpecialization"); }
        }

        /// <summary>
        /// Gets the mail.
        /// </summary>
        public string Mail
        {
            get { return FindAttributeByName("mail"); }
        }

        /// <summary>
        /// Gets the member of.
        /// </summary>
        public string MemberOf
        {
            get { return FindAttributeByName("memberOf"); }
        }

        /// <summary>
        /// Gets the mobile.
        /// </summary>
        public string Mobile
        {
            get { return FindAttributeByName("mobile"); }
        }

        /// <summary>
        /// Gets the organization.
        /// </summary>
        public string Organization
        {
            get { return FindAttributeByName("o"); }
        }

        /// <summary>
        /// Gets the name of the office.
        /// </summary>
        /// <value>
        /// The name of the office.
        /// </value>
        public string OfficeName
        {
            get { return FindAttributeByName("physicalDeliveryOfficeName"); }
        }

        /// <summary>
        /// Gets the telephone number.
        /// </summary>
        public string TelephoneNumber
        {
            get { return FindAttributeByName("physicalDeliveryOfficeName"); }
        }

        /// <summary>
        /// Gets the uid.
        /// </summary>
        public string Uid
        {
            get { return FindAttributeByName("uid"); }
        }

        /// <summary>
        /// Gets the user certificate.
        /// </summary>
        public X509Certificate2 UserCertificate
        {
            get
            {
                byte[] data;
                data = FindAttributeBytesByName("userCertificate");
                try
                {
                    var cert = new X509Certificate2(data);
                    return cert;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private string FindAttributeByName(string attributeName)
        {
            var firstOrDefault = _attributes.FirstOrDefault(a => a.Item1 == attributeName);

            return firstOrDefault != null ? firstOrDefault.Item2 : string.Empty;
        }


        private byte[] FindAttributeBytesByName(string attributeName)
        {
            var firstOrDefault = _attributes.FirstOrDefault(a => a.Item1 == attributeName);

            return firstOrDefault != null ? firstOrDefault.Item3 : null;
        }

        private string FindAttributeByValue(string attributeValue)
        {
            Tuple<string, string, byte[]> firstOrDefault = _attributes.FirstOrDefault(a => a.Item2 == attributeValue);

            return firstOrDefault != null ? firstOrDefault.Item1 : string.Empty;
        }
    }
}