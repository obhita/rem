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
using Microsoft.Practices.Prism.Regions;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// NotificationService class.
    /// </summary>
    public class NotificationService : INotificationService
    {
        #region Constants and Fields

        private readonly IRegionManager _regionManager;
        private readonly IRegionNavigationContentLoader _regionNavigationContentLoader;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationService"/> class.
        /// </summary>
        /// <param name="regionNavigationContentLoader">The region navigation content loader.</param>
        /// <param name="regionManager">The region manager.</param>
        public NotificationService (
            IRegionNavigationContentLoader regionNavigationContentLoader,
            IRegionManager regionManager )
        {
            _regionNavigationContentLoader = regionNavigationContentLoader;
            _regionManager = regionManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <param name="viewUri">The view URI.</param>
        public void ShowNotification ( Uri viewUri )
        {
            var region = _regionManager.Regions["NotificationsRegion"];
            InternalShowNotification ( region, viewUri );
        }

        /// <summary>
        /// Shows the notification popup.
        /// </summary>
        /// <param name="viewUri">The view URI.</param>
        public void ShowNotificationPopup ( Uri viewUri )
        {
            var popupWindow = new PopupWindow ();
            popupWindow.SubContent = InternalShowNotification ( new Region (), viewUri );
            popupWindow.Show ();
        }

        #endregion

        #region Methods

        private object InternalShowNotification ( IRegion region, Uri viewUri )
        {
            var navigationContext = new NavigationContext ( region.NavigationService, viewUri );
            var view = _regionNavigationContentLoader.LoadContent ( region, navigationContext );
            if ( view is FrameworkElement )
            {
                var navigatable = ( view as FrameworkElement ).DataContext as INavigationAware;
                if ( navigatable != null )
                {
                    navigatable.OnNavigatedTo ( navigationContext );
                }
            }
            return view;
        }

        #endregion
    }
}
