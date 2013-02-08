using System.Collections;
using System.Collections.Generic;
using Pillar.Common.Extension;

namespace Pillar.FluentRuleEngine.Resources
{
    /// <summary>
    /// String extensions to Handle formating Rule Engine Messages
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Formats a Compare Rule Engine Message Correctly replaces names variables in the string.
        /// </summary>
        /// <param name="formatString">String to format.</param>
        /// <param name="compareValue">Value of the comparison object to put in format string.</param>
        /// <param name="operatorString">Operator the comparison is using, to put in format string.</param>
        /// <returns>A formated <see cref="string"/></returns>
        public static string FormatCompareRuleEngineMessage ( this string formatString, object compareValue, string operatorString )
        {
            var nameDictionary = new Dictionary<string, string>
                {
                    { "CompareValue", compareValue.ToString () },
                    { "ComparisonOperator", operatorString }
                };
            return formatString.FormatRuleEngineMessage ( nameDictionary );
        }

        /// <summary>
        /// Formats a Compare Rule Engine Message Correctly replaces names variables in the string.
        /// </summary>
        /// <param name="formatString">String to format.</param>
        /// <param name="operatorString">Operator the comparison is using, to put in format string.</param>
        /// <returns>A formated <see cref="string"/></returns>
        public static string FormatCompareRuleEngineMessage ( this string formatString, string operatorString )
        {
            var nameDictionary = new Dictionary<string, string> { { "ComparisonOperator", operatorString } };
            return formatString.FormatRuleEngineMessage ( nameDictionary );
        }

        /// <summary>
        /// Formats a Rule Error Message correctly replacing named variables in the string.
        /// </summary>
        /// <param name="formatString">String to format.</param>
        /// <param name="propertyRuleName">Name of property to put in the format string.</param>
        /// <param name="nameDictionary"><see cref="IDictionary{TKey,TValue}">Dictionary</see> of key value pairs to replace in format string.</param>
        /// <returns>A formated <see cref="string"/></returns>
        public static string FormatRuleEngineMessage (
            this string formatString, string propertyRuleName, IDictionary<string, string> nameDictionary = null )
        {
            if ( nameDictionary == null )
            {
                nameDictionary = new Dictionary<string, string> ();
            }
            if (!nameDictionary.ContainsKey ( "PropertyName" ) )
            {
                nameDictionary.Add ( "PropertyName", propertyRuleName );
            }
            return formatString.FormatRuleEngineMessage ( nameDictionary );
        }

        /// <summary>
        /// Formats a Rule Error Message correctly replacing named variables in the string.
        /// </summary>
        /// <param name="formatString">String to format.</param>
        /// <param name="nameDictionary"><see cref="IDictionary{TKey,TValue}">Dictionary</see> of key value pairs to replace in format string.</param>
        /// <returns>A formated <see cref="string"/></returns>
        public static string FormatRuleEngineMessage ( this string formatString, IDictionary<string, string> nameDictionary = null )
        {
            var dictionary = ( nameDictionary ?? new Dictionary<string, string> () );
            return formatString.Inject ( ( IDictionary )dictionary );
        }
    }
}
