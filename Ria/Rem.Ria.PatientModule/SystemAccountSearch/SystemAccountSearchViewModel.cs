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
using System.Windows.Data;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.PatientModule.Web.SystemAccountSearch;

namespace Rem.Ria.PatientModule.SystemAccountSearch
{
    /// <summary>
    /// View Model for SystemAccountSearch class.
    /// </summary>
    public class SystemAccountSearchViewModel : SearchViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private IList<LocationDisplayNameDto> _locationList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountSearchViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public SystemAccountSearchViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory, new SystemAccountAdvancedSearchCriteria () )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            ApplyContextChanges = true;
            ContextChanged += OnContextChanged;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the advanced search criteria.
        /// </summary>
        public SystemAccountAdvancedSearchCriteria AdvancedSearchCriteria
        {
            get { return ( SystemAccountAdvancedSearchCriteria )_advancedSearchCriteria; }
        }

        /// <summary>
        /// Gets or sets the locations list.
        /// </summary>
        /// <value>The locations list.</value>
        public IList<LocationDisplayNameDto> LocationsList
        {
            get { return _locationList; }
            set { ApplyPropertyChange ( ref _locationList, () => LocationsList, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected override ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory )
        {
            return CommandFactoryHelper.CreateHelper ( this, commandFactory );
        }

        /// <summary>
        /// Does the advanced search.
        /// </summary>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoAdvancedSearch ( bool isPageChange )
        {
            var request = new GetSystemAccountsByAdvancedSearchRequest
                {
                    FirstName = AdvancedSearchCriteria.FirstNameToSearch,
                    MiddleName = AdvancedSearchCriteria.MiddleNameToSearch,
                    LastName = AdvancedSearchCriteria.LastNameToSearch,
                    AccountName = AdvancedSearchCriteria.AccountNameToSearch,
                    LocationKey = AdvancedSearchCriteria.LocationToSearch == null ? ( long? )null : AdvancedSearchCriteria.LocationToSearch.Key,
                    ActiveIndicator = AdvancedSearchCriteria.ActiveIndicatorToSearch
                };

            if ( isPageChange )
            {
                request.PageIndex = PageIndex;
                request.PageSize = PageSize;
            }
            else
            {
                request.PageIndex = 0;
                request.PageSize = Pagesize;
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetSystemAccountsByAdvancedSearchCompleted, HandleSearchSystemAccountsException );
        }

        /// <summary>
        /// Does the quick search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoQuickSearch ( string search, bool isPageChange )
        {
            var request = new GetSystemAccountsByKeywordRequest { SearchCriteria = search };

            if ( isPageChange )
            {
                request.PageIndex = PageIndex;
                request.PageSize = PageSize;
            }
            else
            {
                request.PageIndex = 0;
                request.PageSize = Pagesize;
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetSystemAccountsByKeywordCompleted, HandleSearchSystemAccountsException );
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private void GetLocationsByAgencyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetLocationsByAgencyResponse> ();
            LocationsList = response.Locations;
            IsLoading = false;
        }

        private void HandleGetLocationsByAgencyException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Getting locations failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleGetSystemAccountsByAdvancedSearchCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetSystemAccountsByAdvancedSearchResponse> ();
            var result = response.PagedSystemAccountSearchResultDto;

            if ( result != null )
            {
                if ( result.PagedList.Count > 0 )
                {
                    ChangeSearchState (
                        SearchState.AdvancedSearchFoundState,
                        new PagedCollectionView ( result.PagedList ),
                        result.PageIndex,
                        result.PageSize,
                        result.TotalCount );
                }
                else
                {
                    ChangeSearchState (
                        SearchState.AdvancedSearchNotFoundState,
                        new PagedCollectionView ( result.PagedList ),
                        result.PageIndex,
                        result.PageSize,
                        result.TotalCount );
                }
            }
            else
            {
                ChangeSearchState ( SearchState.AdvancedSearchNotFoundState, null );
            }
            IsLoading = false;
        }

        private void HandleGetSystemAccountsByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetSystemAccountsByKeywordResponse> ();

            if ( response.SearchCriteria == _quickSearchCriteria )
            {
                var result = response.PagedSystemAccountSearchResultDto;

                if ( result != null )
                {
                    if ( result.PagedList.Count > 0 )
                    {
                        ChangeSearchState (
                            SearchState.QuickSearchFoundState,
                            new PagedCollectionView ( result.PagedList ),
                            result.PageIndex,
                            result.PageSize,
                            result.TotalCount );
                    }
                    else
                    {
                        ChangeSearchState (
                            SearchState.QuickSearchNotFoundState,
                            new PagedCollectionView ( result.PagedList ),
                            result.PageIndex,
                            result.PageSize,
                            result.TotalCount );
                    }
                }
                else
                {
                    ChangeSearchState ( SearchState.QuickSearchNotFoundState, null );
                }
            }
            IsLoading = false;
        }

        private void HandleSearchSystemAccountsException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        private void OnContextChanged ( object sender, EventArgs e )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add (
                new GetLocationsByAgencyRequest
                    {
                        AgencyKey = CurrentUserContext.Agency.Key
                    } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( GetLocationsByAgencyCompleted, HandleGetLocationsByAgencyException );
        }

        #endregion
    }
}
