using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// Base class for Metadata
    /// </summary>
    public abstract class MetadataBase : IMetadata
    {
        #region Constants and Fields

        private List<IMetadata> _children;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataBase"/> class.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        protected MetadataBase ( string resourceName )
        {
            Check.IsNotNullOrWhitespace ( resourceName, () => ResourceName );

            ResourceName = resourceName;
            _children = new List<IMetadata> ();
            MetadataItems = new List<IMetadataItem> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the children.
        /// </summary>
        public IList<IMetadata> Children
        {
            get { return _children.AsReadOnly (); }
            private set { _children = new List<IMetadata> ( value ); }
        }

        /// <summary>
        /// Gets or sets the metadata items.
        /// </summary>
        /// <value>The metadata items.</value>
        public IList<IMetadataItem> MetadataItems { get; set; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        public string ResourceName { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        public abstract IMetadata AddChild ( string resourceName );

        /// <summary>
        /// Finds the child metadata.
        /// </summary>
        /// <param name="childResourceName">Name of the child resource.</param>
        /// <returns>A <see cref="Pillar.Common.Metadata.IMetadata"/></returns>
        public IMetadata FindChildMetadata ( string childResourceName )
        {
            return _children.SingleOrDefault ( c => c.ResourceName == childResourceName );
        }

        /// <summary>
        /// Determines whether the specified child resource name has child.
        /// </summary>
        /// <param name="childResourceName">Name of the child resource.</param>
        /// <returns><c>true</c> if the specified child resource name has child; otherwise, <c>false</c>.</returns>
        public bool HasChild ( string childResourceName )
        {
            return _children.Any ( c => c.ResourceName == childResourceName );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        protected void AddChild ( IMetadata metadata )
        {
            _children.Add ( metadata );
        }

        #endregion
    }
}
