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
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Context;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.ScheduleView;

namespace Rem.Ria.PatientModule.PatientSearch
{
    /// <summary>
    /// View Model for MainPatientSearch class.
    /// </summary>
    public class MainPatientSearchViewModel : PatientSearchViewModel
    {
        #region Constants and Fields

        private const string PatientDashboardView = "PatientDashboardView";
        private const string PatientEditorView = "PatientEditorView";
        private const string WorkspacesRegion = "WorkspacesRegion";
        private const string PatientWorkspaceView = "PatientWorkspaceView";

        private readonly IUserDialogService _userDialogService;
        private readonly INavigationService _navigationService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPatientSearchViewModel"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public MainPatientSearchViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base (
                userDialogService,
                asyncRequestDispatcherFactory,
                accessControlManager,
                navigationService,
                commandFactory
                )
        {
            _userDialogService = userDialogService;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            DragStartingCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => DragStartingCommand, ExecuteDragStartingCommand );
            ViewDashboardCommand = commandFactoryHelper.BuildDelegateCommand<PatientSearchResultDto> (
                () => ViewDashboardCommand, ExecuteViewDashboard );
            ViewProfileCommand = commandFactoryHelper.BuildDelegateCommand<PatientSearchResultDto> ( () => ViewProfileCommand, ExecuteViewProfile );
            ApplyPaymentCommand = commandFactoryHelper.BuildDelegateCommand<PatientSearchResultDto> ( () => ApplyPaymentCommand, ExecuteApplyPayment );
            CreateNewAppointmentCommand = commandFactoryHelper.BuildDelegateCommand<PatientSearchResultDto> (
                () => CreateNewAppointmentCommand, ExecuteCreateNewAppointment );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the apply payment command.
        /// </summary>
        public ICommand ApplyPaymentCommand { get; private set; }

        /// <summary>
        /// Gets the create new appointment command.
        /// </summary>
        public ICommand CreateNewAppointmentCommand { get; private set; }

        /// <summary>
        /// Gets the drag starting command.
        /// </summary>
        public ICommand DragStartingCommand { get; private set; }

        /// <summary>
        /// Gets the view dashboard command.
        /// </summary>
        public ICommand ViewDashboardCommand { get; private set; }

        /// <summary>
        /// Gets the view profile command.
        /// </summary>
        public ICommand ViewProfileCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns><see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.</returns>
        public override bool IsNavigationTarget ( NavigationContext navigationContext )
        {
            return true;
        }

        #endregion

        #region Methods

        private void ExecuteApplyPayment ( PatientSearchResultDto patientQuickSearchResultDto )
        {
            _navigationService.Navigate (
                WorkspacesRegion,
                "FrontDeskDashboardView",
                "MakePayment",
                new KeyValuePair<string, string> ( "PatientKey", patientQuickSearchResultDto.Key.ToString () ),
                new KeyValuePair<string, string> (
                    "PatientFullName", string.Format ( "{0} {1}", patientQuickSearchResultDto.FirstName, patientQuickSearchResultDto.LastName ) ) );
        }

        private void ExecuteCreateNewAppointment ( PatientSearchResultDto patientQuickSearchResultDto )
        {
            _navigationService.NavigateToActiveView (
                WorkspacesRegion,
                "CreateAppointment",
                new[]
                    {
                        new KeyValuePair<string, string> ( "PatientKey", patientQuickSearchResultDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "PatientFirstName", patientQuickSearchResultDto.FirstName ),
                        new KeyValuePair<string, string> ( "PatientLastName", patientQuickSearchResultDto.LastName )
                    } );
        }

        private void ExecuteDragStartingCommand ( object obj )
        {
            var args = obj as DragDropQueryEventArgs;
            if ( args != null )
            {
                var result = args.Options.Payload as PatientSearchResultDto;
                if ( result != null )
                {
                    var appointmentDto = new ClinicianAppointmentDto ();
                    appointmentDto.PatientFirstName = result.FirstName;
                    appointmentDto.PatientLastName = result.LastName;
                    appointmentDto.PatientKey = result.Key;
                    appointmentDto.AppointmentStartDateTime = DateTime.Now;
                    appointmentDto.AppointmentEndDateTime = DateTime.Now.AddHours ( 1 );
                    var payload = new ScheduleViewDragDropPayload ( null, new List<IOccurrence> { appointmentDto } );
                    args.Options.Payload = payload;
                }
            }
        }

        private void ExecuteViewDashboard ( PatientSearchResultDto patientQuickSearchResultDto )
        {
            if ( patientQuickSearchResultDto != null )
            {
                _navigationService.Navigate (
                    WorkspacesRegion,
                    PatientWorkspaceView,
                    "SubViewPassThrough",
                    new[]
                        {
                            new KeyValuePair<string, string> ( "PatientKey", patientQuickSearchResultDto.Key.ToString () ),
                            new KeyValuePair<string, string> ( "FullName", patientQuickSearchResultDto.FullName ),
                            new KeyValuePair<string, string> ( "SubViewName", PatientDashboardView )
                        } );
            }
            else
            {
                _userDialogService.ShowDialog (
                    "No patient is selected, couldn't view dashboard.",
                    "Viewing Dashboard Failed",
                    UserDialogServiceOptions.Ok );
            }
        }

        private void ExecuteViewProfile ( PatientSearchResultDto patientQuickSearchResultDto )
        {
            if ( patientQuickSearchResultDto != null )
            {
                _navigationService.Navigate (
                    WorkspacesRegion,
                    PatientWorkspaceView,
                    "ViewPatient",
                    new[]
                        {
                            new KeyValuePair<string, string> ( "PatientKey", patientQuickSearchResultDto.Key.ToString () ),
                            new KeyValuePair<string, string> ( "FullName", patientQuickSearchResultDto.FullName ),
                            new KeyValuePair<string, string> ( "SubViewName", PatientEditorView )
                        } );
            }
            else
            {
                _userDialogService.ShowDialog (
                    "No patient is selected, couldn't view profile.",
                    "Viewing Profile Failed",
                    UserDialogServiceOptions.Ok );
            }
        }

        #endregion
    }
}
