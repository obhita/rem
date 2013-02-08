using System.ComponentModel;
using Pillar.Common.Metadata.Dtos;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Interface for providing metadata.
    /// </summary>
    public interface IMetadataProvider : INotifyPropertyChanged
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the metadata dto.
        /// </summary>
        /// <value>The metadata dto.</value>
        MetadataDto MetadataDto { get; set; }

        #endregion
    }
}
