using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Net.SourceForge.Koogra.Excel2007;

namespace Excel2Code
{
    /// <summary>
    /// REMINDER!!!
    /// Remove '(lookup)' strings or any invalid text from your domain model.
    /// Remove rows that are marked yellow/ the ones that doesnt have to be in code.
    /// QuestionIdentifiers only apply for assessment, please customize as needed.
    /// Configure in app.config
    /// Make sure the excel is closed.
    /// Make sure the codegenpath is not opened in windows explorer.
    /// </summary>
    internal class Program
    {
        private static readonly string CodeGenPath = ConfigurationManager.AppSettings["codegenpath"];
        private static readonly string ExcelFilePath = ConfigurationManager.AppSettings["excelfilepath"];
        private static readonly string DomainNamespaceNamePrefix = ConfigurationManager.AppSettings["domainnamespaceprefix"];
        private static readonly string AgathaHandlerNamespaceName = ConfigurationManager.AppSettings["agathahandlernamespace"];
        private static readonly string FullyQualifiedAggregateRoot = ConfigurationManager.AppSettings["fullyqualifiedaggregateroot"];
        private static readonly string DataElementsSheetName = ConfigurationManager.AppSettings["dataelementssheetname"];
        private static readonly string LookupsSheetName = ConfigurationManager.AppSettings["lookupssheetname"];
        private static readonly string NonResponseTypeMapperName = ConfigurationManager.AppSettings["nonresponsetypemapper"];

        //Note: both the variables have same data.
        private static readonly Dictionary<string, Dictionary<string, Tuple<string, string>>> TypeDescriptions =
            new Dictionary<string, Dictionary<string, Tuple<string, string>>>();
        private static readonly Dictionary<string, Dictionary<string, Tuple<string, string>>> TypeDescriptionsForAgatha =
            new Dictionary<string, Dictionary<string, Tuple<string, string>>>();

        //Note: both the variables have same data.
        private static readonly Dictionary<string, bool> QuestionIdentifiers = new Dictionary<string, bool>();
        private static readonly Dictionary<string, bool> QuestionIdentifiersForAgatha = new Dictionary<string, bool>();

        private static readonly List<string> Lookups = new List<string>();

        private static void Main()
        {
            var wb2007 = new Workbook(ExcelFilePath);

            Worksheet ws2007 = wb2007.GetWorksheetByName(DataElementsSheetName);

            GetDataElements(ws2007);

            ws2007 = wb2007.GetWorksheetByName(LookupsSheetName);

            GetLookups(ws2007);

            GenerateCode();
        }

        private static void GetLookups(Worksheet ws2007)
        {
            for (uint r = ws2007.CellMap.FirstRow  + 1; r <= ws2007.CellMap.LastRow; r++)
            {
                Row row = ws2007.GetRow(r);
                string moduleName = null, lookupName = null;

                bool hasValue = false;
                for (uint c = ws2007.CellMap.FirstCol; c <= ws2007.CellMap.LastCol; c++)
                {
                    if (row.GetCell(c).Value == null || string.IsNullOrWhiteSpace(row.GetCell(c).Value.ToString()))
                        continue;

                    switch (c)
                    {
                        case 0:
                            moduleName = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;

                        case 1:
                            lookupName = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;
                    }
                }

                if (!hasValue)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(moduleName) || string.IsNullOrWhiteSpace(lookupName))
                {
                    throw new ApplicationException("Incomplete data in Sheet : " + LookupsSheetName + " Row: " + r);
                }

                string fullyQualifiedClassName = DomainNamespaceNamePrefix + moduleName + "." + lookupName;

                if (!Lookups.Contains(fullyQualifiedClassName))
                {
                    Lookups.Add(fullyQualifiedClassName);
                }
            }
        }

