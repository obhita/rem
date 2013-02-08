using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem106", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "SinglePrimaryKeyPerTableRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlTable ) )]
    public class SinglePrimaryKeyPerTableRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            if ( context.ModelElement.Name == null )
            {
                return null;
            }

            var sqlTable = ( ISqlTable ) context.ModelElement;

            IEnumerable<ISqlPrimaryKeyConstraint> primaryKeys = sqlTable.Constraints.OfType<ISqlPrimaryKeyConstraint> ();

            var problems = new List<DataRuleProblem> ();

            if ( primaryKeys.Count () > 1 )
            {
                string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                "Table [{0}].[{1}] has more than 1 PrimaryKey.",
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

            return problems;
        }
    }
}