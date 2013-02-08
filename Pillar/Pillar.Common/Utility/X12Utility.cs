#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Globalization;
using System.Linq;

namespace Pillar.Common.Utility
{
    /// <summary>
    ///  Utility for X12 parsing and generation.
    /// </summary>
    public class X12Utility
    {
        #region Public Methods and Operators

        /// <summary>
        /// Builds the composite element.
        /// </summary>
        /// <param name="compositeDelimiter">The composite delimiter.</param>
        /// <param name="components">The components.</param>
        /// <returns>A composite element.</returns>
        public static string BuildCompositeElement ( char compositeDelimiter, params string[] components )
        {
            var result = string.Empty;

            if ( components != null && components.Length > 0 ) 
            {
                result = components.Aggregate ( ( i, j ) => i + compositeDelimiter + j );
            }

            return result;
        }

        /// <summary>
        /// Converts to decimal string.
        /// </summary>
        /// <param name="decimalValue">The decimal value.</param>
        /// <returns>The decimal string.</returns>
        /// <remarks>
        /// What are the X12 syntax rules for decimal numbers? Is the example above correct?
        /// According to the X12 standards syntax rules, the decimal numbers above are correct for reporting. 
        /// In the X12 standards, these types of numbers are R data types. Below are some of the syntax rules (the ones that seem to apply in this case) from the X12.6 Application Architecture Controls Standard. 
        /// • The decimal point always appears in the character stream if the decimal point is at any place other than the right end. 
        /// • If the value is an integer (decimal point at the right end), the decimal point should be omitted.
        /// • Trailing zeros following the decimal point should be suppressed unless necessary to indicate precision.
        /// Sample:
        /// $100.00 reported as 100 
        /// $100.70 reported as 100.7 
        /// $100.75 reported as 100.75 
        /// $.99 reported as .99 
        /// $.90 reported as .9
        /// </remarks>
        public static string ConvertToDecimalString(decimal decimalValue)
        {
            //return decimalValue.ToString ( CultureInfo.InvariantCulture ).TrimEnd ( new[] { '0', '.' } );
            return decimalValue.ToString("G29"); //From http://stackoverflow.com/questions/4525854/remove-trailing-zeros
        }

        #endregion
    }
}
