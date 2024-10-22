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

using System;
using System.Collections.Generic;
using Pillar.Common.Commands;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Commands;
using Rem.Ria.Infrastructure.Navigation;
using Rem.Ria.NewCropModule.Service;

namespace Rem.Ria.NewCropModule
{
    /// <summary>
    /// View Model for NewCropModal class.
    /// </summary>
    public class NewCropModalViewModel : NavigationViewModel
    {
        #region Constants and Fields

        private readonly INewCropSessionLauncher _launcher;
        private string _sourceUrl;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCropModalViewModel"/> class.
        /// </summary>
        /// <param name="launcher">The launcher.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="commandFactory">The command factory.</param>
        public NewCropModalViewModel (
            INewCropSessionLauncher launcher,
            IAccessControlManager accessControlManager,
            ICommandFactory commandFactory )
            : base ( accessControlManager, commandFactory )
        {
            _launcher = launcher;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the source URL.
        /// </summary>
        /// <value>The source URL.</value>
        public string SourceUrl
        {
            get { return _sourceUrl; }
            set { ApplyPropertyChange ( ref _sourceUrl, () => SourceUrl, value ); }
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
            var patientKey = parameters.GetValue<long> ( "PatientKey" );
            var staffKey = parameters.GetValue<long> ( "StaffKey" );
            var agencyKey = parameters.GetValue<long> ( "AgencyKey" );
            var locationKey = parameters.GetValue<long> ( "LocationKey" );

            var url = _launcher.BuildUrl ( patientKey, staffKey, agencyKey, locationKey );
            SourceUrl = url.AbsoluteUri;
        }

        #endregion
    }
}
