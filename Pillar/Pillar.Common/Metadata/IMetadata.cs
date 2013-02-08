using System.Collections.Generic;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// IMetadata interface.
    /// </summary>
    public interface IMetadata
    {
        #region Public Properties

        /// <summary>
        /// Gets the children.
        /// </summary>
        IList<IMetadata> Children { get; }

        /// <summary>
        /// Gets or sets the metadata items.
        /// </summary>
        /// <value>The metadata items.</value>
        IList<IMetadataItem> MetadataItems { get; set; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        string ResourceName { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata AddChild ( string resourceName );

        /// <summary>
        /// Finds the child metadata.
        /// </summary>
        /// <param name="childResourceName">Name of the child resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        IMetadata FindChildMetadata ( string childResourceName );

        /// <summary>
        /// Determines whether the specified child resource name has child.
        /// </summary>
        /// <param name="childResourceName">Name of the child resource.</param>
        /// <returns><c>true</c> if the specified child resource name has child; otherwise, <c>false</c>.</returns>
        bool HasChild ( string childResourceName );

        #endregion
    }
}
