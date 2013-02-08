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
using System.ComponentModel;
using System.Linq.Expressions;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.GpraInterview
{
    /// <summary>
    /// Base class for GpraDto
    /// </summary>
    public class GpraDtoBase : EditableDataTransferObject
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the gpra interview key.
        /// </summary>
        /// <value>The gpra interview key.</value>
        public long GpraInterviewKey { get; set; }

        /// <summary>
        /// Gets or sets the gpra non response filters.
        /// </summary>
        /// <value>The gpra non response filters.</value>
        public Dictionary<string, IEnumerable<string>> GpraNonResponseFilters { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Applies the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        public override void ApplyPropertyChange<TProperty, TField> ( ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            CheckHandleChildPropertyChanges ( ref field, propertyExpression, value );
            base.ApplyPropertyChange ( ref field, propertyExpression, value );
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <typeparam name="TField">The type of the field.</typeparam>
        /// <param name="field">The field.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="value">The value.</param>
        public override void RaisePropertyChanged<TProperty, TField> (
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            CheckHandleChildPropertyChanges ( ref field, propertyExpression, value );
            base.RaisePropertyChanged ( ref field, propertyExpression, value );
        }

        #endregion

        #region Methods

        private void CheckHandleChildPropertyChanges<TProperty, TField> (
            ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            if ( !Equals ( field, value ) )
            {
                var type = typeof( TField );
                if ( type.IsGenericType && type.GetGenericTypeDefinition () == typeof( GpraNonResponseTypeDto<> ) )
                {
                    if ( value != null )
                    {
                        ( value as INotifyPropertyChanged ).PropertyChanged += ( s, e ) => RaisePropertyChanged ( propertyExpression );
                    }
                }
            }
        }

        #endregion
    }
}
