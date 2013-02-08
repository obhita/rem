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
using System.Linq;
using Microsoft.Practices.Prism.Regions;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// NavigationExtensions class.
    /// </summary>
    public static class NavigationExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets the navigation parameter.
        /// </summary>
        /// <typeparam name="T">The type of parameter.</typeparam>
        /// <param name="navigationContext">The navigation context.</param>
        /// <param name="parameterKey">The parameter key.</param>
        /// <returns>An instance of the parameter.</returns>
        public static T GetNavigationParameter<T> ( this NavigationContext navigationContext, string parameterKey )
        {
            var parameterValue = ( T )Convert.ChangeType ( navigationContext.Parameters[parameterKey], typeof( T ), CultureInfo.CurrentCulture );
            return parameterValue;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <param name="parameterKey">The parameter key.</param>
        /// <returns>An instance of the value.</returns>
        public static T GetValue<T> ( this KeyValuePair<string, string>[] parameters, string parameterKey )
        {
            var stringValue = parameters.FirstOrDefault ( p => p.Key == parameterKey );
            if ( stringValue.Key != null )
            {
                var type = typeof( T );
                if( type.IsGenericType && type.GetGenericTypeDefinition () == typeof(Nullable<> ))
                {
                    if(string.IsNullOrEmpty ( stringValue.Value ))
                    {
                        return default(T);
                    }
                    var genericType = typeof( T ).GetGenericArguments ().First ();
                    return (T)Convert.ChangeType(stringValue.Value, genericType, CultureInfo.CurrentCulture);
                }
                var parameterValue = ( T )Convert.ChangeType ( stringValue.Value, typeof( T ), CultureInfo.CurrentCulture );
                return parameterValue;
            }
            return default( T );
        }

        /// <summary>
        /// Determines whether the specified parameters has key.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="parameterKey">The parameter key.</param>
        /// <returns><c>true</c> if the specified parameters has key; otherwise, <c>false</c>.</returns>
        public static bool HasKey( this KeyValuePair<string, string>[] parameters, string parameterKey )
        {
            return parameters.Any ( p => p.Key == parameterKey );
        }

        /// <summary>
        /// Tries the get navigation parameter.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="navigationContext">The navigation context.</param>
        /// <param name="parameterKey">The parameter key.</param>
        /// <returns>An instance of the parameter.</returns>
        public static T TryGetNavigationParameter<T> ( this NavigationContext navigationContext, string parameterKey )
        {
            if ( navigationContext.Parameters.Select ( p => p.Key ).FirstOrDefault ( key => key == parameterKey ) != null )
            {
                var parameterValue = ( T )Convert.ChangeType ( navigationContext.Parameters[parameterKey], typeof( T ), CultureInfo.CurrentCulture );
                return parameterValue;
            }
            return default( T );
        }

        #endregion
    }
}
