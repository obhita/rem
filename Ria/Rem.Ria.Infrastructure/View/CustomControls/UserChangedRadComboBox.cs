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
    /// UserChangedRadComboBox class.
    /// </summary>
    public class UserChangedRadComboBox : RadComboBox
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CommandParameterProperty Property.
        /// </summary>
        public static new readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register (
                "CommandParameter",
                typeof( object ),
                typeof( UserChangedRadComboBox ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectionChangedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectionChangedCommandProperty =
            DependencyProperty.Register (
                "SelectionChangedCommand",
                typeof( ICommand ),
                typeof( UserChangedRadComboBox ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Represents a user defined data value that can be passed to the command when it is executed.
        /// </summary>
        /// <value>The command parameter.</value>
        public new object CommandParameter
        {
            get { return GetValue ( CommandParameterProperty ); }
            set { SetValue ( CommandParameterProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selection changed command.
        /// </summary>
        /// <value>The selection changed command.</value>
        public ICommand SelectionChangedCommand
        {
            get { return ( ICommand )GetValue ( SelectionChangedCommandProperty ); }
            set { SetValue ( SelectionChangedCommandProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>A <see cref="System.Windows.DependencyObject"/></returns>
        protected override DependencyObject GetContainerForItemOverride ()
        {
            var container = base.GetContainerForItemOverride ();
            if ( container is UIElement )
            {
                ( container as UIElement ).AddHandler ( MouseLeftButtonDownEvent, new MouseButtonEventHandler ( MouseLeftButtonDownHandler ), true );
            }
            return container;
        }

        /// <summary>
        /// Mouses the left button down handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        protected void MouseLeftButtonDownHandler ( object sender, MouseButtonEventArgs e )
        {
            if ( e.OriginalSource is FrameworkElement )
            {
                var item = e.OriginalSource as FrameworkElement;
                if ( item.DataContext != SelectedItem && SelectionChangedCommand != null )
                {
                    SelectedItem = item.DataContext;
                    SelectionChangedCommand.Execute ( CommandParameter );
                }
            }
        }

        #endregion
    }
}
