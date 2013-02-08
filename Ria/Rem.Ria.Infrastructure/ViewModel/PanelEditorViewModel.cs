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
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using Agatha.Common;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// View Model for Class for editing panel.
    /// </summary>
    /// <typeparam name="TDto">The type of the dto.</typeparam>
    public abstract class PanelEditorViewModel<TDto> : NavigationViewModel
        where TDto : KeyedDataTransferObject
    {
        #region Constants and Fields

        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUserDialogService _userDialogService;

        private TDto _editingDto;
        private AbstractDataTransferObject _editingPart;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelEditorViewModel&lt;TDto&gt;"/> class.
        /// </summary>
        /// <param name="userDialogService">The user dialog service.</param>
        /// <param name="asyncRequestDispatcherFactory">The async request dispatcher factory.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        protected PanelEditorViewModel (
            IUserDialogService userDialogService,
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _userDialogService = userDialogService;
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            var commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );

            SaveCommand = commandFactoryHelper.BuildDelegateCommand<KeyedDataTransferObject> (
                () => SaveCommand, ExecuteSaveCommand, CanExecuteSaveCommand );
            CancelCommand = commandFactoryHelper.BuildDelegateCommand<KeyedDataTransferObject> (
                () => CancelCommand, ExecuteCancelCommand, CanExecuteCancelCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        public ICommand CancelCommand { get; private set; }

        /// <summary>
        /// Gets or sets the editing dto.
        /// </summary>
        /// <value>The editing dto.</value>
        public TDto EditingDto
        {
            get { return _editingDto; }
            set { ApplyPropertyChange ( ref _editingDto, () => EditingDto, value ); }
        }

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>The save command.</value>
        public ICommand SaveCommand { get; protected set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can execute cancel command] the specified dto.
        /// </summary>
        /// <param name="dto">The dto to check.</param>
        /// <returns><c>true</c> if this instance [can execute cancel command] the specified dto; otherwise, <c>false</c>.</returns>
        protected virtual bool CanExecuteCancelCommand ( KeyedDataTransferObject dto )
        {
            return true;
        }

        /// <summary>
        /// Determines whether this instance [can execute save command] the specified dto.
        /// </summary>
        /// <param name="dto">The dto to check.</param>
        /// <returns><c>true</c> if this instance [can execute save command] the specified dto; otherwise, <c>false</c>.</returns>
        protected virtual bool CanExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            return true;
        }

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        /// <param name="dto">The dto to check.</param>
        protected virtual void ExecuteCancelCommand ( KeyedDataTransferObject dto )
        {
            if (dto.Key > 0)
            {
                _editingPart = dto;

                var dtoType = dto.GetType ();
                var openCancelRequestType = typeof( GetDtoRequest<> );
                var closedCancelRequestType = openCancelRequestType.MakeGenericType ( dtoType );
                var cancelRequest = Activator.CreateInstance ( closedCancelRequestType, dto.Key );
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                requestDispatcher.Add ( cancelRequest as Request );

                _editingPart.IsLoading = true;

                requestDispatcher.ProcessRequests ( RequestCompleted, HandleRequestDispatcherException );
            }
            else
            {
                ExecuteCancelCommandWhenAdding ( dto );
            }
        }

        /// <summary>
        /// Executes the cancel command when adding.
        /// </summary>
        /// <param name="dto">The dto.</param>
        protected virtual void ExecuteCancelCommandWhenAdding(KeyedDataTransferObject dto)
        {
        }

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="dto">The dto to save.</param>
        protected virtual void ExecuteSaveCommand ( KeyedDataTransferObject dto )
        {
            _editingPart = dto;

            if ( dto != null )
            {
                if (!dto.HasErrors ||
                    (dto.HasErrors && dto.DataErrorInfoCollection.Where(p => p.DataErrorInfoType == DataErrorInfoType.PropertyLevel).Count() == 0))
                {
                    var dtoType = dto.GetType ();
                    var openSaveRequestType = typeof( SaveDtoRequest<> );
                    var closedSaveRequestType = openSaveRequestType.MakeGenericType ( dtoType );
                    var saveRequest = Activator.CreateInstance ( closedSaveRequestType, dto );
                    var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher ();
                    requestDispatcher.Add ( saveRequest as Request );

                    _editingPart.IsLoading = true;

                    requestDispatcher.ProcessRequests ( RequestCompleted, HandleRequestDispatcherException );
                }
                else
                {
                    var errors = dto.DataErrorInfoCollection.Where(p => p.ErrorLevel == ErrorLevel.Error).Select(p => p.ToString()).Aggregate ( ( i, j ) => i + Environment.NewLine + j);
                    _userDialogService.ShowDialog ( string.Format("Please fix the following errors before trying to save:{0}{1}", Environment.NewLine, errors), "Errors", UserDialogServiceOptions.Ok );
                }
            }
        }

        /// <summary>
        /// Requests the completed.
        /// </summary>
        /// <param name="receivedResponses">The received responses.</param>
        protected virtual void RequestCompleted ( ReceivedResponses receivedResponses )
        {
            _editingPart.IsLoading = false;

            var response = receivedResponses.Responses.ElementAt ( 0 ) as IDtoResponse;
            var responseDtoType = response.GetType ().GetGenericArguments ().ElementAt ( 0 );
            if ( responseDtoType == EditingDto.GetType () )
            {
                EditingDto = response.GetDto () as TDto;
            }
            else
            {
                foreach ( var propInfo in EditingDto.GetType ().GetProperties () )
                {
                    if ( propInfo.PropertyType == responseDtoType )
                    {
                        propInfo.SetValue (
                            EditingDto, response.GetDto (), BindingFlags.SetProperty, null, null, Thread.CurrentThread.CurrentCulture );
                        break;
                    }
                }
            }
        }

        private void HandleRequestDispatcherException ( ExceptionInfo exceptionInfo )
        {
            _editingPart.IsLoading = false;
            IsLoading = false;
            _userDialogService.ShowDialog ( exceptionInfo.Message, "Failed", UserDialogServiceOptions.Ok );
        }

        #endregion
    }
}
