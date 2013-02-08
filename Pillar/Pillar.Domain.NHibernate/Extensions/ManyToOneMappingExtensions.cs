using System;
using System.Linq;
using FluentNHibernate.MappingModel;
using FluentNHibernate.MappingModel.ClassBased;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// ManyToOneMappingExtensions class.
    /// </summary>
    public static class ManyToOneMappingExtensions
    {
        #region Public Methods

        /// <summary>
        /// Sets the column.
        /// </summary>
        /// <param name="manyToOneMapping">The many to one mapping.</param>
        /// <param name="columnName">Name of the column.</param>
        public static void Column ( this ManyToOneMapping manyToOneMapping, string columnName )
        {
            if ( manyToOneMapping.Columns.UserDefined.Count () > 0 )
            {
                return;
            }

            var originalColumn = manyToOneMapping.Columns.FirstOrDefault ();
            var column = originalColumn == null ? new ColumnMapping () : originalColumn.Clone ();

            column.Name = columnName;

            manyToOneMapping.ClearColumns ();
            manyToOneMapping.AddColumn ( column );
        }

        /// <summary>
        /// Sets the Foreign Key.
        /// </summary>
        /// <param name="manyToOneMapping">The many to one mapping.</param>
        /// <param name="foreignKeyName">Name of the foreign key.</param>
        public static void ForeignKey ( this ManyToOneMapping manyToOneMapping, string foreignKeyName )
        {
            if ( !manyToOneMapping.IsSpecified ( "ForeignKey" ) )
            {
                manyToOneMapping.ForeignKey = foreignKeyName;
            }
        }

        /// <summary>
        /// Gets the name of the foreign key.
        /// </summary>
        /// <param name="manyToOneMapping">The many to one mapping.</param>
        /// <param name="componentMapping">The component mapping.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string GetForeignKeyName ( this ManyToOneMapping manyToOneMapping, IComponentMapping componentMapping, Type entityType )
        {
            var manyToOnePropertyType = manyToOneMapping.Member.PropertyType;
            var propertyTypeName = manyToOnePropertyType.Name;
            if ( typeof( ILookup ).IsAssignableFrom ( manyToOnePropertyType ) )
            {
                propertyTypeName += "Lkp";
            }
            var namingStrategy = componentMapping.GetNamingStrategy ();
            var columnName = namingStrategy.GetColumnName (
                componentMapping.Member.DeclaringType,
                componentMapping.Member.PropertyType,
                componentMapping.Name,
                manyToOneMapping.Member.DeclaringType,
                manyToOneMapping.Member.PropertyType,
                manyToOneMapping.Name,
                true );

            var referenceName = string.Format ( "{0}_{1}", entityType.Name, propertyTypeName );

            const string ForeignKeyNameSuffix = "_FK";
            if ( columnName != propertyTypeName.Replace ( "Lkp", string.Empty ) )
            {
                referenceName = string.Format ( "{0}_{1}_{2}", entityType.Name, propertyTypeName, columnName );
            }

            var foreignKeyName = string.Format ( "{0}{1}", referenceName, ForeignKeyNameSuffix );

            return foreignKeyName;
        }

        /// <summary>
        /// Sets the Index.
        /// </summary>
        /// <param name="manyToOneMapping">The many to one mapping.</param>
        /// <param name="indexName">Name of the index.</param>
        public static void Index ( this ManyToOneMapping manyToOneMapping, string indexName )
        {
            if ( manyToOneMapping.Columns.First ().IsSpecified ( "Index" ) )
            {
                return;
            }

            foreach ( var column in manyToOneMapping.Columns )
            {
                column.Index = indexName;
            }
        }

        #endregion
    }
}
