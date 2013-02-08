using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActiveUp.Net.Mail;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Provides a IMAP Client.
    /// </summary>
    public interface IImapClientProvider
    {
        /// <summary>
        /// Gets the imap4 client.
        /// </summary>
        /// <returns>Imap4 client.</returns>
        Imap4Client GetImapClient ();
    }
}
