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
using System.Windows.Browser;
using System.Windows.Controls;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Common.Extension;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// SecureControl class.
    /// </summary>
    public class SecureControl : Control
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AccessControlManagerProperty Property.
        /// </summary>
        public static readonly DependencyProperty AccessControlManagerProperty =
            DependencyProperty.Register (
                "AccessControlManager",
                typeof( IAccessControlManager ),
                typeof( SecureControl ),
                new PropertyMetadata ( null, AccessControlManagerChanged ) );

        /// <summary>
        /// Dependency Property for VisibilityProperty Property.
        /// </summary>
        public static new readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register (
                "Visibility",
                typeof( Visibility ),
                typeof( SecureControl ),
                new PropertyMetadata ( Visibility.Visible, VisibilityChanged ) );

        private bool _canAccess = true;

        private ResourceRequest _resourceRequest;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [can access changed].
        /// </summary>
        public event EventHandler CanAccessChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the access control manager.
        /// </summary>
        /// <value>The access control manager.</value>
        public IAccessControlManager AccessControlManager
        {
            get { return ( IAccessControlManager )GetValue ( AccessControlManagerProperty ); }
            set { SetValue ( AccessControlManagerProperty, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can access.
        /// </summary>
        public bool CanAccess
        {
            get { return _canAccess; }
            private set
            {
                if ( _canAccess != value )
                {
                    _canAccess = value;
                    CanAccessChanged ( this, new EventArgs () );
                }
            }
        }

        /// <summary>
        /// Gets or sets the visibility of a <see cref="T:System.Windows.UIElement"/>. A <see cref="T:System.Windows.UIElement"/> that is not visible does not render and does not communicate its desired size to layout.
        /// </summary>
        /// <value>The visibility.</value>
        /// <returns>A value of the enumeration. The default value is <see cref="F:System.Windows.Visibility.Visible"/>.</returns>
        public new Visibility Visibility
        {
            get { return ( Visibility )GetValue ( VisibilityProperty ); }
            set { SetValue ( VisibilityProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            if ( string.IsNullOrEmpty ( Name ) )
            {
                throw new InvalidOperationException ( "A secure control requires the Name property to be set." );
            }

            //TODO: adding RadTileViewItem here but should be some cleaner way of adding more parent realm types
            var parentRealm = this.FindAncestor<UserControl> () ?? ( FrameworkElement )this.FindAncestor<RadTileViewItem> ();
            if ( parentRealm == null )
            {
                parentRealm = this.FindAncestor<ChildWindow> ();
            }

            //TODO: revisit this to possibly not require secure control to be in a user control
            if ( parentRealm == null && HtmlPage.IsEnabled )
            {
                throw new InvalidOperationException ( "A secure control must be a child of a user control or Child Window." );
            }
            _resourceRequest = new ResourceRequest { parentRealm.GetType ().FullName, ( string.IsNullOrEmpty ( Name ) ? string.Empty : Name ) };
            CheckRemAccess ();
        }

        #endregion

        #region Methods

        private static void AccessControlManagerChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var secureControl = d as SecureControl;
            if ( secureControl != null )
            {
                var oldValue = e.OldValue as IAccessControlManager;
                if ( oldValue != null )
                {
                    oldValue.CanAccessChanged -= secureControl.HandleCanAccessChanged;
                }
                var newValue = e.NewValue as IAccessControlManager;
                if ( newValue != null )
                {
                    newValue.CanAccessChanged += secureControl.HandleCanAccessChanged;
                }
            }
        }

        private static void VisibilityChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var secureControl = d as SecureControl;
            if ( secureControl != null )
            {
                secureControl.CheckRemAccess ();
                secureControl.UpdateBaseVisibility ();
            }
        }

        private void CheckRemAccess ()
        {
            if ( AccessControlManager == null && HtmlPage.IsEnabled )
            {
                throw new InvalidOperationException ( "A secure control requires the AccessControlManager properyt to be set." );
            }
            if ( AccessControlManager != null && !AccessControlManager.CanAccess ( _resourceRequest ) )
            {
                Visibility = Visibility.Collapsed;
                base.Visibility = Visibility;
                CanAccess = false;
            }
            else if ( Visibility == Visibility.Collapsed )
            {
                Visibility = Visibility.Visible;
                base.Visibility = Visibility;
                CanAccess = true;
            }
        }

        private void HandleCanAccessChanged ( object sender, EventArgs e )
        {
            Visibility = Visibility.Visible;
        }

        private void UpdateBaseVisibility ()
        {
            base.Visibility = Visibility;
        }

        #endregion
    }
}
