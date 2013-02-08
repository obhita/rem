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
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Class for picking scroll to element quick.
    /// </summary>
    public class ScrollToElementQuickPicker : ScrollToQuickPicker
    {
        #region Constants and Fields

        private static readonly DependencyProperty ElementProperty =
            DependencyProperty.Register (
                "Element",
                typeof( FrameworkElement ),
                typeof( ScrollToElementQuickPicker ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>The element.</value>
        private FrameworkElement Element
        {
            get { return ( FrameworkElement )GetValue ( ElementProperty ); }
            set { SetValue ( ElementProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the element.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="System.Windows.FrameworkElement"/></returns>
        protected override FrameworkElement FindElement ( object value )
        {
            if ( value != null )
            {
                if ( value is RadComboBoxItem || value is ComboBoxItem )
                {
                    value = ( value as ContentControl ).Content;
                }
                if ( value is FrameworkElement )
                {
                    Element = value as FrameworkElement;
                }
                else
                {
                    SetBinding ( ElementProperty, new Binding { ElementName = value.ToString ().Replace ( " ", string.Empty ) } );
                }
            }
            return Element;
        }

        #endregion
    }
}
