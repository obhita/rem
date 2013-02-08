using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// ManyToOneInspectorExtensions class.
    /// </summary>
    public static class ManyToOneInspectorExtensions
    {
        #region Public Methods

        /// <summary>
        /// Sets the Column.
        /// </summary>
        /// <param name="manyToOneInspector">The many to one inspector.</param>
        /// <param name="columnName">Name of the column.</param>
        public static void Column ( this IManyToOneInspector manyToOneInspector, string columnName )
        {
            var mapping = manyToOneInspector.GetMapping ();
            mapping.Column ( columnName );
        }

        /// <summary>
        /// Sets the Foreign Key.
        /// </summary>
        /// <param name="manyToOneInspector">The many to one inspector.</param>
        /// <param name="foreignKeyName">Name of the foreign key.</param>
        public static void ForeignKey ( this IManyToOneInspector manyToOneInspector, string foreignKeyName )
        {
            ManyToOneMapping mapping = manyToOneInspector.GetMapping ();
            mapping.ForeignKey ( foreignKeyName );
        }

        /// <summary>
        /// Gets the mapping.
        /// </summary>
        /// <param name="manyToOneInspector">The many to one inspector.</param>
        /// <returns>A <see cref="FluentNHibernate.MappingModel.ManyToOneMapping"/></returns>
        public static ManyToOneMapping GetMapping ( this IManyToOneInspector manyToOneInspector )
        {
            var fieldInfo = manyToOneInspector.GetType ().GetField ( "mapping", BindingFlags.NonPublic | BindingFlags.Instance );
            if ( fieldInfo != null )
            {
                var manyToOneMapping = fieldInfo.GetValue ( manyToOneInspector ) as ManyToOneMapping;
                return manyToOneMapping;
            }
            return null;
        }

        /// <summary>
        /// Sets the Index.
        /// </summary>
        /// <param name="manyToOneInspector">The many to one inspector.</param>
        /// <param name="indexName">Name of the index.</param>
        public static void Index ( this IManyToOneInspector manyToOneInspector, string indexName )
        {
            ManyToOneMapping mapping = manyToOneInspector.GetMapping ();
            mapping.Index ( indexName );
        }

        #endregion
    }
}
