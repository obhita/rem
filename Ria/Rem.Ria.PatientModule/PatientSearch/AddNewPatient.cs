﻿#region License

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
using System.Reflection;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.PatientModule.PatientSearch
{
    /// <summary>
    /// Object for adding a new patient
    /// </summary>
    public class AddNewPatient : CustomNotificationObject
    {
        #region Constants and Fields

        private DateTime? _birthDate;
        private string _firstName;
        private LookupValueDto _gender;
        private string _lastName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the birth date.
        /// </summary>
        /// <value>The birth date.</value>
        public DateTime? BirthDate
        {
            get { return _birthDate; }
            set { ApplyPropertyChange ( ref _birthDate, () => BirthDate, value ); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get { return _firstName; }

            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                _firstName = value;
                RaisePropertyChanged ( () => FirstName );
            }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public LookupValueDto Gender
        {
            get { return _gender; }
            set { ApplyPropertyChange ( ref _gender, () => Gender, value ); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get { return _lastName; }

            set
            {
                value = string.IsNullOrWhiteSpace ( value ) ? null : value.Trim ();
                ApplyPropertyChange ( ref _lastName, () => LastName, value );
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Cleans up all fields.
        /// </summary>
        public void CleanUpAllFields ()
        {
            var publicProperties = GetType ().GetProperties ();
            foreach ( var publicProperty in publicProperties )
            {
                if ( publicProperty.CanWrite )
                {
                    publicProperty.SetValue ( this, null, null );
                }
            }
        }

        #endregion
    }
}
