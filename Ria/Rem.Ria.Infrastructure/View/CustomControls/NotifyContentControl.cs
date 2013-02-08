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

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// NotifyContentControl class.
    /// </summary>
    public class NotifyContentControl : ContentControl
    {
        #region Constants and Fields

        private static readonly DependencyProperty ContentChangedWorkaroundProperty = DependencyProperty.Register (
            "ContentChangedWorkaround",
            typeof( object ),
            typeof( NotifyContentControl ),
            new PropertyMetadata ( OnContentChanged ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyContentControl"/> class.
        /// </summary>
        public NotifyContentControl ()
        {
            // content changed event workaround
            var contentBinding = new Binding ( "Content" );
            contentBinding.Source = this;
            contentBinding.Mode = BindingMode.TwoWay;
            SetBinding ( ContentChangedWorkaroundProperty, contentBinding );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [content changed].
        /// </summary>
        public event RoutedEventHandler ContentChanged;

        /// <summary>
        /// Occurs when [content set to null].
        /// </summary>
        public event RoutedEventHandler ContentSetToNull;

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [content changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OnContentChanged ( object sender, DependencyPropertyChangedEventArgs e )
        {
            var ncc = sender as NotifyContentControl;
            if ( ncc != null )
            {
                if ( ncc.ContentChanged != null )
                {
                    ncc.ContentChanged ( ncc, new RoutedEventArgs () );
                }
                if ( e.NewValue == null && ncc.ContentSetToNull != null )
                {
                    ncc.ContentSetToNull ( ncc, new RoutedEventArgs () );
                }
            }
        }

        #endregion
    }
}
