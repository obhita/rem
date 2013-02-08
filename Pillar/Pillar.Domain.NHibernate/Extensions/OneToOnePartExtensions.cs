using FluentNHibernate.Mapping;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// OneToOnePartExtensions class.
    /// </summary>
    public static class OneToOnePartExtensions
    {
        #region Public Methods

        /// <summary>
        /// Maps the child part.
        /// </summary>
        /// <typeparam name="T">The type being mapped.</typeparam>
        /// <param name="childPart">The child part.</param>
        public static void MapChildPart<T> ( this OneToOnePart<T> childPart )
        {
            childPart
                .Cascade.All ()
                .Access.CamelCaseField ( Prefix.Underscore );
        }

        /// <summary>
        /// Maps the parent part.
        /// </summary>
        /// <typeparam name="T">The type being mapped.</typeparam>
        /// <param name="parentPart">The parent part.</param>
        /// <param name="foreignKeyName">Name of the foreign key.</param>
        public static void MapParentPart<T> ( this OneToOnePart<T> parentPart, string foreignKeyName )
        {
            parentPart
                .Access.BackingField ()
                .Constrained ()
                .ForeignKey ( foreignKeyName );
        }

        #endregion
    }
}
