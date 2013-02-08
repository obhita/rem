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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Pillar.Common.Utility;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.PatientModule.PatientSearch
{
    /// <summary>
    /// This class requires all properties are nullable properties
    /// </summary>
    public sealed class PatientAdvancedSearchCriteria : AdvancedSearchCriteriaBase
    {
        // Any propery group in this list is interdependent and treated as one atomic property for outside usage

        #region Constants and Fields

        private readonly IList<IList<string>> _interDependentPublicPropertyGroups;

        private string _addressLineOneToSearch;
        private DateTime? _birthDateToSearch;
        private string _cityToSearch;
        private string _firstNameToSearch;
        private LookupValueDto _genderToSearch;
        private string _identifierToSearch;
        private LookupValueDto _identifierTypeToSearch;
        private string _lastNameToSearch;
        private string _middleNameToSearch;
        private string _motherMaidenNameToSearch;
        private LookupValueDto _stateToSearch;
        private LookupValueDto _suffixToSearch;
        private string _uniqueIdentifierToSearch;
        private string _zipCodeToSearch;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAdvancedSearchCriteria"/> class.
        /// </summary>
        public PatientAdvancedSearchCriteria ()
        {
            var interDependentPublicPropertyGroup1 = new List<string>
                {
                    PropertyUtil.ExtractPropertyName (
                        () => IdentifierToSearch ),
                    PropertyUtil.ExtractPropertyName (
                        () => IdentifierTypeToSearch )
                };

            _interDependentPublicPropertyGroups = new List<IList<string>> { interDependentPublicPropertyGroup1 };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the address line one to search.
        /// </summary>
        /// <value>The address line one to search.</value>
        public string AddressLineOneToSearch
        {
            get { return _addressLineOneToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _addressLineOneToSearch, () => AddressLineOneToSearch, value );
            }
        }

        /// <summary>
        /// Gets or sets the birth date to search.
        /// </summary>
        /// <value>The birth date to search.</value>
        public DateTime? BirthDateToSearch
        {
            get { return _birthDateToSearch; }
            set { ApplyPropertyChange ( ref _birthDateToSearch, () => BirthDateToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the city to search.
        /// </summary>
        /// <value>The city to search.</value>
        public string CityToSearch
        {
            get { return _cityToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _cityToSearch, () => CityToSearch, value );
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
                _firstNameToSearch = value;
                RaisePropertyChanged ( () => FirstNameToSearch );
            }
        }

        /// <summary>
        /// Gets or sets the gender to search.
        /// </summary>
        /// <value>The gender to search.</value>
        public LookupValueDto GenderToSearch
        {
            get { return _genderToSearch; }
            set { ApplyPropertyChange ( ref _genderToSearch, () => GenderToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the identifier to search.
        /// </summary>
        /// <value>The identifier to search.</value>
        public string IdentifierToSearch
        {
            get { return _identifierToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _identifierToSearch, () => IdentifierToSearch, value );
            }
        }

        /// <summary>
        /// Gets or sets the identifier type to search.
        /// </summary>
        /// <value>The identifier type to search.</value>
        public LookupValueDto IdentifierTypeToSearch
        {
            get { return _identifierTypeToSearch; }
            set { ApplyPropertyChange ( ref _identifierTypeToSearch, () => IdentifierTypeToSearch, value ); }
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

        /// <summary>
        /// Gets or sets the mother maiden name to search.
        /// </summary>
        /// <value>The mother maiden name to search.</value>
        public string MotherMaidenNameToSearch
        {
            get { return _motherMaidenNameToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _motherMaidenNameToSearch, () => MotherMaidenNameToSearch, value );
            }
        }

        /// <summary>
        /// Gets or sets the state to search.
        /// </summary>
        /// <value>The state to search.</value>
        public LookupValueDto StateToSearch
        {
            get { return _stateToSearch; }
            set { ApplyPropertyChange ( ref _stateToSearch, () => StateToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the suffix to search.
        /// </summary>
        /// <value>The suffix to search.</value>
        public LookupValueDto SuffixToSearch
        {
            get { return _suffixToSearch; }
            set { ApplyPropertyChange ( ref _suffixToSearch, () => SuffixToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the unique identifier to search.
        /// </summary>
        /// <value>The unique identifier to search.</value>
        public string UniqueIdentifierToSearch
        {
            get { return _uniqueIdentifierToSearch; }
            set { ApplyPropertyChange ( ref _uniqueIdentifierToSearch, () => UniqueIdentifierToSearch, value ); }
        }

        /// <summary>
        /// Gets or sets the zip code to search.
        /// </summary>
        /// <value>The zip code to search.</value>
        public string ZipCodeToSearch
        {
            get { return _zipCodeToSearch; }
            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _zipCodeToSearch, () => ZipCodeToSearch, value );
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
            var publicProperties = typeof( PatientAdvancedSearchCriteria ).GetProperties ();
            var numOfPublicProperties = publicProperties.Count ();
            if ( numOfPropertiesWithValueRequired > numOfPublicProperties )
            {
                throw new ArgumentException (
                    "The number of properties required is more than the total number of all properties.",
                    "numOfPropertiesWithValueRequired" );
            }

            var numOfInterdependentProperties =
                _interDependentPublicPropertyGroups.SelectMany (
                    interDependentPublicPropertyGroup => interDependentPublicPropertyGroup ).Count ();
            var numOfInterdependentPropertyGroup = _interDependentPublicPropertyGroups.Count;
            var numOfIndependentProperties = numOfPublicProperties - numOfInterdependentProperties +
                                             numOfInterdependentPropertyGroup;

            if ( numOfPropertiesWithValueRequired > numOfIndependentProperties )
            {
                throw new ArgumentException (
                    "The number of properties required is more than the total number of all independent properties.",
                    "numOfPropertiesWithValueRequired" );
            }

            var hasEnoughPublicPropertiesWithValue = false;

            if ( numOfPropertiesWithValueRequired <= 0 )
            {
                hasEnoughPublicPropertiesWithValue = true;
            }
            else
            {
                var numOfPropertiesWithValue = 0;

                var inDependentPublicProperties =
                    typeof( PatientAdvancedSearchCriteria ).GetProperties ().Where (
                        p =>
                        !( _interDependentPublicPropertyGroups.Any (
                            interDependentPublicPropertyGroup => interDependentPublicPropertyGroup.Contains ( p.Name ) ) ) );

                foreach ( var property in inDependentPublicProperties )
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

                    Debug.WriteLine ( property.Name + ": Line in the indepedent property check;" );
                }

                if ( !hasEnoughPublicPropertiesWithValue &&
                     numOfPropertiesWithValue + numOfInterdependentPropertyGroup >= numOfPropertiesWithValueRequired )
                {
                    foreach ( var interDependentPublicPropertyGroup in _interDependentPublicPropertyGroups )
                    {
                        var hasNullValueInPropertyGroup = false;

                        foreach ( var interDependentPublicProperty in interDependentPublicPropertyGroup )
                        {
                            var property = GetType ().GetProperty ( interDependentPublicProperty );
                            var propertyValue = property.GetValue ( this, null );

                            if ( propertyValue == null )
                            {
                                hasNullValueInPropertyGroup = true;
                                break;
                            }

                            Debug.WriteLine ( property.Name + ": Line in the interdepedent property check;" );
                        }

                        if ( !hasNullValueInPropertyGroup )
                        {
                            numOfPropertiesWithValue++;

                            if ( numOfPropertiesWithValue == numOfPropertiesWithValueRequired )
                            {
                                hasEnoughPublicPropertiesWithValue = true;
                                break;
                            }
                        }
                    }
                }
            }

            return hasEnoughPublicPropertiesWithValue;
        }

        #endregion
    }
}
