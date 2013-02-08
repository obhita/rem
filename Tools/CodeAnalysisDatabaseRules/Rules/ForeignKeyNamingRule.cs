using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility ( typeof ( DatabaseSchemaProvider ) )]
    [DataRuleAttribute ( "CodeAnalysisDatabaseRules", "Rem104", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "ForeignKeyNamingRule_RuleName", "CategoryResource" )]
    [SupportedElementType ( typeof ( ISqlForeignKeyConstraint ) )]
    public class ForeignKeyNamingRule : StaticCodeAnalysisRule
    {
        public override IList<DataRuleProblem> Analyze ( DataRuleSetting ruleSetting, DataRuleExecutionContext context )
        {
            var foreignKeyConstraint = context.ModelElement as ISqlForeignKeyConstraint;

            if ( foreignKeyConstraint == null )
            {
                return null;
            }

            ISqlSpecifiesTable definingTable = foreignKeyConstraint.DefiningTable;
            ISqlTable foreignTable = foreignKeyConstraint.ForeignTable;

            IList<DataRuleProblem> problems = new List<DataRuleProblem> ();

            string prefix = definingTable.Name.Parts[ 1 ] + "_" + foreignTable.Name.Parts[ 1 ];
            const string suffix = "_FK";

            string foreignKeyPattern = @"^" + prefix + @"[a-zA-Z0-9_]*" + suffix;

            if ( !Regex.IsMatch ( foreignKeyConstraint.Name.Parts[ 1 ], foreignKeyPattern ) )
            {
                string ruleProblemDescription = string.Format ( CultureInfo.CurrentCulture,
                                                                "Defining Table [{0}].[{1}]: ForeignKey is named improperly. Expected to start with {2} and end with {3}. Found {4}",
                                                                definingTable.Name.Parts[ 0 ],
                                                                definingTable.Name.Parts[ 1 ],
                                                                prefix,
                                                                suffix,
                                                                foreignKeyConstraint.Name.Parts[ 1 ] );

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