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
using System.Linq;
using System.Reflection;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// DtsAdvancedSearchCriteria class.
    /// </summary>
    public class DtsAdvancedSearchCriteria : AdvancedSearchCriteriaBase
    {
        #region Constants and Fields

        private string _codeSystemCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code system code.
        /// </summary>
        /// <value>The code system code.</value>
        public string CodeSystemCode
        {
            get { return _codeSystemCode; }
            set { ApplyPropertyChange ( ref _codeSystemCode, () => CodeSystemCode, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether [has enough public properties with value] [the specified num of properties with value required].
        /// </summary>
        /// <param name="numOfPropertiesWithValueRequired">The num of properties with value required.</param>
        /// <returns><c>true</c> if [has enough public properties with value] [the specified num of properties with value required]; otherwise, <c>false</c>.</returns>
        public override bool HasEnoughPublicPropertiesWithValue ( int numOfPropertiesWithValueRequired )
        {
            var publicProperties = typeof( DtsAdvancedSearchCriteria ).GetProperties ();
            var numOfPublicProperties = publicProperties.Count ();
            if ( numOfPropertiesWithValueRequired > numOfPublicProperties )
            {
                throw new ArgumentException (
                    "The number of properties required is more than the total number of all properties.",
                    "numOfPropertiesWithValueRequired" );
            }

            var hasEnoughPublicPropertiesWithValue = false;

            if ( numOfPropertiesWithValueRequired <= 0 )
            {
                hasEnoughPublicPropertiesWithValue = true;
            }
            else
            {
                //Starting at -2 because ActiveIndicatorType and ActiveIndicatorList always have a values.
                var numOfPropertiesWithValue = 0;

                foreach ( var property in publicProperties )
                {
                    var propertyValue = property.GetValue ( this, null );

                    if ( propertyValue != null )
                    {
                        numOfPropertiesWithValue++;
                    }

                    if ( numOfPropertiesWithValue == numOfPropertiesWithValueRequired )
                    {
                        hasEnoughPublicPropertiesWithValue = true;
                        break;
                    }
                }
            }

            return hasEnoughPublicPropertiesWithValue;
        }

        #endregion
    }
}
