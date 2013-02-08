using Agatha.Common;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Get IMAP mail response
    /// </summary>
    public class GetImapMailResponse : Response
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the messsage.
        /// </summary>
        /// <value>
        /// The messsage.
        /// </value>
        public DirectMailDto Messsage { get; set; }

        #endregion
    }
}