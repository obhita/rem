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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// OpenFileDialogBoxTriggerAction class.
    /// </summary>
    public class OpenFileDialogBoxTriggerAction : TargetedTriggerAction<Button>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for DialogFilterProperty Property.
        /// </summary>
        public static readonly DependencyProperty DialogFilterProperty =
            DependencyProperty.Register (
                "DialogFilter", typeof( string ), typeof( OpenFileDialogBoxTriggerAction ), new PropertyMetadata ( "All Files (*.*)|*.*" ) );

        /// <summary>
        /// Dependency Property for FileDialogDialogResultCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty FileDialogDialogResultCommandProperty =
            DependencyProperty.Register (
                "FileDialogDialogResultCommandProperty",
                typeof( ICommand ),
                typeof( OpenFileDialogBoxTriggerAction ),
                null );

        /// <summary>
        /// Dependency Property for MultiSelectProperty Property.
        /// </summary>
        public static readonly DependencyProperty MultiSelectProperty =
            DependencyProperty.Register ( "MultiSelect", typeof( bool ), typeof( OpenFileDialogBoxTriggerAction ), null );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the dialog filter.
        /// </summary>
        /// <value>The dialog filter.</value>
        public string DialogFilter
        {
            get { return ( string )GetValue ( DialogFilterProperty ); }
            set { SetValue ( DialogFilterProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the file dialog dialog result command.
        /// </summary>
        /// <value>The file dialog dialog result command.</value>
        public ICommand FileDialogDialogResultCommand
        {
            get { return ( ICommand )GetValue ( FileDialogDialogResultCommandProperty ); }
            set { SetValue ( FileDialogDialogResultCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [multi select].
        /// </summary>
        /// <value><c>true</c> if [multi select]; otherwise, <c>false</c>.</value>
        public bool MultiSelect
        {
            get { return ( bool )GetValue ( MultiSelectProperty ); }
            set { SetValue ( MultiSelectProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke ( object parameter )
        {
            var objOpenFileDialog = new OpenFileDialog { Filter = DialogFilter, Multiselect = MultiSelect };
            var result = objOpenFileDialog.ShowDialog ();
            if ( result.Value && FileDialogDialogResultCommand != null )
            {
                FileDialogDialogResultCommand.Execute ( objOpenFileDialog.Files );
            }
        }

        #endregion
    }
}
