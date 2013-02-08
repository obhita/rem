using System.Linq;
using Microsoft.Data.Schema.Sql.SchemaModel;

namespace CodeAnalysisDatabaseRules
{
    public static class SqlForeignKeyConstraintExtensions
    {
        public static bool IsDefinedOnSameColumnsAsPrimaryKey ( this ISqlForeignKeyConstraint foreignKeyConstraint )
        {
            var primaryKeyConstraint = foreignKeyConstraint.DefiningTable.Constraints.OfType<ISqlPrimaryKeyConstraint>().SingleOrDefault();
            if (primaryKeyConstraint != null)
            {
                IOrderedEnumerable<string> primaryKeyColumnNamesOrdered = primaryKeyConstraint.ColumnSpecifications.Select ( p => p.Column.Name.Parts[ 2 ] ).OrderBy ( p => p );
                IOrderedEnumerable<string> foreignKeyColumnNamesOrdered = foreignKeyConstraint.Columns.Select ( p => p.Name.Parts[ 2 ] ).OrderBy ( p => p );
                if ( primaryKeyColumnNamesOrdered.SequenceEqual ( foreignKeyColumnNamesOrdered ) )
                {
                    return true;
                }
            }
            return false;
        }
    }
}