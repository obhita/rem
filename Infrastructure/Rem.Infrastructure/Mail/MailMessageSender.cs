namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Send out mail message.
    /// </summary>
    public class MailMessageSender : IMailMessageSender
    {
        private readonly ISmtpMailMessageSender _smtpMailMessageSender;
        private readonly IImapMailMessageAppender _imailMessageAppender;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailMessageSender"/> class.
        /// </summary>
        /// <param name="smtpMailMessageSender">The SMTP mail message sender.</param>
        /// <param name="imailMessageAppender">The imail message appender.</param>
        public MailMessageSender(ISmtpMailMessageSender smtpMailMessageSender, IImapMailMessageAppender imailMessageAppender)
        {
            _smtpMailMessageSender = smtpMailMessageSender;
            _imailMessageAppender = imailMessageAppender;
        }

        /// <summary>
        /// Sends the specified mail message.
        /// </summary>
        /// <param name="mailMessage">The mail message.</param>
        public void Send ( MailMessage<MailMessageHeader> mailMessage )
        {
            _smtpMailMessageSender.Send(mailMessage);
            _imailMessageAppender.Append(mailMessage);
        }
    }
}