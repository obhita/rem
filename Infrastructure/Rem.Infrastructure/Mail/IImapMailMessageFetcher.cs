using System.Collections.Generic;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// This interface defines the functionality of IMAP message fetcher.
    /// </summary>
    public interface IImapMailMessageFetcher
    {
        /// <summary>
        /// Fetches the message list.
        /// </summary>
        /// <param name="mailFolderName">Name of the mail folder.</param>
        /// <param name="lastReceivedMailId">The last received mail id.</param>
        /// <returns>A List of ImapMessage.</returns>
        IList<ImapMailMessageHeader> FetchMessageList(string mailFolderName, int lastReceivedMailId);

        /// <summary>
        /// Fetches the message.
        /// </summary>
        /// <param name="mailFolderName">Name of the mail folder.</param>
        /// <param name="mailId">The mail id.</param>
        /// <returns>A mail message with ImapMailMessageHeader.</returns>
        MailMessage<ImapMailMessageHeader> FetchMessage(string mailFolderName, int mailId);
    }
}