        private static void GetDataElements(Worksheet ws2007)
        {
            for (uint r = ws2007.CellMap.FirstRow + 1; r <= ws2007.CellMap.LastRow; r++)
            {
                Row row = ws2007.GetRow(r);

                string moduleName = null;
                string className = null;
                string propertyName = null;
                string propertyType = null;
                string questionNumber = null;

                bool hasValue = false;
                for (uint c = ws2007.CellMap.FirstCol; c <= ws2007.CellMap.LastCol; c++)
                {
                    if (row.GetCell(c).Value == null || string.IsNullOrWhiteSpace(row.GetCell(c).Value.ToString()))
                        continue;

                    switch (c)
                    {
                        case 0:
                            moduleName = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;

                        case 1:
                            className = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;

                        case 2:
                            propertyName = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;

                        case 3:
                            propertyType = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;

                        case 4:
                            questionNumber = (string) row.GetCell(c).Value;
                            hasValue = true;
                            continue;
                    }
                }

                if (!hasValue)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(moduleName) || string.IsNullOrWhiteSpace(className) ||
                    string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(propertyType))
                {
                    throw new ApplicationException("Incomplete data in Sheet : " + DataElementsSheetName + " Row: " + r);
                }

                string fullyQualifiedClassName = DomainNamespaceNamePrefix + moduleName + "." + className;

                AddTypeDescriptionAndQuestionIdentifiers(propertyName, propertyType, fullyQualifiedClassName, questionNumber, TypeDescriptions, QuestionIdentifiers);
                AddTypeDescriptionAndQuestionIdentifiers(propertyName, propertyType, fullyQualifiedClassName, questionNumber, TypeDescriptionsForAgatha, QuestionIdentifiersForAgatha);
            }
        }

        private static void AddTypeDescriptionAndQuestionIdentifiers(string propertyName, string propertyType, string fullyQualifiedClassName, string questionNumber, Dictionary<string, Dictionary<string, Tuple<string, string>>> typeDescription, Dictionary<string, bool> questionIdentifiers)
        {
            if (!typeDescription.ContainsKey(fullyQualifiedClassName))
            {
                var properties = new Dictionary<string, Tuple<string, string>>
                                     {
                                         {propertyName, new Tuple<string, string>(propertyType, questionNumber)}
                                     };

                typeDescription.Add(fullyQualifiedClassName, properties);
            }
            else
            {
                Dictionary<string, Tuple<string, string>> properties = typeDescription[fullyQualifiedClassName];
                if (propertyName != null)
                {
                    if (properties.ContainsKey(propertyName))
                    {
                        return;
                    }

                    properties.Add(propertyName, new Tuple<string, string>(propertyType, questionNumber));
                }
            }

            string questionIdentifier = GetQuestionIdentifier(fullyQualifiedClassName, questionNumber);
            if (!string.IsNullOrWhiteSpace(questionIdentifier))
            {
                if (!questionIdentifiers.ContainsKey(questionIdentifier))
                {
                    questionIdentifiers.Add(questionIdentifier, false);
                }
                else
                {
                    //Has multiple fields for given question.
                    questionIdentifiers[questionIdentifier] = true;
                }
            }
        }

        private static void GenerateCode()
        {
            try
            {
                if (Directory.Exists(CodeGenPath))
                {
                    Directory.Delete(CodeGenPath, true);
                }
            }
            catch (Exception ex1)
            {
                Console.WriteLine("Unable to delete " + CodeGenPath +" directory : " + ex1);
                return;
            }

            try
            {
                Directory.CreateDirectory(CodeGenPath);
            }
            catch (Exception ex2)
            {
                Console.WriteLine("Unable to delete " + CodeGenPath + " directory : " + ex2);
                return;
            }

            try
            {
                #region Lookups
                foreach (string lookup in Lookups)
                {
                    string lookupName = GetClassNameFromFullyQualifiedClassName(lookup);

                    string nameSpaceName = GetNamespaceNameFromFullyQualifiedClassName(lookup);

                    string folderName = CodeGenPath + "\\" + nameSpaceName;

                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    string code = GetLookupCode(lookup);
                    using (FileStream fs = File.Create(folderName + @"\\" + lookupName + ".cs"))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(code);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                #endregion

                #region Domain classes
                foreach (var typeDescription in TypeDescriptions)
                {
                    string code = GetCode(typeDescription);

                    string className = GetClassNameFromFullyQualifiedClassName(typeDescription.Key);

                    string nameSpaceName = GetNamespaceNameFromFullyQualifiedClassName(typeDescription.Key);

                    string folderName = CodeGenPath + "\\" + nameSpaceName;

                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    using (FileStream fs = File.Create(folderName + @"\\" + className + ".cs"))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(code);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                #endregion

                #region Agatha handlers

                foreach (var typeDescription in TypeDescriptionsForAgatha)
                {
                    if (typeDescription.Key == FullyQualifiedAggregateRoot)
                    {
                        continue;
                    }

                    string code = GenerateAgathaHandlerCode(typeDescription);

                    string agathaHandlerClassName = "Save" + GetClassNameFromFullyQualifiedClassName(typeDescription.Key) + "RequestHandler";

                    string folderName = CodeGenPath + "\\" + AgathaHandlerNamespaceName;

                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }

                    using (FileStream fs = File.Create(folderName + @"\\" + agathaHandlerClassName + ".cs"))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(code);
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
                #endregion
            }
            catch (Exception ex3)
            {
                Console.WriteLine("Unable to create code files :" + ex3);
                return;
            }
        }

        private static string GetLookupCode(string lookup)
        {
            var sb = new StringBuilder();
            string namespaceName = GetNamespaceNameFromFullyQualifiedClassName(lookup);
            string className = GetClassNameFromFullyQualifiedClassName(lookup);

            sb.AppendLine("namespace " + namespaceName);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic class " + className + " : LookupBase");
            sb.AppendLine("\t{");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string GetCode(KeyValuePair<string, Dictionary<string, Tuple<string, string>>> typeDescription)
        {
            var sb = new StringBuilder();
            string namespaceName = GetNamespaceNameFromFullyQualifiedClassName(typeDescription.Key);
            string className = GetClassNameFromFullyQualifiedClassName(typeDescription.Key);

            sb.AppendLine("namespace " + namespaceName);
            sb.AppendLine("{");

            sb.AppendLine("\tpublic class " + className);
            sb.AppendLine("\t{");

            sb.AppendLine("\t\tprotected internal " + className + " ()");
            sb.AppendLine("\t\t{}");

            for (int i = 0; i < typeDescription.Value.Keys.Count; i++)
            {
                string propertyKey = typeDescription.Value.Keys.ElementAt(i);
                Tuple<string, string> propertyValue = typeDescription.Value[propertyKey];

                string fieldName = GetFieldName(propertyKey);
                sb.AppendLine();
                sb.AppendLine("\t\tprivate " + propertyValue.Item1 + " " + fieldName + ";");
                sb.AppendLine();
                if (!string.IsNullOrWhiteSpace(propertyValue.Item2))
                {
                    sb.Append(GetPropertyXmlCommentCode(propertyValue.Item2));
                }
                sb.AppendLine("\t\tpublic virtual " + propertyValue.Item1 + " " + propertyKey);
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tget { return " + fieldName + "; }");
                sb.AppendLine("\t\t\tset { ApplyPropertyChange ( ref " + fieldName + ", () => " + propertyKey +
                              ", value ); }");
                sb.AppendLine("\t\t}");

                //// Note: Special case for DENS-ASI (generates a note for each question).
                //if (!string.IsNullOrWhiteSpace(propertyValue.Item2) &&
                //    !HasMoreThanOneFieldWithGivenQuestionNumber(propertyValue.Item2, typeDescription.Value))
                //{
                //    string questionIdentifier = GetQuestionIdentifier(typeDescription.Key, propertyValue.Item2);
                //    if (QuestionIdentifiers.ContainsKey(questionIdentifier))
                //    {
                //        string noteFieldName = QuestionIdentifiers[questionIdentifier]
                //                                   ? GetFieldName(propertyValue.Item2) + "QuestionNote"
                //                                   : fieldName + "Note";
                //        string notePropertyName = QuestionIdentifiers[questionIdentifier] ? propertyValue.Item2 + "QuestionNote" : propertyKey + "Note";
                //        sb.AppendLine("\t\tprivate string " + noteFieldName + ";");
                //        sb.AppendLine();
                //        if (!string.IsNullOrWhiteSpace(propertyValue.Item2))
                //        {
                //            sb.Append(GetPropertyXmlCommentCode(propertyValue.Item2));
                //        }
                //        sb.AppendLine("\t\tpublic virtual string " + notePropertyName);
                //        sb.AppendLine("\t\t{");
                //        sb.AppendLine("\t\t\tget { return " + noteFieldName + "; }");
                //        sb.AppendLine("\t\t\tset { ApplyPropertyChange ( ref " + noteFieldName + ", () => " +
                //                      notePropertyName + ", value ); }");
                //        sb.AppendLine("\t\t}");

                //        QuestionIdentifiers.Remove(questionIdentifier);
                //    }
                //}

                typeDescription.Value.Remove(propertyKey);
                i--;
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }

        private static string GetPropertyXmlCommentCode(string questionNumber)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\t\t/// <summary>");
            sb.AppendLine(string.Format("\t\t/// Question Number: {0}", questionNumber));
            sb.AppendLine("\t\t/// </summary>");

            return sb.ToString();
        }

        private static string GenerateAgathaHandlerCode(KeyValuePair<string, Dictionary<string, Tuple<string, string>>> typeDescription)
        {
            string className = GetClassNameFromFullyQualifiedClassName(typeDescription.Key);
            string agathaRequestHandlerName = "Save" + className + "RequestHandler";
            string dtoClassName = className + "Dto";
            string baseClassName = "SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<" + dtoClassName + ">, DtoResponse<" + dtoClassName +">, " + dtoClassName +", " +
                                   FullyQualifiedAggregateRoot + ">";
            string dtoObjectName = GetParameterName(dtoClassName);
            string aggregateRootObjectName = GetParameterName(FullyQualifiedAggregateRoot);
            string mappingMethodParameterSignature = dtoClassName + " " + dtoObjectName +
                                            ", " + FullyQualifiedAggregateRoot + " " + aggregateRootObjectName;

            var sb = new StringBuilder();

            sb.AppendLine("namespace " + AgathaHandlerNamespaceName);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic class " + agathaRequestHandlerName + " : " + baseClassName);
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tprivate readonly IDtoToDomainMappingHelper _mappingHelper;");
            sb.AppendLine("\t\tprivate bool _mappingResult = true;");
            sb.AppendLine();
            sb.AppendLine("\t\tpublic " + agathaRequestHandlerName + " ( IDtoToDomainMappingHelper mappingHelper )");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_mappingHelper = mappingHelper;");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\t#region Overrides of " + baseClassName);
            sb.AppendLine("\t\tprotected override bool ProcessSingleAggregate ( " + mappingMethodParameterSignature + " )");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t_mappingResult &= MappingProperties ( " + dtoObjectName + ", " + aggregateRootObjectName +
                          " );");
            sb.AppendLine();
            sb.AppendLine("\t\t\treturn _mappingResult;");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\t#endregion");
            sb.AppendLine();
            sb.AppendLine("\t\tprivate bool MappingProperties ( " + mappingMethodParameterSignature + " )");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tbool result = new PropertyMapper<" + FullyQualifiedAggregateRoot+"> (" + aggregateRootObjectName + "," + dtoObjectName + " )");

            for (int i = 0; i < typeDescription.Value.Keys.Count; i++)
            {
                string propertyKey = typeDescription.Value.Keys.ElementAt(i);
                Tuple<string, string> propertyValue = typeDescription.Value[propertyKey];
                string nonResponseTemplateArgumentType = GetNonResponseTemplateArgumentType(propertyValue.Item1);

                if (propertyValue.Item1.Contains("NonResponseType"))
                {
                    if (nonResponseTemplateArgumentType.StartsWith("DensAsi"))
                    {
                        sb.AppendLine("\t\t\t\t.MapProperty ( x => x." + className + "." + propertyKey + ", " +
                                      NonResponseTypeMapperName + ".MapToDensAsiNonResponseType<" +
                                      nonResponseTemplateArgumentType + "> ( " + dtoObjectName + "." +
                                      propertyKey + ", _mappingHelper ) )");
                    }
                    else
                    {
                        sb.AppendLine("\t\t\t\t.MapProperty ( x => x." + className + "." + propertyKey + ", " +
                                      NonResponseTypeMapperName + ".MapToDensAsiNonResponseType ( " + dtoObjectName + "." +
                                      propertyKey + ", _mappingHelper ) )");
                    }
                }
                else if (propertyValue.Item1.Contains("DensAsi"))
                {
                    string paramaterName = GetParameterName(propertyKey);

                    sb.AppendLine("\t\t\t\t.MapProperty ( x => x." + className + "." + propertyKey + ", " + paramaterName + " )");
                }
                else
                {
                    sb.AppendLine("\t\t\t\t.MapProperty ( x => x." + className + "." + propertyKey + ", " + dtoObjectName + "." + propertyKey + " )");
                }

                // Note: Special case for DENS-ASI (generates a note for each question).
                if (!string.IsNullOrWhiteSpace(propertyValue.Item2) &&
                    !HasMoreThanOneFieldWithGivenQuestionNumber(propertyValue.Item2, typeDescription.Value))
                {
                    string questionIdentifier = GetQuestionIdentifier(typeDescription.Key, propertyValue.Item2);
                    if (QuestionIdentifiersForAgatha.ContainsKey(questionIdentifier))
                    {
                        string notePropertyName = QuestionIdentifiersForAgatha[questionIdentifier] ? propertyValue.Item2 + "QuestionNote" : propertyKey + "Note";

                        sb.AppendLine("\t\t\t\t.MapProperty ( x => x." + className + "." + notePropertyName + ", " + dtoObjectName + "." + notePropertyName + " )");

                        QuestionIdentifiersForAgatha.Remove(questionIdentifier);
                    }
                }

                typeDescription.Value.Remove(propertyKey);
                i--;
            }

            sb.AppendLine("\t\t\t\t.Map ();");
            sb.AppendLine();
            sb.AppendLine("\t\t\treturn result;");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        #region Helpers

        private static string GetNonResponseTemplateArgumentType(string fullyQualifiedClassName)
        {
            string className = fullyQualifiedClassName.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last();
            return className.Split(new[] {'<', '>'}, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        private static string GetQuestionIdentifier(string fullyQualifiedClassName, string questionNumber)
        {
            if (string.IsNullOrWhiteSpace(questionNumber))
                return null;

            return fullyQualifiedClassName + "_" + questionNumber;
        }

        private static string GetClassNameFromFullyQualifiedClassName(string fullyQualifiedClassName)
        {
            return fullyQualifiedClassName.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        private static string GetNamespaceNameFromFullyQualifiedClassName(string fullyQualifiedClassName)
        {
            return fullyQualifiedClassName.Remove(fullyQualifiedClassName.LastIndexOf('.'));
        }

        /// <summary>
        /// Used to is the given question is the last one, and add a note property.
        /// </summary>
        /// <param name="questionNumber"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static bool HasMoreThanOneFieldWithGivenQuestionNumber(string questionNumber,
                                                                       Dictionary<string, Tuple<string, string>>
                                                                           properties)
        {
            int count = 0;
            foreach (var property in properties)
            {
                if (!string.IsNullOrWhiteSpace(property.Value.Item2) &&
                    property.Value.Item2.Equals(questionNumber, StringComparison.OrdinalIgnoreCase))
                    count++;

                if (count > 1)
                    return true;
            }

            return false;
        }

        private static string GetFieldName(string propertyName)
        {
            return "_" + propertyName[0].ToString().ToLower() + propertyName.Substring(1);
        }

        private static string GetParameterName(string fullyQualifiedClassNameOrPropertyName)
        {
            string className = fullyQualifiedClassNameOrPropertyName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
            return className[0].ToString().ToLower() + className.Substring(1);
        }


        public static string RemoveSuffix(string propertyName)
        {
            var suffixList = new List<string> { "Name", "Code", "Description", "Note", "Number", "Address", "Identifier", "Value", 
                "Age", "Score", "Amount", "Count", "Number", "Interval", "Measure", "Result", "Timestamp", "Date", "Time", "DateTime", "Date", "Time", "DateTime", "DateRange", "TimeRange", "Key", 
                "Identifier", "Number", "Score", "Amount", "Average", "Measure", "Percentage", "Rate", "Score", "Value",  "Indicator", "Document", "Graphic", "Picture", "Video" , "DayOfWeek", "MedicationDoseTiming", "LabFacility",  "LabResult"};

            foreach (var suffix in suffixList)
            {
                if (propertyName.EndsWith(suffix))
                {
                    propertyName = propertyName.Remove(propertyName.Length - suffix.Length);
                }
            }

            return propertyName;
        }

        #endregion
    }
}