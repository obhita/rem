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
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Pillar.Security.AccessControl;
using Rem.Infrastructure.Bootstrapper;
using Rem.Ria.Infrastructure;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.NewCropModule.InteroperabilityWidget;
using Rem.Ria.NewCropModule.Service;

namespace Rem.Ria.NewCropModule
{
    /// <summary>
    /// NewCropModule class.
    /// </summary>
    public class NewCropModule : IModule
    {
        #region Constants and Fields

        private readonly IAccessControlManager _accessControlManager;
        private readonly IUnityContainer _container;
        private readonly IMetadataService _metadataService;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewCropModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="metadataService">The metadata service.</param>
        public NewCropModule (
            IUnityContainer container,
            IAccessControlManager accessControlManager,
            IMetadataService metadataService )
        {
            _container = container;
            _accessControlManager = accessControlManager;
            _metadataService = metadataService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize ()
        {
            RegisterScreens ();
            RegisterServices ();
        }

        #endregion

        #region Methods

        private void RegisterScreens ()
        {
            _container.RegisterType<object, NewCropView> ( "NewCropView" );
            _container.RegisterType<object, NewCropModalView> ( "NewCropModalView" );
            _container.RegisterType<object, NewCropButtonsView> ( "NewCropButtonsView" );
        }

        private void RegisterServices ()
        {
            var genericsToApply = KnownTypeHelper.GetGenerics ( typeof( Const ).Assembly );
            KnownTypeHelper.RegisterNonGenericRequestsAndResponses ( typeof( NewCropModule ).Assembly );
            KnownTypeHelper.RegisterGenerics ( genericsToApply, typeof( NewCropModule ).Assembly );

            _container.RegisterType<INewCropSessionLauncher, NewCropSessionLauncher> ();
            _accessControlManager.RegisterPermissionDescriptor (
                new ClientPermissionDescriptor () );
            _metadataService.LoadMetadataForModule ( this );
        }

        #endregion
    }
}
