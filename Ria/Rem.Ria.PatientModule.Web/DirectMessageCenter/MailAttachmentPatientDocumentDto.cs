using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Data Transfer Object class for mail attachment patient document.
    /// </summary>
    public partial class MailAttachmentPatientDocumentDto : PatientDocumentDto
    {
        /// <summary>
        /// Gets or sets the mail id.
        /// </summary>
        /// <value>
        /// The mail id.
        /// </value>
        public int MailId { get; set; }

        /// <summary>
        /// Gets or sets the name of the mail folder.
        /// </summary>
        /// <value>
        /// The name of the mail folder.
        /// </value>
        public string MailFolderName { get; set; }
    }
}
