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
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using NLog;
using Pillar.Common.InversionOfControl;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// Class for loading region navigation content.
    /// </summary>
    public class RegionNavigationContentLoader : IRegionNavigationContentLoader
    {
        #region Constants and Fields

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger ();
        private readonly IContainer _container;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionNavigationContentLoader"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        public RegionNavigationContentLoader ( IContainer container )
        {
            _container = container;
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
        public virtual object LoadContent ( IRegion region, NavigationContext navigationContext )
        {
            if ( region == null )
            {
                throw new ArgumentNullException ( "region" );
            }
            if ( navigationContext == null )
            {
                throw new ArgumentNullException ( "navigationContext" );
            }
            _logger.Debug ( "Navigation request - Region: {0} - Uri: {1}", region.Name, navigationContext.Uri );

            var candidateTargetContract = GetContractFromNavigationContext ( navigationContext );

            var candidates = GetCandidatesFromRegion ( region, candidateTargetContract );

            var acceptingCandidates =
                candidates.Where (
                    v =>
                        {
                            var navigationAware = v as INavigationAware;
                            if ( navigationAware != null && !navigationAware.IsNavigationTarget ( navigationContext ) )
                            {
                                return false;
                            }

                            var frameworkElement = v as FrameworkElement;
                            if ( frameworkElement == null )
                            {
                                return true;
                            }

                            navigationAware = frameworkElement.DataContext as INavigationAware;
                            return navigationAware == null || navigationAware.IsNavigationTarget ( navigationContext );
                        } );

            var view = acceptingCandidates.FirstOrDefault ();

            if ( view != null )
            {
                return view;
            }

            view = CreateNewRegionItem ( candidateTargetContract );

            var createScopedRegion = region.Behaviors.ContainsKey ( "CreateScopedRegionBehavior" );
            var regionManager = region.Add ( view, null, createScopedRegion );

            TrySetRegionManager ( view, regionManager );

            return view;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Provides a new item for the region based on the supplied candidate target contract name.
        /// </summary>
        /// <param name="candidateTargetContract">The target contract to build.</param>
        /// <returns>An instance of an item to put into the <see cref="IRegion"/>.</returns>
        protected virtual object CreateNewRegionItem ( string candidateTargetContract )
        {
            var newRegionItem = _container.Resolve<object> ( candidateTargetContract );

            return newRegionItem;
        }

        /// <summary>
        /// Returns the set of candidates that may satisfiy this navigation request.
        /// </summary>
        /// <param name="region">The region containing items that may satisfy the navigation request.</param>
        /// <param name="candidateNavigationContract">The candidate navigation target as determined by <see cref="GetContractFromNavigationContext"/></param>
        /// <returns>An enumerable of candidate objects from the <see cref="IRegion"/></returns>
        protected virtual IEnumerable<object> GetCandidatesFromRegion ( IRegion region, string candidateNavigationContract )
        {
            if ( region == null )
            {
                throw new ArgumentNullException ( "region" );
            }
            return region.Views.Where (
                v =>
                string.Equals ( v.GetType ().Name, candidateNavigationContract, StringComparison.Ordinal ) ||
                string.Equals ( v.GetType ().FullName, candidateNavigationContract, StringComparison.Ordinal ) );
        }

        /// <summary>
        /// Returns the candidate TargetContract based on the <see cref="NavigationContext"/>.
        /// </summary>
        /// <param name="navigationContext">The navigation contract.</param>
        /// <returns>The candidate contract to seek within the <see cref="IRegion"/>
        /// and to use, if not found, when resolving from the container.</returns>
        protected virtual string GetContractFromNavigationContext ( NavigationContext navigationContext )
        {
            if ( navigationContext == null )
            {
                throw new ArgumentNullException ( "navigationContext" );
            }

            var candidateTargetContract = UriParsingHelper.GetAbsolutePath ( navigationContext.Uri );
            candidateTargetContract = candidateTargetContract.TrimStart ( '/' );
            return candidateTargetContract;
        }

        private static void TrySetRegionManager ( object view, IRegionManager regionManager )
        {
            var frameworkElement = view as FrameworkElement;
            if ( frameworkElement != null )
            {
                if ( frameworkElement.DataContext != null && frameworkElement.DataContext is ISetRegionManager )
                {
                    var control = frameworkElement.DataContext as ISetRegionManager;

                    control.RegionManager = regionManager;
                }
            }
        }

        #endregion
    }
}
