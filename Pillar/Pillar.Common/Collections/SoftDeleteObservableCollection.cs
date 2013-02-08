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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Pillar.Common.Collections
{
    /// <summary>
    /// An <see cref="IList"/> implementation that tracks objects that are deleted by storing them in a separate list.
    /// </summary>
    /// <typeparam name="T">The type of object stored in the list.</typeparam>
    [DataContract]
    public class SoftDeleteObservableCollection<T> : IList<T>,
                                                     IList,
                                                     INotifyCollectionChanged,
                                                     INotifyPropertyChanged,
                                                     ISoftDelete<T>,
                                                     ISoftDelete
    {
        #region Constants and Fields

        private ObservableCollection<T> _currentItems;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteObservableCollection{T}"/> class.
        /// </summary>
        public SoftDeleteObservableCollection ()
        {
            _currentItems = new ObservableCollection<T> ();
            Initialize ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public SoftDeleteObservableCollection ( IEnumerable<T> collection )
        {
            _currentItems = new ObservableCollection<T> ( collection );
            Initialize ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="list">The list to observe.</param>
        public SoftDeleteObservableCollection ( List<T> list )
        {
            _currentItems = new ObservableCollection<T> ( list );
            Initialize ();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };

        /// <summary>
        /// Occurs when an item is soft deleted from a list.
        /// </summary>
        public event SoftDeletedEventHandler SoftDeleted;

        #endregion

        #region Explicit Interface Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { ( _currentItems as INotifyPropertyChanged ).PropertyChanged += value; }
            remove { ( _currentItems as INotifyPropertyChanged ).PropertyChanged -= value; }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="ICollection{T}"/>.
        /// </returns>
        public int Count
        {
            get { return _currentItems.Count; }
        }

        /// <summary>
        /// Gets or sets the current items.
        /// </summary>
        /// <value>The current items.</value>
        [DataMember]
        public ObservableCollection<T> CurrentItems
        {
            get { return _currentItems; }

            set
            {
                if ( _currentItems != null )
                {
                    _currentItems.CollectionChanged -= CurrentItemsCollectionChanged;
                }

                _currentItems = value;

                if ( _currentItems != null )
                {
                    _currentItems.CollectionChanged += CurrentItemsCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Gets or sets the removed items.
        /// </summary>
        /// <value>The removed items.</value>
        [DataMember]
        public ObservableCollection<T> RemovedItems { get; set; }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        int ICollection.Count
        {
            get { return ( _currentItems as IList ).Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList"/> has a fixed size.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.IList"/> has a fixed size; otherwise, false.</returns>
        bool IList.IsFixedSize
        {
            get { return ( _currentItems as IList ).IsFixedSize; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        bool IList.IsReadOnly
        {
            get { return ( _currentItems as IList ).IsReadOnly; }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.</returns>
        bool ICollection.IsSynchronized
        {
            get { return ( _currentItems as IList ).IsFixedSize; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.</returns>
        object ICollection.SyncRoot
        {
            get { return ( _currentItems as IList ).SyncRoot; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        bool ICollection<T>.IsReadOnly
        {
            get { return ( _currentItems as ICollection<T> ).IsReadOnly; }
        }

        /// <summary>
        /// Gets the current items.
        /// </summary>
        IList ISoftDelete.CurrentItems
        {
            get { return CurrentItems; }
        }

        /// <summary>
        /// Gets the removed items.
        /// </summary>
        IList ISoftDelete.RemovedItems
        {
            get { return RemovedItems; }
        }

        /// <summary>
        /// Gets the current items.
        /// </summary>
        IList<T> ISoftDelete<T>.CurrentItems
        {
            get { return CurrentItems; }
        }

        /// <summary>
        /// Gets the removed items.
        /// </summary>
        IList<T> ISoftDelete<T>.RemovedItems
        {
            get { return RemovedItems; }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">Index to get.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="IList{T}"/>.
        /// </exception>   
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the <see cref="IList{T}"/> is read-only.
        /// </exception>
        public T this [ int index ]
        {
            get { return _currentItems[index]; }
            set { _currentItems[index] = value; }
        }

        #endregion

        #region Explicit Interface Indexers

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">Index to get.</param>
        /// <returns>
        /// The element at the specified index.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        object IList.this [ int index ]
        {
            get { return ( _currentItems as IList )[index]; }
            set { ( _currentItems as IList )[index] = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="ICollection{T}"/> is read-only.
        /// </exception>
        public void Add ( T item )
        {
            _currentItems.Add ( item );
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="ICollection{T}"/> is read-only.
        /// </exception>
        public void Clear ()
        {
            _currentItems.Clear ();
        }

        /// <summary>
        /// Determines whether the <see cref="ICollection{T}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="ICollection{T}"/>.</param>
        /// <returns>true if <paramref name="item"/> is found in the <see cref="ICollection{T}"/>; otherwise, false.</returns>
        public bool Contains ( T item )
        {
            return _currentItems.Contains ( item );
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo ( T[] array, int arrayIndex )
        {
            _currentItems.CopyTo ( array, arrayIndex );
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator ()
        {
            return _currentItems.GetEnumerator ();
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="IList{T}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="IList{T}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf ( T item )
        {
            return _currentItems.IndexOf ( item );
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="IList{T}"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="IList{T}"/>.
        /// </exception>   
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="IList{T}"/> is read-only.
        /// </exception>
        public void Insert ( int index, T item )
        {
            _currentItems.Insert ( index, item );
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"/>.</param>
        /// <returns>true if <paramref name="item"/> was successfully removed from the <see cref="ICollection{T}"/>;
        /// otherwise, false. This method also returns false if <paramref name="item"/> is not found
        /// in the original <see cref="ICollection{T}"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// The <see cref="ICollection{T}"/> is read-only.
        /// </exception>
        public bool Remove ( T item )
        {
            return _currentItems.Remove ( item );
        }

        /// <summary>
        /// Removes the <see cref="IList{T}"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="IList{T}"/>.
        /// </exception>   
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="IList{T}"/> is read-only.
        /// </exception>
        public void RemoveAt ( int index )
        {
            _currentItems.RemoveAt ( index );
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The object to add to the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection,</returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"/> is read-only.-or- The <see cref="T:System.Collections.IList"/> has a fixed size. </exception>
        int IList.Add ( object value )
        {
            return ( _currentItems as IList ).Add ( value );
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        void IList.Clear ()
        {
            ( _currentItems as IList ).Clear ();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IList"/> contains a specific value.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>true if the <see cref="T:System.Object"/> is found in the <see cref="T:System.Collections.IList"/>; otherwise, false.</returns>
        bool IList.Contains ( object value )
        {
            return ( _currentItems as IList ).Contains ( value );
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null. </exception>   
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index"/> is less than zero. </exception>   
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>. </exception>
        /// <exception cref="T:System.ArgumentException">The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>. </exception>
        void ICollection.CopyTo ( Array array, int index )
        {
            ( _currentItems as IList ).CopyTo ( array, index );
        }

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>The index of <paramref name="value"/> if found in the list; otherwise, -1.</returns>
        int IList.IndexOf ( object value )
        {
            return ( _currentItems as IList ).IndexOf ( value );
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.IList"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value"/> should be inserted.</param>
        /// <param name="value">The object to insert into the <see cref="T:System.Collections.IList"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Index is not a valid index in the <see cref="T:System.Collections.IList"/>. </exception>   
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"/> is read-only.-or- The <see cref="T:System.Collections.IList"/> has a fixed size. </exception>   
        /// <exception cref="T:System.NullReferenceException">
        /// Value is null reference in the <see cref="T:System.Collections.IList"/>.</exception>
        void IList.Insert ( int index, object value )
        {
            ( _currentItems as IList ).Insert ( index, value );
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="T:System.Collections.IList"/>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"/> is read-only.-or- The <see cref="T:System.Collections.IList"/> has a fixed size. </exception>
        void IList.Remove ( object value )
        {
            ( _currentItems as IList ).Remove ( value );
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>   
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        void IList.RemoveAt ( int index )
        {
            ( _currentItems as IList ).RemoveAt ( index );
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator ()
        {
            return ( _currentItems as IEnumerable ).GetEnumerator ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Currents the items collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void CurrentItemsCollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            CollectionChanged ( sender, e );

            if ( e.Action == NotifyCollectionChangedAction.Remove )
            {
                foreach ( var oldItem in e.OldItems )
                {
                    var oldItemT = ( T )oldItem;
                    RemovedItems.Add ( oldItemT );
                }

                if ( SoftDeleted != null )
                {
                    SoftDeleted ( this, new SoftDeletedEventArgs ( e.OldItems ) );
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize ()
        {
            RemovedItems = new ObservableCollection<T> ();
            _currentItems.CollectionChanged += CurrentItemsCollectionChanged;
        }

        #endregion
    }
}
