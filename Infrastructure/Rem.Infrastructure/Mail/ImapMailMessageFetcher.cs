using System;
using System.Collections.Generic;
using System.Linq;
using ActiveUp.Net.Mail;
using Pillar.Common.Utility;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// This class defines an implementation of <see cref="IImapMailMessageFetcher"/>.
    /// </summary>
    public class ImapMailMessageFetcher : IImapMailMessageFetcher
    {
        private readonly ImapClientProvider _imapClientProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImapMailMessageFetcher"/> class.
        /// </summary>
        /// <param name="imapClientProvider">The imap client provider.</param>
        public ImapMailMessageFetcher(ImapClientProvider imapClientProvider )
        {
            _imapClientProvider = imapClientProvider;
        }

        /// <summary>
        /// Fetches the message list.
        /// </summary>
        /// <param name="mailFolderName">Name of the mail folder.</param>
        /// <param name="lastReceivedMailId">The last received mail id.</param>
        /// <returns>A List of ImapMessage Header.</returns>
        public IList<ImapMailMessageHeader> FetchMessageList(string mailFolderName, int lastReceivedMailId)
        {
            var result = DoImapWork(
                p =>
                {
                    var imapMessages = new List<ImapMailMessageHeader>();

                    var mailBox = p.SelectMailbox(mailFolderName);
                    var messageCount = mailBox.MessageCount;

                    for (int n = 1; n < messageCount + 1; n++)
                    {
                        var uid = mailBox.Fetch.Uid(n);

                        if (uid > lastReceivedMailId)
                        {
                            var nativeMessage = mailBox.Fetch.UidHeaderObject(uid);
                            var imapMessage = GetImapMessage(nativeMessage, mailFolderName, uid);
                            imapMessages.Add(imapMessage.Header);
                        }
                    }

                    return imapMessages;
                });

            return result;
        }

        /// <summary>
        /// Fetches the message.
        /// </summary>
        /// <param name="mailFolderName">Name of the mail folder.</param>
        /// <param name="mailId">The mail id.</param>
        /// <returns>A mail message with IMAP mail message header. </returns>
        public MailMessage<ImapMailMessageHeader> FetchMessage(string mailFolderName, int mailId)
        {
            var result = DoImapWork (
                p =>
                    {
                        var mailBox = p.SelectMailbox(mailFolderName);

                        Message nativeMessage = mailBox.Fetch.UidMessageObject(mailId);
                        if (nativeMessage == null)
                        {
                            throw new ApplicationException("The mail is not found.");
                        }
                        var imapMessage = GetImapMessage(nativeMessage, mailFolderName, mailId);

                        return imapMessage;
                    } );

            return result;
        }

        private TResult DoImapWork<TResult>(Func<Imap4Client, TResult> workToDo)
        {
            Imap4Client client = null;

            try
            {
                client = _imapClientProvider.GetImapClient();
                return workToDo ( client);
            }
            finally
            {
                if (client != null && client.IsConnected)
                {
                    client.Disconnect();
                }
            }
        }

        private MailMessage<ImapMailMessageHeader> GetImapMessage(Header nativeMail, string mailFolderName, int mailId)
        {
            Check.IsNotNull(nativeMail, "Native mail should not be null.");

            var imapMessage = new MailMessage<ImapMailMessageHeader>();

            imapMessage.Header.FromAddress = nativeMail.From == null ? string.Empty : nativeMail.From.Email;
            imapMessage.Header.FromName = nativeMail.From == null ? string.Empty : nativeMail.From.Name;
            imapMessage.Header.ToAddress = nativeMail.To.FirstOrDefault () == null ? string.Empty : nativeMail.To.First ().Email;
            imapMessage.Header.ToName = nativeMail.To.FirstOrDefault () == null ? string.Empty : nativeMail.To.First ().Name;
            imapMessage.Header.SentDateTime = nativeMail.Date;
            imapMessage.Header.Subject = nativeMail.Subject;

            imapMessage.Header.Id = mailId;
            imapMessage.Header.FolderName = mailFolderName;

            //TODO: Check the flag
            imapMessage.Header.IsRead = true;

            if (nativeMail is Message)
            {
                var nativeMailMessage = nativeMail as Message;

                imapMessage.BodyText = nativeMailMessage.BodyText.Text;

                // TODO: Now only get the first attachment
                if (nativeMailMessage.Attachments.Count > 0)
                {
                    imapMessage.Attachments.Add(new MailAttachment(nativeMailMessage.Attachments[0].BinaryContent, nativeMailMessage.Attachments[0].Filename));
                }
            }

            return imapMessage;
        }
    }
}