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

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.EmergencyAccess;
using Rem.Ria.Infrastructure.QuickPickers;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.Infrastructure
{
    /// <summary>
    /// InfrastructureModule class.
    /// </summary>
    public class InfrastructureModule : IModule
    {
        #region Constants and Fields

        private const string HeaderRightRegion = "HeaderRightRegion";
        private readonly IAccessControlManager _accessControlManager;
        private readonly IUnityContainer _container;
        private readonly IMetadataService _metadataService;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="container">The container.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        /// <param name="metadataService">The metadata service.</param>
        public InfrastructureModule (
            IRegionManager regionManager,
            IUnityContainer container,
            IAccessControlManager accessControlManager,
            IMetadataService metadataService )
        {
            _regionManager = regionManager;
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
            // DON'T switch the order of these two lines: register services first
            RegisterServices ();
            RegisterScreens ();
        }

        #endregion

        #region Methods

        private void RegisterScreens ()
        {
            _regionManager.RegisterViewWithRegion ( HeaderRightRegion, () => _container.Resolve<EmergencyAccessView> () );

            _regionManager.RegisterViewWithRegion (
                "LookupQuickPicker",
                () => _container.Resolve<LookupQuickPickerView> () );
        }

        private void RegisterServices ()
        {
            _metadataService.LoadMetadataForModule ( this );
            _accessControlManager.RegisterPermissionDescriptor (
                new ClientPermissionDescriptor () );
        }

        #endregion
    }
}
