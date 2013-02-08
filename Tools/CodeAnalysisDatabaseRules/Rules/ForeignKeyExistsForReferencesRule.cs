using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem103", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "ForeignKeyExistsForReferencesRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlTable ) )]
    public class ForeignKeyExistsForReferencesRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            var table = context.ModelElement as ISqlTable;

            if ( table == null || table.Name.Parts[ 1 ] == "HiValue" )
            {
                return null;
            }

            var problems = new List<DataRuleProblem> ();

            string columnName;

            foreach ( ISqlColumn column in table.Columns )
            {
                columnName = column.Name.Parts[ 2 ];

                if ( columnName.EndsWith ( "Key" ) )
                {
                    if ( columnName == "CreatedAccountKey" ||
                         columnName == "UpdatedAccountKey" ||
                         columnName == "RevisedAccountKey" ||
                         column.IsPrimaryKey(table))
                    {
                        continue;
                    }

                    ISqlForeignKeyConstraint foreignKey = table.GetForeignKeyForColumn(column);

                    if ( foreignKey == null )
                    {
                        string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                        "Table [{0}].[{1}] does not have a Foreign Key for Column {2}.",
                                                                        table.Name.Parts[ 0 ],
                                                                        table.Name.Parts[ 1 ],
                                                                        columnName );

                        var problem = new DataRuleProblem ( this, ruleProblemDescription, table )
                                          {
                                              FileName = table.PrimarySource.SourceName,
                                              StartLine = table.PrimarySource.StartLine,
                                              StartColumn = table.PrimarySource.StartColumn
                                          };

                        problems.Add ( problem );
                    }
                }
            }

            return problems;
        }
    }
}