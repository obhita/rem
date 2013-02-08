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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Base class for QuickPickerViewModel
    /// </summary>
    public abstract class QuickPickerViewModelBase : ViewModelBase
    {
        #region Constants and Fields

        private readonly int _minimumQuickSearchCriteriaLength;
        private readonly PagedCollectionView _searchResults;
        private readonly int _throttleInMilliseconds;
        private IDisposable _curCommunicaterListener;
        private int _pageIndex;
        private int _pageSize;
        private string _quickSearchCriteria;

        private QuickPickerCommunicator _searchCommunicator = new QuickPickerCommunicator ();
        private int _totalItemCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickPickerViewModelBase"/> class.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        protected QuickPickerViewModelBase ( ICommandFactory commandFactory )
        {
            _searchResults = new PagedCollectionView ( new ObservableCollection<ISearchResultDto> () );

            _minimumQuickSearchCriteriaLength = 1;
            _throttleInMilliseconds = 200;

            _pageSize = 0;
            _totalItemCount = 0;
            _pageIndex = -1;

            HandleQuickSearchCriteriaChangedEventDeclaratively ();

            var commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );

            ShowFullListCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowFullListCommand, ExecuteShowFullListCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                var oldPageIndex = _pageIndex;
                if ( _pageIndex != value )
                {
                    _pageIndex = value;
                    RaisePropertyChanged ( () => PageIndex );

                    if ( oldPageIndex != -1 && _pageIndex != -1 )
                    {
                        GetNewPage ();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the quick search criteria.
        /// </summary>
        /// <value>The quick search criteria.</value>
        public string QuickSearchCriteria
        {
            get { return _quickSearchCriteria; }
            set
            {
                ApplyPropertyChange ( ref _quickSearchCriteria, () => QuickSearchCriteria, value );

                if (_quickSearchCriteria != null && _quickSearchCriteria.Trim() == string.Empty)
                {
                    ( SearchResults.SourceCollection as IList ).Clear ();
                }
            }
        }

        /// <summary>
        /// Gets or sets the search communicater.
        /// </summary>
        /// <value>The search communicater.</value>
        public QuickPickerCommunicator SearchCommunicater
        {
            get { return _searchCommunicator; }
            set
            {
                if ( _curCommunicaterListener != null )
                {
                    _curCommunicaterListener.Dispose ();
                }
                var valueToSet = value ?? new QuickPickerCommunicator ();
                if ( value != null && value.SelectedItem != null )
                {
                    QuickSearchCriteria = value.SelectedItem.SelectedText;
                }
                ApplyPropertyChange ( ref _searchCommunicator, () => SearchCommunicater, valueToSet );
                if ( _searchCommunicator != null )
                {
                    HandleSelectedItemChanged ();
                }

                _pageSize = valueToSet.PageSize;
            }
        }

        /// <summary>
        /// Gets the search results.
        /// </summary>
        public PagedCollectionView SearchResults
        {
            get { return _searchResults; }
        }

        /// <summary>
        /// Gets the show full list command.
        /// </summary>
        public ICommand ShowFullListCommand { get; private set; }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        public int TotalItemCount
        {
            get { return _totalItemCount; }
            private set { ApplyPropertyChange ( ref _totalItemCount, () => TotalItemCount, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected abstract ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory );

        /// <summary>
        /// Does the search.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        protected abstract void DoSearch ( string searchCriteria, int pageIndex, int pageSize );

        /// <summary>
        /// Resultses the received.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        protected void ResultsReceived ( IEnumerable results, int pageIndex, int pageSize, int totalCount )
        {
            if ( results == null )
            {
                TotalItemCount = 0;
                PageIndex = -1;
            }
            else
            {
                //this.SearchResults.DeferRefresh ();
                //TODO - this is going to cache all values returned for the life of the app
                //Need to come up with a strategy of when to clean the cache
                //Also need to change to replace values with the same key, this kind of works like eventual consistency!
                ( SearchResults.SourceCollection as IList ).Clear ();

                foreach ( var result in results )
                {
                    ( SearchResults.SourceCollection as IList ).Add ( result );
                }
                SearchResults.Refresh ();

                _pageIndex = pageIndex;
                RaisePropertyChanged ( () => PageIndex );
                TotalItemCount = totalCount;
            }
        }

        private void ExecuteShowFullListCommand ()
        {
            QuickSearchCriteria = string.Empty;
            DoSearch ( string.Empty, 0, _pageSize );
        }

        private void GetNewPage ()
        {
            DoSearch ( _quickSearchCriteria, _pageIndex, _pageSize );
        }

        private void HandleQuickSearchCriteriaChangedEventDeclaratively ()
        {
            var input =
                (
                    from evt in Observable.FromEventPattern<PropertyChangedEventArgs> ( this, "PropertyChanged" )
                    where evt.EventArgs.PropertyName == PropertyUtil.ExtractPropertyName ( () => QuickSearchCriteria )
                    select ( ( QuickPickerViewModelBase )evt.Sender ).QuickSearchCriteria
                )
                    .Throttle ( TimeSpan.FromMilliseconds ( _throttleInMilliseconds ) )
                    .DistinctUntilChanged ();

            var inputSubscription = input.ObserveOnDispatcher ().Subscribe (
                    search =>
                        {
                            search = search == null ? string.Empty : search.Trim ();
                            if ( ( search.Length >= _minimumQuickSearchCriteriaLength )
                                 && ( SearchCommunicater.SelectedItem == null || SearchCommunicater.SelectedItem.SelectedText != search ) )
                            {
                                DoSearch ( search, 0, _pageSize );
                            }
                        }
                );
        }

        private void HandleSelectedItemChanged ()
        {
            var input =
                (
                    from evt in Observable.FromEventPattern<SelectionChangedEventArgs> ( SearchCommunicater, "SelectedItemChanged" )
                    select ( ( QuickPickerCommunicator )evt.Sender ).SelectedItem
                )
                    .Throttle ( TimeSpan.FromMilliseconds ( _throttleInMilliseconds ) )
                    .DistinctUntilChanged ();

            _curCommunicaterListener = input.ObserveOnDispatcher ().Subscribe (
                    item =>
                        {
                            if ( item != null )
                            {
                                QuickSearchCriteria = item.SelectedText;
                            }
                        } );
        }

        #endregion
    }
}
