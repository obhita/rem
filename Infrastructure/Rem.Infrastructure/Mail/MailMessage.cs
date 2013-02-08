using System;
using System.Collections.Generic;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Represents an implementation agnostic Mail Message
    /// </summary>
    /// <typeparam name="THeader">The type of the header.</typeparam>
    public class MailMessage<THeader> where THeader : MailMessageHeader
    {
        private readonly THeader _header;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailMessage&lt;THeader&gt;"/> class.
        /// </summary>
        public MailMessage ()
        {
            _header = Activator.CreateInstance<THeader> ();
            Attachments = new List<MailAttachment> ();
        }

        #region Public Properties

        /// <summary>
        /// Gets the mail message header.
        /// </summary>
        public THeader Header { get { return _header; } }

        /// <summary>
        /// Gets or sets the body text.
        /// </summary>
        /// <value>
        /// The body text.
        /// </value>
        public string BodyText { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public IList<MailAttachment> Attachments { get; set; }

        #endregion
    }
}
