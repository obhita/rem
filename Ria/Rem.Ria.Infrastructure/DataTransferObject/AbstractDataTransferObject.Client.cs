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
using System.ComponentModel;
using System.Linq;

namespace Rem.Infrastructure.Service.DataTransferObject
{
    /// <summary>
    /// AbstractDataTransferObject class.
    /// </summary>
    public abstract partial class AbstractDataTransferObject
    {
        // TODO : For Alex and Ron in Sprint 9 - Fix AbstractDataTransferObject
        //
        // Issues: 
        //         1. Sub-Dtos (i.e. Phone) might have errors and you shouldn't be able to save a dto that has sub-dto errors
        //            For example, a Patient with an invalid phone shouldn't be saveable.
        //         2. The GetErrors method should return all warning and errors but should the HasErrors also?  We need to 
        //            distinguish the situation where there are warnings but no errors.  Why?  Because you can save with
        //            warnings but not with errors.
        //         3. Need a strategy for dealing with ObjectLevel errors.  How to show them.

        private bool _isLoading;

        #region Public Events

        /// <summary>
        /// Occurs when [errors changed].
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = ( o, e ) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        public bool HasErrors
        {
            get { return _dataErrorInfoCollection.Count > 0; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { ApplyPropertyChange(ref _isLoading, () => IsLoading, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A <see cref="System.Collections.IEnumerable"/></returns>
        public IEnumerable GetErrors ( string propertyName )
        {
            IEnumerable<DataErrorInfo> errors;

            if ( string.IsNullOrEmpty ( propertyName ) )
            {
                errors = _dataErrorInfoCollection.Where ( dei => dei.DataErrorInfoType == DataErrorInfoType.ObjectLevel );
            }
            else
            {
                errors = _dataErrorInfoCollection.Where (
                    dei => ( dei.Properties != null ) && dei.Properties.Contains ( propertyName ) );
            }

            return errors;
        }

        /// <summary>
        /// Raises the errors changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void RaiseErrorsChanged ( string propertyName )
        {
            if ( !string.IsNullOrEmpty ( propertyName ) )
            {
                ValidatePropertyExists ( propertyName );
            }

            if ( ErrorsChanged != null )
            {
                ErrorsChanged ( this, new DataErrorsChangedEventArgs ( propertyName ) );
            }
        }

        /// <summary>
        /// Tries the add data error info.
        /// </summary>
        /// <param name="dataErrorInfo">The data error info.</param>
        public void TryAddDataErrorInfo ( DataErrorInfo dataErrorInfo )
        {
            if ( !DataErrorInfoCollection.Contains ( dataErrorInfo ) )
            {
                AddDataErrorInfo ( dataErrorInfo );
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reports when errors have changed.
        /// </summary>
        /// <param name="dataErrorInfo">Error Info.</param>
        partial void ReportErrorsChanged(DataErrorInfo dataErrorInfo)
        {
            if ( dataErrorInfo.DataErrorInfoType == DataErrorInfoType.ObjectLevel )
            {
                ReportErrorsChanged ( string.Empty );
            }
            else
            {
                foreach ( var propertyName in dataErrorInfo.Properties )
                {
                    ReportErrorsChanged ( propertyName );
                }
            }
        }

        /// <summary>
        /// Reports when errors have changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        partial void ReportErrorsChanged ( string propertyName )
        {
            RaiseErrorsChanged ( propertyName );
        }

        #endregion
    }
}
