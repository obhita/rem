using System;
using ActiveUp.Net.Mail;
using Pillar.Common.Configuration;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Configuration;
using Rem.Infrastructure.Domain;
using Rem.Infrastructure.Service;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Provides a IMAP Client.
    /// </summary>
    public class ImapClientProvider : IImapClientProvider
    {
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;
        private readonly ISessionProvider _sessionProvider;
        private readonly IUserInformationDtoFactory _userInformationDtoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImapClientProvider"/> class.
        /// </summary>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        /// <param name="sessionProvider">The session provider.</param>
        /// <param name="userInformationDtoFactory">The user information dto factory.</param>
        public ImapClientProvider(
            IConfigurationPropertiesProvider configurationPropertiesProvider,
            ISessionProvider sessionProvider,
            IUserInformationDtoFactory userInformationDtoFactory)
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
            _sessionProvider = sessionProvider;
            _userInformationDtoFactory = userInformationDtoFactory;
        }

        /// <summary>
        /// Gets the imap client.
        /// </summary>
        /// <returns>Imap 4 client.</returns>
        public Imap4Client GetImapClient()
        {
            string imapServer = _configurationPropertiesProvider.GetProperty(SettingKeyNames.ImapServer);
            int imapPort = _configurationPropertiesProvider.GetPropertyInt(SettingKeyNames.ImapPort);

            // Retrieves the Current User Context 
            UserInformationDto info = _userInformationDtoFactory.CreateUserInformationDto();

            // Retrieves the corresponding Staff record for the current user
            Staff staff = _sessionProvider.GetSession().QueryOver<Staff>().Where(s => s.Key == info.StaffKey).SingleOrDefault();

            //string username = "system@direct.obhita-stage.org";
            //string password = "P@$$w0rd";

            string username = null;
            string password = null;

            if (staff.DirectAddressCredential.DirectAddress != null)
            {
                username = staff.DirectAddressCredential.DirectAddress.Address;
                password = staff.DirectAddressCredential.DirectAddressPassword;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Username/password are not provided to access IMAP mail server.");
            }

            var imap4Client = new Imap4Client();
            imap4Client.Connect(imapServer, imapPort, username, password);

            return imap4Client;
        }
    }
}