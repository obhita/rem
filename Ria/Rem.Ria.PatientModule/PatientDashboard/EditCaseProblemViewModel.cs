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
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.ClinicalCaseModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for EditCaseProblem class.
    /// </summary>
    public class EditCaseProblemViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private static readonly string ProblemStatusLookupName = "ProblemStatus";
        private static readonly string ProblemTypeLookupName = "ProblemType";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly PagedCollectionViewWrapper<ProblemDto> _pagedCollectionViewWrapper;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;

        private long _clinicalCaseKey;
        private EditableDtoWrapper _editableWrapper;
        private bool _isCreate;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;

        private ProblemDto _problem;
        private ProblemDto _selectedProblemCode;
        private LookupValueDto _selectedProblemType;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCaseProblemViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public EditCaseProblemViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _popupService = popupService;

            Wrapper = new EditableDtoWrapper ();
            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<ProblemDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveProblemCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> ( () => SaveProblemCommand, ExecuteSaveProblem );

            DtsSearchSelectionChangedCommand = commandFactoryHelper.BuildDelegateCommand<ProblemDto> (
                () => DtsSearchSelectionChangedCommand, ExecuteDtsSearchSelectionChangedCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the DTS search selection changed command.
        /// </summary>
        public ICommand DtsSearchSelectionChangedCommand { get; private set; }

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
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<ProblemDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets or sets the problem.
        /// </summary>
        /// <value>The problem.</value>
        public ProblemDto Problem
        {
            get { return _problem; }
            set
            {
                _problem = value;
                RaisePropertyChanged ( () => Problem );
                Wrapper.EditableDto = Problem;
            }
        }

        /// <summary>
        /// Gets the save problem command.
        /// </summary>
        public ICommand SaveProblemCommand { get; private set; }

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public LookupValueDto Status { get; set; }

        /// <summary>
        /// Gets or sets the wrapper.
        /// </summary>
        /// <value>The wrapper.</value>
        public EditableDtoWrapper Wrapper
        {
            get { return _editableWrapper; }
            set
            {
                _editableWrapper = value;
                RaisePropertyChanged ( () => Wrapper );
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the new problem status.
        /// </summary>
        /// <value>The new problem status.</value>
        protected LookupValueDto NewProblemStatus { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "ProblemKey" );
            return Problem == null || key == Problem.Key;
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
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected override void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "ProblemKey" );
            _clinicalCaseKey = parameters.GetValue<long> ( "ClinicalCaseKey" );
            var dispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            if ( key == 0 )
            {
                Problem = new ProblemDto { Key = key };
                _isCreate = true;

                dispatcher.Add ( new GetDtoRequest<StaffSummaryDto> { Key = CurrentUserContext.Staff.Key } );
                Problem.ClinicalCaseKey = _clinicalCaseKey;
                Problem.StatusChangedDate = DateTime.Now;
            }
            else
            {
                dispatcher.Add ( new GetProblemByKeyRequest { Key = key } );
            }
            dispatcher
                .AddLookupValuesRequest ( ProblemTypeLookupName )
                .AddLookupValuesRequest ( ProblemStatusLookupName );
            dispatcher.ProcessRequests ( HandleRequestComplete, HandleRequestDispatcherException );
            IsLoading = true;
        }

        private void ExecuteDtsSearchSelectionChangedCommand ( ProblemDto obj )
        {
            Problem.ProblemCodeCodedConcept = obj.ProblemCodeCodedConcept;
        }

        private void ExecuteSaveProblem ( ProblemDto problemDto )
        {
            SaveProblem ( Problem );
        }

        private void GetProblemByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            if ( receivedResponses.HasResponse<GetProblemByKeyResponse> () )
            {
                IsLoading = false;
                var response = receivedResponses.Get<GetProblemByKeyResponse> ();
                Problem = response.Problem;
            }
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

        private void HandleRequestComplete ( ReceivedResponses receivedResponses )
        {
            HandleGetLookupValuesCompleted ( receivedResponses );
            if ( !_isCreate )
            {
                GetProblemByKeyCompleted ( receivedResponses );
            }
            else
            {
                //TODO: The following code is for current requirement
                var problemTypeList = LookupValueLists[ProblemTypeLookupName];
                if ( problemTypeList != null && problemTypeList.Count >= 0 )
                {
                    Problem.ProblemType = problemTypeList.First ( p => p.WellKnownName == ProblemType.Diagnosis );
                }

                var problemStatusList = LookupValueLists[ProblemStatusLookupName];
                if ( problemStatusList != null )
                {
                    Problem.ProblemStatus = problemStatusList.First ( p => p.WellKnownName == ProblemStatus.Active );
                }

                var staffDto = receivedResponses.Get<DtoResponse<StaffSummaryDto>> ();
                Problem.ObservedByStaff = staffDto.DataTransferObject;
            }
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Problem operation failed.", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleSaveProblemCompleted ( ReceivedResponses receivedResponse )
        {
            var response = receivedResponse.Get<DtoResponse<ProblemDto>> ();
            var dto = response.DataTransferObject;
            IsLoading = false;
            if ( dto.HasErrors )
            {
                var message = string.Empty;
                foreach ( var dataErrorInfo in dto.DataErrorInfoCollection )
                {
                    message += dataErrorInfo.ErrorLevel + ": " + dataErrorInfo.Message + Environment.NewLine;
                }

                _userDialogService.ShowDialog ( message, "Could not save", UserDialogServiceOptions.Ok );
            }
            else
            {
                _popupService.ClosePopup ( "EditCaseProblemView" );
            }
        }

        private void HandleSaveProblemException ( ExceptionInfo ex )
        {
            IsLoading = false;

            // TODO: Need to implement an exception handling mechanism.
            //       For now we'll simply use a messagebox
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void SaveProblem ( ProblemDto dto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<ProblemDto> { DataTransferObject = dto } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleSaveProblemCompleted, HandleSaveProblemException );
        }

        #endregion
    }
}
