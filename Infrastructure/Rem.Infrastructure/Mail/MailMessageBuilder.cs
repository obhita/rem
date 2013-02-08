using System.Collections.Generic;
using System.Text;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Provides the capabilities to Send Mail Messages 
    /// </summary>
    public class MailMessageBuilder
    {
        #region Constants and Fields

        private const string Disclaimer =
            @"-------------------------------------------------------------------------------------------------
This message and its attachments may contain personally identifiable health information and are intended solely for the use of the individual to whom it is addressed. 
If you are not the intended recipient of this message and its attachments, you must take no action based upon them, nor may you copy or show them to anyone; doing so may be illegal.   
Please contact the sender if you believe you have received this message in error.";

        private readonly MailMessage<MailMessageHeader> _message;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MailMessageBuilder"/> class.
        /// </summary>
        public MailMessageBuilder ()
        {
            _message = new MailMessage<MailMessageHeader> { Attachments = new List<MailAttachment> () };
        }

        #endregion

        /// <summary>
        /// Gets the mail message.
        /// </summary>
        public MailMessage<MailMessageHeader> MailMessage { get { return _message; } }

        #region Public Methods

        /// <summary>
        /// Composes the specified sender address.
        /// </summary>
        /// <param name="senderAddress">The sender address.</param>
        /// <param name="senderDisplayName">Display name of the sender.</param>
        /// <param name="recipientAddress">The recipient address.</param>
        /// <param name="recipientDisplayName">Display name of the recipient.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>The same instance of <see cref="MailMessageBuilder"/> class</returns>
        public MailMessageBuilder Compose (
            string senderAddress,
            string senderDisplayName,
            string recipientAddress,
            string recipientDisplayName,
            string subject,
            string body )
        {
            var sb = new StringBuilder ();
            sb.AppendLine ( body );
            sb.AppendLine ( Disclaimer );

            _message.Header.Subject = subject;
            _message.BodyText = sb.ToString ();
            _message.Header.ToAddress = recipientAddress;
            _message.Header.ToName = recipientDisplayName;
            _message.Header.FromAddress = senderAddress;
            _message.Header.FromName = senderDisplayName;

            return this;
        }

        /// <summary>
        /// Configures the DirectMessageSender to add the specified attachment to the message being sent.
        /// </summary>
        /// <param name="contentString">Content of the file.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// The same instance of <see cref="MailMessageBuilder"/> class
        /// </returns>
        public MailMessageBuilder WithAttachment ( string contentString, string filename)
        {
            _message.Attachments.Add ( new MailAttachment ( contentString, filename) );
            return this;
        }

        /// <summary>
        /// Withes the attachment.
        /// </summary>
        /// <param name="contentBytes">Content of the file.</param>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// Returns the instance.
        /// </returns>
        public MailMessageBuilder WithAttachment(byte[] contentBytes, string filename)
        {
            _message.Attachments.Add(new MailAttachment(contentBytes, filename));
            return this;
        }

        #endregion
    }
}
