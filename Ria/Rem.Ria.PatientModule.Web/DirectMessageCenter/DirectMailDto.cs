using System;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Direct Mail Data Transfer Object 
    /// </summary>
    public class DirectMailDto : AbstractDataTransferObject
    {
        #region Constants and Fields

        private string _attachmentName;
        private string _from;
        private string _fromName;
        private int _id;
        private bool _isRead;
        private string _message;
        private DateTime _sent;
        private string _subject;
        private string _to;
        private string _toName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the attachment.
        /// </summary>
        /// <value>
        /// The name of the attachment.
        /// </value>
        public virtual string AttachmentName
        {
            get { return _attachmentName; }
            set { ApplyPropertyChange ( ref _attachmentName, () => AttachmentName, value ); }
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From address.
        /// </value>
        public virtual string From
        {
            get { return _from; }
            set { ApplyPropertyChange ( ref _from, () => From, value ); }
        }

        /// <summary>
        /// Gets or sets from name.
        /// </summary>
        /// <value>
        /// From name.
        /// </value>
        public virtual string FromName
        {
            get { return _fromName; }
            set { ApplyPropertyChange(ref _fromName, () => FromName, value); }
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public virtual int Id
        {
            get { return _id; }
            set { ApplyPropertyChange ( ref _id, () => Id, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is read; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsRead
        {
            get { return _isRead; }
            set { ApplyPropertyChange ( ref _isRead, () => IsRead, value ); }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public virtual string Message
        {
            get { return _message; }
            set { ApplyPropertyChange ( ref _message, () => Message, value ); }
        }

        /// <summary>
        /// Gets or sets the sent.
        /// </summary>
        /// <value>
        /// The sent.
        /// </value>
        public virtual DateTime Sent
        {
            get { return _sent; }
            set { ApplyPropertyChange ( ref _sent, () => Sent, value ); }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public virtual string Subject
        {
            get { return _subject; }
            set { ApplyPropertyChange ( ref _subject, () => Subject, value ); }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To Address.
        /// </value>
        public virtual string To
        {
            get { return _to; }
            set { ApplyPropertyChange ( ref _to, () => To, value ); }
        }

        /// <summary>
        /// Gets or sets to name.
        /// </summary>
        /// <value>
        /// To name.
        /// </value>
        public virtual string ToName
        {
            get { return _toName; }
            set { ApplyPropertyChange(ref _toName, () => ToName, value); }
        }

        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        /// <value>
        /// The name of the folder.
        /// </value>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [headers only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [headers only]; otherwise, <c>false</c>.
        /// </value>
        public bool HeadersOnly { get; set; }

        #endregion
    }
}
