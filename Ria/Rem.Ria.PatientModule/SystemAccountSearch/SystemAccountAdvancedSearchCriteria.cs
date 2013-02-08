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
using System.Linq;
using System.Reflection;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.PatientModule.SystemAccountSearch
{
    /// <summary>
    /// SystemAccountAdvancedSearchCriteria class.
    /// </summary>
    public sealed class SystemAccountAdvancedSearchCriteria : AdvancedSearchCriteriaBase
    {
        #region Constants and Fields

        private readonly List<ActiveIndicatorTypes> _activeIndicatorList;
        private string _accountNameToSearch;
        private ActiveIndicatorTypes _activityIndicatorType;
        private string _firstNameToSearch;
        private string _lastNameToSearch;
        private LocationSummaryDto _locationToSearch;
        private string _middleNameToSearch;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountAdvancedSearchCriteria"/> class.
        /// </summary>
        public SystemAccountAdvancedSearchCriteria ()
        {
            _activeIndicatorList = new List<ActiveIndicatorTypes> { ActiveIndicatorTypes.None, ActiveIndicatorTypes.Active };
        }

        #endregion

        #region Enums

        /// <summary>
        /// Active indicator types
        /// </summary>
        public enum ActiveIndicatorTypes
        {
            /// <summary>
            /// None indicator type
            /// </summary>
            None,

            /// <summary>
            /// Active indicator type
            /// </summary>
            Active
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the account name to search.
        /// </summary>
        /// <value>The account name to search.</value>
        public string AccountNameToSearch
        {
            get { return _accountNameToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _accountNameToSearch, () => AccountNameToSearch, value );
            }
        }

        /// <summary>
        /// Gets the active indicator list.
        /// </summary>
        public List<ActiveIndicatorTypes> ActiveIndicatorList
        {
            get { return _activeIndicatorList; }
        }

        /// <summary>
        /// Gets the active indicator to search.
        /// </summary>
        public bool? ActiveIndicatorToSearch
        {
            get
            {
                if ( _activityIndicatorType == ActiveIndicatorTypes.None )
                {
                    return null;
                }
                return true;
            }
        }

        /// <summary>
        /// Gets or sets the type of the active indicator.
        /// </summary>
        /// <value>The type of the active indicator.</value>
        public ActiveIndicatorTypes ActiveIndicatorType
        {
            get { return _activityIndicatorType; }
            set
            {
                ApplyPropertyChange ( ref _activityIndicatorType, () => ActiveIndicatorType, value );
                RaisePropertyChanged ( () => ActiveIndicatorToSearch );
            }
        }

        /// <summary>
        /// Gets or sets the first name to search.
        /// </summary>
        /// <value>The first name to search.</value>
        public string FirstNameToSearch
        {
            get { return _firstNameToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _firstNameToSearch, () => FirstNameToSearch, value );
            }
        }

        /// <summary>
        /// Gets or sets the last name to search.
        /// </summary>
        /// <value>The last name to search.</value>
        public string LastNameToSearch
        {
            get { return _lastNameToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _lastNameToSearch, () => LastNameToSearch, value );
            }
        }

        /// <summary>
        /// Gets or sets the location to search.
        /// </summary>
        /// <value>The location to search.</value>
        public LocationSummaryDto LocationToSearch
        {
            get { return _locationToSearch; }
            set { ApplyPropertyChange ( ref _locationToSearch, () => LocationToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the middle name to search.
        /// </summary>
        /// <value>The middle name to search.</value>
        public string MiddleNameToSearch
        {
            get { return _middleNameToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _middleNameToSearch, () => MiddleNameToSearch, value );
            }
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
            var publicProperties = typeof( SystemAccountAdvancedSearchCriteria ).GetProperties ();
            var numOfPublicProperties = publicProperties.Count ();
            if ( numOfPropertiesWithValueRequired > numOfPublicProperties - 2 )
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
                var numOfPropertiesWithValue = -2;

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
