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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Domain.Primitives;
using Pillar.FluentRuleEngine;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View Model for EditPayorCoverage class.
    /// </summary>
    public class EditPayorCoverageViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private Dictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private PayorCoverageCacheDto _payorCoverage;
        private NotifyPropertyChangedRuleExecutor<EditPayorCoverageViewModel, IDataTransferObject> _ruleExecutor;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditPayorCoverageViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        public EditPayorCoverageViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IPopupService popupService )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;

            CreateCommand = NavigationCommandManager.BuildCommand ( () => CreateCommand, OnNavigateToCreateCommand );
            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, OnNavigateToEditCommand );

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveCommand = commandFactoryHelper.BuildDelegateCommand(() => SaveCommand, ExecuteSaveCommand);

            _ruleExecutor = new NotifyPropertyChangedRuleExecutor<EditPayorCoverageViewModel, IDataTransferObject>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the create command.
        /// </summary>
        public INavigationCommand CreateCommand { get; private set; }

        /// <summary>
        /// Gets the edit command.
        /// </summary>
        public INavigationCommand EditCommand { get; private set; }

        /// <summary>
        /// Gets or sets the lookup value lists.
        /// </summary>
        /// <value>The lookup value lists.</value>
        public Dictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
        }

        /// <summary>
        /// Gets or sets the payor coverage.
        /// </summary>
        /// <value>The payor coverage.</value>
        public PayorCoverageCacheDto PayorCoverage
        {
            get { return _payorCoverage; }
            set { ApplyPropertyChange ( ref _payorCoverage, () => PayorCoverage, value ); }
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets or sets the patient summary.
        /// </summary>
        /// <value>The patient summary.</value>
        public PatientSummaryDto PatientSummary { get; set; }

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

        private void AddLookupRequests ( IAsyncRequestDispatcher requestDispatcher )
        {
            requestDispatcher.AddLookupValuesRequest ( "PayorCoverageCacheType" );
            requestDispatcher.AddLookupValuesRequest ( "PayorSubscriberRelationshipCacheType" );
            requestDispatcher.AddLookupValuesRequest ( "StateProvince" );
            requestDispatcher.AddLookupValuesRequest ( "AdministrativeGender" );
        }

        private void ExecuteSaveCommand()
        {
            if ( PayorCoverage.EditStatus == EditStatus.Noop && PayorCoverage.PayorSubscriberCache.EditStatus == EditStatus.Update )
            {
                PayorCoverage.EditStatus = EditStatus.Update;
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add ( new SaveDtoRequest<PayorCoverageCacheDto> { DataTransferObject = PayorCoverage } );
            requestDispatcher.ProcessRequests ( HandleSavePayorCovergeCompleted, HandleSavePayorCovergeError );
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                            where typeof(GetLookupValuesResponse).IsAssignableFrom(response.GetType())
                            select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse>().ToDictionary(
                response => response.Name, response => response.LookupValues);

            if ( PayorCoverage == null )
            {
                var response = receivedResponses.Get<DtoResponse<PayorCoverageCacheDto>> ();
                PayorCoverage = response.DataTransferObject;

                _ruleExecutor.WatchSubject(this);
            }

            PatientSummary = receivedResponses.Get<DtoResponse<PatientSummaryDto>> ().DataTransferObject;

            IsLoading = false;
        }

        private void HandleInitializationError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Initializing", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleSavePayorCovergeCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PayorCoverageCacheDto>> ();
            PayorCoverage = response.DataTransferObject;

            IsLoading = false;

            if ( !PayorCoverage.HasErrors )
            {
                _popupService.ClosePopup ( "EditPayorCoverageView" );
            }
            else
            {
                if(PayorCoverage.Key == 0)
                {
                  PayorCoverage.EditStatus = EditStatus.Create;  
                }

                var errorMessageBuilder = new StringBuilder ();
                foreach ( var dataErrorInfo in response.DataTransferObject.DataErrorInfoCollection.Where ( e => e.DataErrorInfoType == DataErrorInfoType.ObjectLevel ) )
                {
                    errorMessageBuilder.Append ( dataErrorInfo.Message + "\n" );
                }
                var message = errorMessageBuilder.ToString ();
                if (!string.IsNullOrWhiteSpace(message))
                {
                    _userDialogService.ShowDialog ( message, "Error Saving Payor", UserDialogServiceOptions.Ok );
                }
            }
        }

        private void HandleSavePayorCovergeError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Saving", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void OnNavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            var patientKey = parameters.GetValue<long> ( "PatientKey" );

            if(PayorCoverage != null)
            {
                _ruleExecutor.StopWatchingSubject ( this );
            }

            PayorCoverage = new PayorCoverageCacheDto ();
            PayorCoverage.PatientKey = patientKey;

            _ruleExecutor.WatchSubject ( this );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetDtoRequest<PatientSummaryDto> { Key = patientKey } );
            AddLookupRequests ( requestDispatcher );
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationError );
        }

        private void OnNavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var payorCoverageKey = parameters.GetValue<long> ( "PayorCoverageKey" );
            var patientKey = parameters.GetValue<long>("PatientKey");

            if (PayorCoverage != null)
            {
                _ruleExecutor.StopWatchingSubject(this);
            }

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add ( new GetDtoRequest<PayorCoverageCacheDto> { Key = payorCoverageKey } );
            requestDispatcher.Add ( new GetDtoRequest<PatientSummaryDto> { Key = patientKey } );
            AddLookupRequests ( requestDispatcher );
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationError );
        }

        #endregion
    }
}
