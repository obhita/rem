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

using System.Windows;
using System.Windows.Media;
using Rem.Ria.Infrastructure.View.Behavior;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ShowFocus class.
    /// </summary>
    public class ShowFocus : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for FocusBackgroundProperty Property.
        /// </summary>
        public static readonly DependencyProperty FocusBackgroundProperty =
            DependencyProperty.RegisterAttached (
                "FocusBackground",
                typeof( Brush ),
                typeof( ShowFocus ),
                new PropertyMetadata ( new SolidColorBrush ( Colors.LightGray ), OnFocusBackgroundChanged ) );

        /// <summary>
        /// Dependency Property for PaddingProperty Property.
        /// </summary>
        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.RegisterAttached (
                "Padding",
                typeof( Thickness ),
                typeof( ShowFocus ),
                new PropertyMetadata ( new Thickness(0), OnPaddingChanged ) );

        private static readonly DependencyProperty FocusBehaviorProperty =
            DependencyProperty.RegisterAttached (
                "FocusBehavior",
                typeof( ShowFocusBehavior ),
                typeof( ShowFocus ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the focus background.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.Windows.Media.Brush"/></returns>
        public static Brush GetFocusBackground ( DependencyObject obj )
        {
            return ( Brush )obj.GetValue ( FocusBackgroundProperty );
        }

        /// <summary>
        /// Gets the padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.Windows.Thickness"/></returns>
        public static Thickness GetPadding ( DependencyObject obj )
        {
            return ( Thickness )obj.GetValue ( PaddingProperty );
        }

        /// <summary>
        /// Sets the focus background.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetFocusBackground ( DependencyObject obj, Brush value )
        {
            obj.SetValue ( FocusBackgroundProperty, value );
        }

        /// <summary>
        /// Sets the padding.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetPadding ( DependencyObject obj, Thickness value )
        {
            obj.SetValue ( PaddingProperty, value );
        }

        #endregion

        #region Methods

        private static ShowFocusBehavior GetFocusBehavior ( DependencyObject obj )
        {
            return ( ShowFocusBehavior )obj.GetValue ( FocusBehaviorProperty );
        }

        private static void OnFocusBackgroundChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ( sender is FrameworkElement )
            {
                var focusBehavior = GetFocusBehavior ( sender );
                if ( e.NewValue == null )
                {
                    if ( focusBehavior != null )
                    {
                        focusBehavior.Detach ();
                        SetFocusBehavior ( sender, null );
                    }
                }
                else
                {
                    if ( focusBehavior == null )
                    {
                        focusBehavior = new ShowFocusBehavior ();
                        focusBehavior.Attach ( sender as FrameworkElement );
                        SetFocusBehavior ( sender, focusBehavior );
                        if ( sender.ReadLocalValue ( PaddingProperty ) != DependencyProperty.UnsetValue )
                        {
                            focusBehavior.Padding = GetPadding ( sender );
                        }
                    }
                    focusBehavior.FocusBackgroundBrush = GetFocusBackground ( sender );
                }
            }
        }

        private static void OnPaddingChanged ( DependencyObject sender, DependencyPropertyChangedEventArgs e )
        {
            var focusBehavior = GetFocusBehavior ( sender );
            if (focusBehavior != null)
            {
                focusBehavior.Padding = GetPadding ( sender );
            }
        }

        private static void SetFocusBehavior ( DependencyObject obj, ShowFocusBehavior value )
        {
            obj.SetValue ( FocusBehaviorProperty, value );
        }

        #endregion
    }
}
