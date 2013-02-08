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

using System.Collections;
using System.Windows;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ToolSetListButton class.
    /// </summary>
    public class ToolSetListButton : ToolSetButton
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for DisplayMemberPathProperty Property.
        /// </summary>
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register (
                "DisplayMemberPath",
                typeof( string ),
                typeof( ToolSetListButton ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for EventNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.Register (
                "EventName",
                typeof( string ),
                typeof( ToolSetListButton ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ItemsSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register (
                "ItemsSource",
                typeof( IList ),
                typeof( ToolSetListButton ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectedItemProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register (
                "SelectedItem",
                typeof( object ),
                typeof( ToolSetListButton ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the display member path.
        /// </summary>
        /// <value>The display member path.</value>
        public string DisplayMemberPath
        {
            get { return ( string )GetValue ( DisplayMemberPathProperty ); }
            set { SetValue ( DisplayMemberPathProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName
        {
            get { return ( string )GetValue ( EventNameProperty ); }
            set { SetValue ( EventNameProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IList ItemsSource
        {
            get { return ( IList )GetValue ( ItemsSourceProperty ); }
            set { SetValue ( ItemsSourceProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return GetValue ( SelectedItemProperty ); }
            set { SetValue ( SelectedItemProperty, value ); }
        }

        #endregion
    }
}
