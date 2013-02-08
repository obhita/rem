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
using System.Windows;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.PatientDashboard;
using Rem.WellKnownNames.PatientModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// View Model for MedicationList class.
    /// </summary>
    public class MedicationListViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly Predicate<object> _filter;
        private readonly INavigationService _navigationService;
        private readonly PagedCollectionViewWrapper<MedicationDto> _pagedCollectionViewWrapper;
        private readonly DelegateCommand _showActiveOnlyCommand;
        private readonly DelegateCommand _showAllCommand;
        private readonly IUserDialogService _userDialogService;

        private IList<MedicationDto> _medicationList;
        private long _patientKey;

        private ShowOption _showOption;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationListViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        [InjectionConstructor]
        public MedicationListViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IEventAggregator eventAggregator,
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;

            _pagedCollectionViewWrapper = new PagedCollectionViewWrapper<MedicationDto> ();

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            _showAllCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowAllCommand, ExecuteShowAll );
            _showActiveOnlyCommand = commandFactoryHelper.BuildDelegateCommand ( () => ShowActiveOnlyCommand, ExecuteShowActiveOnly );
            ToggleActiveIndicatorCommand = commandFactoryHelper.BuildDelegateCommand<MedicationDto> (
                () => ToggleActiveIndicatorCommand, ExecuteToggleActiveIndicator );

            ShowMedicationCommand = commandFactoryHelper.BuildDelegateCommand<MedicationDto> (
                () => ShowMedicationCommand, ExecuteShowMedicationCommand );

            _showOption = ShowOption.ShowActive;
            _filter = FilterByActiveStatus;

            InitializeGroupingDescriptions ();
            _pagedCollectionViewWrapper.SortDescription = new SortDescription ( "StartDate", ListSortDirection.Ascending );

            _eventAggregator.GetEvent<MedicationChangedEvent> ().Subscribe (
                MedicationChangedEventHandler,
                ThreadOption.BackgroundThread,
                false,
                FilterMedicationChangedEvents );
        }

        #endregion

        #region Enums

        /// <summary>
        /// Show options
        /// </summary>
        public enum ShowOption
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
        /// Gets the paged collection view wrapper.
        /// </summary>
        public PagedCollectionViewWrapper<MedicationDto> PagedCollectionViewWrapper
        {
            get { return _pagedCollectionViewWrapper; }
        }

        /// <summary>
        /// Gets the show active only command.
        /// </summary>
        public ICommand ShowActiveOnlyCommand
        {
            get { return _showActiveOnlyCommand; }
        }

        /// <summary>
        /// Gets the show all command.
        /// </summary>
        public ICommand ShowAllCommand
        {
            get { return _showAllCommand; }
        }

        /// <summary>
        /// Gets the show medication command.
        /// </summary>
        public ICommand ShowMedicationCommand { get; private set; }

        /// <summary>
        /// Gets the toggle active indicator command.
        /// </summary>
        public ICommand ToggleActiveIndicatorCommand { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the medication changed events.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.MedicationChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterMedicationChangedEvents ( MedicationChangedEventArgs obj )
        {
            return obj.PatientKey == _patientKey;
        }

        /// <summary>
        /// Medications the changed event handler.
        /// </summary>
        /// <param name="obj">The <see cref="Rem.Ria.PatientModule.MedicationChangedEventArgs"/> instance containing the event data.</param>
        public void MedicationChangedEventHandler ( MedicationChangedEventArgs obj )
        {
            Deployment.Current.InvokeIfNeeded ( () => GetAllMedicationsByPatientAsync ( _patientKey ) );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var key = parameters.GetValue<long> ( "PatientKey" );
            return key == 0 || key == _patientKey;
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
            var key = parameters.GetValue<long> ( "PatientKey" );
            GetAllMedicationsByPatientAsync ( key );
            _patientKey = key;

            _navigationService.Navigate (
                RegionManager,
                "NewCropButtonsRegion",
                "NewCropButtonsView",
                null,
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ) } );
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

        private void ExecuteShowMedicationCommand ( MedicationDto medicationDto )
        {
            _navigationService.Navigate (
                "ModalPopupRegion",
                "EditMedicationView",
                null,
                new[]
                    {
                        new KeyValuePair<string, string> ( "MedicationKey", medicationDto.Key.ToString () ),
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () )
                    } );
        }

        private void ExecuteToggleActiveIndicator ( MedicationDto medicationDto )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new SaveDtoRequest<MedicationDto> { DataTransferObject = medicationDto } );
            requestDispatcher.ProcessRequests ( HandleSaveMedicationCompleted, HandleSaveMedicationException );
        }

        private bool FilterByActiveStatus ( object obj )
        {
            var returnValue = true;

            var medicationDto = obj as MedicationDto;

            if ( medicationDto != null )
            {
                if ( _showOption == ShowOption.ShowActive )
                {
                    returnValue = medicationDto.MedicationStatus.WellKnownName == MedicationStatus.Active;
                }
            }

            return returnValue;
        }

        private void GetAllMedicationsByPatientAsync ( long patientKey )
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            requestDispatcher.Add ( new GetAllMedicationsByPatientRequest { PatientKey = patientKey } );
            IsLoading = true;
            requestDispatcher.ProcessRequests ( HandleGetAllMedicationsByPatientCompleted, HandleGetAllMedicationsByPatientException );
        }

        private void HandleGetAllMedicationsByPatientCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetAllMedicationsByPatientResponse> ();
            _medicationList = new ObservableCollection<MedicationDto> ( response.MedicationDtos );
            _pagedCollectionViewWrapper.WrapInPagedCollectionView ( _medicationList, _filter );
            IsLoading = false;
        }

        private void HandleGetAllMedicationsByPatientException ( ExceptionInfo ex )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( ex.Message, "Get medications list failed.", UserDialogServiceOptions.Ok );
        }

        private void HandleSaveMedicationCompleted ( ReceivedResponses receivedResponses )
        {
            GetAllMedicationsByPatientAsync ( _patientKey );
        }

        private void HandleSaveMedicationException ( ExceptionInfo ex )
        {
            _userDialogService.ShowDialog ( ex.Message, "Could not save", UserDialogServiceOptions.Ok );
        }

        private void InitializeGroupingDescriptions ()
        {
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<MedicationDto, object> ( p => p.MedicationCodeCodedConcept ), "Medication Code" ) );
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<MedicationDto, object> ( p => p.RootMedicationCodedConcept ), "Medication" ) );
            _pagedCollectionViewWrapper.GroupingDescriptions.Add (
                new CustomPropertyGroupDescription (
                    PropertyUtil.ExtractPropertyName<MedicationDto, object> ( p => p.PrescribingPhysicianName ), "Prescribed by" ) );
        }

        #endregion
    }
}
