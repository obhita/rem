using System.Windows.Data;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.AgencyModule.Web.StaffSearch;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.StaffSearch
{
    /// <summary>
    /// View Model for StaffSearch class.
    /// </summary>
    public class StaffSearchViewModel : SearchViewModelBase
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffSearchViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public StaffSearchViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory, new AdvancedSearchCriteriaBase () )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
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
            //do nothing
        }

        /// <summary>
        /// Does the quick search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoQuickSearch ( string search, bool isPageChange )
        {
            var request = new GetAgencyStaffsByKeywordRequest
                {
                    SearchCriteria = search,
                    PageIndex = 0,
                    PageSize = Pagesize
                };

            if ( isPageChange )
            {
                request.PageIndex = PageIndex;
                request.PageSize = PageSize;
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( request );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAgencyStaffsByKeywordCompleted, HandleGetAgencyStaffsByKeywordException );
        }

        private void HandleGetAgencyStaffsByKeywordCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAgencyStaffsByKeywordResponse> ();
            var searchedCriteria = response.SearchedCriteria;
            var result = response.PagedAgencyStaffSearchResultDto;

            if ( searchedCriteria == _quickSearchCriteria )
            {
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

        private void HandleGetAgencyStaffsByKeywordException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
