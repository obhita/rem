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
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.NewCropModule.Service;

namespace Rem.Ria.NewCropModule
{
    /// <summary>
    /// View Model for NewCrop class.
    /// </summary>
    public class NewCropViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly INewCropSessionLauncher _newCropSessionLauncher;
        private long _patientKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCropViewModel"/> class.
        /// </summary>
        /// <param name="newCropSessionLauncher">The new crop session launcher.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public NewCropViewModel (
            INewCropSessionLauncher newCropSessionLauncher,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _newCropSessionLauncher = newCropSessionLauncher;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );

            LaunchSessionInBrowserCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => LaunchSessionInBrowserCommand, ExecuteLaunchInBrowserSession );
            LaunchSessionInSilverlightCommand = commandFactoryHelper.BuildDelegateCommand<object> (
                () => LaunchSessionInSilverlightCommand, ExecuteLaunchInSilverlightSession );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the launch session in browser command.
        /// </summary>
        public ICommand LaunchSessionInBrowserCommand { get; private set; }

        /// <summary>
        /// Gets the launch session in silverlight command.
        /// </summary>
        public ICommand LaunchSessionInSilverlightCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance [can navigate to default command] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to default command] the specified parameters; otherwise, <c>false</c>.</returns>
        protected override bool CanNavigateToDefaultCommand ( KeyValuePair<string, string>[] parameters )
        {
            try
            {
                var patientKey = parameters.GetValue<long> ( "PatientKey" );
                return _patientKey == patientKey;
            }
            catch ( ArgumentException )
            {
                // Patient Key was not provided, so I must be outside of the context of a patient. I.e.:Interoperability workspace
                return true;
            }
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
            try
            {
                _patientKey = parameters.GetValue<long> ( "PatientKey" );
            }
            catch ( ArgumentException )
            {
                // Patient Key was not provided, so I must be outside of the context of a patient. I.e.:Interoperability workspace
                _patientKey = 0;
            }
        }

        private void ExecuteLaunchInBrowserSession ( object obj )
        {
            _newCropSessionLauncher.LaunchSession (
                _patientKey, CurrentUserContext.Staff.Key, CurrentUserContext.Agency.Key, CurrentUserContext.Location.Key );
        }

        private void ExecuteLaunchInSilverlightSession ( object obj )
        {
            _newCropSessionLauncher.LaunchSession (
                _patientKey, CurrentUserContext.Staff.Key, CurrentUserContext.Agency.Key, CurrentUserContext.Location.Key, false );
        }

        #endregion
    }
}
