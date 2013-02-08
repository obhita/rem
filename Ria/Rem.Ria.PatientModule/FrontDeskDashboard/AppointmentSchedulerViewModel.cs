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
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Common;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View Model for scheduling appointment.
    /// </summary>
    public class AppointmentSchedulerViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private const string TilesRegion = "TilesRegion";

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly ICommand _goToTodayCommand;
        private readonly INavigationService _navigationService;
        private readonly ICommand _nextDayCommand;
        private readonly ICommand _previousDayCommand;
        private readonly IUserDialogService _userDialogService;
        private DateTime _selectedDate;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentSchedulerViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public AppointmentSchedulerViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            _nextDayCommand = commandFactoryHelper.BuildDelegateCommand ( () => NextDayCommand, ExecuteNextDayCommand, CanExecuteNextDayCommand );
            _previousDayCommand = commandFactoryHelper.BuildDelegateCommand (
                () => PreviousDayCommand, ExecutePreviousDayCommand, CanExecutePreviousDayCommand );
            _goToTodayCommand = commandFactoryHelper.BuildDelegateCommand (
                () => GoToTodayCommand, ExecuteGoToTodayCommand, CanExecuteGoToTodayCommand );

            eventAggregator.GetEvent<FrontDeskDashboardDateChangedEvent> ().Subscribe (
                OnFrontDeskDashboardDateChanged, ThreadOption.PublisherThread, false, FilterFrontDeskDashboardDateChanged );

            _selectedDate = DateTime.Now.Date;

            ApplyContextChanges = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the go to today command.
        /// </summary>
        public ICommand GoToTodayCommand
        {
            get { return _goToTodayCommand; }
        }

        /// <summary>
        /// Gets the next day command.
        /// </summary>
        public ICommand NextDayCommand
        {
            get { return _nextDayCommand; }
        }

        /// <summary>
        /// Gets the previous day command.
        /// </summary>
        public ICommand PreviousDayCommand
        {
            get { return _previousDayCommand; }
        }

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>The selected date.</value>
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if ( _selectedDate != value )
                {
                    ApplyPropertyChange ( ref _selectedDate, () => SelectedDate, value );
                    ChangeDate ( SelectedDate );
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the front desk dashboard date changed.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.FrontDeskDashboardDateChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterFrontDeskDashboardDateChanged ( FrontDeskDashboardDateChangedEventArgs args )
        {
            return args.Source != this && args.LocationKey == CurrentUserContext.Location.Key;
        }

        /// <summary>
        /// Raises the <see cref="E:FrontDeskDashboardDateChanged"/> event.
        /// </summary>
        /// <param name="args">The <see cref="Rem.Ria.PatientModule.FrontDeskDashboard.FrontDeskDashboardDateChangedEventArgs"/> instance containing the event data.</param>
        public void OnFrontDeskDashboardDateChanged ( FrontDeskDashboardDateChangedEventArgs args )
        {
            //Need to do this so the Change Date event is not fired again.
            _selectedDate = args.Date;
            RaisePropertyChanged ( () => SelectedDate );
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
        protected override void NavigateToDefaultCommand(KeyValuePair<string, string>[] parameters)
        {
            var reload = parameters.GetValue<bool?> ( "ShouldReload" );
            if ( reload != false )
            {
                OnCurrentContextChanged ( this, new EventArgs () );
                ContextChanged += OnCurrentContextChanged;
            }
        }

        private bool CanExecuteGoToTodayCommand ()
        {
            return true;
        }

        private bool CanExecuteNextDayCommand ()
        {
            return true;
        }

        private bool CanExecutePreviousDayCommand ()
        {
            return true;
        }

        private void ChangeDate ( DateTime newDate )
        {
            _eventAggregator.GetEvent<FrontDeskDashboardDateChangedEvent> ().Publish (
                new FrontDeskDashboardDateChangedEventArgs
                    {
                        Source = this,
                        Date = newDate,
                        LocationKey = CurrentUserContext.Location.Key
                    } );
        }

        private void ExecuteGoToTodayCommand ()
        {
            SelectedDate = DateTime.Now.Date;
        }

        private void ExecuteNextDayCommand ()
        {
            SelectedDate = SelectedDate.AddDays ( 1 );
        }

        private void ExecutePreviousDayCommand ()
        {
            SelectedDate = SelectedDate.AddDays ( -1 );
        }

        private void HandleGetCliniciansByLocationCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetCliniciansByLocationKeyResponse> ();

            var clinicianDtos = new ObservableCollection<StaffNameDto> ( response.Clinicians );
            foreach ( var clinicianDto in clinicianDtos.Take ( 8 ) )
            {
                _navigationService.Navigate (
                    RegionManager,
                    TilesRegion,
                    "ClinicianScheduleTileView",
                    null,
                    new[]
                        {
                            new KeyValuePair<string, string> ( "ClinicianKey", clinicianDto.Key.ToString () ),
                            new KeyValuePair<string, string> ( "SelectedDate", SelectedDate.ToShortDateString () )
                        } );
            }
            IsLoading = false;
        }

        private void HandleGetCliniciansByLocationException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "GetCliniciansByLocationCompleted", UserDialogServiceOptions.Ok );
        }

        private void OnCurrentContextChanged ( object sender, EventArgs e )
        {
            var locationKey = CurrentUserContext.Location.Key;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetCliniciansByLocationKeyRequest { LocationKey = locationKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetCliniciansByLocationCompleted, HandleGetCliniciansByLocationException );
        }

        #endregion
    }
}
