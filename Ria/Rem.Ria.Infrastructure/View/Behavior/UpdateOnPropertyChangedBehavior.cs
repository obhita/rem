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
using System.Windows.Controls;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing update on property changed.
    /// </summary>
    public class UpdateOnPropertyChangedBehavior
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for UpdateOnPropertyChangedProperty Property.
        /// </summary>
        public static readonly DependencyProperty UpdateOnPropertyChangedProperty = DependencyProperty.RegisterAttached (
            "UpdateOnPropertyChanged",
            typeof( bool ),
            typeof( UpdateOnPropertyChangedBehavior ),
            new PropertyMetadata ( UpdateOnPropertyChangedChanged ) );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the update on property changed.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool GetUpdateOnPropertyChanged ( DependencyObject obj )
        {
            return ( bool )obj.GetValue ( UpdateOnPropertyChangedProperty );
        }

        /// <summary>
        /// Sets the update on property changed.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="propertyValue">The property value.</param>
        public static void SetUpdateOnPropertyChanged ( DependencyObject obj, string propertyValue )
        {
            obj.SetValue ( UpdateOnPropertyChangedProperty, propertyValue );
        }

        #endregion

        #region Methods

        private static void TextChanged ( object sender, TextChangedEventArgs e )
        {
            var textBox = sender as TextBox;
            if ( textBox != null )
            {
                var bindingExpression = textBox.GetBindingExpression ( TextBox.TextProperty );
                if ( bindingExpression != null )
                {
                    bindingExpression.UpdateSource ();
                }
            }
        }

        private static void UpdateOnPropertyChangedChanged ( object sender, DependencyPropertyChangedEventArgs args )
        {
            var textBox = sender as TextBox;
            if ( textBox != null )
            {
                if ( ( bool )args.NewValue )
                {
                    textBox.TextChanged += TextChanged;
                }
                else
                {
                    textBox.TextChanged -= TextChanged;
                }
            }
        }

        #endregion
    }
}
