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
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// View Model for Navigation class.
    /// </summary>
    public abstract class NavigationViewModel : ViewModelBase, INavigationAware, ISetRegionManager, IViewClosable, IActiveAware
    {
        #region Constants and Fields

        private readonly IAccessControlManager _accessControlManager;
        private readonly IDictionary<string, INavigationCommand> _commandsList;
        private readonly object _loadingSync = new object ();
        private bool _isActive;
        private bool _isLoading;
        private int _loadingCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        protected NavigationViewModel (
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
        {
            _accessControlManager = accessControlManager;

            var commandFactoryHelper = CreateCommandFactoryHelper ( commandFactory );

            CloseViewCommand = commandFactoryHelper.BuildDelegateCommand ( () => CloseViewCommand, ExecuteCloseViewCommand );

            _commandsList = new Dictionary<string, INavigationCommand> ();

            NavigationCommandManager = new NavigationCommandManager ( _commandsList );

            DefaultCommand = NavigationCommandManager.BuildCommand ( () => DefaultCommand, NavigateToDefaultCommand, CanNavigateToDefaultCommand );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Notifies that the value for <see cref="P:Microsoft.Practices.Prism.IActiveAware.IsActive"/> property has changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// Occurs when [view closing].
        /// </summary>
        public event EventHandler<EventArgs> ViewClosing = ( s, e ) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the access control manager.
        /// </summary>
        public IAccessControlManager AccessControlManager
        {
            get { return _accessControlManager; }
        }

        /// <summary>
        /// Gets or sets the close view command.
        /// </summary>
        /// <value>The close view command.</value>
        public ICommand CloseViewCommand { get; set; }

        /// <summary>
        /// Gets the default command.
        /// </summary>
        public INavigationCommand DefaultCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true"/> if the object is active; otherwise <see langword="false"/>.</value>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if ( ApplyPropertyChange ( ref _isActive, () => IsActive, value ) )
                {
                    ActiveChanged ();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                lock ( _loadingSync )
                {
                    if ( value )
                    {
                        _loadingCount++;
                        ApplyPropertyChange ( ref _isLoading, () => IsLoading, true );
                    }
                    else
                    {
                        if ( _loadingCount > 0 )
                        {
                            _loadingCount--;
                        }
                        if ( _loadingCount == 0 )
                        {
                            ApplyPropertyChange ( ref _isLoading, () => IsLoading, false );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the navigation command manager.
        /// </summary>
        public INavigationCommandManager NavigationCommandManager { get; private set; }

        /// <summary>
        /// Gets or sets the region manager.
        /// </summary>
        /// <value>The region manager.</value>
        public IRegionManager RegionManager { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns><see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.</returns>
        public virtual bool IsNavigationTarget ( NavigationContext navigationContext )
        {
            var commandToExecute = FindCommand ( navigationContext );
            return commandToExecute.CanNavigateTo ( navigationContext.Parameters.ToArray () );
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedFrom ( NavigationContext navigationContext )
        {
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public virtual void OnNavigatedTo ( NavigationContext navigationContext )
        {
            var commandToExecute = FindCommand ( navigationContext );
            commandToExecute.NavigateTo ( navigationContext.Parameters.ToArray () );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Actives the changed.
        /// </summary>
        protected virtual void ActiveChanged ()
        {
            if ( IsActiveChanged != null )
            {
                IsActiveChanged ( this, EventArgs.Empty );
            }
        }

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected virtual bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            return true;
        }

        /// <summary>
        /// Creates the command factory helper.
        /// </summary>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Commands.ICommandFactoryHelper"/></returns>
        protected abstract ICommandFactoryHelper CreateCommandFactoryHelper ( ICommandFactory commandFactory );

        /// <summary>
        /// Executes the close view command.
        /// </summary>
        protected virtual void ExecuteCloseViewCommand ()
        {
            RaiseViewClosing ();
        }

        /// <summary>
        /// Navigates to default command.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        protected virtual void NavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            var commandName = parameters.GetValue<string> ( "CommandName" );
            throw new ArgumentException ( "Invalid Command: " + commandName );
        }

        /// <summary>
        /// Raises the view closing.
        /// </summary>
        protected void RaiseViewClosing ()
        {
            ViewClosing ( this, null );
        }

        private INavigationCommand FindCommand ( NavigationContext navigationContext )
        {
            var commandName = navigationContext.TryGetNavigationParameter<string> ( "CommandName" );
            INavigationCommand commandToExecute;

            if ( string.IsNullOrWhiteSpace ( commandName ) )
            {
                commandToExecute = DefaultCommand;
            }
            else
            {
                if ( !commandName.EndsWith ( "Command" ) )
                {
                    commandName += "Command";
                }
                commandToExecute = _commandsList.ContainsKey ( commandName ) ? _commandsList[commandName] : DefaultCommand;
            }
            return commandToExecute;
        }

        #endregion
    }
}
