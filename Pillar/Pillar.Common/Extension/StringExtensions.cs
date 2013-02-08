using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if !SILVERLIGHT

#endif

namespace Pillar.Common.Extension
{
    /// <summary>
    /// Extension methods for the <see cref="string"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Extension method that receives an incoming pascal case string and separates the words.
        /// </summary>
        /// <param name="pascalCaseString">Incoming pascal case word</param>
        /// <returns>The seperated string.</returns>
        /// <example>
        /// String str = "PascalCaseWord";
        /// string result = str.SeparatePascalCaseWords ();
        /// -- result = "Pascal Case Word";
        /// </example>
        public static string SeparatePascalCaseWords ( this string pascalCaseString )
        {
            var arry = pascalCaseString.ToCharArray ();
            var sb = new StringBuilder ();
            foreach ( var ch in arry )
            {
                if ( char.IsUpper ( ch ) && sb.Length > 0 )
                {
                    sb.Append ( ' ' );
                }
                sb.Append ( ch );
            }

            var newstr = sb.ToString ();

            return newstr;
        }

        /// <summary>
        /// Replaces all instances of a 'key' (e.g. {foo} or {foo:SomeFormat}) in a string with an optionally formatted value, and returns the result.
        /// </summary>
        /// <param name="formatString">The string containing the key; unformatted ({foo}), or formatted ({foo:SomeFormat})</param>
        /// <param name="key">The key name (foo)</param>
        /// <param name="replacementValue">The replacement value; if null is replaced with an empty string</param>
        /// <returns>The input string with any instances of the key replaced with the replacement value</returns>
        public static string InjectSingleValue ( this string formatString, string key, object replacementValue )
        {
            var result = formatString;

            //regex replacement of key with value, where the generic key format is:
            //Regex foo = new Regex("{(foo)(?:}|(?::(.[^}]*)}))");
            var attributeRegex = new Regex ( "{(" + key + ")(?:}|(?::(.[^}]*)}))" ); //for key = foo, matches {foo} and {foo:SomeFormat}

            //loop through matches, since each key may be used more than once (and with a different format string)
            foreach ( Match m in attributeRegex.Matches ( formatString ) )
            {
                string replacement;
                if ( m.Groups[2].Length > 0 )
                {
                    //do a double string.Format - first to build the proper format string, and then to format the replacement value
                    var attributeFormatString = string.Format ( CultureInfo.InvariantCulture, "{{0:{0}}}", m.Groups[2] );
                    replacement = string.Format ( CultureInfo.CurrentCulture, attributeFormatString, replacementValue );
                }
                else
                {
                    replacement = ( replacementValue ?? string.Empty ).ToString ();
                }

                //perform replacements, one match at a time
                result = result.Replace ( m.ToString (), replacement ); //attributeRegex.Replace(result, replacement, 1);
            }

            return result;
        }

        /// <summary>
        /// Splits the string the into distinct words.
        /// </summary>
        /// <param name="phrase">The given string.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of strings.</returns>
        public static IEnumerable<string> SplitIntoDistinctWords ( this string phrase )
        {
            var distinctWords =
                phrase.ToLower ().Split ( default( string[] ), StringSplitOptions.RemoveEmptyEntries ).ToList ().Distinct ();
            return distinctWords;
        }

        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching hashtable entries.
        /// <remarks>Uses <see cref="string.Format(string,object)"/> internally; custom formats should match those used for that method.</remarks>
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="attributes">A <see cref="IDictionary"/> with keys and values to inject into the string</param>
        /// <returns>A version of the formatString string with hastable keys replaced by (formatted) key values.</returns>
        public static string Inject ( this string formatString, IDictionary attributes )
        {
            var result = formatString;
            if ( attributes == null || formatString == null )
            {
                return result;
            }

            foreach ( string attributeKey in attributes.Keys )
            {
                result = result.InjectSingleValue ( attributeKey, attributes[attributeKey] );
            }
            return result;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Extension method that replaces keys in a string with the values of matching object properties.
        /// </summary>
        /// <param name="formatString">The format string, containing keys like {foo} and {foo:SomeFormat}.</param>
        /// <param name="injectionObject">The object whose properties should be injected in the string</param>
        /// <returns>A version of the formatString string with keys replaced by (formatted) key values.</returns>
        public static string Inject ( this string formatString, object injectionObject )
        {
            return formatString.Inject ( GetPropertyHash ( injectionObject ) );
        }

        /// <summary>
        /// Creates a HashTable based on current object state.
        /// <remarks>Copied from the MVCToolkit HtmlExtensionUtility class</remarks>
        /// </summary>
        /// <param name="properties">The object from which to get the properties</param>
        /// <returns>A <see cref="Hashtable"/> containing the object instance's property names and their values</returns>
        private static IDictionary GetPropertyHash ( object properties )
        {
            IDictionary values = null;
            if ( properties != null )
            {
                values = new Dictionary<string, object> ();
                var props = TypeDescriptor.GetProperties ( properties );
                foreach ( PropertyDescriptor prop in props )
                {
                    values.Add ( prop.Name, prop.GetValue ( properties ) );
                }
            }
            return values;
        }
#endif
        
        /// <summary>
        /// Removes the non alphanumeric characters.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A string without alphanumeric char.</returns>
        public static string RemoveNonAlphanumericChar(this string phoneNumber)
        {
            //var arr = phoneNumber.ToCharArray ();

            //arr = System.Array.FindAll<char> ( arr, ( c => ( char.IsDigit ( c ) ) ) );
            //return new string ( arr );

            return Regex.Replace(phoneNumber, "[^0-9]", string.Empty);
        }

        /// <summary>
        /// Removes the end.
        /// </summary>
        /// <param name="theString">The string.</param>
        /// <param name="stringToRemove">The string to remove.</param>
        /// <returns>A string.</returns>
        public static string RemoveEnd(this string theString, string stringToRemove)
        {
            var result = theString;

            var index = theString.LastIndexOf(stringToRemove, System.StringComparison.Ordinal);
            if (stringToRemove.Length == theString.Length - index)
            {
                result = theString.Substring ( 0, index );
            }

            return result;
        }
    }
}
