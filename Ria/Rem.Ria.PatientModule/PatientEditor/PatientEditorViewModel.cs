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
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientEditor;
using Rem.Ria.PatientModule.Web.PatientSearch;

namespace Rem.Ria.PatientModule.PatientEditor
{
    /// <summary>
    /// View Model for editing patient.
    /// </summary>
    public class PatientEditorViewModel : PanelEditorViewModel<PatientDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly Dictionary<bool, string> _choices = new Dictionary<bool, string> { { true, "Yes" }, { false, "No" } };
        private readonly IEventAggregator _eventAggregator;
        private readonly IPatientAccessService _patientAccessService;
        private readonly IPopupService _popupService;
        private readonly IUserDialogService _userDialogService;
        private bool _isCreateMode;
        private IDictionary<string, IList<LookupValueDto>> _lookupValueLists;
        private long _patientKey;
        private bool _removePatientContactLoading;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="patientAccessService">The patient access service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientEditorViewModel (
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IPatientAccessService patientAccessService,
            IPopupService popupService,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _patientAccessService = patientAccessService;
            _popupService = popupService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            AddPatientContactCommand = commandFactoryHelper.BuildDelegateCommand ( () => AddPatientContactCommand, ExecuteAddPatientContactCommand );
            EditPatientContactCommand = commandFactoryHelper.BuildDelegateCommand<PatientContactDto> (
                () => EditPatientContactCommand, ExecuteEditPatientContactCommand );
            RemovePatientContactCommand = commandFactoryHelper.BuildDelegateCommand<PatientContactDto> (
                () => RemovePatientContactCommand, ExecuteRemovePatientContactCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add patient contact command.
        /// </summary>
        public ICommand AddPatientContactCommand { get; private set; }

        /// <summary>
        /// Gets the choices.
        /// </summary>
        public Dictionary<bool, string> Choices
        {
            get { return _choices; }
        }

        /// <summary>
        /// Gets the edit patient contact command.
        /// </summary>
        public ICommand EditPatientContactCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is create mode.
        /// </summary>
        /// <value><c>true</c> if this instance is create mode; otherwise, <c>false</c>.</value>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
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
        /// Gets the remove patient contact command.
        /// </summary>
        public ICommand RemovePatientContactCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove patient contact loading].
        /// </summary>
        /// <value><c>true</c> if [remove patient contact loading]; otherwise, <c>false</c>.</value>
        public bool RemovePatientContactLoading
        {
            get { return _removePatientContactLoading; }
            set { ApplyPropertyChange ( ref _removePatientContactLoading, () => RemovePatientContactLoading, value ); }
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
            _patientKey = parameters.GetValue<long> ( "PatientKey" );
            IsCreateMode = parameters.GetValue<bool> ( "IsCreate" );

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetPatientByKeyRequest { Key = _patientKey } );
            requestDispatcher.AddLookupValuesRequest ( "PatientGender" );
            requestDispatcher.AddLookupValuesRequest ( "StateProvince" );
            requestDispatcher.AddLookupValuesRequest ( "CountyArea" );
            requestDispatcher.AddLookupValuesRequest ( "GeographicalRegion" );
            requestDispatcher.AddLookupValuesRequest ( "PatientAliasType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientPhoneType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientAddressType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientIdentifierType" );
            requestDispatcher.AddLookupValuesRequest ( "Race" );
            requestDispatcher.AddLookupValuesRequest ( "Disability" );
            requestDispatcher.AddLookupValuesRequest ( "SpecialNeed" );
            requestDispatcher.AddLookupValuesRequest ( "ImmigrationStatus" );
            requestDispatcher.AddLookupValuesRequest ( "CustodialStatus" );
            requestDispatcher.AddLookupValuesRequest ( "AllergyType" );
            requestDispatcher.AddLookupValuesRequest ( "AllergyStatus" );
            requestDispatcher.AddLookupValuesRequest ( "SmokingStatus" );
            requestDispatcher.AddLookupValuesRequest ( "Reaction" );
            requestDispatcher.AddLookupValuesRequest ( "ContactPreference" );
            requestDispatcher.AddLookupValuesRequest ( "VeteranStatus" );
            requestDispatcher.AddLookupValuesRequest ( "VeteranDischargeStatus" );
            requestDispatcher.AddLookupValuesRequest ( "VeteranServiceBranch" );

            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleInitializationCompleted, HandleInitializationException );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );
            RaisePatientChangedEvent ();
        }

        private void ExecuteAddPatientContactCommand ()
        {
            _popupService.ShowPopup (
                "PatientContactEditorView",
                "Create",
                null,
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) },
                true,
                RefreshPatientContacts );
        }

        private void ExecuteEditPatientContactCommand ( PatientContactDto patientContactDto )
        {
            _popupService.ShowPopup (
                "PatientContactEditorView",
                "Edit",
                null,
                new[] { new KeyValuePair<string, string> ( "PatientContactKey", patientContactDto.Key.ToString () ) },
                true,
                RefreshPatientContacts );
        }

        private void ExecuteHaveServedInMilitaryIndicatorChangedCommand ( object value )
        {
            if ( value != null )
            {
                var haveServedInMilitaryIndicator = ( bool )value;
                if ( !haveServedInMilitaryIndicator )
                {
                    EditingDto.VeteranInformation.DisabilityDescription = null;
                    EditingDto.VeteranInformation.DisabilityPercentageValue = null;
                    EditingDto.VeteranInformation.HaveCombatHistoryIndicator = null;
                    EditingDto.VeteranInformation.RegisteredVaHospitalName = null;
                    EditingDto.VeteranInformation.ServiceEndDate = null;
                    EditingDto.VeteranInformation.ServiceStartDate = null;
                    EditingDto.VeteranInformation.VaCaseNumber = null;
                    EditingDto.VeteranInformation.VeteranDischargeStatus = null;
                    EditingDto.VeteranInformation.VeteranServiceBranch = null;
                    EditingDto.VeteranInformation.VeteranStatus = null;
                }
            }
        }

        private void ExecuteRemovePatientContactCommand ( PatientContactDto patientContactDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new RemovePatientContactRequest { PatientContactKey = patientContactDto.Key, PatientKey = _patientKey } );
            requestDispatcher.ProcessRequests ( HandleRemovePatientContactComplete, HandleRemovePatientContactException );
            RemovePatientContactLoading = true;
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;

            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetPatientByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPatientByKeyResponse> ();

            if ( EditingDto != null && EditingDto.VeteranInformation != null )
            {
                EditingDto.VeteranInformation.PropertyChanged -= HandlePropertyChanged;
            }
            EditingDto = response.PatientDto;
            if ( EditingDto != null && EditingDto.VeteranInformation != null )
            {
                EditingDto.VeteranInformation.PropertyChanged += HandlePropertyChanged;
            }
        }

        private void HandleInitializationCompleted ( ReceivedResponses receivedResponses )
        {
            GetPatientByKeyCompleted ( receivedResponses );
            GetLookupValuesCompleted ( receivedResponses );
            LogPatientAccess ();
            IsLoading = false;
        }

        private void HandleInitializationException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Patient editor initialization failed.", UserDialogServiceOptions.Ok );
        }

        private void HandlePropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => EditingDto.VeteranInformation.HaveServedInMilitaryIndicator ) )
            {
                ExecuteHaveServedInMilitaryIndicatorChangedCommand ( EditingDto.VeteranInformation.HaveServedInMilitaryIndicator );
            }
        }

        private void HandleRemovePatientContactComplete ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PatientContactKeyDto>> ();
            if ( response.DataTransferObject.HasErrors )
            {
                var errorBuilder = new StringBuilder ();
                foreach ( var error in response.DataTransferObject.DataErrorInfoCollection )
                {
                    errorBuilder.Append ( error.Message + "\n" );
                }
                HandleRemovePatientContactException ( new ExceptionInfo { Message = errorBuilder.ToString () } );
            }
            else
            {
                var contact = EditingDto.PatientContacts.Contacts.FirstOrDefault ( pc => pc.Key == response.DataTransferObject.Key );
                if ( contact != null )
                {
                    EditingDto.PatientContacts.Contacts.Remove ( contact );
                }
            }
            RemovePatientContactLoading = false;
        }

        private void HandleRemovePatientContactException ( ExceptionInfo exceptionInfo )
        {
            RemovePatientContactLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Removing Patient Contact Failed", UserDialogServiceOptions.Ok );
        }

        private void LogPatientAccess ()
        {
            _patientAccessService.LogEventAccess ( _patientKey, "Patient Profile", "The Patient Profile for Patient {0} was accessed" );
        }

        private void RaisePatientChangedEvent ()
        {
            _eventAggregator.GetEvent<PatientChangedEvent> ().Publish ( new PatientChangedEventArgs { Sender = this, PatientKey = EditingDto.Key } );
            if ( EditingDto != null && EditingDto.VeteranInformation != null )
            {
                EditingDto.VeteranInformation.PropertyChanged -= HandlePropertyChanged;
                EditingDto.VeteranInformation.PropertyChanged += HandlePropertyChanged;
            }
        }

        private void RefreshPatientContacts ()
        {
            CancelCommand.Execute ( EditingDto.PatientContacts );
        }

        #endregion
    }
}
