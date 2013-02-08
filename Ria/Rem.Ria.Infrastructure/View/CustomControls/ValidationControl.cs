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

using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ValidationControl class.
    /// </summary>
    [TemplateVisualState ( GroupName = "ValidationStates", Name = "Valid" )]
    [TemplateVisualState ( GroupName = "ValidationStates", Name = "InvalidUnfocused" )]
    [TemplateVisualState ( GroupName = "ValidationStates", Name = "InvalidFocused" )]
    public class ValidationControl : Control
    {
        #region Constants and Fields

        private bool _hasFocus;

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();

            BindingValidationError += ( s, e ) => UpdateValidationState ();
            UpdateValidationState ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.GotFocus"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnGotFocus ( RoutedEventArgs e )
        {
            base.OnGotFocus ( e );
            if ( !_hasFocus )
            {
                _hasFocus = true;
                UpdateValidationState ();
            }
        }

        /// <summary>
        /// Called before the <see cref="E:System.Windows.UIElement.LostFocus"/> event occurs.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnLostFocus ( RoutedEventArgs e )
        {
            base.OnLostFocus ( e );
            if ( _hasFocus )
            {
                _hasFocus = false;
                UpdateValidationState ();
            }
        }

        private void UpdateValidationState ()
        {
            if ( Validation.GetErrors ( this ).Any () )
            {
                VisualStateManager.GoToState ( this, _hasFocus ? "InvalidFocused" : "InvalidUnfocused", true );
            }
            else
            {
                VisualStateManager.GoToState ( this, "Valid", true );
            }
        }

        #endregion
    }
}
