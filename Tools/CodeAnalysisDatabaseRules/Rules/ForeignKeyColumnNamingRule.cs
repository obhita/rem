using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    /// <summary>
    /// Foreign key column should named after (matches or ends with) the primary key column name of the referential table if the referential table's primary key column is NOT a foeign key column.
    /// Foreign key column should named after (matches or ends with) the referential  table's name + "Key" if the referential  table's primary key column IS a foeign key column.
    /// </summary>
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRule ( "CodeAnalysisDatabaseRules", "Rem107", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "ForeignKeyColumnNamingRule_RuleName", "CategoryResource")]
    [SupportedElementType ( typeof ( ISqlForeignKeyConstraint ) )]
    public class ForeignKeyColumnNamingRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            var foreignKeyConstraint = context.ModelElement as ISqlForeignKeyConstraint;

            if ( foreignKeyConstraint == null )
            {
                return null;
            }

            ISqlSpecifiesTable definingTable = foreignKeyConstraint.DefiningTable;
            ISqlTable referentialTable = foreignKeyConstraint.ForeignTable;

            string foreignKeyColumnName = foreignKeyConstraint.Columns.First().Name.Parts[2];

            IList<DataRuleProblem> problems = new List<DataRuleProblem>();

            if (referentialTable.HasFoeignKeyAlsoDefinedOnPrimaryKeyColumns())
            {
                bool result = foreignKeyColumnName == referentialTable.Name.Parts[1] + "Key";

                if (!result)
                {
                    string ruleProblemDescription = string.Format(CultureInfo.CurrentCulture,
                                                                    "Defining Table [{0}].[{1}]: Foreign key [{2}] column is not named after the table name of the referential table [{3}].[{4}].  Expected {5}. Found {6}",
                                                                    definingTable.Name.Parts[0],
                                                                    definingTable.Name.Parts[1],
                                                                    foreignKeyConstraint.Name.Parts[1],
                                                                    referentialTable.Name.Parts[0],
                                                                    referentialTable.Name.Parts[1],
                                                                    referentialTable.Name.Parts[1] + "Key",
                                                                    foreignKeyColumnName);

                    var problem = CreateDateRuleProblem(foreignKeyConstraint, ruleProblemDescription);
                    problems.Add(problem);
                }
            }
            else
            {
                ISqlPrimaryKeyConstraint referentialTablePrimaryKey =
                referentialTable.Constraints.OfType<ISqlPrimaryKeyConstraint>().SingleOrDefault();

                string referentialTablePrimaryKeyColumnName =
                    referentialTablePrimaryKey.ColumnSpecifications[0].Column.Name.Parts[2];

                bool columnsNameMatch = foreignKeyColumnName == referentialTablePrimaryKeyColumnName;

                bool result = columnsNameMatch || foreignKeyColumnName.EndsWith(referentialTablePrimaryKeyColumnName);

                if (!result)
                {

                    string ruleProblemDescription = string.Format(CultureInfo.CurrentCulture,
                                                                    "Defining Table [{0}].[{1}]: Foreign key [{2}] column is not named after (matches or ends with) the primary key column name of the referential table [{3}].[{4}].  Expected {5}. Found {6}",
                                                                    definingTable.Name.Parts[0],
                                                                    definingTable.Name.Parts[1],
                                                                    foreignKeyConstraint.Name.Parts[1],
                                                                    referentialTable.Name.Parts[0],
                                                                    referentialTable.Name.Parts[1],
                                                                    referentialTablePrimaryKeyColumnName,
                                                                    foreignKeyColumnName);

                    var problem = CreateDateRuleProblem ( foreignKeyConstraint, ruleProblemDescription );
                    problems.Add(problem);
                }
            }

            return problems;
        }

        private DataRuleProblem CreateDateRuleProblem(ISqlForeignKeyConstraint foreignKeyConstraint, string ruleProblemDescription)
        {
            var problem = new DataRuleProblem(this, ruleProblemDescription, foreignKeyConstraint)
            {
                FileName = foreignKeyConstraint.PrimarySource.SourceName,
                StartLine = foreignKeyConstraint.PrimarySource.StartLine,
                StartColumn = foreignKeyConstraint.PrimarySource.StartColumn
            };

            return problem;
        }
    }
}