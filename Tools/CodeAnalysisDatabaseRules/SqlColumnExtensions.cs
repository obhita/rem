using System.Linq;
using Microsoft.Data.Schema.Sql.SchemaModel;

namespace CodeAnalysisDatabaseRules
{
    public static class SqlColumnExtensions
    {
        public static bool IsPrimaryKey(this ISqlColumn column, ISqlTable table)
        {
            var primaryKey = table.Constraints.OfType<ISqlPrimaryKeyConstraint>().FirstOrDefault();

            return primaryKey != null &&
                   primaryKey.ColumnSpecifications[0].Column.Name.Parts[2] == column.Name.Parts[2];
        }
    }
}
