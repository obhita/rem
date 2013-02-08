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
using System.Windows.Data;
using System.Windows.Input;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.View.Behavior;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Primitives;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// PopupHeader class.
    /// </summary>
    public class PopupHeader : Popup
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for PlacementTargetProperty Property.
        /// </summary>
        public static new readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register (
                "PlacementTarget",
                typeof( UIElement ),
                typeof( PopupHeader ),
                new PropertyMetadata ( null, PlacementTargetChanged ) );

        /// <summary>
        /// Dependency Property for ShownWhenProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShownWhenProperty =
            DependencyProperty.Register (
                "ShownWhen",
                typeof( OpenOptions ),
                typeof( PopupHeader ),
                new PropertyMetadata ( OpenOptions.TargetHasFocus, ShownWhenChanged ) );

        /// <summary>
        /// Dependency Property for HasFocusProperty Property.
        /// </summary>
        public static readonly DependencyProperty TargetHasFocusProperty =
            DependencyProperty.Register (
                "TargetHasFocus",
                typeof( bool ),
                typeof( PopupHeader ),
                new PropertyMetadata ( false ) );

        private bool _isLoaded;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupHeader"/> class.
        /// </summary>
        public PopupHeader ()
        {
            Closed += ( sender, args ) => HandleShowWhenChanged ();
            Opened += PopupOpened;
            Loaded += ( sender, args ) =>
                {
                    _isLoaded = true;
                    HandleShowWhenChanged ();
                };
            Unloaded += ( sender, args ) => { _isLoaded = false; };

            Placement = PlacementMode.Top;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        #endregion

        #region Enums

        /// <summary>
        /// Options for when to open popup.
        /// </summary>
        public enum OpenOptions
        {
            /// <summary>
            /// Open when Target Control has Focus.
            /// </summary>
            TargetHasFocus,

            /// <summary>
            /// Always open.
            /// </summary>
            Always
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the placement target.
        /// </summary>
        /// <value>The placement target.</value>
        public new UIElement PlacementTarget
        {
            get { return ( UIElement )GetValue ( PlacementTargetProperty ); }
            set { SetValue ( PlacementTargetProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the shown when.
        /// </summary>
        /// <value>The shown when.</value>
        public OpenOptions ShownWhen
        {
            get { return ( OpenOptions )GetValue ( ShownWhenProperty ); }
            set { SetValue ( ShownWhenProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has focus.
        /// </summary>
        /// <value><c>true</c> if this instance has focus; otherwise, <c>false</c>.</value>
        public bool TargetHasFocus
        {
            get { return ( bool )GetValue ( TargetHasFocusProperty ); }
            set { SetValue ( TargetHasFocusProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            CloseOnOutsideClick = false;
            CatchClickOutsidePopup = false;
        }

        #endregion

        #region Methods

        private void PopupOpened(object sender, EventArgs args)
        {
            if((RealPopup.Child as FrameworkElement).ActualWidth < Owner.ActualWidth)
            {
                switch ( HorizontalContentAlignment )
                {
                    case HorizontalAlignment.Stretch:
                        ( RealPopup.Child as FrameworkElement ).Width = Owner.ActualWidth;
                        break;
                    case HorizontalAlignment.Right:
                        RealPopup.HorizontalOffset += Owner.ActualWidth - (RealPopup.Child as FrameworkElement).ActualWidth;
                        break;
                    case HorizontalAlignment.Center:
                        RealPopup.HorizontalOffset += (Owner.ActualWidth - (RealPopup.Child as FrameworkElement).ActualWidth) / 2.0;
                        break;
                }
            }
            HandleShowWhenChanged ();
        }

        private static void PlacementTargetChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var popupHeader = sender as PopupHeader;
            if ( popupHeader != null )
            {
                popupHeader.HandlePlacementTargetChanged ( e.NewValue as UIElement, e.OldValue as UIElement );
            }
        }

        private static void ShownWhenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var popupHeader = d as PopupHeader;
            if ( popupHeader != null )
            {
                popupHeader.ClearValue ( IsOpenProperty );
                popupHeader.HandleShowWhenChanged ();
            }
        }

        private void HandlePlacementTargetChanged(UIElement newValue, UIElement oldValue)
        {
            if ( oldValue != null )
            {
                oldValue.GotFocus -= TargetGotFocus;
                oldValue.LostFocus -= TargetLostFocus;
            }
            if ( newValue != null )
            {
                newValue.GotFocus += TargetGotFocus;
                newValue.LostFocus += TargetLostFocus;
            }
            base.PlacementTarget = newValue;
            Owner = newValue as FrameworkElement;
        }

        private void HandleShowWhenChanged()
        {
            if (_isLoaded)
            {
                switch ( ShownWhen )
                {
                    case OpenOptions.TargetHasFocus:
                        SetBinding (
                            IsOpenProperty, new Binding { Source = this, Path = new PropertyPath ( "TargetHasFocus" ), Mode = BindingMode.OneWay } );
                        break;
                    case OpenOptions.Always:
                        //If you do not invoke this on the dispatcher it does not work and I have no idea why!?!?!?!?!!?
                        Dispatcher.BeginInvoke ( () => IsOpen = true );
                        break;
                }
            }
        }

        private bool IsKeyboardFocusWithinTarget()
        {
            object curFocus = FocusManager.GetFocusedElement ();
            return this == curFocus || PlacementTarget.IsChild ( curFocus );
        }

        private void TargetGotFocus(object sender, RoutedEventArgs e)
        {
            TargetHasFocus = true;
        }

        private void TargetLostFocus(object sender, RoutedEventArgs e)
        {
            if ( !IsKeyboardFocusWithinTarget () )
            {
                TargetHasFocus = false;
            }
        }

        #endregion

        #region HeaderTemplate Attached Property

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.RegisterAttached (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( PopupHeader ),
                new PropertyMetadata ( null, OnHeaderTemplateChanged ) );

        /// <summary>
        /// Sets the header template.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetHeaderTemplate ( DependencyObject obj, DataTemplate value )
        {
            obj.SetValue ( HeaderTemplateProperty, value );
        }

        /// <summary>
        /// Gets the header template.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.Windows.DataTemplate"/></returns>
        public static DataTemplate GetHeaderTemplate ( DependencyObject obj )
        {
            return ( DataTemplate )obj.GetValue ( HeaderTemplateProperty );
        }

        private static void OnHeaderTemplateChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            var frameworkElement = sender as FrameworkElement;
            if(frameworkElement != null)
            {
                var headerTemplate = GetHeaderTemplate(sender);
                if(headerTemplate == null)
                {
                    foreach ( var behavior in frameworkElement.GetBehaviors<PopupHeaderBehavior> () )
                    {
                        behavior.Detach ();
                    }
                }
                else if (!frameworkElement.HasBehavior<PopupHeaderBehavior> ())
                {
                    var popupHeaderBehavior = new PopupHeaderBehavior { HeaderTemplate = headerTemplate };
                    popupHeaderBehavior.Attach ( frameworkElement );
                }
            }
        }

        #endregion
    }
}
