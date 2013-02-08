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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Rem.Ria.Infrastructure.View;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Class for wrapping paged collection view.
    /// </summary>
    /// <typeparam name="T">The type in the wrapper.</typeparam>
    public class PagedCollectionViewWrapper<T> : CustomNotificationObject
    {
        #region Constants and Fields

        private readonly ObservableCollection<CustomPropertyGroupDescription> _groupingDescriptions;
        private PagedCollectionView _pagedCollectionView;
        private CustomPropertyGroupDescription _selectedGroupingDescription;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedCollectionViewWrapper&lt;T&gt;"/> class.
        /// </summary>
        public PagedCollectionViewWrapper ()
        {
            _pagedCollectionView = new PagedCollectionView ( new List<T> () );

            _groupingDescriptions = new ObservableCollection<CustomPropertyGroupDescription> ();

            DetailLevelChangedCommand = new DelegateCommand ( ExecuteDetailLevelChanged );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the detail level changed command.
        /// </summary>
        public ICommand DetailLevelChangedCommand { get; private set; }

        /// <summary>
        /// Gets the grouping descriptions.
        /// </summary>
        public ObservableCollection<CustomPropertyGroupDescription> GroupingDescriptions
        {
            get { return _groupingDescriptions; }
        }

        /// <summary>
        /// Gets the paged collection view.
        /// </summary>
        public PagedCollectionView PagedCollectionView
        {
            get { return _pagedCollectionView; }
        }

        /// <summary>
        /// Gets or sets the selected grouping description.
        /// </summary>
        /// <value>The selected grouping description.</value>
        public CustomPropertyGroupDescription SelectedGroupingDescription
        {
            get { return _selectedGroupingDescription; }
            set
            {
                _selectedGroupingDescription = value;
                RaisePropertyChanged ( () => SelectedGroupingDescription );

                ReGroup ();
            }
        }

        /// <summary>
        /// Gets or sets the sort description.
        /// </summary>
        /// <value>The sort description.</value>
        public SortDescription? SortDescription { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        public void SetFilter ( Predicate<object> filter )
        {
            PagedCollectionView.Filter = filter;
            PagedCollectionView.Refresh ();
        }

        /// <summary>
        /// Wraps the in paged collection view.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        public void WrapInPagedCollectionView ( IEnumerable<T> list )
        {
            if ( list == null )
            {
                _pagedCollectionView = new PagedCollectionView ( new List<T> () );
            }
            else
            {
                _pagedCollectionView = new PagedCollectionView ( list );
            }

            RaisePropertyChanged ( () => PagedCollectionView );
            ReGroup ();
        }

        /// <summary>
        /// Wraps the in paged collection view.
        /// </summary>
        /// <param name="list">The list to wrap.</param>
        /// <param name="filter">The filter.</param>
        public void WrapInPagedCollectionView ( IEnumerable<T> list, Predicate<object> filter )
        {
            WrapInPagedCollectionView ( list );

            PagedCollectionView.Filter = filter;
        }

        #endregion

        #region Methods

        private void ExecuteDetailLevelChanged ()
        {
            _pagedCollectionView.Refresh ();
        }

        private void ReGroup ()
        {
            _pagedCollectionView.GroupDescriptions.Clear ();

            if ( _selectedGroupingDescription != null )
            {
                _pagedCollectionView.GroupDescriptions.Add ( _selectedGroupingDescription );
            }
        }

        #endregion
    }
}
