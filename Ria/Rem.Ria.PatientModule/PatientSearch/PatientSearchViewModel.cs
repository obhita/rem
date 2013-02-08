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
using System.Reactive.Linq;
using System.Text;
using System.Windows.Data;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.PatientSearch;

namespace Rem.Ria.PatientModule.PatientSearch
{
    /// <summary>
    /// Add new states
    /// </summary>
    public enum AddNewState
    {
        /// <summary>
        /// Unknown state
        /// </summary>
        Unknown,

        /// <summary>
        /// Successful state
        /// </summary>
        Successful,

        /// <summary>
        /// Error state
        /// </summary>
        Error
    }

    /// <summary>
    /// View Model for PatientSearch class.
    /// </summary>
    public class PatientSearchViewModel : SearchViewModelBase
    {
        #region Constants and Fields

        private const string PatientWorkspaceView = "PatientWorkspaceView";
        private const string WorkspacesRegion = "WorkspacesRegion";

        private readonly IUserDialogService _userDialogService;
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly INavigationService _navigationService;

        private AddNewPatient _addNewPatient;
        private AddNewState _addNewState; 
        private IDictionary<string, ObservableCollection<LookupValueDto>> _lookupValueLists;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientSearchViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientSearchViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory
            )
            : base ( accessControlManager, commandFactory, new PatientAdvancedSearchCriteria () )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _navigationService = navigationService;
            _userDialogService = userDialogService;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher
                .AddLookupValuesRequest ( "PatientGender" )
                .AddLookupValuesRequest ( "StateProvince" )
                .AddLookupValuesRequest ( "PatientIdentifierType" )
                .AddLookupValuesRequest ( "Suffix" );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( LookupValueRequestDispatcherCompleted, HandleRequestDispatcherException );

            var advancedSearchCriteriaPropertyChangedEvent =
                from evt in
                    Observable.FromEventPattern<PropertyChangedEventArgs> ( _advancedSearchCriteria, "PropertyChanged" )
                select evt.ToString ();

            advancedSearchCriteriaPropertyChangedEvent
                .ObserveOnDispatcher().Subscribe (
                    arg =>
                        {
                            if ( arg == PropertyUtil.ExtractPropertyName ( () => AdvancedSearchCriteria.IdentifierTypeToSearch ) )
                            {
                                RaisePropertyChanged ( () => IsIdentifierTypeToSearchNotNull );
                            }
                        }
                );

            ApplyContextChanges = true;
            AddNewPatient = new AddNewPatient ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the add new patient.
        /// </summary>
        /// <value>The add new patient.</value>
        public AddNewPatient AddNewPatient
        {
            get { return _addNewPatient; }
            set { ApplyPropertyChange ( ref _addNewPatient, () => AddNewPatient, value ); }
        }

        /// <summary>
        /// Gets or sets the new state of the add.
        /// </summary>
        /// <value>The new state of the add.</value>
        public AddNewState AddNewState
        {
            get { return _addNewState; }
            set { ApplyPropertyChange ( ref _addNewState, () => AddNewState, value ); }
        }

