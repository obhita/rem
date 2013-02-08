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
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ExtendedChildWindow class.
    /// </summary>
    public class ExtendedChildWindow : ChildWindow
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for IsMaximizedProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register (
                "IsMaximized",
                typeof( bool ),
                typeof( ExtendedChildWindow ),
                new PropertyMetadata ( false, IsMaximizedChanged ) );

        /// <summary>
        /// Dependency Property for IsModalProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register (
                "IsModal",
                typeof( bool ),
                typeof( ExtendedChildWindow ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for IsOpenProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register (
                "IsOpen",
                typeof( bool ),
                typeof( ExtendedChildWindow ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for PlacementTargetProperty Property.
        /// </summary>
        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register (
                "PlacementTarget",
                typeof( FrameworkElement ),
                typeof( ExtendedChildWindow ),
                new PropertyMetadata ( null ) );

        private bool _isMaximized;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedChildWindow"/> class.
        /// </summary>
        public ExtendedChildWindow ()
        {
            DefaultStyleKey = typeof( ExtendedChildWindow );

            CloseCommand = new DelegateCommand ( Close );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the close command.
        /// </summary>
        public ICommand CloseCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is maximized.
        /// </summary>
        /// <value><c>true</c> if this instance is maximized; otherwise, <c>false</c>.</value>
        public bool IsMaximized
        {
            get { return ( bool )GetValue ( IsMaximizedProperty ); }
            set { SetValue ( IsMaximizedProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is modal.
        /// </summary>
        /// <value><c>true</c> if this instance is modal; otherwise, <c>false</c>.</value>
        public bool IsModal
        {
            get { return ( bool )GetValue ( IsModalProperty ); }
            set { SetValue ( IsModalProperty, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is open.
        /// </summary>
        public bool IsOpen
        {
            get { return ( bool )GetValue ( IsOpenProperty ); }
            private set { SetValue ( IsOpenProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the placement target.
        /// </summary>
        /// <value>The placement target.</value>
        public FrameworkElement PlacementTarget
        {
            get { return ( FrameworkElement )GetValue ( PlacementTargetProperty ); }
            set { SetValue ( PlacementTargetProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the visual tree for the <see cref="T:System.Windows.Controls.ChildWindow"/> control when a new template is applied.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            if ( PlacementTarget != null )
            {
                var centerScreen = new Point (
                    Application.Current.Host.Content.ActualWidth / 2.0, Application.Current.Host.Content.ActualHeight / 2.0 );
                var gt = PlacementTarget.TransformToVisual ( Application.Current.RootVisual );
                var offset = gt.Transform ( new Point ( 0, 0 ) );
                var controlTop = offset.Y;
                var controlCenter = offset.X + ( PlacementTarget.ActualWidth / 2.0 );
                var childWindowOffset = new Point ( controlCenter - ( ActualWidth / 2.0 ), controlTop - ActualHeight );
                if ( childWindowOffset.Y > Application.Current.Host.Content.ActualHeight )
                {
                    childWindowOffset.Y = Application.Current.Host.Content.ActualHeight;
                }
                if ( childWindowOffset.X + ActualWidth > Application.Current.Host.Content.ActualWidth )
                {
                    childWindowOffset.X = Application.Current.Host.Content.ActualWidth - ActualWidth;
                }
                childWindowOffset = new Point ( childWindowOffset.X - centerScreen.X, childWindowOffset.Y - centerScreen.Y );
                var contentRoot = GetTemplateChild ( "ContentRoot" ) as FrameworkElement;
                if ( contentRoot == null )
                {
                    return;
                }

                var group = contentRoot.RenderTransform as TransformGroup;
                if ( group == null )
                {
                    return;
                }

                TranslateTransform translateTransform = null;
                foreach ( var transform in group.Children.OfType<TranslateTransform> () )
                {
                    translateTransform = transform;
                }

                if ( translateTransform == null )
                {
                    return;
                }

                // reset transform
                translateTransform.X = childWindowOffset.X;
                translateTransform.Y = childWindowOffset.Y;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Resized event of the HostContent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void HostContent_Resized ( object sender, EventArgs e )
        {
            //Width = Application.Current.Host.Content.ActualWidth - 100;
            Height = Application.Current.Host.Content.ActualHeight - 100;
        }

        /// <summary>
        /// Maximizes this instance.
        /// </summary>
        protected virtual void Maximize ()
        {
            if ( !_isMaximized )
            {
                Application.Current.Host.Content.Resized += HostContent_Resized;
                HostContent_Resized ( null, null );
                _isMaximized = true;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Controls.ChildWindow.Closed"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnClosed ( EventArgs e )
        {
            base.OnClosed ( e );
            IsOpen = false;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Controls.ChildWindow.Closing"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        protected override void OnClosing ( CancelEventArgs e )
        {
            base.OnClosing ( e );
            Application.Current.Host.Content.Resized -= HostContent_Resized;
        }

        /// <summary>
        /// This method is called every time a <see cref="T:System.Windows.Controls.ChildWindow"/> is displayed.
        /// </summary>
        protected override void OnOpened ()
        {
            base.OnOpened ();
            IsOpen = true;
        }

        /// <summary>
        /// Uns the maximize.
        /// </summary>
        protected virtual void UnMaximize ()
        {
            if ( _isMaximized )
            {
                Width = double.NaN;
                Height = double.NaN;
                Application.Current.Host.Content.Resized -= HostContent_Resized;
                UpdateLayout ();
                _isMaximized = false;
            }
        }

        private static void IsMaximizedChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var extendedChildWindow = d as ExtendedChildWindow;
            if ( extendedChildWindow != null )
            {
                if ( ( bool )e.NewValue )
                {
                    extendedChildWindow.Maximize ();
                }
                else
                {
                    extendedChildWindow.UnMaximize ();
                }
            }
        }

        #endregion

        //TODO: fix this to handle maximize correclty with how we want this to work.
    }
}
