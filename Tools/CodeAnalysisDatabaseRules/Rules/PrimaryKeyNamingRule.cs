using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem102", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "PrimaryKeyNamingRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlPrimaryKeyConstraint ) )]
    public class PrimaryKeyNamingRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            if ( context.ModelElement.Name == null )
            {
                return null;
            }

            var primaryKey = ( ISqlPrimaryKeyConstraint ) context.ModelElement;
            string tableName = primaryKey.DefiningTable.Name.Parts[ 1 ];

            var problems = new List<DataRuleProblem> ();

            string pkName = primaryKey.Name.Parts[ 1 ] ?? string.Empty;
            string properPrimaryKeyName = primaryKey.Name.Parts[ 1 ] != null ? tableName + "_PK" : string.Empty;

            if ( pkName != properPrimaryKeyName )
            {
                string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                "Table [{0}].[{1}] does not have a properly named PrimaryKey. Expected {2}.  Found {3}.",
                                                                context.ModelElement.Name.Parts[ 0 ],
                                                                tableName,
                                                                properPrimaryKeyName,
                                                                pkName );

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