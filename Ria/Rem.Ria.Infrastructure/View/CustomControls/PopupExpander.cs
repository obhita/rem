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
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for expanding popup.
    /// </summary>
    public class PopupExpander : ContentControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ButtonStyleProperty Property.
        /// </summary>
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register (
                "ButtonStyle",
                typeof( Style ),
                typeof( PopupExpander ),
                new PropertyMetadata ( new Style ( typeof( RadToggleButton ) ) ) );

        /// <summary>
        /// Dependency Property for CloseOnOutsideClickProperty Property.
        /// </summary>
        public static readonly DependencyProperty CloseOnOutsideClickProperty =
            DependencyProperty.Register (
                "CloseOnOutsideClick",
                typeof( bool ),
                typeof( PopupExpander ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( PopupExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HeaderProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register (
                "Header",
                typeof( object ),
                typeof( PopupExpander ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( PopupExpander ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for IsPopupOpenProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register (
                "IsPopupOpen",
                typeof( bool ),
                typeof( PopupExpander ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for MaxDropDownHeightProperty Property.
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register (
                "MaxDropDownHeight",
                typeof( double ),
                typeof( PopupExpander ),
                new PropertyMetadata ( double.MaxValue ) );

        /// <summary>
        /// Dependency Property for PopupTargetProperty Property.
        /// </summary>
        public static readonly DependencyProperty PopupTargetProperty =
            DependencyProperty.Register (
                "PopupTarget",
                typeof( FrameworkElement ),
                typeof( PopupExpander ),
                new PropertyMetadata ( PopupTargetChanged ) );

        /// <summary>
        /// Dependency Property for PopupWidthProperty Property.
        /// </summary>
        public static readonly DependencyProperty PopupWidthProperty =
            DependencyProperty.Register (
                "PopupWidth",
                typeof( double ),
                typeof( PopupExpander ),
                new PropertyMetadata ( 400.0 ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupExpander"/> class.
        /// </summary>
        public PopupExpander ()
        {
            PopupTarget = this;
            DefaultStyleKey = typeof( PopupExpander );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the button style.
        /// </summary>
        /// <value>The button style.</value>
        public Style ButtonStyle
        {
            get { return ( Style )GetValue ( ButtonStyleProperty ); }
            set { SetValue ( ButtonStyleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [close on outside click].
        /// </summary>
        /// <value><c>true</c> if [close on outside click]; otherwise, <c>false</c>.</value>
        public bool CloseOnOutsideClick
        {
            get { return ( bool )GetValue ( CloseOnOutsideClickProperty ); }
            set { SetValue ( CloseOnOutsideClickProperty, value ); }
        }

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
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public object Header
        {
            get { return GetValue ( HeaderProperty ); }
            set { SetValue ( HeaderProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return ( DataTemplate )GetValue ( HeaderTemplateProperty ); }
            set { SetValue ( HeaderTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is popup open.
        /// </summary>
        /// <value><c>true</c> if this instance is popup open; otherwise, <c>false</c>.</value>
        public bool IsPopupOpen
        {
            get { return ( bool )GetValue ( IsPopupOpenProperty ); }
            set { SetValue ( IsPopupOpenProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the height of the max drop down.
        /// </summary>
        /// <value>The height of the max drop down.</value>
        public double MaxDropDownHeight
        {
            get { return ( double )GetValue ( MaxDropDownHeightProperty ); }
            set { SetValue ( MaxDropDownHeightProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the popup target.
        /// </summary>
        /// <value>The popup target.</value>
        public FrameworkElement PopupTarget
        {
            get { return ( FrameworkElement )GetValue ( PopupTargetProperty ); }
            set { SetValue ( PopupTargetProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the width of the popup.
        /// </summary>
        /// <value>The width of the popup.</value>
        public double PopupWidth
        {
            get { return ( double )GetValue ( PopupWidthProperty ); }
            set { SetValue ( PopupWidthProperty, value ); }
        }

        #endregion

        #region Methods

        private static void PopupTargetChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var expander = d as PopupExpander;
            if ( expander != null )
            {
                if ( e.OldValue != null )
                {
                    ( e.OldValue as FrameworkElement ).SizeChanged -= expander.PopupTargetSizeChanged;
                }
                if ( e.NewValue != null )
                {
                    ( e.NewValue as FrameworkElement ).SizeChanged += expander.PopupTargetSizeChanged;
                    expander.PopupTargetSizeChanged ( expander, null );
                }
            }
        }

        private void PopupTargetSizeChanged ( object sender, SizeChangedEventArgs e )
        {
            Dispatcher.BeginInvoke (
                () =>
                    {
                        if ( PopupTarget != null )
                        {
                            PopupWidth = PopupTarget.ActualWidth;
                        }
                    } );
        }

        #endregion
    }
}
