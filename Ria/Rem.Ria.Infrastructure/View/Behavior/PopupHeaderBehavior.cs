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
using System.Windows.Interactivity;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for popup header behavior.
    /// </summary>
    public class PopupHeaderBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for HeaderTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register (
                "HeaderTemplate",
                typeof( DataTemplate ),
                typeof( PopupHeaderBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for PanelRootProperty Property.
        /// </summary>
        public static readonly DependencyProperty PanelRootProperty =
            DependencyProperty.RegisterAttached (
                "PanelRoot",
                typeof( Panel ),
                typeof( PopupHeaderBehavior ),
                new PropertyMetadata ( null ) );

        private PopupHeader _popupHeader;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return ( DataTemplate )GetValue ( HeaderTemplateProperty ); }
            set { SetValue ( HeaderTemplateProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the panel root.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>A <see cref="System.Windows.Controls.Panel"/></returns>
        public static Panel GetPanelRoot ( DependencyObject obj )
        {
            return ( Panel )obj.GetValue ( PanelRootProperty );
        }

        /// <summary>
        /// Sets the panel root.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetPanelRoot ( DependencyObject obj, Panel value )
        {
            obj.SetValue ( PanelRootProperty, value );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            _popupHeader = new PopupHeader ();
            _popupHeader.PlacementTarget = AssociatedObject;
            var content = new ContentPresenter { ContentTemplate = HeaderTemplate, HorizontalAlignment = HorizontalAlignment.Stretch };
            _popupHeader.Content = content;

            if ( AssociatedObject is Panel )
            {
                SetPanelRoot ( AssociatedObject, AssociatedObject as Panel );
            }
            else
            {
                var binding = new Binding ();
                binding.RelativeSource = new RelativeSource ( RelativeSourceMode.FindAncestor ) { AncestorType = typeof( Panel ) };
                BindingOperations.SetBinding ( AssociatedObject, PanelRootProperty, binding );
            }

            var panel = GetPanelRoot ( AssociatedObject );
            panel.Children.Add ( _popupHeader );
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();

            var panel = GetPanelRoot ( AssociatedObject );
            panel.Children.Remove ( _popupHeader );

            _popupHeader.PlacementTarget = null;
            _popupHeader = null;
        }

        #endregion
    }
}
