using Agatha.Common;
using Rem.Infrastructure.Service;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Request to get an IMAP mail.
    /// </summary>
    public class GetImapMailRequest : Request, IQueryRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the Folder Name.
        /// </summary>
        /// <value>The Folder Name.</value>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the mail id.
        /// </summary>
        /// <value>
        /// The mail id.
        /// </value>
        public int MailId { get; set; }

        #endregion
    }
}
