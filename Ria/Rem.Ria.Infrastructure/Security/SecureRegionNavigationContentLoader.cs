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
using System.Diagnostics;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.InversionOfControl;
using Pillar.Security.AccessControl;
using RegionNavigationContentLoader = Rem.Ria.Infrastructure.Navigation.RegionNavigationContentLoader;

namespace Rem.Ria.Infrastructure.Security
{
    /// <summary>
    /// Class for loading secure region navigation content.
    /// </summary>
    public class SecureRegionNavigationContentLoader : RegionNavigationContentLoader
    {
        #region Constants and Fields

        private readonly IAccessControlManager _accessControlManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecureRegionNavigationContentLoader"/> class with a service locator.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="accessControlManager">The access control manager.</param>
        public SecureRegionNavigationContentLoader ( IContainer container, IAccessControlManager accessControlManager )
            : base ( container )
        {
            _accessControlManager = accessControlManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the view to which the navigation request represented by <paramref name="navigationContext"/> applies.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="navigationContext">The context representing the navigation request.</param>
        /// <returns>The view to be the target of the navigation request.</returns>
        /// <exception cref="ArgumentException">When a new view cannot be created for the navigation request.</exception>   
        /// <exception cref="InvalidOperationException">When user does not have permission to navigate to view.</exception>
        [DebuggerHidden]
        public override object LoadContent ( IRegion region, NavigationContext navigationContext )
        {
            var view = base.LoadContent ( region, navigationContext );

            var type = view.GetType ();

            if ( !_accessControlManager.CanAccess ( new ResourceRequest { type.FullName } ) )
            {
                //Need to reset the Context because it gets cleared when a view is removed from the region
                var regionContext = region.Context;
                region.Remove ( view );
                region.Context = regionContext;
                throw new SecurityNavigationAccessDeniedException ( "Current user does not have access to view:" + type );
            }

            return view;
        }

        #endregion
    }
}
