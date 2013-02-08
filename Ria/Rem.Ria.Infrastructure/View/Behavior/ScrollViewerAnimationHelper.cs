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

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for helping scroll viewer animation.
    /// </summary>
    public class ScrollViewerAnimationHelper : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for ScrollViewerProperty Property.
        /// </summary>
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register (
                "ScrollViewer",
                typeof( ScrollViewer ),
                typeof( ScrollViewerAnimationHelper ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for VerticalOffsetProperty Property.
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register (
                "VerticalOffset",
                typeof( double ),
                typeof( ScrollViewerAnimationHelper ),
                new PropertyMetadata ( 0.0, VerticalOffsetChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the scroll viewer.
        /// </summary>
        /// <value>The scroll viewer.</value>
        public ScrollViewer ScrollViewer
        {
            get { return ( ScrollViewer )GetValue ( ScrollViewerProperty ); }
            set { SetValue ( ScrollViewerProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the vertical offset.
        /// </summary>
        /// <value>The vertical offset.</value>
        public double VerticalOffset
        {
            get { return ( double )GetValue ( VerticalOffsetProperty ); }
            set { SetValue ( VerticalOffsetProperty, value ); }
        }

        #endregion

        #region Methods

        private static void VerticalOffsetChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var animationHelper = d as ScrollViewerAnimationHelper;
            if ( animationHelper != null && animationHelper.ScrollViewer != null )
            {
                animationHelper.ScrollViewer.ScrollToVerticalOffset ( ( double )e.NewValue );
            }
        }

        #endregion
    }
}
