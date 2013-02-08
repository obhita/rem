using Pillar.Common.Utility;

namespace Rem.Infrastructure.Mail
{
    /// <summary>
    /// Represents a implementation-agnostic mail attachment
    /// </summary>
    public class MailAttachment
    {
        private readonly string _contentString;
        private readonly byte[] _contentBytes;
        private readonly string _fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAttachment"/> class.
        /// </summary>
        /// <param name="contentString">Content of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        public MailAttachment ( string contentString, string fileName)
        {
            Check.IsNotNullOrWhitespace(contentString, () => this.ContentString);
            Check.IsNotNullOrWhitespace(fileName, () => this.FileName);

            _contentString = contentString;
            _fileName = fileName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAttachment"/> class.
        /// </summary>
        /// <param name="contentBytes">The content bytes.</param>
        /// <param name="fileName">Name of the file.</param>
        public MailAttachment(byte[] contentBytes, string fileName)
        {
            Check.IsNotNull(contentBytes, () => this.ContentBytes);
            Check.IsNotNullOrWhitespace(fileName, () => this.FileName);

            _contentBytes = contentBytes;
            _fileName = fileName;
        }

        /// <summary>
        /// Gets the content string.
        /// </summary>
        public string ContentString
        {
            get { return _contentString; }
        }

        /// <summary>
        /// Gets the content bytes.
        /// </summary>
        public byte[] ContentBytes 
        {
            get { return _contentBytes; }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
        }
    }
}
