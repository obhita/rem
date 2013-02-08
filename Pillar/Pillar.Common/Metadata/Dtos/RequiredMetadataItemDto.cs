using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for RequiredMetadataItem class.
    /// </summary>
    [DataContract]
    public class RequiredMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value><c>true</c> if this instance is required; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsRequired { get; set; }

        #endregion
    }
}
