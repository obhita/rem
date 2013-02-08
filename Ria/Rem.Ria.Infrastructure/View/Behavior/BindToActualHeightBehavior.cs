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

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// This Behavior is necessary due to a bug in Silverlight 4 with the ability to bind to Actual Height property.
    /// </summary>
    public class BindToActualHeightBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for BindToElementProperty Property.
        /// </summary>
        public static readonly DependencyProperty BindToElementProperty =
            DependencyProperty.Register (
                "BindToElement",
                typeof( FrameworkElement ),
                typeof( BindToActualHeightBehavior ),
                new PropertyMetadata ( null, BindToElementChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bind to element.
        /// </summary>
        /// <value>The bind to element.</value>
        public FrameworkElement BindToElement
        {
            get { return ( FrameworkElement )GetValue ( BindToElementProperty ); }
            set { SetValue ( BindToElementProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.SizeChanged += AssociatedObjectSizeChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.SizeChanged -= AssociatedObjectSizeChanged;
        }

        private static void BindToElementChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = d as BindToActualHeightBehavior;
            if ( behavior != null )
            {
                behavior.AssociatedObjectSizeChanged ( null, null );
            }
        }

        private void AssociatedObjectSizeChanged ( object sender, SizeChangedEventArgs e )
        {
            if ( BindToElement != null )
            {
                var height = ( AssociatedObject.ActualHeight -
                                              BindToElement.Margin.Top -
                                              BindToElement.Margin.Bottom - 4 );
                            BindToElement.Height = height >= 0 ? height : 0;
            }
        }

        #endregion
    }
}
