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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Base class for SearchViewModel
    /// </summary>
    public abstract class SearchViewModelBase : NavigationViewModel
    {
        #region Constants and Fields

        /// <summary>
        /// Page size.
        /// </summary>
        protected const int Pagesize = 3;

        private readonly int _minimumQuickSearchCriteriaLength;

        /// <summary>
        /// Advanced search criteria.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter",
Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate",
Justification = "Reviewed. Suppression is OK here.")]
        protected AdvancedSearchCriteriaBase _advancedSearchCriteria;

        /// <summary>
        /// Quick search criteria.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter",
Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate",
Justification = "Reviewed. Suppression is OK here.")]
        protected string _quickSearchCriteria;

        private readonly int _numOfFieldsRequiredForAdvancedSearch;

        private readonly int _throttleInMilliseconds;
        private IDisposable _curCommunicaterListener;

        private int _pageIndex;
        private int _pageSize;

        private SearchCommunicater _searchCommunicator = new SearchCommunicater ();
        private PagedCollectionView _searchResults;
        private string _status;
        private int _totalItemCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModelBase"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="advancedSearchCriteriaBase">The advanced search criteria base.</param>
        protected SearchViewModelBase (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            AdvancedSearchCriteriaBase advancedSearchCriteriaBase )
            : this ( accessControlManager, commandFactory, advancedSearchCriteriaBase, true )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchViewModelBase"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="advancedSearchCriteriaBase">The advanced search criteria base.</param>
        /// <param name="handleQuickSearchChanged">If set to <c>true</c> [handle quick search changed].</param>
        protected SearchViewModelBase (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            AdvancedSearchCriteriaBase advancedSearchCriteriaBase,
            bool handleQuickSearchChanged )
            : base ( accessControlManager, commandFactory )
        {
            // TODO: Hard-coded constant. 
            _numOfFieldsRequiredForAdvancedSearch = 1;
            _minimumQuickSearchCriteriaLength = 3;
            _throttleInMilliseconds = 0; // Throttle causes all kinds of tricky usability issues

            _searchResults = null;

            _pageSize = 0;
            _totalItemCount = 0;
            _pageIndex = -1;

            var commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );

            RunAdvancedSearchCommand = commandFactoryHelper.BuildDelegateCommand (
                () => RunAdvancedSearchCommand, ExecuteRunAdvancedSearch, CanExecuteRunAdvancedSearch );
            ResetAdvancedSearchCommand = commandFactoryHelper.BuildDelegateCommand ( () => ResetAdvancedSearchCommand, ExecuteResetAdvancedSearch );
            InternalSelectionChangedCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => InternalSelectionChangedCommand, ExecuteInternalSelectionChangedCommand );
            ClearSelectedItemCommand = commandFactoryHelper.BuildDelegateCommand ( () => ClearSelectedItemCommand, ExecuteClearSelectedItemCommand );
            SearchAbortedCommand = commandFactoryHelper.BuildDelegateCommand ( () => SearchAbortedCommand, ExecuteSearchAbortedCommand );
            AddNewCommand = commandFactoryHelper.BuildDelegateCommand ( () => AddNewCommand, ExecuteAddNewCommand, CanExecuteAddNewCommand );

            if ( handleQuickSearchChanged )
            {
                HandleQuickSearchCriteriaChangedEventDeclaratively ();
            }
            else
            {
                ManualSearchCommand = new DelegateCommand ( ExecuteManualSearchCommand, CanExecuteManualSearchCommand );
                var input =
                    (
                        from evt in Observable.FromEventPattern<PropertyChangedEventArgs> ( this, "PropertyChanged" )
                        where evt.EventArgs.PropertyName == PropertyUtil.ExtractPropertyName ( () => QuickSearchCriteria )
                        select ( ( SearchViewModelBase )evt.Sender ).QuickSearchCriteria
                    );

                var inputSubscription = input.ObserveOnDispatcher ()
                    .Subscribe (
                        search => { ( ManualSearchCommand as DelegateCommand ).RaiseCanExecuteChanged (); }
                    );
            }

            _advancedSearchCriteria = advancedSearchCriteriaBase;

            var advancedSearchCriteriaPropertyChangedEvent =
                from evt in
                    Observable.FromEventPattern<PropertyChangedEventArgs> ( _advancedSearchCriteria, "PropertyChanged" )
                select evt.ToString ();

            var advancedSearchCriteriaPropertyChangedEventSubscription = advancedSearchCriteriaPropertyChangedEvent
                .ObserveOnDispatcher ().Subscribe (
                    ( arg ) => { ( RunAdvancedSearchCommand as DelegateCommandBase ).RaiseCanExecuteChanged (); }
                );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add new command.
        /// </summary>
        public ICommand AddNewCommand { get; private set; }

        /// <summary>
        /// Gets the clear selected item command.
        /// </summary>
        public ICommand ClearSelectedItemCommand { get; private set; }

        /// <summary>
        /// Gets the state of the current search.
        /// </summary>
        public SearchState CurrentSearchState { get; private set; }

        /// <summary>
        /// Gets the internal selection changed command.
        /// </summary>
        public ICommand InternalSelectionChangedCommand { get; private set; }

        /// <summary>
        /// Gets the manual search command.
        /// </summary>
        public ICommand ManualSearchCommand { get; private set; }

        /// <summary>
        /// Gets the minimum length of the quick search criteria.
        /// </summary>
        public int MinimumQuickSearchCriteriaLength
        {
            get { return _minimumQuickSearchCriteriaLength; }
        }

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

                    if ( oldPageIndex != -1 )
                    {
                        GetNewPage ();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            private set { ApplyPropertyChange ( ref _pageSize, () => PageSize, value ); }
        }

        /// <summary>
        /// Gets or sets the quick search criteria.
        /// </summary>
        /// <value>The quick search criteria.</value>
        public string QuickSearchCriteria
        {
            get { return _quickSearchCriteria; }
            set { ApplyPropertyChange ( ref _quickSearchCriteria, () => QuickSearchCriteria, value ); }
        }

        /// <summary>
        /// Gets the reset advanced search command.
        /// </summary>
        public ICommand ResetAdvancedSearchCommand { get; private set; }

        /// <summary>
        /// Gets the run advanced search command.
        /// </summary>
        public ICommand RunAdvancedSearchCommand { get; private set; }

        /// <summary>
        /// Gets the search aborted command.
        /// </summary>
        public ICommand SearchAbortedCommand { get; private set; }

        /// <summary>
        /// Gets or sets the search communicater.
        /// </summary>
        /// <value>The search communicater.</value>
        public SearchCommunicater SearchCommunicater
        {
            get { return _searchCommunicator; }
            set
            {
                if ( _curCommunicaterListener != null )
                {
                    _curCommunicaterListener.Dispose ();
                }
                var valueToSet = value ?? new SearchCommunicater ();
                if ( value != null && value.SelectedItem != null )
                {
                    _quickSearchCriteria = value.SelectedItem.SelectedText;
                }
                ApplyPropertyChange ( ref _searchCommunicator, () => SearchCommunicater, valueToSet );
                if ( _searchCommunicator != null )
                {
                    HandleSelectedItemChanged ();
                }
                _searchCommunicator.ClearSelectedItemCommand = ClearSelectedItemCommand;
            }
        }

        /// <summary>
        /// Gets the search results.
        /// </summary>
        public PagedCollectionView SearchResults
        {
            get { return _searchResults; }
            private set { ApplyPropertyChange ( ref _searchResults, () => SearchResults, value ); }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status
        {
            get { return _status; }
            private set { ApplyPropertyChange ( ref _status, () => Status, value ); }
        }

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
        /// Adds the new.
        /// </summary>
        protected virtual void AddNew ()
        {
        }

        /// <summary>
        /// Determines whether this instance [can execute add new command].
        /// </summary>
        /// <returns><c>true</c> if this instance [can execute add new command]; otherwise, <c>false</c>.</returns>
        protected virtual bool CanExecuteAddNewCommand ()
        {
            return true;
        }

        /// <summary>
        /// Determines whether this instance [can execute manual search command].
        /// </summary>
        /// <returns><c>true</c> if this instance [can execute manual search command]; otherwise, <c>false</c>.</returns>
        protected virtual bool CanExecuteManualSearchCommand ()
        {
            return QuickSearchCriteria != null && QuickSearchCriteria.Length >= _minimumQuickSearchCriteriaLength;
        }

        /// <summary>
        /// Changes the state of the search.
        /// </summary>
        /// <param name="newState">The new state.</param>
        /// <param name="view">The paged view.</param>
        protected void ChangeSearchState ( SearchState newState, PagedCollectionView view )
        {
            ChangeSearchState ( newState, view, -1, 0, 0 );
        }

        /// <summary>
        /// Changes the state of the search.
        /// </summary>
        /// <param name="newState">The new state.</param>
        /// <param name="view">The paged view.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        protected void ChangeSearchState ( SearchState newState, PagedCollectionView view, int pageIndex, int pageSize, int totalCount )
        {
            CurrentSearchState = newState;
            if ( newState == SearchState.AdvancedSearchingState ||
                 newState == SearchState.QuickSearchingState )
            {
                Status = "Searching. Please wait ...";
            }
            else if ( newState == SearchState.AdvancedSearchNotFoundState ||
                      newState == SearchState.QuickSearchNotFoundState )
            {
                if ( _searchCommunicator.SelectedItem != null && _searchCommunicator.ClearSelectionWhenAborted )
                {
                    ExecuteClearSelectedItemCommand ();
                }
                Status = "Matching search not found.";
            }
            else
            {
                Status = " ";
            }

            if ( view == null )
            {
                SearchResults = null;

                PageSize = 0;
                TotalItemCount = 0;
                PageIndex = -1;
            }
            else
            {
                SearchResults = view;

                PageSize = pageIndex != -1 ? pageSize : 0;
                PageIndex = pageIndex;
                TotalItemCount = totalCount;
            }
        }

        /// <summary>
        /// Does the advanced search.
        /// </summary>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected abstract void DoAdvancedSearch ( bool isPageChange );

        /// <summary>
        /// Does the quick search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected abstract void DoQuickSearch ( string search, bool isPageChange );

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private bool CanExecuteRunAdvancedSearch ()
        {
            if ( _advancedSearchCriteria.HasEnoughPublicPropertiesWithValue ( _numOfFieldsRequiredForAdvancedSearch ) )
            {
                return true;
            }

            return false;
        }

        private void ExecuteAddNewCommand ()
        {
            AddNew ();
        }

        private void ExecuteClearSelectedItemCommand ()
        {
            _searchCommunicator.SelectedItem = null;
            QuickSearchCriteria = string.Empty;
        }

        private void ExecuteInternalSelectionChangedCommand ( object obj )
        {
            var result = obj as ISearchResultDto;
            _quickSearchCriteria = result != null ? result.SelectedText : string.Empty;
            if ( _searchCommunicator != null && _searchCommunicator.SelectedItemChangedCommand != null )
            {
                _searchCommunicator.SelectedItemChangedCommand.Execute ( obj );
            }
            if ( ManualSearchCommand != null )
            {
                ( ManualSearchCommand as DelegateCommand ).RaiseCanExecuteChanged ();
            }
        }

        private void ExecuteManualSearchCommand ()
        {
            ChangeSearchState ( SearchState.QuickSearchingState, null );
            DoQuickSearch ( QuickSearchCriteria, false );
        }

        private void ExecuteResetAdvancedSearch ()
        {
            _advancedSearchCriteria.CleanUpAdvancedSearchFields ();
        }

        private void ExecuteRunAdvancedSearch ()
        {
            ChangeSearchState ( SearchState.AdvancedSearchingState, null );
            DoAdvancedSearch ( false );
        }

        private void ExecuteSearchAbortedCommand ()
        {
            _quickSearchCriteria = _searchCommunicator.SelectedItem != null ? _searchCommunicator.SelectedItem.SelectedText : string.Empty;
        }

        private void GetNewPage ()
        {
            if ( CurrentSearchState ==
                 SearchState.QuickSearchFoundState )
            {
                DoQuickSearch ( _quickSearchCriteria, true );
            }
            else if ( CurrentSearchState ==
                      SearchState.AdvancedSearchFoundState )
            {
                DoAdvancedSearch ( true );
            }
        }

        private void HandleQuickSearchCriteriaChangedEventDeclaratively ()
        {
            var input =
                (
                    from evt in Observable.FromEventPattern<PropertyChangedEventArgs> ( this, "PropertyChanged" )
                    where evt.EventArgs.PropertyName == PropertyUtil.ExtractPropertyName ( () => QuickSearchCriteria )
                    select ( ( SearchViewModelBase )evt.Sender ).QuickSearchCriteria
                )
                    .Where (
                        search =>
                        ( search == null ? string.Empty : search.Trim () ).Length >= _minimumQuickSearchCriteriaLength )
                    .Throttle ( TimeSpan.FromMilliseconds ( _throttleInMilliseconds ) );
            ////.DistinctUntilChanged ();

            var inputSubscription = input.ObserveOnDispatcher ().Subscribe (
                    search =>
                        {
                            ChangeSearchState ( SearchState.QuickSearchingState, null );
                            DoQuickSearch ( search, false );
                        }
                );
        }

        private void HandleSelectedItemChanged ()
        {
            var input =
                (
                    from evt in Observable.FromEventPattern<SelectionChangedEventArgs> ( SearchCommunicater, "SelectedItemChanged" )
                    select ( ( SearchCommunicater )evt.Sender ).SelectedItem
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
