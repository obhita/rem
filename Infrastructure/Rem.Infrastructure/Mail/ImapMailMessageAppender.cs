using System.Text;
using ActiveUp.Net.Mail;
using Pillar.Common.Extension;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Appends mail message to IMAP server mail box.
    /// </summary>
    public class ImapMailMessageAppender : IImapMailMessageAppender
    {
        private readonly ImapClientProvider _imapClientProvider;
        private const string SentItemMailBox = "Sent Items";

        /// <summary>
        /// Initializes a new instance of the <see cref="ImapMailMessageAppender"/> class.
        /// </summary>
        /// <param name="imapClientProvider">The imap client provider.</param>
        public ImapMailMessageAppender(ImapClientProvider imapClientProvider)
        {
            _imapClientProvider = imapClientProvider;
        }

        /// <summary>
        /// Appends the specified mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        public void Append(MailMessage<MailMessageHeader> mailMessage)
        {
            Imap4Client imap4Client = null;

            try
            {
                imap4Client = _imapClientProvider.GetImapClient();

                var mailBox = imap4Client.SelectMailbox(SentItemMailBox);

                var imapMail = new Message
                    {
                        From = new Address(mailMessage.Header.FromAddress, mailMessage.Header.FromName),
                        To = new AddressCollection { new Address(mailMessage.Header.ToAddress, mailMessage.Header.ToName) },
                        Subject = mailMessage.Header.Subject,
                        BodyText = new MimeBody ( BodyFormat.Text ) { Text = mailMessage.BodyText }
                    };


                mailMessage.Attachments.ForEach(
                    p =>
                        {
                            if (p.ContentBytes != null)
                            {
                                imapMail.Attachments.Add(( byte[] )p.ContentBytes, p.FileName);
                            }
                            else
                            {
                                imapMail.Attachments.Add(Encoding.Default.GetBytes(( string )p.ContentString), p.FileName);
                            }
                        });

                mailBox.Append(imapMail); // Do not use messageToAdd.Append(mailBox), it does not work
            }
            finally
            {
                if (imap4Client != null && imap4Client.IsConnected)
                {
                    imap4Client.Disconnect();
                }
            }
        }
    }
}