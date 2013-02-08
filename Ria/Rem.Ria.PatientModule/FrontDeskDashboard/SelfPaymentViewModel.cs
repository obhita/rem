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
    /// View Model for show history of Self Payments.
    /// </summary>
    public class SelfPaymentViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private readonly IPopupService _popupService;
        private int _pageIndex;
        private int _pageSize;
        private long _patientKey;

        private ObservableCollection<SelfPaymentDto> _selfPaymentDtos;
        private int _totalCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelfPaymentViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="popupService">The popup service.</param>
        public SelfPaymentViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IPopupService popupService)
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            _popupService = popupService;

            PageSize = 50;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            AddSelfPaymentCommand = commandFactoryHelper.BuildDelegateCommand ( () => AddSelfPaymentCommand, ExecuteAddSelfPaymentCommand );
            DeleteSelfPaymentCommand = commandFactoryHelper.BuildDelegateCommand<SelfPaymentDto> ( () => DeleteSelfPaymentCommand, ExecuteDeleteSelfPaymentCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add self payment command.
        /// </summary>
        public ICommand AddSelfPaymentCommand { get; private set; }

        /// <summary>
        /// Gets the delete self payment command.
        /// </summary>
        public ICommand DeleteSelfPaymentCommand { get; private set; }

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
                    GetSelfPayments ();
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
        /// Gets or sets the self payment dtos.
        /// </summary>
        /// <value>The self payment dtos.</value>
        public ObservableCollection<SelfPaymentDto> SelfPaymentDtos
        {
            get { return _selfPaymentDtos; }
            set { ApplyPropertyChange ( ref _selfPaymentDtos, () => SelfPaymentDtos, value ); }
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

            GetSelfPayments ();
        }

        private void HandleGetSelfPaymentsByPatientKeyRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<GetSelfPaymentsByPatientKeyResponse> ();
            SelfPaymentDtos = new ObservableCollection<SelfPaymentDto> ( response.PagedSelfPaymentDtos.PagedList );
            PageIndex = response.PagedSelfPaymentDtos.PageIndex;
            PageSize = response.PagedSelfPaymentDtos.PageSize;
            TotalCount = response.PagedSelfPaymentDtos.TotalCount;
            IsLoading = false;
        }

        private void HandleGetSelfPaymentsByPatientKeyRequestError ( ExceptionInfo exceptionInfo )
        {
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Fetching Self Payments Error", UserDialogServiceOptions.Ok );
            IsLoading = false;
        }

        private void ExecuteAddSelfPaymentCommand()
        {
            _popupService.ShowPopup (
                "AddSelfPaymentView",
                null,
                "Add Self Payment",
                new[] { new KeyValuePair<string, string> ( "PatientKey", _patientKey.ToString () )},
                false,
                GetSelfPayments );
        }

        private void ExecuteDeleteSelfPaymentCommand( SelfPaymentDto dto)
        {
            var results = _userDialogService.ShowDialog (
                "Are you sure you would like to delete this payment?", "Delete", UserDialogServiceOptions.OkCancel );

            if (results == UserDialogServiceResult.Ok)
            {
                dto.EditStatus = EditStatus.Delete;

                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( new SaveDtoRequest<SelfPaymentDto> { DataTransferObject = dto } );
                requestDispatcher.ProcessRequests ( HandleDeleteSelfPaymentRequestCompleted, HandleDeleteSelfPaymentRequestError );
                IsLoading = true;
            }
        }

        private void HandleDeleteSelfPaymentRequestCompleted ( ReceivedResponses receivedResponses )
        {
            var response = receivedResponses.Get<DtoResponse<SelfPaymentDto>> ();
            if(response.DataTransferObject.HasErrors)
            {
                var errorMessageBuilder = new StringBuilder ();
                foreach ( var dataErrorInfo in response.DataTransferObject.DataErrorInfoCollection )
                {
                    errorMessageBuilder.Append ( dataErrorInfo.Message + "\n" );
                }
                _userDialogService.ShowDialog ( errorMessageBuilder.ToString (), "Error deleting self payment", UserDialogServiceOptions.Ok );
            }
            else
            {
                var selfPaymentToRemove = SelfPaymentDtos.FirstOrDefault ( sp => sp.Key == response.DataTransferObject.Key );
                SelfPaymentDtos.Remove ( selfPaymentToRemove );
            }
            IsLoading = false;
        }

        private void HandleDeleteSelfPaymentRequestError(ExceptionInfo exceptionInfo)
        {
            _userDialogService.ShowDialog(exceptionInfo.Message, "Error Deleting Self payment", UserDialogServiceOptions.Ok);
            IsLoading = false;
        }

        private void GetSelfPayments ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetSelfPaymentsByPatientKeyRequest { PatientKey = _patientKey, PageNumer = PageIndex, PageSize = PageSize });
            requestDispatcher.ProcessRequests(HandleGetSelfPaymentsByPatientKeyRequestCompleted, HandleGetSelfPaymentsByPatientKeyRequestError);
            IsLoading = true;
        }

        #endregion
    }
}
