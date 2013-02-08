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
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// RemCurrencyTextBox class.
    /// </summary>
    public class RemCurrencyTextBox : RadMaskedTextBox
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CanBeNegativeProperty Property.
        /// </summary>
        public static readonly DependencyProperty CanBeNegativeProperty =
            DependencyProperty.Register (
                "CanBeNegative",
                typeof( bool ),
                typeof( RemCurrencyTextBox ),
                new PropertyMetadata ( true ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemCurrencyTextBox"/> class.
        /// </summary>
        public RemCurrencyTextBox ()
        {
            Mask = "C";
            MaskType = MaskType.Numeric;
            SelectionOnFocus = SelectionOnFocus.SelectAll;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance can be negative.
        /// </summary>
        /// <value><c>true</c> if this instance can be negative; otherwise, <c>false</c>.</value>
        public bool CanBeNegative
        {
            get { return ( bool )GetValue ( CanBeNegativeProperty ); }
            set { SetValue ( CanBeNegativeProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.KeyDown"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ( !CanBeNegative && ( e.Key == Key.Subtract || ( e.Key == Key.Unknown && e.PlatformKeyCode == 189 ) ) )
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown ( e );
            }
        }

        #endregion
    }
}
