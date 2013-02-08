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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.View.Converter
{
    /// <summary>
    /// Class for converting get lookup values from binding path non response.
    /// </summary>
    public class GetLookupValuesFromBindingPathNonResponseConverter : IMultiValueConverter
    {
        #region Constants and Fields

        private readonly string _lookupTypeNameStartsWith;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLookupValuesFromBindingPathNonResponseConverter"/> class.
        /// </summary>
        /// <param name="lookupTypeNameStartsWith">The binding path.</param>
        public GetLookupValuesFromBindingPathNonResponseConverter ( string lookupTypeNameStartsWith )
        {
            _lookupTypeNameStartsWith = lookupTypeNameStartsWith;
        }

        #endregion

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
            if ( values.Count () == 2 && values[0] is FrameworkElement && values[1] is IDictionary )
            {
                IEnumerable<string> filters = null;
                if ( parameter is IEnumerable<string> )
                {
                    filters = parameter as IEnumerable<string>;
                }
                var dictionary = values[1] as Dictionary<string, IList<LookupValueDto>>;
                var frameworkElement = values[0] as FrameworkElement;
                var parentRoot = ( frameworkElement.GetTemplatedParent () as FrameworkElement ).GetTemplatedParent ();
                if ( parentRoot is NonResponseQuestionControl )
                {
                    var nonResponseControl = parentRoot as NonResponseQuestionControl;
                    if (nonResponseControl.ReadLocalValue(NonResponseQuestionControl.LookupValueOverrideProperty) == DependencyProperty.UnsetValue)
                    {
                        var valueBinding = nonResponseControl.GetBindingExpression ( NonResponseQuestionControl.NonResponseDtoProperty );
                        var propertyName = valueBinding.ParentBinding.Path.Path;
                        var gpraIndex = propertyName.IndexOf ( _lookupTypeNameStartsWith );
                        if ( gpraIndex >= 0 )
                        {
                            var typeName = propertyName.Substring ( gpraIndex, propertyName.Length - gpraIndex );
                            var items = dictionary[typeName];
                            return filters == null ? items : items.Where ( lkp => !filters.Contains ( lkp.WellKnownName ) );
                        }
                    }
                    else
                    {
                        var typeName = nonResponseControl.LookupValueOverride;
                        var items = dictionary[typeName];
                        return filters == null ? items : items.Where(lkp => !filters.Contains(lkp.WellKnownName));
                    }
                }
                if ( parentRoot is BasicScreenQuestionControl )
                {
                    var questionControl = parentRoot as BasicScreenQuestionControl;
                    if (questionControl.ReadLocalValue(BasicScreenQuestionControl.LookupValueOverrideProperty) == DependencyProperty.UnsetValue)
                    {
                        var valueBinding = questionControl.GetBindingExpression ( BasicScreenQuestionControl.ValueProperty );
                        var propertyName = valueBinding.ParentBinding.Path.Path;
                        var gpraIndex = propertyName.IndexOf ( _lookupTypeNameStartsWith );
                        if ( gpraIndex >= 0 )
                        {
                            var typeName = propertyName.Substring ( gpraIndex, propertyName.Length - gpraIndex );
                            var items = dictionary[typeName];
                            return filters == null ? items : items.Where ( lkp => !filters.Contains ( lkp.WellKnownName ) );
                        }
                    }
                    else
                    {
                        var typeName = questionControl.LookupValueOverride;
                        var items = dictionary[typeName];
                        return filters == null ? items : items.Where(lkp => !filters.Contains(lkp.WellKnownName));
                    }
                }
                return null;
            }
            return null;
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

        #endregion
    }
}
