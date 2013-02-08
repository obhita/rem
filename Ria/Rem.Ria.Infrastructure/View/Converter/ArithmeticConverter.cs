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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.Converter
{
    /// <summary>
    /// Class for converting arithmetic.
    /// </summary>
    public class ArithmeticConverter : IValueConverter, IMultiValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Converts the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A <see cref="System.Object"/></returns>
        public object Convert ( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            if ( parameter is string )
            {
                double tempDouble;
                if ( values.Any ( value => !( double.TryParse ( value.ToString (), out tempDouble ) ) ) )
                {
                    return null;
                }
                var expression = string.Format ( parameter.ToString (), values );
                using ( var reader = new StringReader ( expression ) )
                {
                    return Evaluate ( reader );
                }
            }
            return null;
        }

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
            return Convert ( new[] { value }, targetType, parameter, culture );
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetTypes">The target types.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>An object[].</returns>
        public object[] ConvertBack ( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotSupportedException ();
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
            throw new NotSupportedException ();
        }

        #endregion

        #region Methods

        private static double Evaluate ( TextReader mathExpression )
        {
            var readByte = mathExpression.Read ();
            var numberBuilder = new StringBuilder ();
            var operandList = new LinkedList<object> ();
            while ( true )
            {
                var character = ( char )readByte;
                if ( character != ' ' )
                {
                    if ( character == ')' || readByte == -1 )
                    {
                        if ( numberBuilder.Length > 0 )
                        {
                            operandList.AddLast ( new LinkedListNode<object> ( double.Parse ( numberBuilder.ToString () ) ) );
                            numberBuilder.Clear ();
                        }
                        EvaluateOperands ( new List<char> { '^' }, operandList );
                        EvaluateOperands ( new List<char> { '/', '*', '%' }, operandList );
                        EvaluateOperands ( new List<char> { '+', '-' }, operandList );
                        return ( double )operandList.First.Value;
                    }
                    else if ( IsOperand ( character ) )
                    {
                        if ( numberBuilder.Length > 0 )
                        {
                            operandList.AddLast ( new LinkedListNode<object> ( double.Parse ( numberBuilder.ToString () ) ) );
                            numberBuilder.Clear ();
                        }
                        operandList.AddLast ( new LinkedListNode<object> ( character ) );
                    }
                    else if ( character == '(' )
                    {
                        operandList.AddLast ( new LinkedListNode<object> ( Evaluate ( mathExpression ) ) );
                    }
                    else
                    {
                        numberBuilder.Append ( character );
                    }
                }
                readByte = mathExpression.Read ();
            }
        }

        private static double EvaluateOperand ( char operand, double left, double right )
        {
            switch ( operand )
            {
                case '*':
                    return left * right;
                case '/':
                    return left / right;
                case '%':
                    return left % right;
                case '-':
                    return left - right;
                case '+':
                    return left + right;
                case '^':
                    return Math.Pow ( left, right );
                default:
                    throw new NotSupportedException ( "Invalid Operand." );
            }
        }

        private static void EvaluateOperands ( ICollection<char> operands, LinkedList<object> operandList )
        {
            var node = operandList.First;
            while ( node != null )
            {
                if ( node.Value is char && operands.Contains ( ( char )node.Value ) )
                {
                    var left = node.Previous;
                    var right = node.Next;
                    if ( left == null || right == null )
                    {
                        throw new InvalidOperationException ( "Invalid math expression." );
                    }
                    var next = right.Next;
                    var value = EvaluateOperand ( ( char )node.Value, ( double )left.Value, ( double )right.Value );
                    operandList.Remove ( left );
                    operandList.Remove ( node );
                    operandList.Remove ( right );
                    node = next;
                    if ( next == null )
                    {
                        operandList.AddLast ( new LinkedListNode<object> ( value ) );
                    }
                    else
                    {
                        operandList.AddBefore ( node, new LinkedListNode<object> ( value ) );
                    }
                }
                else
                {
                    node = node.Next;
                }
            }
        }

        private static bool IsOperand ( char character )
        {
            return character == '+' || character == '-' || character == '/' || character == '*' || character == '%' || character == '^';
        }

        #endregion
    }
}
