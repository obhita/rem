﻿#region License

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
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;

namespace Rem.Ria.BillingModule.BillingAdministrationDashboard
{
    /// <summary>
    /// View Model for PayorDashboard class.
    /// </summary>
    public class PayorDashboardViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly INavigationService _navigationService;
        private long _billingOfficeKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorDashboardViewModel"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="commandFactory">The command factory.</param>
        public PayorDashboardViewModel (
            IAccessControlManager accessControlManager,
            INavigationService navigationService,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _navigationService = navigationService;

            var commandFactoryHelper = CommandFactoryHelper.CreateHelper ( this, commandFactory );
            GoToBillingDashboardCommand = commandFactoryHelper.BuildDelegateCommand (
                () => GoToBillingDashboardCommand, ExecuteGoToBillingDashboardCommand );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the go to billing dashboard command.
        /// </summary>
        public ICommand GoToBillingDashboardCommand { get; private set; }

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
            _billingOfficeKey = parameters.GetValue<long> ( "BillingOfficeKey" );

            var shouldReload = true;
            if ( parameters.HasKey ( "ShouldReload" ) )
            {
                shouldReload = parameters.GetValue<bool> ( "ShouldReload" );
            }

            if ( shouldReload )
            {
                _navigationService.Navigate (
                    RegionManager,
                    "PayorTilesRegion",
                    "PayorTypeListView",
                    null,
                    new[] { new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ) } );

                _navigationService.Navigate (
                    RegionManager,
                    "PayorTilesRegion",
                    "PayorListView",
                    null,
                    new[] { new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ) } );
            }
        }

        private void ExecuteGoToBillingDashboardCommand ()
        {
            _navigationService.Navigate (
                RegionManager,
                "BillingDashboardMainRegion",
                "ClaimsDashboardView",
                null,
                new KeyValuePair<string, string> ( "ShouldReload", false.ToString () ),
                new KeyValuePair<string, string> ( "BillingOfficeKey", _billingOfficeKey.ToString () ) );
        }

        #endregion
    }
}
