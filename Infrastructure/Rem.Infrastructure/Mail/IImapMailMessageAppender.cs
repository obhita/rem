namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Appends mail message to IMAP server mail box.
    /// </summary>
    public interface IImapMailMessageAppender
    {
        /// <summary>
        /// Appends the specified mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        void Append(MailMessage<MailMessageHeader> mailMessage);
    }
}
