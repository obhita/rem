namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Send out mail message.
    /// </summary>
    public interface IMailMessageSender
    {
        /// <summary>
        /// Sends the specified mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        void Send(MailMessage<MailMessageHeader> mailMessage);
    }
}