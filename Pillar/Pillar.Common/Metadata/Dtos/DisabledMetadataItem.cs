using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for DisabledMetadataItem class.
    /// </summary>
    [DataContract]
    public class DisabledMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool IsDisabled { get; set; }

        #endregion
    }
}