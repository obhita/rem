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
using Agatha.Common;
using Microsoft.Practices.Prism.Events;
using Pillar.Common.Commands;
using Pillar.Common.Extension;
using Pillar.Common.Metadata;
using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.ViewModel;
using Rem.Ria.PatientModule.Web.Common;
using Rem.WellKnownNames.VisitModule;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// Base class for ActivityViewModel
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    public abstract class ActivityViewModelBase<TDto> : PanelEditorViewModel<TDto>, IActivityViewModel
        where TDto : ActivityDto
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserDialogService _userDialogService;
        private long _activityKey;
        private bool _isCreating;

        private bool _isCurrentActivity;
        private string _visitStatusWellKnownName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityViewModelBase&lt;TDto&gt;"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="commandFactory">The command factory.</param>
        protected ActivityViewModelBase (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            IEventAggregator eventAggregator,
            ICommandFactory commandFactory )
            : base ( userDialogService, asyncRequestDispatcherFactory, accessControlManager, commandFactory )
        {
            _eventAggregator = eventAggregator;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;

            _eventAggregator.GetEvent<VisitChangedEvent> ().Subscribe (
                HandleVisitChanged,
                ThreadOption.BackgroundThread,
                false,
                FilterVisitChangedEvents );

            PropertyChanged += ( s, e ) =>
                {
                    if ( e.PropertyName == PropertyUtil.ExtractPropertyName ( () => EditingDto ) )
                    {
                        UpdateDtoReadOnly ();
                    }
                };

            EditActivityCommand = NavigationCommandManager.BuildCommand (
                () => EditActivityCommand, NavigateToEditActivityCommand, CanNavigateToEditActivityCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the edit activity command.
        /// </summary>
        public INavigationCommand EditActivityCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current activity.
        /// </summary>
        /// <value><c>true</c> if this instance is current activity; otherwise, <c>false</c>.</value>
        public bool IsCurrentActivity
        {
            get { return _isCurrentActivity; }
            set { ApplyPropertyChange ( ref _isCurrentActivity, () => IsCurrentActivity, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the visit status well known.
        /// </summary>
        /// <value>The name of the visit status well known.</value>
        public string VisitStatusWellKnownName
        {
            get { return _visitStatusWellKnownName; }
            set { ApplyPropertyChange ( ref _visitStatusWellKnownName, () => VisitStatusWellKnownName, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Filters the visit changed events.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool FilterVisitChangedEvents ( VisitChangedEventArgs visitChangedEventArgs )
        {
            return EditingDto != null && EditingDto.VisitKey == visitChangedEventArgs.VisitKey && visitChangedEventArgs.VisitStatus != null;
        }

        /// <summary>
        /// Handles the visit changed.
        /// </summary>
        /// <param name="visitChangedEventArgs">The <see cref="Rem.Ria.PatientModule.VisitChangedEventArgs"/> instance containing the event data.</param>
        public void HandleVisitChanged ( VisitChangedEventArgs visitChangedEventArgs )
        {
            VisitStatusWellKnownName = visitChangedEventArgs.VisitStatus.WellKnownName;
            UpdateDtoReadOnly ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to edit activity command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to edit activity command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateToEditActivityCommand ( KeyValuePair<string, string>[] parameters )
        {
            var activityKey = parameters.GetValue<long> ( "ActivityKey" );
            return _activityKey == activityKey;
        }

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        /// <param name="dto">The dto to cancel editing for.</param>
        protected override void ExecuteCancelCommand ( KeyedDataTransferObject dto )
        {
            if ( EditingDto.EditStatus == EditStatus.Create )
            {
                RaiseViewClosing ();
            }
            else
            {
                base.ExecuteCancelCommand ( dto );
            }
        }

        //TODO: Replace logic that handles this to use Client Side Presentation Rules

        /// <summary>
        /// Execute an override of closing an activity tile view. Close the activity tile view only after
        /// successfully performing a server-side activity delete.
        /// </summary>
        protected override void ExecuteCloseViewCommand ()
        {
            var result = _userDialogService.ShowDialog (
                "Are you sure you want to delete?", "Confirmation", UserDialogServiceOptions.OkCancel );

            if ( result == UserDialogServiceResult.Ok )
            {
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new DeleteActivityRequest { ActivityKey = EditingDto.Key } );
                IsLoading = true;
                requestDispatcher.ProcessRequests ( HandleDeleteScheduledActivityCompleted, HandleDeleteScheduledActivityException );
            }
        }

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="dto">The dto to save changes.</param>
        protected override void ExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            base.ExecuteSaveCommand ( dto );

            if ( EditingDto.EditStatus == EditStatus.Create )
            {
                _isCreating = true;
            }
        }

        /// <summary>
        /// Loads the activity.
        /// </summary>
        /// <param name="activityKey">The activity key.</param>
        protected abstract void LoadActivity ( long activityKey );

        /// <summary>
        /// Navigates to edit activity command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected virtual void NavigateToEditActivityCommand ( KeyValuePair<string, string>[] parameters )
        {
            VisitStatusWellKnownName = parameters.GetValue<string> ( "VisitStatusWellKnownName" );
            UpdateDtoReadOnly ();
            _activityKey = parameters.GetValue<long> ( "ActivityKey" );
            _isCurrentActivity = parameters.GetValue<bool> ( "IsCurrentActivity" );
            LoadActivity ( _activityKey );
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected override void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            base.RequestCompleted ( receivedResponses );

            if ( _isCreating )
            {
                _isCreating = false;
                var activityChangedEventArgs = new ActivityChangedEventArgs
                    {
                        Sender = this,
                        VisitKey = EditingDto.VisitKey,
                        ClinicianKey = EditingDto.ClinicianKey,
                        PatientKey = EditingDto.PatientKey
                    };
                _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish ( activityChangedEventArgs );
            }
        }

        /// <summary>
        /// Updates the dto read only.
        /// </summary>
        protected void UpdateDtoReadOnly ()
        {
            if ( EditingDto != null && VisitStatusWellKnownName != null )
            {
                if ( VisitStatusWellKnownName == VisitStatus.CheckedIn )
                {
                    EditingDto.IsNotReadOnly ();
                }
                else
                {
                    EditingDto.IsReadOnly ();
                }
            }
        }

        private void HandleDeleteScheduledActivityCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            var response = receivedResponses.Get<DeleteActivityResponse> ();

            if ( response.Activity == null )
            {
                return;
            }

            if ( response.Activity.DataErrorInfoCollection.Count () == 0 )
            {
                RaiseViewClosing ();
                _eventAggregator.GetEvent<ActivityChangedEvent> ().Publish (
                    new ActivityChangedEventArgs { Sender = this, ClinicalCaseKey = response.ClinicalCaseKey } );
            }
            else
            {
                var errors = new StringBuilder ();
                response.Activity.DataErrorInfoCollection.ForEach ( error => errors.AppendLine ( error.Message ) );
                _userDialogService.ShowDialog ( errors.ToString (), "Delete Activity Status", UserDialogServiceOptions.Ok );
            }
        }

        private void HandleDeleteScheduledActivityException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( "The activity could not be deleted.", "Activity delete operation failed.", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
