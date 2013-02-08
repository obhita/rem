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
using System.Windows;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.Converter
{
    /// <summary>
    /// Class for converting range to visibility.
    /// </summary>
    public class RangeToVisibilityConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var visibility = Visibility.Collapsed;

            if ( parameter != null )
            {
                if ( IsNull ( parameter.ToString ().Trim () ) )
                {
                    if ( value == null )
                    {
                        visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if ( value != null )
                    {
                        ValidateParameter ( parameter );

                        var paramValues = parameter.ToString ().Split ( ',' );
                        var low = paramValues[0].Trim ();
                        var high = paramValues[1].Trim ();

                        visibility = GetVisibility ( value, low, high );
                    }
                }
            }

            return visibility;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException ( "RangeToVisibilityConverter doesn't support ConvertBack." );
        }

        #endregion

        #region Methods

        private Visibility GetVisibility ( object value, string low, string high )
        {
            var visibility = Visibility.Collapsed;

            if ( IsMin ( low ) && IsMax ( high ) )
            {
                visibility = Visibility.Visible;
            }
            else if ( IsMin ( low ) )
            {
                if ( IsLessThanOrEquals ( value, high ) )
                {
                    visibility = Visibility.Visible;
                }
            }
            else if ( IsMax ( high ) )
            {
                if ( IsGreaterThanOrEquals ( value, low ) )
                {
                    visibility = Visibility.Visible;
                }
            }
            else
            {
                if ( IsInRange ( value, low, high ) )
                {
                    visibility = Visibility.Visible;
                }
            }

            return visibility;
        }

        private bool IsGreaterThanOrEquals ( object value, object low )
        {
            var val = System.Convert.ToDecimal ( value );
            var lowVal = System.Convert.ToDecimal ( low );

            return val >= lowVal;
        }

        private bool IsInRange ( object value, object low, object high )
        {
            var val = System.Convert.ToDecimal ( value );
            var lowVal = System.Convert.ToDecimal ( low );
            var highVal = System.Convert.ToDecimal ( high );

            return val >= lowVal && val <= highVal;
        }

        private bool IsLessThanOrEquals ( object value, object high )
        {
            var val = System.Convert.ToDecimal ( value );
            var highVal = System.Convert.ToDecimal ( high );

            return val <= highVal;
        }

        private bool IsMax ( string high )
        {
            return high.ToLower () == "max";
        }

        private bool IsMin ( string low )
        {
            return low.ToLower () == "min";
        }

        private bool IsNull ( string param )
        {
            return param.ToLower () == "null";
        }

        private bool IsNumber ( string value )
        {
            var result = false;

            try
            {
                System.Convert.ToDecimal ( value );
                result = true;
            }
            catch ( Exception )
            {
                result = false;
            }

            return result;
        }

        private void ValidateParameter ( object parameter )
        {
            var paramValues = parameter.ToString ().Split ( ',' );

            if ( paramValues.Length != 2 )
            {
                throw new ArgumentException (
                    "The parameter should be a comma seperated range string 'low, high' (e.g. '0, 5') or a single 'null'. "
                    + "Using 'min' or 'max' to indicate the minimum value or maximum value." );
            }

            var low = paramValues[0].Trim ();
            var high = paramValues[1].Trim ();

            if ( !IsMin ( low ) && !IsNumber ( low ) )
            {
                throw new ArgumentException ( "Lower bound value should be 'min' or a number." );
            }

            if ( !IsMax ( high ) && !IsNumber ( high ) )
            {
                throw new ArgumentException ( "Upper bound value should be 'max' or a number." );
            }
        }

        #endregion
    }
}
