using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.Schema;
using Microsoft.Data.Schema.Extensibility;
using Microsoft.Data.Schema.Sql.SchemaModel;
using Microsoft.Data.Schema.StaticCodeAnalysis;
using Net.SourceForge.Koogra.Excel2007;

namespace CodeAnalysisDatabaseRules.Rules
{
    [DatabaseSchemaProviderCompatibility(typeof(DatabaseSchemaProvider))]
    [DataRule("CodeAnalysisDatabaseRules", "Rem109", "CodeAnalysisDataBaseRules.CodeAnalysisDataBaseRules",
        "ColumnNamingRule_RuleName", "CategoryResource")]
    [SupportedElementType(typeof(ISqlColumn))]
    public class ColumnNamingRule : StaticCodeAnalysisRule
    {
        static readonly Dictionary<string, List<string>> SqlTypeColumnNameSuffixMapping = GetSqlTypeColumnNameSuffixMappingFromExcelFile(); // GetSqlTypeColumnNameSuffixMapping();

        public override IList<DataRuleProblem> Analyze(DataRuleSetting ruleSetting, DataRuleExecutionContext context)
        {
            var problems = new List<DataRuleProblem>();

            var column = (ISqlColumn) context.ModelElement;

            var schemaName = column.Name.Parts[0];
            var tableName = column.Name.Parts[1];
            var columnName = column.Name.Parts[2];
            var columnTypeName = string.Format(CultureInfo.CurrentCulture, "{0}", column.TypeSpecifier.Type);

            string ruleProblemDescription = null;

            if (columnName != "Version")
            {
                if (!SqlTypeColumnNameSuffixMapping.ContainsKey(columnTypeName))
                {
                    ruleProblemDescription = string.Format(CultureInfo.CurrentCulture,
                                                           "Defining Table [{0}].[{1}]: For column [{2}] of type [{3}], there is no column name suffix mapping defined for [{4}] sql data type.",
                                                           schemaName,
                                                           tableName,
                                                           columnName,
                                                           columnTypeName,
                                                           columnTypeName);
                }
                else
                {
                    var expectedSuffixList = SqlTypeColumnNameSuffixMapping[columnTypeName];
                    var result = expectedSuffixList.Any(columnName.EndsWith);

                    if (!result)
                    {
                        ruleProblemDescription = string.Format(CultureInfo.CurrentCulture,
                                                               "Defining Table [{0}].[{1}]: Column [{2}] of type [{3}] does not end with suffix defined in the column name suffix mapping for [{4}] sql data type.",
                                                               schemaName,
                                                               tableName,
                                                               columnName,
                                                               columnTypeName,
                                                               columnTypeName);
                    }
                }
            }

            if (!String.IsNullOrEmpty(ruleProblemDescription))
            {
                var problem = new DataRuleProblem(this, Environment.NewLine + ruleProblemDescription + Environment.NewLine, column)
                                  {
                                      FileName = column.PrimarySource.SourceName,
                                      StartLine = column.PrimarySource.StartLine,
                                      StartColumn = column.PrimarySource.StartColumn
                                  };

                problems.Add(problem);
            }

            return problems;
        }

        private static Dictionary<string, List<string>> GetSqlTypeColumnNameSuffixMapping()
        {
            var sqlTypeColumnNameSuffixMapping = new Dictionary<string, List<string>>();

            #region Exact Numerics: bigint, int, smallint, tinyint,bit, numeric, decimal, smallmoney, money

            sqlTypeColumnNameSuffixMapping["bigint"] = new List<string> { "Key", "Identifier", "Number", "TimeSpan" };
            sqlTypeColumnNameSuffixMapping["int"] = new List<string> { "Age", "Score", "Amount", "Count", "Number", "Interval", "Measure", "Result" };
            sqlTypeColumnNameSuffixMapping["tinyint"] = new List<string> { "DayOfWeek" };

            sqlTypeColumnNameSuffixMapping["bit"] = new List<string> { "Indicator" };

            sqlTypeColumnNameSuffixMapping["decimal"] = new List<string> { "Score", "Amount", "Average", "Measure", "Percentage", "Rate", "Value" };

            #endregion

            #region Approximate Numerics: float

            sqlTypeColumnNameSuffixMapping["float"] = new List<string> { "Score", "Amount", "Average", "Measure", "Percentage", "Rate", "Value" };

            #endregion

            #region Date and Time : date, datetimeoffset, datetime2, smalldatetime, datetime, time

            sqlTypeColumnNameSuffixMapping["datetimeoffset"] = new List<string> { "Timestamp" };
            sqlTypeColumnNameSuffixMapping["date"] = new List<string> { "Date" };
            sqlTypeColumnNameSuffixMapping["datetime"] = new List<string> { "DateTime" };
            sqlTypeColumnNameSuffixMapping["time"] = new List<string> { "Time" };

            #endregion

            #region Character Strings: char, varchar, text, nchar, nvarchar, ntext

            sqlTypeColumnNameSuffixMapping["nvarchar"] = new List<string> { "Name", "Code", "Description", "Note", "Number", "Address", "Identifier", "Value" };

            #endregion

            #region Binary Strings: binary, varbinary, image

            sqlTypeColumnNameSuffixMapping["varbinary"] = new List<string> { "Document", "Graphic", "Picture", "Video" };

            #endregion

            return sqlTypeColumnNameSuffixMapping;
        }

        private static Dictionary<string, List<string>> GetSqlTypeColumnNameSuffixMappingFromExcelFile()
        {
            const string filePath = "CodeAnalysisDatabaseRules.Rules.ColumnNamingRule.xlsx";
            var assembly = Assembly.GetExecutingAssembly();
            var fileStream = assembly.GetManifestResourceStream(filePath);
            if (fileStream == null)
                throw new FileNotFoundException("xlsx resource not found", filePath);

            var wb2007 = new Workbook(fileStream);
            Worksheet ws2007 = wb2007.GetWorksheet(0);

            const string oneBlank = " ";
            var sqlTypeColumnNameSuffixMapping = new Dictionary<string, List<string>>();

            for (var r = ws2007.CellMap.FirstRow + 1; r <= ws2007.CellMap.LastRow; r++)
            {
                var row = ws2007.GetRow(r);

                string sqlDataTypeName = null;
                string columnNameSuffixListString = null;

                for (var c = ws2007.CellMap.FirstCol; c <= 1; c++)
                {
                    if (row.GetCell(c).Value == null || String.IsNullOrWhiteSpace(row.GetCell(c).Value.ToString()))
                        continue;

                    switch (c)
                    {
                        case 0:
                            sqlDataTypeName = (string)row.GetCell(c).Value;
                            break;

                        case 1:
                            columnNameSuffixListString = (string)row.GetCell(c).Value;
                            break;
                    }
                }

                if (!String.IsNullOrWhiteSpace(sqlDataTypeName) && !String.IsNullOrWhiteSpace(columnNameSuffixListString))
                {
                    var sqlDataTypeKey = sqlDataTypeName.Trim();

                    var regex = new Regex(@"\W+");
                    columnNameSuffixListString = regex.Replace(columnNameSuffixListString, oneBlank);

                    if (columnNameSuffixListString.Trim() != String.Empty)
                    {
                        var columnNameSuffixList = columnNameSuffixListString.Split(new string[] { oneBlank },
                                                                                    StringSplitOptions.
                                                                                        RemoveEmptyEntries);

                        sqlTypeColumnNameSuffixMapping[sqlDataTypeKey] = columnNameSuffixList.ToList();
                    }
                }
            }

            return sqlTypeColumnNameSuffixMapping;
        }
    }
}
