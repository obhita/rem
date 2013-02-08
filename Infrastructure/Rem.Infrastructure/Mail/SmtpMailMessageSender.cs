using System.IO;
using System.Net.Mail;
using Pillar.Common.Configuration;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Rem.Infrastructure.Configuration;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Sends mail message using SMTP.
    /// </summary>
    public class SmtpMailMessageSender : ISmtpMailMessageSender
    {
        private readonly IConfigurationPropertiesProvider _configurationPropertiesProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpMailMessageSender"/> class.
        /// </summary>
        /// <param name="configurationPropertiesProvider">The configuration properties provider.</param>
        public SmtpMailMessageSender (IConfigurationPropertiesProvider configurationPropertiesProvider)
        {
            _configurationPropertiesProvider = configurationPropertiesProvider;
        }

        /// <summary>
        /// Sends the specified mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        public void Send(MailMessage<MailMessageHeader> mailMessage)
        {
            Check.IsNotNull(mailMessage, "Mail message cannot be null.");

            var smtpServer = _configurationPropertiesProvider.GetProperty(SettingKeyNames.SmtpServer);
            var smtpPort = _configurationPropertiesProvider.GetPropertyInt(SettingKeyNames.SmtpPort);

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                var message = new MailMessage(
                    new MailAddress(mailMessage.Header.FromAddress, mailMessage.Header.FromName),
                    new MailAddress(mailMessage.Header.ToAddress)) { Subject = mailMessage.Header.Subject, Body = mailMessage.BodyText };

                mailMessage.Attachments.ForEach (
                    p =>
                        {
                            if (!string.IsNullOrWhiteSpace(p.FileName))
                            {
                                Attachment smtpAttachment;
                                if ( p.ContentBytes != null )
                                {
                                    smtpAttachment = CreateAttachmentFromBytes ( p.ContentBytes, p.FileName );
                                }
                                else
                                {
                                    smtpAttachment = CreateAttachmentFromString ( p.ContentString, p.FileName );
                                }
                                message.Attachments.Add ( smtpAttachment );
                            }
                        }
                    );

                client.Send(message);
            }
        }

        private static Attachment CreateAttachmentFromString(string content, string name)
        {
            var attachment = Attachment.CreateAttachmentFromString(content, name);
            return attachment;
        }

        private static Attachment CreateAttachmentFromBytes(byte[] contentBytes, string name)
        {
            var contentStream = new MemoryStream(contentBytes);
            var attachment = new Attachment(contentStream, name);
            return attachment;
        }
    }
}