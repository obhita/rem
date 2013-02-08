using System.Linq;
using Microsoft.Data.Schema.Sql.SchemaModel;

namespace CodeAnalysisDatabaseRules
{
    public static class SqlTableExtensions
    {
        public static ISqlConstraint GetPrimaryKey(this ISqlTable table)
        {
            var primaryKey = table.Constraints.OfType<ISqlPrimaryKeyConstraint>().FirstOrDefault();
            return primaryKey;
        }

        public static ISqlForeignKeyConstraint GetForeignKeyForColumn(this ISqlTable table, ISqlColumn column)
        {
            var foreignKeys =
                table.Constraints.OfType<ISqlForeignKeyConstraint>();

            foreach ( var foreignKey in foreignKeys )
            {
                var columns = foreignKey.Columns.Where(p => p.Name.Parts[2] == column.Name.Parts[2]);
                
                if (columns.Count() == 1)
                {
                    return foreignKey;
                }
            }
            
            return null;
        }

        public static bool HasFoeignKeyAlsoDefinedOnPrimaryKeyColumns(this ISqlTable table)
        {
            var foreignKeyConstraints = table.Constraints.OfType<ISqlForeignKeyConstraint> ().ToList();

            return foreignKeyConstraints.Count != 0 && 
                foreignKeyConstraints.Any ( foreignKeyConstraint => foreignKeyConstraint.IsDefinedOnSameColumnsAsPrimaryKey () );
        }
    }
}
