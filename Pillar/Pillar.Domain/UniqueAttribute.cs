using System;

namespace Pillar.Domain
{
    /// <summary>
    /// UniqueAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property )]
    public sealed class UniqueAttribute : Attribute
    {
        #region Public Properties

        /// <summary>
        /// It means that all items with this same GroupName are in one group which is unique.
        /// </summary>
        /// <value>The name of the group.</value>
        public string GroupName { get; set; }

        #endregion
    }
}
