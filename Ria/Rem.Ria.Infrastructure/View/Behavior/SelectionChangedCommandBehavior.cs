#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
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
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for Handling selection changed event and calling command.
    /// The default behavior if you do not set the commandparameter is to set
    /// the command parameter to be the first item added or null if no items where added.
    /// This is not meant to be used if multiple selections are possible.
    /// </summary>
    public class SelectionChangedCommandBehavior : Behavior<Selector>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CommandParameterProperty Property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register (
                "CommandParameter",
                typeof( object ),
                typeof( SelectionChangedCommandBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register (
                "Command",
                typeof( ICommand ),
                typeof( SelectionChangedCommandBehavior ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get { return ( ICommand )GetValue ( CommandProperty ); }
            set { SetValue ( CommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>The command parameter.</value>
        public object CommandParameter
        {
            get { return GetValue ( CommandParameterProperty ); }
            set { SetValue ( CommandParameterProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            var parameter = CommandParameter;
            if ( ReadLocalValue ( CommandParameterProperty ) == DependencyProperty.UnsetValue && e.AddedItems.Count >= 1 )
            {
                parameter = e.AddedItems[0];
            }
            if ( Command != null && Command.CanExecute ( parameter ) )
            {
                Command.Execute ( parameter );
            }
        }

        #endregion
    }
}
