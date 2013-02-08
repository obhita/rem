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
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Service;
using Rem.Ria.PatientModule.Web.ClinicalCaseEditor;

namespace Rem.Ria.PatientModule.ClinicalCaseEditor
{
    /// <summary>
    /// View Model for editing clinical case.
    /// </summary>
    public class ClinicalCaseEditorViewModel : PanelEditorViewModel<ClinicalCaseDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IPatientAccessService _patientAccessService;
        private readonly IUserDialogService _userDialogService;

        private long _clinicalCaseKey;
        private StaffNameDto _currentStaffName;
        private bool _isCreateMode;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalCaseEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="patientAccessService">The patient access service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ClinicalCaseEditorViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPatientAccessService patientAccessService,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _patientAccessService = patientAccessService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SignCommentCommand = commandFactoryHelper.BuildDelegateCommand<string> (
                () => SignCommentCommand, ExecuteSignCommentCommand, CanExecuteSignCommentCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether this instance is create mode.
        /// </summary>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            private set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
        }

        /// <summary>
        /// Gets the lookup value lists.
        /// </summary>
        public IDictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            private set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets the sign comment command.
        /// </summary>
        public ICommand SignCommentCommand { get; private set; }

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
            IsCreateMode = parameters.GetValue<bool> ( "IsCreateMode" );
            _currentStaffName = new StaffNameDto
                {
                    FirstName = CurrentUserContext.Staff.FirstName,
                    LastName = CurrentUserContext.Staff.LastName,
                    MiddleInitial = CurrentUserContext.Staff.MiddleName == null ? null : CurrentUserContext.Staff.MiddleName.Substring ( 0, 1 ),
                    Key = CurrentUserContext.Staff.Key
                };

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<ClinicalCaseDto> { Key = _clinicalCaseKey } );
            requestDispatcher.AddLookupValuesRequest ( "InitialContactMethod" );
            requestDispatcher.AddLookupValuesRequest ( "ReferralType" );
            requestDispatcher.AddLookupValuesRequest ( "SpecialInitiative" );
            requestDispatcher.AddLookupValuesRequest ( "PriorityPopulation" );
            requestDispatcher.AddLookupValuesRequest ( "DischargeReason" );
            requestDispatcher.AddLookupValuesRequest ( "ClinicalCaseStatus" );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        private static bool CanExecuteSignCommentCommand ( string comment )
        {
            return !string.IsNullOrEmpty ( comment );
        }

        private void ExecuteSignCommentCommand ( string comment )
        {
            var signedCommentDto = new ClinicalCaseSignedCommentDto
                {
                    Staff = _currentStaffName,
                    SignedTimestamp = DateTime.Now,
                    SignedNote = comment
                };
            EditingDto.ClinicalCaseProfile.SignedComments.Add ( signedCommentDto );
        }

        private void GetClinicalCaseByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<ClinicalCaseDto>> ();
            EditingDto = response.DataTransferObject;
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
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
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetClinicalCaseByKeyCompleted ( receivedResponses );
            GetLookupValuesCompleted ( receivedResponses );
            LogPatientAccess ();
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Clinical Case editor initialization failed.", UserDialogServiceOptions.Ok );
        }

        private void LogPatientAccess ()
        {
            _patientAccessService.LogEventAccess ( _patientKey, "Clinical Case Editor", "The Clinical Case Editor for Patient {0} was accessed" );
        }

        #endregion
    }
}
