using Rem.Ria.PatientModule.Web.DirectMessageCenter;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// Message Sent Event Arguments class
    /// </summary>
    public class MessageSentEventArgs
    {
        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>
        /// The mail.
        /// </value>
        public DirectMailDto Mail { get; set; }
    }
}
