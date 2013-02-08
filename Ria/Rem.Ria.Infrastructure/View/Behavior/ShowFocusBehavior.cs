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
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for the behavior of showing a backround color when focused.
    /// </summary>
    public class ShowFocusBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for FocusBackgroundBrushProperty Property.
        /// </summary>
        public static readonly DependencyProperty FocusBackgroundBrushProperty =
            DependencyProperty.Register (
                "FocusBackgroundBrush",
                typeof( Brush ),
                typeof( ShowFocusBehavior ),
                new PropertyMetadata ( new SolidColorBrush ( Colors.LightGray ) ) );

        /// <summary>
        /// Dependency Property for PaddingProperty Property.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register (
                "Padding",
                typeof( Thickness ),
                typeof( ShowFocusBehavior ),
                new PropertyMetadata ( new Thickness ( 0 ) ) );

        private Brush _brushCache;
        private Thickness _paddingCache;
        private bool _isFocused;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the focus background brush.
        /// </summary>
        /// <value>The focus background brush.</value>
        public Brush FocusBackgroundBrush
        {
            get { return ( Brush )GetValue ( FocusBackgroundBrushProperty ); }
            set { SetValue ( FocusBackgroundBrushProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get { return ( Thickness )GetValue ( PaddingProperty ); }
            set { SetValue ( PaddingProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            if ( ! ( AssociatedObject is Panel || AssociatedObject is Control ) )
            {
                throw new InvalidOperationException ( "Type must be either derive from Panel or Control." );
            }
            base.OnAttached ();
            AssociatedObject.GotFocus += GotFocus;
            AssociatedObject.LostFocus += LostFocus;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.GotFocus -= GotFocus;
            AssociatedObject.LostFocus -= LostFocus;
        }

        private void GotFocus ( object sender, RoutedEventArgs e )
        {
            if ( !_isFocused )
            {
                if ( AssociatedObject is Panel )
                {
                    _brushCache = ( AssociatedObject as Panel ).Background;
                    ( AssociatedObject as Panel ).Background = FocusBackgroundBrush;
                    if ( AssociatedObject is EditableGrid && ReadLocalValue ( PaddingProperty ) != DependencyProperty.UnsetValue )
                    {
                        _paddingCache = ( AssociatedObject as EditableGrid ).Padding;
                        ( AssociatedObject as EditableGrid ).Padding = Padding;
                    }
                }
                else
                {
                    _brushCache = ( AssociatedObject as Control ).Background;
                    ( AssociatedObject as Control ).Background = FocusBackgroundBrush;
                    if (ReadLocalValue(PaddingProperty) != DependencyProperty.UnsetValue)
                    {
                        _paddingCache = ( AssociatedObject as Control ).Padding;
                        ( AssociatedObject as Control ).Padding = Padding;
                    }
                }
                _isFocused = true;
            }
        }

        private bool IsKeyboardFocusWithinAssociatedObject ()
        {
            var curFocus = FocusManager.GetFocusedElement ();
            return this == curFocus || AssociatedObject.IsChild ( curFocus );
        }

        private void LostFocus ( object sender, RoutedEventArgs e )
        {
            if ( _isFocused && !IsKeyboardFocusWithinAssociatedObject () )
            {
                if (AssociatedObject is Panel)
                {
                    (AssociatedObject as Panel).Background = _brushCache;

                    if (AssociatedObject is EditableGrid && ReadLocalValue(PaddingProperty) != DependencyProperty.UnsetValue)
                    {
                        (AssociatedObject as EditableGrid).Padding = _paddingCache;
                    }
                }
                else
                {
                    ( AssociatedObject as Control ).Background = _brushCache;
                    if ( ReadLocalValue ( PaddingProperty ) != DependencyProperty.UnsetValue )
                    {
                        ( AssociatedObject as Control ).Padding = _paddingCache;
                    }
                }
                _brushCache = null;
                _paddingCache = new Thickness(0);
                _isFocused = false;
            }
        }

        #endregion
    }
}
