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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Utility;
using Rem.Ria.Infrastructure.Security;
using Rem.Ria.Infrastructure.View.Configuration;
using Rem.Ria.Infrastructure.View.CustomControls;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Infrastructure.Service
{
    /// <summary>
    /// PopupService class.
    /// </summary>
    public class PopupService : IPopupService
    {
        #region Constants and Fields

        private readonly Dictionary<string, IPopup> _popupWindows = new Dictionary<string, IPopup>();
        private readonly IRegionNavigationContentLoader _regionNavigationContentLoader;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupService"/> class.
        /// </summary>
        /// <param name="regionNavigationContentLoader">The region navigation content loader.</param>
        public PopupService ( IRegionNavigationContentLoader regionNavigationContentLoader )
        {
            _regionNavigationContentLoader = regionNavigationContentLoader;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Closes the popup.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public bool ClosePopup ( string viewName )
        {
            var popupClosed = false;
            if ( _popupWindows.ContainsKey ( viewName ) )
            {
                _popupWindows[viewName].Close ();
                popupClosed = true;
            }
            return popupClosed;
        }


        /// <summary>
        /// Shows the popup.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="title">The title.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="maximize">If set to <c>true</c> [maximize].</param>
        /// <param name="completedAction">The completed action.</param>
        /// <param name="isModal">If set to <c>true</c> [is modal].</param>
        public void ShowPopup(
            string viewName,
            string commandName = null,
            string title = null,
            KeyValuePair<string, string>[] parameters = null,
            bool maximize = false,
            Action completedAction = null,
            bool isModal = true)
        {
            var uriQuery = new UriQuery ();
            if ( commandName != null )
            {
                uriQuery.Add ( "CommandName", commandName );
            }
            if ( parameters != null )
            {
                foreach ( var parameter in parameters )
                {
                    uriQuery.Add ( parameter.Key, parameter.Value );
                }
            }

            ShowPopup ( viewName, new Uri ( viewName + uriQuery, UriKind.Relative ), title, completedAction ?? ( () => { } ), maximize, isModal );
        }

        #endregion

        #region Methods

        private void OnLoadContentException ( Exception exception )
        {
            if ( !( exception is SecurityNavigationAccessDeniedException ) )
            {
                throw exception;
            }
        }

        private void ShowPopup(string viewName, Uri uri, string title, Action completedAction, bool maximize = false, bool isModal = false)
        {
            if ( !_popupWindows.ContainsKey ( viewName ) )
            {
                var navigationContext = new NavigationContext ( null, uri );
                object view;
                try
                {
                    view = _regionNavigationContentLoader.LoadContent ( new Region (), navigationContext );
                }
                catch ( Exception e )
                {
                    OnLoadContentException ( e );
                    view = null;
                }
                if ( view != null )
                {
                    if ( view is FrameworkElement )
                    {
                        var navigatable = ( view as FrameworkElement ).DataContext as INavigationAware;
                        if ( navigatable != null )
                        {
                            navigatable.OnNavigatedTo ( navigationContext );
                        }
                    }

                    var configurationBehaviorService = new ConfigurationBehaviorService();
                    if ( isModal )
                    {
                        var popupWindow = new PopupWindow();
                        popupWindow.IsMaximized = maximize;
                        if (title == null)
                        {
                            SetupPopupWindowTitle ( popupWindow, view );
                        }
                        else
                        {
                            popupWindow.Title = title;
                        }
                        
                        popupWindow.SubContent = view;
                        popupWindow.IsModal = isModal;
                        _popupWindows.Add(viewName, popupWindow);
                        
                        configurationBehaviorService.Watch(popupWindow);
                        EventHandler closedEventHandler = null;
                        closedEventHandler = (s, e) =>
                        {
                            popupWindow.Closed -= closedEventHandler;
                            configurationBehaviorService.Stop();
                            _popupWindows.Remove(viewName);
                            completedAction.Invoke();
                        };
                        popupWindow.Closed += closedEventHandler;
                        popupWindow.Show();
                    }
                    else
                    {
                        var floatableWindow = new FloatableWindow ();

                        if( floatableWindow.Title == null)
                        {
                            SetupPopupWindowTitle ( floatableWindow, view );
                        }
                        else
                        {
                            floatableWindow.Title = title;
                        }
                        
                        floatableWindow.Content = view;
                        _popupWindows.Add(viewName, floatableWindow);
                        configurationBehaviorService.Watch(floatableWindow);

                        EventHandler closedEventHandler = null;
                        closedEventHandler = (s, e) =>
                        {
                            floatableWindow.Closed -= closedEventHandler;
                            configurationBehaviorService.Stop();
                            _popupWindows.Remove(viewName);
                            completedAction.Invoke();
                        };
                        floatableWindow.Closed += closedEventHandler;
                        floatableWindow.Show();
                    }
                }
            }
        }

        private void SetupPopupWindowTitle(ContentControl popupWindow, object view)
        {
            if ( view is FrameworkElement && ( view as FrameworkElement ).DataContext is IPopupTitleProvider )
            {
                if ( popupWindow is PopupWindow )
                {
                    ( popupWindow as PopupWindow ).SetBinding (
                        PopupWindow.TitleProperty,
                        new Binding
                            {
                                Source = ( view as FrameworkElement ).DataContext,
                                Path = new PropertyPath ( PropertyUtil.ExtractPropertyName<IPopupTitleProvider, string> ( p => p.PopupTitle ) )
                            } );
                }

                if ( popupWindow is FloatableWindow )
                {
                    ( popupWindow as FloatableWindow ).SetBinding (
                        FloatableWindow.TitleProperty,
                        new Binding
                            {
                                Source = ( view as FrameworkElement ).DataContext,
                                Path = new PropertyPath ( PropertyUtil.ExtractPropertyName<IPopupTitleProvider, string> ( p => p.PopupTitle ) )
                            } );
                }
            }
        }

        #endregion
    }
}
