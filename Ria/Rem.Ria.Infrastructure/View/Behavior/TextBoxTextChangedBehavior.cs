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
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing text box text changed.
    /// </summary>
    public class TextBoxTextChangedBehavior : Behavior<TextBox>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for TextProperty Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register (
                "Text",
                typeof( string ),
                typeof( TextBoxTextChangedBehavior ),
                new PropertyMetadata ( string.Empty, TextPropertyChangedCallback ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text of the text box.</value>
        public string Text
        {
            get { return ( string )GetValue ( TextProperty ); }
            set { SetValue ( TextProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
            UpdateAssociatedObjectText ();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
        }

        private static void TextPropertyChangedCallback ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var textBoxBehaviro = d as TextBoxTextChangedBehavior;
            if ( textBoxBehaviro != null && textBoxBehaviro.AssociatedObject != null )
            {
                textBoxBehaviro.UpdateAssociatedObjectText ();
            }
        }

        private void AssociatedObject_TextChanged ( object sender, TextChangedEventArgs e )
        {
            Text = AssociatedObject.Text;
        }

        private void UpdateAssociatedObjectText ()
        {
            if ( Text != null )
            {
                AssociatedObject.Text = Text;
            }
            else
            {
                AssociatedObject.Text = string.Empty;
            }
        }

        #endregion
    }
}
