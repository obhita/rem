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
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using Microsoft.Practices.Prism.Regions;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing close view.
    /// </summary>
    public class CloseViewBehavior : Behavior<FrameworkElement>
    {
        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.Loaded += AssociatedObjectLoaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            var viewModel = AssociatedObject.DataContext;

            if ( viewModel is IViewClosable )
            {
                var view = viewModel as IViewClosable;
                view.ViewClosing -= CloseView;
            }
        }

        private void AssociatedObjectLoaded ( object sender, RoutedEventArgs e )
        {
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            var viewModel = AssociatedObject.DataContext;

            if ( viewModel is IViewClosable )
            {
                var view = viewModel as IViewClosable;
                view.ViewClosing += CloseView;
            }
        }

        private void CloseView ( object sender, EventArgs eventArgs )
        {
            var view = AssociatedObject;
            var regionManager = RegionManager.GetRegionManager ( AssociatedObject );

            RemoveView ( view, regionManager );
        }

        private IRegionManager FindParentRegionManager ( DependencyObject view )
        {
            var element = VisualTreeHelper.GetParent ( view );
            var regionManager = RegionManager.GetRegionManager ( element ) ?? FindParentRegionManager ( element );

            return regionManager;
        }

        private void RemoveView ( FrameworkElement view, IRegionManager regionManager )
        {
            if ( view != null )
            {
                var isViewInRegion = false;
                if ( regionManager != null )
                {
                    foreach ( var region in regionManager.Regions )
                    {
                        if ( region.ActiveViews.Contains ( view ) )
                        {
                            // Retain a reference to the Context before removing the View from the Region.
                            // This is necessary because PRISM sets the Region Context to NULL when a view is removed from it.
                            var context = region.Context;

                            region.Remove ( view );
                            isViewInRegion = true;

                            region.Context = context;

                            break;
                        }
                    }

                    if ( !isViewInRegion )
                    {
                        regionManager = FindParentRegionManager ( view );
                        RemoveView ( view, regionManager );
                    }
                }
            }
        }

        #endregion
    }
}
