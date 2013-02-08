using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for HiddenMetadataItem class.
    /// </summary>
    [DataContract]
    public class HiddenMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hidden.
        /// </summary>
        /// <value><c>true</c> if this instance is hidden; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsHidden { get; set; }

        #endregion
    }
}
