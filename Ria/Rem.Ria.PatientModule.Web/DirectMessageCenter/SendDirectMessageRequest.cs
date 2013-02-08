using Agatha.Common;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// SendDirectMessageRequest class
    /// </summary>
    public class SendDirectMessageRequest : Request
    {
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The subject.</value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets to direct email.
        /// </summary>
        /// <value>To direct email.</value>
        public string ToDirectEmail { get; set; }
        
        /// <summary>
        /// Gets or sets the staff key.
        /// </summary>
        /// <value>The staff key.</value>
        public long StaffKey { get; set; }

        /// <summary>
        /// Gets or sets the attachment data.
        /// </summary>
        /// <value>
        /// The attachment data.
        /// </value>
        public byte[] AttachmentData { get; set; }

        /// <summary>
        /// Gets or sets the name of the attachment file.
        /// </summary>
        /// <value>
        /// The name of the attachment file.
        /// </value>
        public string AttachmentFileName { get; set; }
    }
}
