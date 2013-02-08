using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem101", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "PrimaryKeyRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlTable ) )]
    public class PrimaryKeyRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            if ( context.ModelElement.Name == null )
            {
                return null;
            }

            var problems = new List<DataRuleProblem> ();

            var sqlTable = ( ISqlTable ) context.ModelElement;

            if ( sqlTable.Name.Parts[ 1 ] != "HiValue" )
            {
                ISqlConstraint primaryKey = sqlTable.GetPrimaryKey();

                if ( primaryKey == null )
                {
                    string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                    "Table [{0}].[{1}] does not have a Primary Key.",
                                                                    context.ModelElement.Name.Parts[ 0 ],
                                                                    context.ModelElement.Name.Parts[ 1 ] );

                    var problem = new DataRuleProblem ( this, ruleProblemDescription, sqlTable )
                                      {
                                          FileName = sqlTable.PrimarySource.SourceName,
                                          StartLine = sqlTable.PrimarySource.StartLine,
                                          StartColumn = sqlTable.PrimarySource.StartColumn
                                      };

                    problems.Add ( problem );
                }
            }

            return problems;
        }
    }
}