        /// <summary>
        /// Gets the advanced search criteria.
        /// </summary>
        public PatientAdvancedSearchCriteria AdvancedSearchCriteria
        {
            get { return ( PatientAdvancedSearchCriteria )_advancedSearchCriteria; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is identifier type to search not null.
        /// </summary>
        public bool IsIdentifierTypeToSearchNotNull
        {
            get { return AdvancedSearchCriteria.IdentifierTypeToSearch != null; }
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

        #endregion

        #region Methods

        /// <summary>
        /// Adds the new.
        /// </summary>
        protected override void AddNew ()
        {
            AddNewState = AddNewState.Unknown;
            var createPatientRequest = new CreateNewPatientRequest
                {
                    AgencyKey = CurrentUserContext.Agency.Key,
                    FirstName = AddNewPatient.FirstName,
                    LastName = AddNewPatient.LastName,
                    BirthDate = AddNewPatient.BirthDate,
                    Gender = AddNewPatient.Gender
                };
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( createPatientRequest );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( CreatePatientRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        /// <summary>
        /// Determines whether this instance [can execute add new command].
        /// </summary>
        /// <returns><c>true</c> if this instance [can execute add new command]; otherwise, <c>false</c>.</returns>
        protected override bool CanExecuteAddNewCommand ()
        {
            return true;
        }

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
            var request = new GetPatientsByAdvancedSearchRequest
                {
                    FirstName = AdvancedSearchCriteria.FirstNameToSearch,
                    MiddleName = AdvancedSearchCriteria.MiddleNameToSearch,
                    LastName = AdvancedSearchCriteria.LastNameToSearch,
                    GenderWellKnownName =
                        AdvancedSearchCriteria.GenderToSearch == null ? string.Empty : AdvancedSearchCriteria.GenderToSearch.WellKnownName,
                    BirthDate = AdvancedSearchCriteria.BirthDateToSearch,
                    MotherMaidenName = AdvancedSearchCriteria.MotherMaidenNameToSearch,
                    IdentifierTypeWellKnownName =
                        AdvancedSearchCriteria.IdentifierTypeToSearch == null
                            ? string.Empty
                            : AdvancedSearchCriteria.IdentifierTypeToSearch.WellKnownName,
                    Identifier = AdvancedSearchCriteria.IdentifierToSearch,
                    AddressLineOne = AdvancedSearchCriteria.AddressLineOneToSearch,
                    City = AdvancedSearchCriteria.CityToSearch,
                    StateWellKnownName =
                        AdvancedSearchCriteria.StateToSearch == null ? string.Empty : AdvancedSearchCriteria.StateToSearch.WellKnownName,
                    SuffixName = AdvancedSearchCriteria.SuffixToSearch == null ? string.Empty : AdvancedSearchCriteria.SuffixToSearch.Name,
                    ZipCode = AdvancedSearchCriteria.ZipCodeToSearch,
                    UniqueIdentifier = AdvancedSearchCriteria.UniqueIdentifierToSearch
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
            requestDispatcher.ProcessRequests ( HandleGetPatientsByAdvancedSearchCompleted, HandleSearchPatientsException );
        }

        /// <summary>
        /// Does the quick search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="isPageChange">If set to <c>true</c> [is page change].</param>
        protected override void DoQuickSearch ( string search, bool isPageChange )
        {
            var request = new GetPatientsByKeywordsRequest
                {
                    SearchCriteria = search
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
            requestDispatcher.ProcessRequests ( HandleGetPatientsByKeywordsCompleted, HandleSearchPatientsException );
        }

        private void CreatePatientRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<CreateNewPatientResponse> ();
            var validationErrors = response.DataTransferObject.DataErrorInfoCollection.ToList ();

            if ( validationErrors.Count > 0 )
            {
                var errors = new StringBuilder ();
                errors.AppendLine ( "The following errors occurred: " );

                foreach ( var validationError in validationErrors )
                {
                    errors.AppendLine ( validationError.Message );
                }

                _userDialogService.ShowDialog ( errors.ToString (), "Errors", UserDialogServiceOptions.Ok );
            }
            else
            {
                AddNewState = AddNewState.Successful;
                AddNewPatient.CleanUpAllFields ();

                _navigationService.Navigate (
                    WorkspacesRegion,
                    PatientWorkspaceView,
                    "SubViewPassThrough",
                    new[]
                        {
                            new KeyValuePair<string, string> ( "PatientKey", response.PatientDto.Key.ToString () ),
                            new KeyValuePair<string, string> ( "FullName", response.PatientDto.PatientProfile.FullName ),
                            new KeyValuePair<string, string> ( "SubViewName", "PatientEditorView" ),
                            new KeyValuePair<string, string> ( "IsCreate", true.ToString () )
                        } );
            }
        }

        private void HandleGetPatientsByAdvancedSearchCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPatientsByAdvancedSearchResponse> ();
            var result = response.PagedPatientSearchResultDto;

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

            ( AddNewCommand as DelegateCommandBase ).RaiseCanExecuteChanged ();
            IsLoading = false;
        }

        private void HandleGetPatientsByKeywordsCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPatientsByKeywordsResponse> ();

            if ( response.SearchCriteria == _quickSearchCriteria )
            {
                var result = response.PagedPatientSearchResultDto;

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

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            AddNewState = AddNewState.Error;
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Request Failed", UserDialogServiceOptions.Ok );
        }

        private void HandleSearchPatientsException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Search Failed", UserDialogServiceOptions.Ok );
        }

        private void LookupValueRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            IDictionary<string, ObservableCollection<LookupValueDto>> localLookupValueLists =
                new Dictionary<string, ObservableCollection<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                var lookupValueDtos = new ObservableCollection<LookupValueDto> ( response.LookupValues );

                if ( response.Name == "PatientGender" )
                {
                    var genderList = from item in lookupValueDtos orderby item.WellKnownName select item;
                    localLookupValueLists.Add ( response.Name, new ObservableCollection<LookupValueDto> ( genderList ) );
                }
                else
                {
                    localLookupValueLists.Add ( response.Name, lookupValueDtos );
                }
            }

            LookupValueLists = localLookupValueLists;
            IsLoading = false;
        }

        #endregion
    }
}
