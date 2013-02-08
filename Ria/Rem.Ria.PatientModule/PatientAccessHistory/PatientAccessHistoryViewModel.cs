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
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientAccessHistory;

namespace Rem.Ria.PatientModule.PatientAccessHistory
{
    /// <summary>
    /// View Model for History for patient access.
    /// </summary>
    public class PatientAccessHistoryViewModel : NavigationViewModel, IWorkspaceHeaderContextProvider
    {
        #region Constants and Fields

        private const string PatientAccessEventType = "PatientAccessEventType";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly LookupValueDto _defaultAccessType;
        private readonly CustomPropertyGroupDescription _defaultGroupingDescription;
        private readonly ObservableCollection<CustomPropertyGroupDescription> _groupingDescriptions;
        private readonly IUserDialogService _userDialogService;
        private LookupValueDto _accessType;
        private DateTime? _endDate;

        private IDictionary<string, ObservableCollection<LookupValueDto>> _lookupValueLists;
        private int _pageIndex;
        private int _pageSize;
        private PatientSearchResultDto _patient;
        private PagedCollectionView _patientAccessEvents;

        private CustomPropertyGroupDescription _selectedGroupingDescription;
        private string _sortBy;
        private ListSortDirection _sortDirection;
        private DateTime? _startDate;
        private int _totalItemCount;
        private SystemAccountSearchResultDto _user;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientAccessHistoryViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public PatientAccessHistoryViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SearchCommand = commandFactoryHelper.BuildDelegateCommand ( () => SearchCommand, ExecuteSearch );

            ResetCommand = commandFactoryHelper.BuildDelegateCommand ( () => ResetCommand, ExecuteReset );

            _defaultAccessType = new LookupValueDto { Name = "None" };
            _accessType = _defaultAccessType;

            PageSizeList = new List<int> { 100, 125, 150, 175, 200 };
            _pageSize = 100;
            _totalItemCount = 0;
            _pageIndex = 0;

            var lookupValueNames = new[] { PatientAccessEventType };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            foreach ( var lookupValueName in lookupValueNames )
            {
                requestDispatcher.AddLookupValuesRequest ( lookupValueName );
            }
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetLookupvaluesCompleted, HandleGetLookupValuesException );

            _defaultGroupingDescription = new CustomPropertyGroupDescription ( string.Empty, "None" );
            _groupingDescriptions = new ObservableCollection<CustomPropertyGroupDescription> ();
            _selectedGroupingDescription = _defaultGroupingDescription;
            _groupingDescriptions.Add ( _selectedGroupingDescription );

            InitializeGroupingDescriptions ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the type of the access.
        /// </summary>
        /// <value>The type of the access.</value>
        public LookupValueDto AccessType
        {
            get { return _accessType; }
            set { ApplyPropertyChange ( ref _accessType, () => AccessType, value ); }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set { ApplyPropertyChange ( ref _endDate, () => EndDate, value ); }
        }

        /// <summary>
        /// Gets the grouping descriptions.
        /// </summary>
        public ObservableCollection<CustomPropertyGroupDescription> GroupingDescriptions
        {
            get { return _groupingDescriptions; }
        }

        /// <summary>
        /// Gets the header context.
        /// </summary>
        public object HeaderContext
        {
            get { return "Patient Access History"; }
        }

        /// <summary>
        /// Gets or sets the lookup value lists.
        /// </summary>
        /// <value>The lookup value lists.</value>
        public IDictionary<string, ObservableCollection<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
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
                if ( _pageIndex == value )
                {
                    return;
                }

                _pageIndex = value;
                RaisePropertyChanged ( () => PageIndex );

                ExecuteSearch ();
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if ( _pageSize == value || _patientAccessEvents == null )
                {
                    return;
                }

                _pageSize = value;
                RaisePropertyChanged ( () => PageSize );

