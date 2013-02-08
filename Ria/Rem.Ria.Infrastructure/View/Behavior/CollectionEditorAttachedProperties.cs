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

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// CollectionEditorAttachedProperties class.
    /// </summary>
    public class CollectionEditorAttachedProperties
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AddCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddCommandProperty =
            DependencyProperty.RegisterAttached (
                "AddCommand",
                typeof( ICommand ),
                typeof( ListBox ),
                new PropertyMetadata ( null, OnAddCommandPropertyChanged ) );

        /// <summary>
        /// Dependency Property for DeleteCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.RegisterAttached (
                "DeleteCommand",
                typeof( ICommand ),
                typeof( ListBox ),
                new PropertyMetadata ( null, OnDeleteCommandPropertyChanged ) );

        /// <summary>
        /// Dependency Property for ListHeaderProperty Property.
        /// </summary>
        public static readonly DependencyProperty ListHeaderProperty =
            DependencyProperty.RegisterAttached (
                "ListHeader",
                typeof( string ),
                typeof( CollectionEditorAttachedProperties ),
                null );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the add command.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <returns>A <see cref="System.Windows.Input.ICommand"/></returns>
        public static ICommand GetAddCommand ( DependencyObject depObj )
        {
            return ( ICommand )depObj.GetValue ( AddCommandProperty );
        }

        /// <summary>
        /// Gets the delete command.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <returns>A <see cref="System.Windows.Input.ICommand"/></returns>
        public static ICommand GetDeleteCommand ( DependencyObject depObj )
        {
            return ( ICommand )depObj.GetValue ( DeleteCommandProperty );
        }

        /// <summary>
        /// Gets the list header.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string GetListHeader ( DependencyObject dependencyObject )
        {
            return ( string )dependencyObject.GetValue ( ListHeaderProperty );
        }

        /// <summary>
        /// Sets the add command.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="command">The command.</param>
        public static void SetAddCommand ( DependencyObject depObj, ICommand command )
        {
            depObj.SetValue ( AddCommandProperty, command );
        }

        /// <summary>
        /// Sets the delete command.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="command">The command.</param>
        public static void SetDeleteCommand ( DependencyObject depObj, ICommand command )
        {
            depObj.SetValue ( DeleteCommandProperty, command );
        }

        /// <summary>
        /// Sets the list header.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="header">The header.</param>
        public static void SetListHeader ( DependencyObject dependencyObject, string header )
        {
            dependencyObject.SetValue ( ListHeaderProperty, header );
        }

        #endregion

        #region Methods

        private static void OnAddCommandPropertyChanged (
            DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
        }

        private static void OnDeleteCommandPropertyChanged (
            DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
        }

        #endregion
    }
}
