using System.Runtime.Serialization;

namespace Pillar.Common.Metadata.Dtos
{
    /// <summary>
    /// Data transfer object for DisplayNameMetadataItem class.
    /// </summary>
    [DataContract]
    public class DisplayNameMetadataItemDto : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name of the item.</value>
        [DataMember]
        public string Name { get; set; }

        #endregion
    }
}
