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
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// NotificationTongue class.
    /// </summary>
    public class NotificationTongue : ItemsControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( NotificationTongue ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for MaxExpandedHeightProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxExpandedHeightProperty =
            DependencyProperty.Register (
                "MaxExpandedHeight",
                typeof( double ),
                typeof( NotificationTongue ),
                new PropertyMetadata ( 400.0 ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationTongue"/> class.
        /// </summary>
        public NotificationTongue ()
        {
            DefaultStyleKey = typeof( NotificationTongue );
            Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue ( CornerRadiusProperty ); }
            set { SetValue ( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the height of the max expanded.
        /// </summary>
        /// <value>The height of the max expanded.</value>
        public double MaxExpandedHeight
        {
            get { return ( double )GetValue ( MaxExpandedHeightProperty ); }
            set { SetValue ( MaxExpandedHeightProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows the detail popup.
        /// </summary>
        /// <param name="content">The content.</param>
        public void ShowDetailPopup ( object content )
        {
            var popupWindow = new PopupWindow ();
            popupWindow.SubContent = content;
            var closeViewBinding = new Binding ();
            closeViewBinding.Source = content;
            closeViewBinding.Path = new PropertyPath ( "DataContext.CloseViewCommand" );
            popupWindow.SetBinding ( PopupWindow.ClosingCommandProperty, closeViewBinding );
            popupWindow.Show ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>The element that is used to display the given item.</returns>
        protected override DependencyObject GetContainerForItemOverride ()
        {
            var item = new NotificationItem ( this );

            return item;
        }

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is (or is eligible to be) its own container; otherwise, false.</returns>
        protected override bool IsItemItsOwnContainerOverride ( object item )
        {
            return false;
        }

        /// <summary>
        /// Called when the value of the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> that contains the event data</param>
        protected override void OnItemsChanged ( NotifyCollectionChangedEventArgs e )
        {
            base.OnItemsChanged ( e );
            Visibility = Items.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            if ( e.Action == NotifyCollectionChangedAction.Add )
            {
                EventHandler eh = null;
                eh = ( o, args ) =>
                    {
                        VisualStateManager.GoToState ( this, "MouseOver", true );
                        LayoutUpdated -= eh;
                    };
                LayoutUpdated += eh;
            }
        }

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.MouseEnter"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnMouseEnter ( MouseEventArgs e )
        {
            base.OnMouseEnter ( e );
            VisualStateManager.GoToState ( this, "MouseOver", true );
        }

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.MouseLeave"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnMouseLeave ( MouseEventArgs e )
        {
            base.OnMouseLeave ( e );
            VisualStateManager.GoToState ( this, "Normal", true );
        }

        #endregion
    }
}
