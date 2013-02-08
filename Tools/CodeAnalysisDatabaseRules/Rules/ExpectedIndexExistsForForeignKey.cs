using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    /// <summary>
    /// Index with the name as (foreignKeyName + "_IDX") should exist for Foreign Key constraint 
    /// except the columns on which Foreign Key defined are the same columns on which Primary Key defined.
    /// </summary>
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem108", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "ExpectedIndexExistsForForeignKey_RuleName", "CategoryResource")]
    [SupportedElementType ( typeof ( ISqlForeignKeyConstraint ) )]
    public class ExpectedIndexExistsForForeignKey : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            var foreignKeyConstraint = context.ModelElement as ISqlForeignKeyConstraint;
            if (foreignKeyConstraint == null)
            {
                return null;
            }

            if (foreignKeyConstraint.IsDefinedOnSameColumnsAsPrimaryKey())
            {
                return null;
            }

            string foreignKeyName = foreignKeyConstraint.Name.Parts[ 1 ];
            string expectedIndexName = foreignKeyName + "_IDX";

            var definingTable = foreignKeyConstraint.DefiningTable as ISql100Table;

            if ( definingTable == null )
            {
                return null;
            }

            IEnumerable<ISqlIndex> index = definingTable.Indexes.Where ( p => p.Name.Parts[ 2 ] == expectedIndexName );

            var problems = new List<DataRuleProblem> ();

            if ( index.Count () == 0 )
            {
                string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                "Defining Table [{0}].[{1}]: ForeignKey [{3}] does not have an Index named [{2}].",
                                                                definingTable.Name.Parts[ 0 ],
                                                                definingTable.Name.Parts[ 1 ],
                                                                expectedIndexName,
                                                                foreignKeyName );

                var problem = new DataRuleProblem ( this, ruleProblemDescription, foreignKeyConstraint )
                                  {
                                      FileName = foreignKeyConstraint.PrimarySource.SourceName,
                                      StartLine = foreignKeyConstraint.PrimarySource.StartLine,
                                      StartColumn = foreignKeyConstraint.PrimarySource.StartColumn
                                  };

                problems.Add ( problem );
            }

            return problems;
        }
    }
}