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
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames;
using Rem.WellKnownNames.ClinicalCaseModule;
using Rem.WellKnownNames.PatientModule;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for CaseProblemList class.
    /// </summary>
    public class CaseProblemListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly string ProblemStatusLookupName = "ProblemStatus";
        private static readonly string ProblemTypeLookupName = "ProblemType";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly Predicate<object> _filter;

        private readonly PagedCollectionViewWrapper<ProblemDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private long _clinicalCaseKey;
        private long _patientKey;
        private IList<ProblemDto> _problemList;
        private ProblemDto _selectedProblemCode;
        private LookupValueDto _selectedProblemType;
        private ShowOption _showOption;
        private ObservableCollection<StaffSummaryDto> _staffList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseProblemListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public CaseProblemListViewModel ( IAccessControlManager accessControlManager, ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseProblemListViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public CaseProblemListViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _popupService = popupService;
            _showOption = ShowOption.ShowActive;
            _filter = FilterByActiveStatus;

            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            IsLoading = true;
            requestDispatcher
                .AddLookupValuesRequest ( ProblemTypeLookupName )
                .AddLookupValuesRequest ( ProblemStatusLookupName )
                .ProcessRequests ( HandleGetLookupValuesCompleted, HandleGetLookupValuesException );

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProblemDto> ();

            InitializeGroupingDescriptions ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            ShowAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            ShowActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );
            DeleteProblemCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> (
                () => DeleteProblemCommand, ExecuteDeleteProblem, CanExecuteDeleteProblem );

            _eventAggregator.GetEvent<ClinicalCaseChangedEvent> ().Subscribe (
                ClinicalCaseChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterClinicalCaseChangedEvents );

            EditProblemCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> ( () => EditProblemCommand, ExecuteEditProblemCommand );
            DragQueryCommand = commandFactoryHelper.BuildDelegateCommand<DragDropQueryEventArgs> ( () => DragQueryCommand, ExecuteDragQueryCommand );
            GenerateHL7ProblemCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> (
                () => GenerateHL7ProblemCommand, ExecuteGenerateHL7ProblemCommand );
        }

        #endregion

        #region Enums

        /// <summary>
        /// Show options
        /// </summary>
        private enum ShowOption
        {
            /// <summary>
            /// Show all option
            /// </summary>
            ShowAll,

            /// <summary>
            /// Show active option
            /// </summary>
            ShowActive
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the delete problem command.
        /// </summary>
        public ICommand DeleteProblemCommand { get; private set; }

        /// <summary>
        /// Gets the drag query command.
        /// </summary>
        public ICommand DragQueryCommand { get; private set; }

        /// <summary>
        /// Gets the edit problem command.
        /// </summary>
        public ICommand EditProblemCommand { get; private set; }

        /// <summary>
        /// Gets the generate H l7 problem command.
        /// </summary>
        public ICommand GenerateHL7ProblemCommand { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has selected problem code.
        /// </summary>
        public bool HasSelectedProblemCode
        {
            get { return SelectedProblemCode != null; }
        }

        /// <summary>
        /// Gets or sets the lookup value lists.
        /// </summary>
        /// <value>The lookup value lists.</value>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists { get; set; }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProblemDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets or sets the selected problem code.
        /// </summary>
        /// <value>The selected problem code.</value>
        public ProblemDto SelectedProblemCode
        {
            get { return _selectedProblemCode; }
            set
            {
                ApplyPropertyChange ( ref _selectedProblemCode, () => SelectedProblemCode, value );
                RaisePropertyChanged ( () => HasSelectedProblemCode );
            }
        }

        /// <summary>
        /// Gets or sets the selected problem dto.
        /// </summary>
        /// <value>The selected problem dto.</value>
        public ProblemDto SelectedProblemDto { get; set; }

        /// <summary>
        /// Gets or sets the type of the selected problem.
        /// </summary>
        /// <value>The type of the selected problem.</value>
        public LookupValueDto SelectedProblemType
        {
            get { return _selectedProblemType; }
            set { ApplyPropertyChange ( ref _selectedProblemType, () => SelectedProblemType, value ); }
        }

        /// <summary>
        /// Gets the show active only command.
        /// </summary>
        public ICommand ShowActiveOnlyCommand { get; private set; }

        /// <summary>
        /// Gets the show all command.
        /// </summary>
        public ICommand ShowAllCommand { get; private set; }

        /// <summary>
        /// Gets or sets the staff list.
        /// </summary>
        /// <value>The staff list.</value>
        public ObservableCollection<StaffSummaryDto> StaffList
        {
            get { return _staffList; }
            set
            {
                _staffList = value;
                RaisePropertyChanged ( () => StaffList );
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public LookupValueDto Status { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clinicals the case changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        public void ClinicalCaseChangedEventHandler ( ClinicalCaseChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded (
                () =>
                    {
                        _clinicalCaseKey = obj.ClinicalCaseKey;
                        GetAllProblemsByClinicalCase ( _clinicalCaseKey );
                    } );
        }

        /// <summary>
        /// Filters the clinical case changed events.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.ClinicalCaseChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterClinicalCaseChangedEvents ( ClinicalCaseChangedEventArgs args )
        {
            return args.PatientKey == _patientKey && args.ClinicalCaseKey != _clinicalCaseKey;
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
            _clinicalCaseKey = parameters.GetValue<long> ( "ClinicalCaseKey" );
            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            GetAllProblemsByClinicalCase ( _clinicalCaseKey );
        }

        private bool CanExecuteDeleteProblem ( ProblemDto arg )
        {
            if ( arg != null )
            {
                return ( arg.AssociatedIndicator.HasValue ? !arg.AssociatedIndicator.Value : true );
            }
            return false;
        }

        private void DeleteProblem ( ProblemDto dto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new DeleteProblemRequest { ProblemKey = dto.Key } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleDeleteProblemCompleted, HandleDeleteProblemException );
        }

        private void ExecuteDeleteProblem ( ProblemDto problemDto )
        {
            DeleteProblem ( problemDto );
        }

        private void ExecuteDragQueryCommand ( DragDropQueryEventArgs obj )
        {
            var dragItem = obj.Options.Payload as ProblemDto;
            if ( dragItem != null )
            {
                obj.QueryResult = dragItem.ProblemStatus.WellKnownName == ProblemStatus.Active;
            }
        }

        private void ExecuteEditProblemCommand ( ProblemDto problemDto )
        {
            _popupService.ShowPopup (
                "EditCaseProblemView",
                "Create",
                "Problem",
                new[]
                    {
                        new KeyValuePair<string, string> ( "ProblemKey", problemDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "ClinicalCaseKey", _clinicalCaseKey.ToString () )
                    },
                false,
                PopupClosed );
        }

        private void ExecuteGenerateHL7ProblemCommand ( ProblemDto problemDto )
        {
            var relativePath = string.Format (
                "../{0}?{1}={2}&{3}={4}",
                HttpHandlerPaths.PatientModuleHttpHandlerPath,
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.RequestName ),
                HttpUtility.UrlEncode ( HttpHandlerRequestNames.DownloadHl7SyndromicSurveillanceDocument ),
                HttpUtility.UrlEncode ( HttpHandlerQueryStrings.ProblemKey ),
                problemDto.Key );
            var uri = new Uri ( Application.Current.Host.Source, relativePath );
            HtmlPage.Window.Navigate ( uri, "_blank" );
        }

        private void ExecuteShowActiveOnly ()
        {
            if ( _showOption != ShowOption.ShowActive )
            {
                _showOption = ShowOption.ShowActive;

                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private void ExecuteShowAll ()
        {
            if ( _showOption != ShowOption.ShowAll )
            {
                _showOption = ShowOption.ShowAll;
                PagedCollectionViewWrapper.SetFilter ( _filter );
            }
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;

            var problemDto = obj as ProblemDto;

            if ( problemDto != null && _showOption == ShowOption.ShowActive )
            {
                if ( problemDto.ProblemStatus == null ||
                     problemDto.ProblemStatus.WellKnownName != ProblemStatus.Active )
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        private void GetAllProblemsByClinicalCase ( long clinicalCaseKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllProblemsByClinicalCaseRequest { ClinicalCaseKey = clinicalCaseKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAllProblemsByClinicalCaseCompleted, HandleGetAllProblemsByClinicalCaseException );
        }

        private void HandleDeleteProblemCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DeleteProblemResponse> ();
            _problemList = new List<ProblemDto> ( response.ProblemDtos );
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( _problemList, _filter );
            IsLoading = false;
        }

        private void HandleDeleteProblemException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not delete", UserDialogServiceOptions.Ok );
        }

        private void HandleGetAllProblemsByClinicalCaseCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllProblemsByClinicalCaseResponse> ();

            _problemList = new List<ProblemDto> ( response.ProblemDtos );
            PagedCollectionViewWrapper.WrapInPagedCollectionView ( _problemList, _filter );
            IsLoading = false;
        }

        private void HandleGetAllProblemsByClinicalCaseException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Could not get all problems by clinical case key", UserDialogServiceOptions.Ok );
        }

        private void HandleGetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var lookupValueLists = new Dictionary<string, IList<LookupValueDto>> ();

            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            foreach ( GetLookupValuesResponse response in responses )
            {
                lookupValueLists.Add ( response.Name, response.LookupValues );
            }

            LookupValueLists = lookupValueLists;

            RaisePropertyChanged ( () => LookupValueLists );
            IsLoading = false;
        }

        private void HandleGetLookupValuesException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog (
                "Could not retrieve the lookup values. Error: " + ex.Message,
                "An error has occurred",
                UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            PagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProblemDto, object> ( p => p.ProblemCodeCodedConcept ), "Problem Code" ) );
            PagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<ProblemDto, object> ( p => p.ProblemType ), "Problem Type" ) );
        }

        private void PopupClosed ()
        {
            GetAllProblemsByClinicalCase ( _clinicalCaseKey );
        }

        #endregion
    }
}
