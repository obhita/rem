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
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.ClinicianDashboard;

namespace Rem.Ria.PatientModule.ClinicianDashboard
{
    /// <summary>
    /// View Model for ClinicianPatientList class.
    /// </summary>
    public class ClinicianPatientListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly INavigationService _navigationService;
        private readonly IUserDialogService _userDialogService;
        private IEnumerable<ClinicianPatientDto> _patientList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicianPatientListViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public ClinicianPatientListViewModel (
            IAccessControlManager accessControlManager,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            GoToPatientCommand = commandFactoryHelper.BuildDelegateCommand<ClinicianPatientDto> (
                () => GoToPatientCommand, ExecuteGoToPatientCommand );

            ApplyContextChanges = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the go to patient command.
        /// </summary>
        public ICommand GoToPatientCommand { get; private set; }

        /// <summary>
        /// Gets or sets the patient list.
        /// </summary>
        /// <value>The patient list.</value>
        public IEnumerable<ClinicianPatientDto> PatientList
        {
            get { return _patientList; }
            set { ApplyPropertyChange ( ref _patientList, () => PatientList, value ); }
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
            var clinicianKey = CurrentUserContext.Staff.Key;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetClinicianPatientsRequest { ClinicianKey = clinicianKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleRequestCompleted, HandleRequestException );
        }

        private void ExecuteGoToPatientCommand ( ClinicianPatientDto clinicianPatientDto )
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "PatientWorkspaceView",
                "ViewPatient",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", clinicianPatientDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "FullName", clinicianPatientDto.FirstName + " " + clinicianPatientDto.LastName ),
                        new KeyValuePair<string, string> ( "SubViewName", "PatientDashboardView" ),
                    } );
        }

        private void HandleRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetClinicianPatientsResponse> ();
            PatientList = response.PatientList;
            IsLoading = false;
        }

        private void HandleRequestException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Geting Patient List", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
