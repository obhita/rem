using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Sends mail message using SMTP.
    /// </summary>
    public interface ISmtpMailMessageSender : IMailMessageSender
    {
    }
}
