using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Common.Extension;
using Pillar.Security.AccessControl;
using Rem.Ria.AgencyModule.Web.Service;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.AgencyWorkspace
{
    /// <summary>
    /// View Model for SaveLabResult class.
    /// </summary>
    public class SaveLabResultViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;
        private bool _enableSave;
        private bool _errorState;
        private string _hl7Message;
        private bool _postProcessing;
        private bool _preProcessing;
        private string _userMessage;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLabResultViewModel"/> class.
        /// </summary>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public SaveLabResultViewModel (
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUserDialogService userDialogService,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _userDialogService = userDialogService;
            PostProcessing = false;
            PreProcessing = true;
            EnableSave = true;
            ErrorState = false;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            SaveLabResultCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => SaveLabResultCommand, ExecuteSaveLabResultCommand );
            FinishCommand = commandFactoryHelper.BuildDelegateCommand<object> ( () => FinishCommand, ExecuteFinishCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [enable save].
        /// </summary>
        /// <value><c>true</c> if [enable save]; otherwise, <c>false</c>.</value>
        public bool EnableSave
        {
            get { return _enableSave; }
            set { ApplyPropertyChange ( ref _enableSave, () => EnableSave, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [error state].
        /// </summary>
        /// <value><c>true</c> if [error state]; otherwise, <c>false</c>.</value>
        public bool ErrorState
        {
            get { return _errorState; }
            set { ApplyPropertyChange ( ref _errorState, () => ErrorState, value ); }
        }

        /// <summary>
        /// Gets the finish command.
        /// </summary>
        public ICommand FinishCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [post processing].
        /// </summary>
        /// <value><c>true</c> if [post processing]; otherwise, <c>false</c>.</value>
        public bool PostProcessing
        {
            get { return _postProcessing; }
            set { ApplyPropertyChange ( ref _postProcessing, () => PostProcessing, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [pre processing].
        /// </summary>
        /// <value><c>true</c> if [pre processing]; otherwise, <c>false</c>.</value>
        public bool PreProcessing
        {
            get { return _preProcessing; }
            set { ApplyPropertyChange ( ref _preProcessing, () => PreProcessing, value ); }
        }

        /// <summary>
        /// Gets the save lab result command.
        /// </summary>
        public ICommand SaveLabResultCommand { get; private set; }

        /// <summary>
        /// Gets or sets the user message.
        /// </summary>
        /// <value>The user message.</value>
        public string UserMessage
        {
            get { return _userMessage; }
            set { ApplyPropertyChange ( ref _userMessage, () => UserMessage, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the save lab result command.
        /// </summary>
        /// <param name="obj">The object.</param>
        public void ExecuteSaveLabResultCommand ( object obj )
        {
            SaveLabResult ();
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
            _hl7Message = parameters.GetValue<string> ( "Message" );
        }

        private void ExecuteFinishCommand ( object obj )
        {
            if ( EnableSave )
            {
                SaveLabResult ();
            }

            RaiseViewClosing ();
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Error Saving Lab Results", UserDialogServiceOptions.Ok );
        }

        private void SaveLabResult ()
        {
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
            var encoding = new UTF8Encoding ();
            var byteEncodedMessage = encoding.GetBytes ( _hl7Message );

            requestDispatcher.Add ( new SaveLabResultRequest { HL7Message = byteEncodedMessage } );
            requestDispatcher.ProcessRequests ( SaveLabResultCompleted, HandleRequestDispatcherException );
            IsLoading = true;
        }

        private void SaveLabResultCompleted ( ReceivedResponses receivedResponses )
        {
            IsLoading = false;
            PostProcessing = true;
            PreProcessing = false;
            var response = receivedResponses.Get<SaveLabResultResponse> ();
            if ( response.ErrorMessages.Count > 0 )
            {
                var errorMessages = new StringBuilder ();

                response.ErrorMessages.ForEach (
                    em =>
                        {
                            errorMessages.AppendLine ( em );
                            errorMessages.AppendLine ();
                        } );

                UserMessage = errorMessages.ToString ();
                ErrorState = true;
            }
            else
            {
                UserMessage = "Lab Result has been successfully saved.";
                EnableSave = false;
            }
        }

        #endregion
    }
}