                ExecuteSearch ();
            }
        }

        /// <summary>
        /// Gets or sets the page size list.
        /// </summary>
        /// <value>The page size list.</value>
        public IList<int> PageSizeList { get; set; }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>The patient.</value>
        public PatientSearchResultDto Patient
        {
            get { return _patient; }
            set { ApplyPropertyChange ( ref _patient, () => Patient, value ); }
        }

        /// <summary>
        /// Gets the patient access events.
        /// </summary>
        public PagedCollectionView PatientAccessEvents
        {
            get { return _patientAccessEvents; }
            private set { ApplyPropertyChange ( ref _patientAccessEvents, () => PatientAccessEvents, value ); }
        }

        /// <summary>
        /// Gets the reset command.
        /// </summary>
        public ICommand ResetCommand { get; private set; }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        public ICommand SearchCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected grouping description.
        /// </summary>
        /// <value>The selected grouping description.</value>
        public CustomPropertyGroupDescription SelectedGroupingDescription
        {
            get { return _selectedGroupingDescription; }
            set { ApplyPropertyChange ( ref _selectedGroupingDescription, () => SelectedGroupingDescription, value ); }
        }

        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>The sort by.</value>
        public string SortBy
        {
            get { return _sortBy; }
            set { ApplyPropertyChange ( ref _sortBy, () => SortBy, value ); }
        }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>The sort direction.</value>
        public ListSortDirection SortDirection
        {
            get { return _sortDirection; }
            set { ApplyPropertyChange ( ref _sortDirection, () => SortDirection, value ); }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set { ApplyPropertyChange ( ref _startDate, () => StartDate, value ); }
        }

        /// <summary>
        /// Gets or sets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        public int TotalItemCount
        {
            get { return _totalItemCount; }
            set { ApplyPropertyChange ( ref _totalItemCount, () => TotalItemCount, value ); }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public WorkspaceType Type
        {
            get { return WorkspaceType.Administrative; }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user for the account.</value>
        public SystemAccountSearchResultDto User
        {
            get { return _user; }
            set { ApplyPropertyChange ( ref _user, () => User, value ); }
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
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
        }

        private bool CanExecuteSearch ()
        {
            return Patient != null || User != null;
        }

        private void ExecuteReset ()
        {
            Patient = null;
            StartDate = null;
            EndDate = null;
            AccessType = _defaultAccessType;
            User = null;
            SelectedGroupingDescription = _defaultGroupingDescription;
            IsLoading = false;
        }

        private void ExecuteSearch ()
        {
            string sortingMemberName = null;

            // SelectedGroupingDescription denotes the first order, sort/grid grouping.
            if ( SelectedGroupingDescription != _defaultGroupingDescription )
            {
                sortingMemberName = SelectedGroupingDescription.PropertyName;
            }

            // SecondOrderSortBy denotes the second order, directional sort.
            var secondOrderSortBy = SortBy;
            var secondOrderSortDirection = SortDirection.ToString ();

            if ( CanExecuteSearch () )
            {
                IsLoading = true;
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add (
                    new GetPatientAccessHistoryBySearchCriteriaRequest
                        {
                            PatientKey = Patient == null ? 0 : Patient.Key,
                            UserKey = User == null ? 0 : User.Key,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            AccessType = AccessType == null ? string.Empty : AccessType.WellKnownName,
                            PageIndex = _pageIndex,
                            PageSize = _pageSize,
                            SortingMemberName = sortingMemberName,
                            SecondOrderSortBy = secondOrderSortBy,
                            SecondOrderSortDirection = secondOrderSortDirection
                        } );
                IsLoading = true;
                requestDispatcher.ProcessRequests (
                    HandleGetPatientAccessHistoryBySearchCriteriaCompleted, HandleGetPatientAccessHistoryBySearchCriteriaException );
            }
            else
            {
                _userDialogService.ShowDialog (
                    "A Patient or User must be selected in the search criteria in order to refresh the search results.",
                    "Search Validation",
                    UserDialogServiceOptions.Ok );
            }
        }

        private void HandleGetLookupValuesException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                "Could not retrieve the lookup values. Error: " + ex.Message,
                "An error has occurred",
                UserDialogServiceOptions.Ok );
        }

        private void HandleGetLookupvaluesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            var localLookupValueLists = new Dictionary<string, ObservableCollection<LookupValueDto>> ();

            foreach ( var list in lookupValueLists )
            {
                ObservableCollection<LookupValueDto> lookupValueDtos;

                if ( list.Key == PatientAccessEventType )
                {
                    lookupValueDtos = new ObservableCollection<LookupValueDto> { _defaultAccessType };

                    foreach ( var lookupValueDto in list.Value )
                    {
                        lookupValueDtos.Add ( lookupValueDto );
                    }
                }
                else
                {
                    lookupValueDtos = new ObservableCollection<LookupValueDto> ( list.Value );
                }

                localLookupValueLists.Add ( list.Key, lookupValueDtos );
            }

            LookupValueLists = localLookupValueLists;
            AccessType = _defaultAccessType;
            IsLoading = false;
        }

        private void HandleGetPatientAccessHistoryBySearchCriteriaCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPatientAccessHistoryBySearchCriteriaResponse> ();

            var result = response.PagedPatientAccessEventSearchResultDto;
            TotalItemCount = result.TotalCount;
            PatientAccessEvents = new PagedCollectionView ( result.PagedList );

            _patientAccessEvents.GroupDescriptions.Clear ();
            _patientAccessEvents.SortDescriptions.Clear ();
            IsLoading = false;

            if ( _selectedGroupingDescription != _defaultGroupingDescription )
            {
                _patientAccessEvents.GroupDescriptions.Add ( _selectedGroupingDescription );
                _patientAccessEvents.SortDescriptions.Add (
                    new SortDescription ( _selectedGroupingDescription.PropertyName, ListSortDirection.Ascending ) );
            }

            if ( SortBy != null )
            {
                _patientAccessEvents.SortDescriptions.Add ( new SortDescription ( SortBy, SortDirection ) );
            }
            IsLoading = false;
        }

        private void HandleGetPatientAccessHistoryBySearchCriteriaException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<PatientAccessEventDto, object> (
                        p => p.PatientName ),
                    "Patient Name" ) );
            GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<PatientAccessEventDto, object> (
                        p => p.UserName ),
                    "User Name" ) );
            GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<PatientAccessEventDto, object> (
                        p => p.PatientAccessEventTypeName ),
                    "Access Type" ) );
        }

        #endregion
    }
}
