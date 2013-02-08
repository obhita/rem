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
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.Security;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// NavigationService class.
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Constants and Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public NavigationService ( IRegionManager regionManager )
        {
            _regionManager = regionManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Navigates the specified region manager.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="parameters">The parameters.</param>
        public void Navigate (
            IRegionManager regionManager,
            string regionName,
            string viewName,
            string commandName = null,
            params KeyValuePair<string, string>[] parameters )
        {
            var uriQuery = new UriQuery ();
            if ( commandName != null )
            {
                uriQuery.Add ( "CommandName", commandName );
            }
            foreach ( var parameter in parameters.Where ( parameter => parameter.Value != null ) )
            {
                uriQuery.Add ( parameter.Key, parameter.Value );
            }

            var uri = new Uri ( viewName + uriQuery, UriKind.Relative );

            regionManager.RequestNavigate ( regionName, uri, NavigationCallBack );
        }

        /// <summary>
        /// Navigates the specified region name.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="parameters">The parameters.</param>
        public void Navigate ( string regionName, string viewName, string commandName = null, params KeyValuePair<string, string>[] parameters )
        {
            Navigate ( _regionManager, regionName, viewName, commandName, parameters );
        }

        /// <summary>
        /// Navigates to active view.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="parameters">The parameters.</param>
        public void NavigateToActiveView ( string regionName, string commandName = null, params KeyValuePair<string, string>[] parameters )
        {
            NavigateToActiveView ( _regionManager, regionName, commandName, parameters );
        }

        /// <summary>
        /// Navigates to active view.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="parameters">The parameters.</param>
        public void NavigateToActiveView (
            IRegionManager regionManager, string regionName, string commandName = null, params KeyValuePair<string, string>[] parameters )
        {
            var uriQuery = new UriQuery ();
            if ( commandName != null )
            {
                uriQuery.Add ( "CommandName", commandName );
            }
            foreach ( var parameter in parameters )
            {
                uriQuery.Add ( parameter.Key, parameter.Value );
            }
            regionManager.NavigateToActiveView ( regionName, uriQuery, NavigationCallBack );
        }

        #endregion

        #region Methods

        private void NavigationCallBack ( NavigationResult navigationResult )
        {
            if ( navigationResult.Error != null )
            {
                if ( !( navigationResult.Error is SecurityNavigationAccessDeniedException ) )
                {
                    throw navigationResult.Error;
                }
            }
        }

        #endregion
    }
}
