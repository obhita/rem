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
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.Infrastructure.Web.Service;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.PatientContactEditor
{
    /// <summary>
    /// View Model for editing patient contact.
    /// </summary>
    public class PatientContactEditorViewModel : PanelEditorViewModel<PatientContactDto>
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;

        private bool _isCreateMode;

        private Dictionary<string, IList<LookupValueDto>> _lookupValueLists;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientContactEditorViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PatientContactEditorViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            EditCommand = NavigationCommandManager.BuildCommand ( () => EditCommand, NavigateToEditCommand );
            CreateCommand = NavigationCommandManager.BuildCommand ( () => CreateCommand, NavigateToCreateCommand );
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
        /// Gets or sets a value indicating whether this instance is create.
        /// </summary>
        /// <value><c>true</c> if this instance is create; otherwise, <c>false</c>.</value>
        public bool IsCreateMode
        {
            get { return _isCreateMode; }
            set { ApplyPropertyChange ( ref _isCreateMode, () => IsCreateMode, value ); }
        }

        /// <summary>
        /// Gets or sets the lookup value lists.
        /// </summary>
        /// <value>The lookup value lists.</value>
        public Dictionary<string, IList<LookupValueDto>> LookupValueLists
        {
            get { return _lookupValueLists; }
            set { ApplyPropertyChange ( ref _lookupValueLists, () => LookupValueLists, value ); }
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
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );
            if ( IsCreateMode )
            {
                EditingDto.ContactInformation.Key = EditingDto.Profile.Key;
            }
        }

        private void AddLookups ( IAsyncRequestDispatcher requestDispatcher )
        {
            requestDispatcher.AddLookupValuesRequest ( "PatientContactType" );
            requestDispatcher.AddLookupValuesRequest ( "LegalAuthorizationType" );
            requestDispatcher.AddLookupValuesRequest ( "PatientContactRelationshipType" );
            requestDispatcher.AddLookupValuesRequest ( "StateProvince" );
            requestDispatcher.AddLookupValuesRequest ( "Gender" );
            requestDispatcher.AddLookupValuesRequest ( "Country" );
            requestDispatcher.AddLookupValuesRequest ( "CountyArea" );
            requestDispatcher.AddLookupValuesRequest ( "PatientContactPhoneType" );
        }

        private void GetLookupValuesCompleted ( ReceivedResponses receivedResponses )
        {
            var responses = from response in receivedResponses.Responses
                                              where typeof( GetLookupValuesResponse ).IsAssignableFrom ( response.GetType () )
                                              select response;
            LookupValueLists = responses.Cast<GetLookupValuesResponse> ().ToDictionary (
                response => response.Name, response => response.LookupValues );
        }

        private void GetPatientContactByKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PatientContactDto>> ();
            EditingDto = response.DataTransferObject;
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Patient Contact Editor Initialization Failed.", UserDialogServiceOptions.Ok );
        }

        private void NavigateToCreateCommand ( KeyValuePair<string, string>[] parameters )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            EditingDto = new PatientContactDto ();
            EditingDto.Profile = new PatientContactProfileDto { PatientKey = patientKey };
            EditingDto.ContactInformation = new PatientContactContactInformationDto ();
            IsCreateMode = true;

            AddLookups ( requestDispatcher );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void NavigateToEditCommand ( KeyValuePair<string, string>[] parameters )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            var patientContactKey = parameters.GetValue<long> ( "PatientContactKey" );
            requestDispatcher.Add ( new GetDtoRequest<PatientContactDto> { Key = patientContactKey } );

            AddLookups ( requestDispatcher );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( NavigatedToRequestDispatcherCompleted, HandleRequestDispatcherException );
        }

        private void NavigatedToRequestDispatcherCompleted ( ReceivedResponses receivedResponses )
        {
            if ( !IsCreateMode )
            {
                GetPatientContactByKeyCompleted ( receivedResponses );
            }
            GetLookupValuesCompleted ( receivedResponses );
            IsLoading = false;
        }

        #endregion
    }
}
