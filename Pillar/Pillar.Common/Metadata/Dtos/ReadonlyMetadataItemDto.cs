using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for ReadonlyMetadataItem class.
    /// </summary>
    [DataContract]
    public class ReadonlyMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is readonly.
        /// </summary>
        /// <value><c>true</c> if this instance is readonly; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsReadonly { get; set; }

        #endregion
    }
}
