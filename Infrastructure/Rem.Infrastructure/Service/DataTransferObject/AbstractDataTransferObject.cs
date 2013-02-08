#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Pillar.Common.Collections;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Infrastructure.Service.DataTransferObject
{
    /// <summary>
    /// The <see cref="AbstractDataTransferObject"/> is the base class of all dtos.
    /// </summary>
    [DataContract]
    public abstract partial class AbstractDataTransferObject : IDataTransferObject, IMetadataProvider
    {
        #region Constants and Fields

        private IList<DataErrorInfo> _dataErrorInfoCollection = new List<DataErrorInfo> ();

        private bool _isDirty;

        private MetadataDto _metadataDto = new MetadataDto ( string.Empty );

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Collection of  data errors.
        /// </summary>
        [DataMember]
        public IEnumerable<DataErrorInfo> DataErrorInfoCollection
        {
            get { return _dataErrorInfoCollection; }
            internal set
            {
                if ( value != null )
                {
                    _dataErrorInfoCollection = new List<DataErrorInfo> ( value );
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether this instance is dirty.
        /// </summary>
        /// <value> <c>true</c> if this instance is dirty; otherwise, <c>false</c> . </value>
        public bool IsDirty
        {
            get { return _isDirty; }
            protected set
            {
                if ( _isDirty != value )
                {
                    _isDirty = value;
                    RaisePropertyChanged ( () => IsDirty );
                }
            }
        }

        /// <summary>
        /// The dto that contains the metadata.
        /// </summary>
        public MetadataDto MetadataDto
        {
            get { return _metadataDto; }
            set
            {
                // NOTE: we don't call ApplyPropertyChange directly because we don't want to change IsDirty to true (we modifying metadata, not the DTO itself)
                if ( !Equals ( _metadataDto, value ) )
                {
                    _metadataDto = value;
                    RaisePropertyChanged ( () => MetadataDto );
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the data error info.
        /// </summary>
        /// <param name="dataErrorInfo">
        /// The data error info. 
        /// </param>
        public void AddDataErrorInfo ( DataErrorInfo dataErrorInfo )
        {
            if ( dataErrorInfo.DataErrorInfoType != DataErrorInfoType.ObjectLevel )
            {
                foreach ( var propertyName in dataErrorInfo.Properties )
                {
                    ValidatePropertyExists ( propertyName );
                }
            }

            _dataErrorInfoCollection.Add ( dataErrorInfo );
            ReportErrorsChanged ( dataErrorInfo );
        }

        /// <summary>
        /// Applies the collection change.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property. 
        /// </typeparam>
        /// <typeparam name="TField">
        /// The type of the field. 
        /// </typeparam>
        /// <param name="field">
        /// The field. 
        /// </param>
        /// <param name="propertyExpression">
        /// The property expression. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        public void ApplyCollectionChange<TProperty, TField> (
            ref ObservableCollection<TField> field, Expression<Func<TProperty>> propertyExpression, ObservableCollection<TField> value )
        {
            if ( field != null )
            {
                field.CollectionChanged -= NotifyCollectionChanged;
            }

            field = value;
            RaisePropertyChanged ( propertyExpression );

            IsDirty = true;

            if ( field != null )
            {
                field.CollectionChanged += NotifyCollectionChanged;
            }
        }

        /// <summary>
        /// Applies the property change.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property. 
        /// </typeparam>
        /// <typeparam name="TField">
        /// The type of the field. 
        /// </typeparam>
        /// <param name="field">
        /// The field. 
        /// </param>
        /// <param name="propertyExpression">
        /// The property expression. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        public virtual void ApplyPropertyChange<TProperty, TField> ( ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            if ( !Equals ( field, value ) )
            {
                field = value;
                RaisePropertyChanged ( propertyExpression );
                IsDirty = true;
            }
        }

        /// <summary>
        /// Applies the soft delete observable collection change.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property. 
        /// </typeparam>
        /// <typeparam name="TField">
        /// The type of the field. 
        /// </typeparam>
        /// <param name="field">
        /// The field. 
        /// </param>
        /// <param name="propertyExpression">
        /// The property expression. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        public void ApplySoftDeleteObservableCollectionChange<TProperty, TField> (
            ref SoftDeleteObservableCollection<TField> field, 
            Expression<Func<TProperty>> propertyExpression, 
            SoftDeleteObservableCollection<TField> value )
        {
            if ( field != null )
            {
                field.CollectionChanged -= NotifyCollectionChanged;
            }

            field = value;
            RaisePropertyChanged ( propertyExpression );

            IsDirty = true;

            if ( field != null )
            {
                field.CollectionChanged += NotifyCollectionChanged;
            }
        }


        /// <summary>
        /// Clears all data error information.
        /// </summary>
        public void ClearAllDataErrorInfo ()
        {
            RemoveDataErrorInfo ( _dataErrorInfoCollection );
        }

        /// <summary>
        /// Called when [deserialization].
        /// </summary>
        /// <param name="context">The context.</param>
        [OnDeserialized]
        public void OnDeserialization ( StreamingContext context )
        {
            IsDirty = false;
            MetadataDto = new MetadataDto ( GetType ().FullName );
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property. 
        /// </typeparam>
        /// <param name="propertyExpression">
        /// The property expression. 
        /// </param>
        public void RaisePropertyChanged<TProperty> ( Expression<Func<TProperty>> propertyExpression )
        {
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            if ( PropertyChanged != null )
            {
                PropertyChanged ( this, new PropertyChangedEventArgs ( propertyName ) );
            }
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="TProperty">
        /// The type of the property. 
        /// </typeparam>
        /// <typeparam name="TField">
        /// The type of the field. 
        /// </typeparam>
        /// <param name="field">
        /// The field. 
        /// </param>
        /// <param name="propertyExpression">
        /// The property expression. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        public virtual void RaisePropertyChanged<TProperty, TField> ( ref TField field, Expression<Func<TProperty>> propertyExpression, TField value )
        {
            field = value;
            RaisePropertyChanged ( propertyExpression );
            IsDirty = true;
        }

        /// <summary>
        /// Removes the data error info.
        /// </summary>
        /// <param name="dataErrorInfo">The data error info.</param>
        public void RemoveDataErrorInfo ( DataErrorInfo dataErrorInfo )
        {
            ISet<string> propertyNames = new HashSet<string> ();
            _dataErrorInfoCollection.Remove ( dataErrorInfo );
            if ( dataErrorInfo.DataErrorInfoType == DataErrorInfoType.ObjectLevel )
            {
                propertyNames.Add ( string.Empty );
            }
            else
            {
                foreach ( string name in dataErrorInfo.Properties )
                {
                    propertyNames.Add ( name );
                }
            }

            foreach ( string reportablePropertyName in propertyNames )
            {
                ReportErrorsChanged ( reportablePropertyName );
            }
        }

        /// <summary>
        /// Removes the data error information.
        /// </summary>
        /// <param name="propertyName">Name of the property which has erroneous data.</param>
        public void RemoveDataErrorInfo ( string propertyName )
        {
            // what if the property is null, empty, or doesn't exist?
            if ( !string.IsNullOrEmpty ( propertyName ) )
            {
                ValidatePropertyExists ( propertyName );
            }

            var dataErrorInfoList = GetDataErrorInfos ( propertyName );

            RemoveDataErrorInfo ( dataErrorInfoList );
        }

        #endregion

        #region Methods

        private IEnumerable<DataErrorInfo> GetDataErrorInfos ( string propertyName )
        {
            IList<DataErrorInfo> list;

            if ( string.IsNullOrEmpty ( propertyName ) )
            {
                list = _dataErrorInfoCollection.Where (
                    p => p.DataErrorInfoType == DataErrorInfoType.ObjectLevel ).ToList ();
            }
            else
            {
                list = _dataErrorInfoCollection.Where (
                    p => p.Properties != null && p.Properties.Contains ( propertyName )
                    ).ToList ();
            }

            return list;
        }

        private void NotifyCollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            this.IsDirty = true;

            var editableDto = this as EditableDataTransferObject;
            if ( editableDto != null && editableDto.Key > 0 )
            {
                editableDto.EditStatus = EditStatus.Update;
            }
        }

        private void RemoveDataErrorInfo ( IEnumerable<DataErrorInfo> dataErrorInfoList )
        {
            if ( dataErrorInfoList != null )
            {
                ISet<string> propertyNames = new HashSet<string> ();
                IList<DataErrorInfo> iterationList = new List<DataErrorInfo> ( dataErrorInfoList );
                foreach ( DataErrorInfo dataErrorInfo in iterationList )
                {
                    _dataErrorInfoCollection.Remove ( dataErrorInfo );

                    if ( dataErrorInfo.DataErrorInfoType == DataErrorInfoType.ObjectLevel )
                    {
                        propertyNames.Add ( string.Empty );
                    }
                    else
                    {
                        foreach ( string name in dataErrorInfo.Properties )
                        {
                            propertyNames.Add ( name );
                        }
                    }
                }

                foreach ( string reportablePropertyName in propertyNames )
                {
                    ReportErrorsChanged ( reportablePropertyName );
                }
            }
        }

        /// <summary>
        /// Reports the errors changed.
        /// </summary>
        /// <param name="dataErrorInfo">The data error info.</param>
        partial void ReportErrorsChanged ( DataErrorInfo dataErrorInfo );

        /// <summary>
        /// Reports the errors changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        partial void ReportErrorsChanged ( string propertyName );

        private void ValidatePropertyExists ( string propertyName )
        {
            var prop = GetType ().GetProperty ( propertyName );
            if ( prop == null )
            {
                throw new PropertyNotFoundException ( GetType (), propertyName );
            }
        }

        #endregion
    }
}
