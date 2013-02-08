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
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.Converter
{
    /// <summary>
    /// Class for converting integer to CSV value.
    /// </summary>
    public class IntegerToCsvValueConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Gets the list index of given value. List is specified as Csv.
        /// </summary>
        /// <param name="value">Example: 0</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            object returnValue = null;

            if ( value != null && parameter != null )
            {
                var csvValues = parameter.ToString ().Split ( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

                for ( var index = 0; index < csvValues.Length; index++ )
                {
                    if ( value.ToString ().Equals ( csvValues[index] ) )
                    {
                        returnValue = index;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets the value at a given list index. List is specified as Csv.
        /// </summary>
        /// <param name="value">Example: 2</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>Csv value.</returns>
        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            object returnValue = null;

            if ( value != null && value is int && parameter != null )
            {
                var index = ( int )value;
                var csvValues = parameter.ToString ().Split ( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

                if ( csvValues.Length > index && index >= 0 )
                {
                    returnValue = csvValues[index];
                }
            }

            return returnValue;
        }

        #endregion
    }
}
