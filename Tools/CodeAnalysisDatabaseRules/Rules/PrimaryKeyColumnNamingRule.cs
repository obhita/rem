using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRule ( "CodeAnalysisDatabaseRules", "Rem105", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "PrimaryKeyColumnNamingRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlPrimaryKeyConstraint ) )]
    public class PrimaryKeyColumnNamingRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            if ( context.ModelElement.Name == null )
            {
                return null;
            }

            var primaryKey = ( ISqlPrimaryKeyConstraint ) context.ModelElement;
            string columnName = primaryKey.ColumnSpecifications[ 0 ].Column.Name.Parts[ 2 ];
            string tableName = primaryKey.DefiningTable.Name.Parts[ 1 ];

            var problems = new List<DataRuleProblem> ();

            string properyPrimaryKeyColumnName = tableName + "Key";
            if ( columnName != properyPrimaryKeyColumnName )
            {
                string errorMessage =
                    string.Format (
                        "Table [{0}].[{1}] does not have a properly named PrimaryKey Column. Expected {2}.  Found {3}.",
                        context.ModelElement.Name.Parts[ 0 ],
                        tableName,
                        properyPrimaryKeyColumnName,
                        columnName );

                string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture, errorMessage );

                var problem = new DataRuleProblem ( this, ruleProblemDescription, primaryKey.DefiningTable )
                                  {
                                      FileName = primaryKey.DefiningTable.PrimarySource.SourceName,
                                      StartLine = primaryKey.DefiningTable.PrimarySource.StartLine,
                                      StartColumn = primaryKey.DefiningTable.PrimarySource.StartColumn
                                  };

                problems.Add ( problem );
            }

            return problems;
        }
    }
}