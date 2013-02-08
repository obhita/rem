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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Domain.Primitives;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.PatientModule.Web.FrontDeskDashboard;

namespace Rem.Ria.PatientModule.FrontDeskDashboard
{
    /// <summary>
    /// View Model for PayorCoverage class.
    /// </summary>
    public class PayorCoverageViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private readonly IPopupService _popupService;
        private readonly INavigationService _navigationService;
        private int _pageIndex;
        private int _pageSize;
        private long _patientKey;
        private ObservableCollection<PayorCoverageCacheDto> _payorCoverageHistory;
        private ObservableCollection<PayorCoverageCacheDto> _primary;
        private ObservableCollection<PayorCoverageCacheDto> _secondary;
        private ObservableCollection<PayorCoverageCacheDto> _tertiary;
        private int _totalCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCoverageViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        /// <param name="navigationService">The navigation service.</param>
        public PayorCoverageViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IPopupService popupService,
            INavigationService navigationService)
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;
            _navigationService = navigationService;

            PageSize = 50;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            DeletePayorCoverageCommand = commandFactoryHelper.BuildDelegateCommand<PayorCoverageCacheDto> (
                () => DeletePayorCoverageCommand, ExecuteDeletePayorCoverageCommand );
            ShowPayorCoverageEditorCommand = commandFactoryHelper.BuildDelegateCommand<PayorCoverageCacheDto> (
                () => ShowPayorCoverageEditorCommand, ExecuteShowPayorCoverageEditorCommand );
            GoToPatientHistoryCommand = commandFactoryHelper.BuildDelegateCommand (
                () => GoToPatientHistoryCommand, ExecuteGoToPatientHistoryCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the go to patient history command.
        /// </summary>
        public ICommand GoToPatientHistoryCommand { get; private set; }

        /// <summary>
        /// Gets the delete payor coverage command.
        /// </summary>
        public ICommand DeletePayorCoverageCommand { get; private set; }

        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if ( ApplyPropertyChange ( ref _pageIndex, () => PageIndex, value ) )
                {
                    GetPayorCoverages ();
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return _pageSize; }
            set { ApplyPropertyChange ( ref _pageSize, () => PageSize, value ); }
        }

        /// <summary>
        /// Gets or sets the payor coverage history.
        /// </summary>
        /// <value>The payor coverage history.</value>
        public ObservableCollection<PayorCoverageCacheDto> PayorCoverageHistory
        {
            get { return _payorCoverageHistory; }
            set { ApplyPropertyChange ( ref _payorCoverageHistory, () => PayorCoverageHistory, value ); }
        }

        /// <summary>
        /// Gets or sets the primary.
        /// </summary>
        /// <value>The primary.</value>
        public ObservableCollection<PayorCoverageCacheDto> Primary
        {
            get { return _primary; }
            set { ApplyPropertyChange ( ref _primary, () => Primary, value ); }
        }

        /// <summary>
        /// Gets or sets the secondary.
        /// </summary>
        /// <value>The secondary.</value>
        public ObservableCollection<PayorCoverageCacheDto> Secondary
        {
            get { return _secondary; }
            set { ApplyPropertyChange ( ref _secondary, () => Secondary, value ); }
        }

        /// <summary>
        /// Gets the show payor coverage editor command.
        /// </summary>
        public ICommand ShowPayorCoverageEditorCommand { get; private set; }

        /// <summary>
        /// Gets or sets the tertiary.
        /// </summary>
        /// <value>The tertiary.</value>
        public ObservableCollection<PayorCoverageCacheDto> Tertiary
        {
            get { return _tertiary; }
            set { ApplyPropertyChange ( ref _tertiary, () => Tertiary, value ); }
        }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount
        {
            get { return _totalCount; }
            set { ApplyPropertyChange ( ref _totalCount, () => TotalCount, value ); }
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

            GetPayorCoverages ();
        }

        private void ExecuteDeletePayorCoverageCommand ( PayorCoverageCacheDto dto )
        {
            var results = _userDialogService.ShowDialog (
                "Are you sure you would like to delete this payor?", "Delete", UserDialogServiceOptions.OkCancel );

            if (results == UserDialogServiceResult.Ok)
            {
                dto.EditStatus = EditStatus.Delete;

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

                requestDispatcher.Add ( new SaveDtoRequest<PayorCoverageCacheDto> { DataTransferObject = dto } );
                requestDispatcher.ProcessRequests ( HandleSavePayorCoverageCompleted, HandleSavePayorCoverageError );
                IsLoading = true;
            }
        }

        private void GetPayorCoverages ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();

            requestDispatcher.Add (
                new GetPayorCoverageCachesByPatientKeyRequest { PatientKey = _patientKey, PageIndex = PageIndex, PageSize = PageSize } );
            requestDispatcher.ProcessRequests ( HandleGetPayorCoveragesByPatientKeyCompleted, HandleGetPayorCoveragesByPatientKeyError );
            IsLoading = true;
        }

        private void HandleGetPayorCoveragesByPatientKeyCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetPayorCoverageCachesByPatientKeyResponse> ();

            Primary = new ObservableCollection<PayorCoverageCacheDto> ( response.CurrentPrimaryList );
            Secondary = new ObservableCollection<PayorCoverageCacheDto>(response.CurrentSecondaryList);
            Tertiary = new ObservableCollection<PayorCoverageCacheDto>(response.CurrentTertiaryList);

            PayorCoverageHistory = new ObservableCollection<PayorCoverageCacheDto>(response.PagedHistory.PagedList);
            PageIndex = response.PagedHistory.PageIndex;
            PageSize = response.PagedHistory.PageSize;
            TotalCount = response.PagedHistory.TotalCount;
            IsLoading = false;
        }

        private void HandleGetPayorCoveragesByPatientKeyError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Fetching Self Payments Error", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void HandleSavePayorCoverageCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<PayorCoverageCacheDto>>();
            if ( response.DataTransferObject.HasErrors )
            {
                var errorMessageBuilder = new StringBuilder ();
                foreach ( var dataErrorInfo in response.DataTransferObject.DataErrorInfoCollection )
                {
                    errorMessageBuilder.Append ( dataErrorInfo.Message + "\n" );
                }
                _userDialogService.ShowDialog ( errorMessageBuilder.ToString (), "Error Deleting Payor", UserDialogServiceOptions.Ok );
            }
            else
            {
                var payorToDelete = Primary.FirstOrDefault ( pc => pc.Key == response.DataTransferObject.Key );
                if(payorToDelete != null)
                {
                    Primary.Remove ( payorToDelete );
                }
                else if ((payorToDelete = Secondary.FirstOrDefault ( pc => pc.Key == response.DataTransferObject.Key)) != null)
                {
                    Secondary.Remove ( payorToDelete );
                }
                else if ((payorToDelete = Tertiary.FirstOrDefault(pc => pc.Key == response.DataTransferObject.Key)) != null)
                {
                    Tertiary.Remove(payorToDelete);
                }
                else if ((payorToDelete = PayorCoverageHistory.FirstOrDefault(pc => pc.Key == response.DataTransferObject.Key)) != null)
                {
                    PayorCoverageHistory.Remove(payorToDelete);
                }
            }
            IsLoading = false;
        }

        private void HandleSavePayorCoverageError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Deleting Payor", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void ExecuteShowPayorCoverageEditorCommand(PayorCoverageCacheDto dto)
        {
            var command = dto == null ? "Create" : "Edit";
            var title = dto == null ? "Create Payor" : "Edit Payor";
            var key = dto == null ? 0 : dto.Key;
            _popupService.ShowPopup (
                "EditPayorCoverageView",
                command,
                title,
                new[]
                    {
                        new KeyValuePair<string, string> ( "PayorCoverageKey", key.ToString () ),
                        new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () )
                    },
                false,
                GetPayorCoverages );
        }

        private void ExecuteGoToPatientHistoryCommand()
        {
            _navigationService.Navigate (
                "WorkspacesRegion",
                "PatientWorkspaceView",
                "SubViewPassThrough",
                new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () ),
                new KeyValuePair<string, string> ( "SubViewName", "ExternalPatientHistoryView" ) );
        }

        #endregion
    }
}
