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
using System.Windows.Interactivity;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing editable expander loading.
    /// </summary>
    public class EditableExpanderLoadingBehavior : Behavior<EditableExpander>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for IsLoadingProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register (
            "IsLoading", typeof( bool ), typeof( EditableExpanderLoadingBehavior ), new PropertyMetadata ( false, IsLoadingChanged ) );

        private bool _isLoading;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is loading.
        /// </summary>
        /// <value><c>true</c> if this instance is loading; otherwise, <c>false</c>.</value>
        public bool IsLoading
        {
            get { return ( bool )GetValue ( IsLoadingProperty ); }
            set { SetValue ( IsLoadingProperty, value ); }
        }

        #endregion

        #region Methods

        private static void IsLoadingChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = d as EditableExpanderLoadingBehavior;
            if ( behavior != null )
            {
                if ( e.NewValue is bool )
                {
                    behavior._isLoading = ( bool )e.NewValue;
                    if ( behavior.AssociatedObject != null && behavior.AssociatedObject.EditableWrapper != null )
                    {
                        behavior.AssociatedObject.EditableWrapper.IsLoading = behavior._isLoading;
                    }
                }
            }
        }

        #endregion
    }
}
