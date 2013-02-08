namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Defines an imap message header.
    /// </summary>
    public class ImapMailMessageHeader : MailMessageHeader
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read.
        /// </summary>
        public bool IsRead { get; set; }
    }
}