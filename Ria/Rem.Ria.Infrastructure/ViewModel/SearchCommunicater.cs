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

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Class for communicating search.
    /// </summary>
    public class SearchCommunicater : DependencyObject
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AdvancedSearchVisibilityProperty Property.
        /// </summary>
        public static readonly DependencyProperty AdvancedSearchVisibilityProperty =
            DependencyProperty.Register (
                "AdvancedSearchVisibility",
                typeof( Visibility ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( Visibility.Visible ) );

        /// <summary>
        /// Dependency Property for ClearSelectedItemCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClearSelectedItemCommandProperty =
            DependencyProperty.Register (
                "ClearSelectedItemCommand",
                typeof( ICommand ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ClearSelectionWhenAbortedProperty Property.
        /// </summary>
        public static readonly DependencyProperty ClearSelectionWhenAbortedProperty =
            DependencyProperty.Register (
                "ClearSelectionWhenAborted",
                typeof( bool ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for EmptyTextProperty Property.
        /// </summary>
        public static readonly DependencyProperty EmptyTextProperty =
            DependencyProperty.Register (
                "EmptyText",
                typeof( string ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( string.Empty ) );

        /// <summary>
        /// Dependency Property for SelectedItemChangedCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemChangedCommandProperty =
            DependencyProperty.Register (
                "SelectedItemChangedCommand",
                typeof( ICommand ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SelectedItemProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register (
                "SelectedItem",
                typeof( ISearchResultDto ),
                typeof( SearchCommunicater ),
                new PropertyMetadata ( null, SelectedItemChangedHandler ) );

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [selected item changed].
        /// </summary>
        public event SelectionChangedEventHandler SelectedItemChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the advanced search visibility.
        /// </summary>
        /// <value>The advanced search visibility.</value>
        public Visibility AdvancedSearchVisibility
        {
            get { return ( Visibility )GetValue ( AdvancedSearchVisibilityProperty ); }
            set { SetValue ( AdvancedSearchVisibilityProperty, value ); }
        }

        /// <summary>
        /// Gets the clear selected item command.
        /// </summary>
        public ICommand ClearSelectedItemCommand
        {
            get { return ( ICommand )GetValue ( ClearSelectedItemCommandProperty ); }
            internal set { SetValue ( ClearSelectedItemCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [clear selection when aborted].
        /// </summary>
        /// <value><c>true</c> if [clear selection when aborted]; otherwise, <c>false</c>.</value>
        public bool ClearSelectionWhenAborted
        {
            get { return ( bool )GetValue ( ClearSelectionWhenAbortedProperty ); }
            set { SetValue ( ClearSelectionWhenAbortedProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the empty text.
        /// </summary>
        /// <value>The empty text.</value>
        public string EmptyText
        {
            get { return ( string )GetValue ( EmptyTextProperty ); }
            set { SetValue ( EmptyTextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public ISearchResultDto SelectedItem
        {
            get { return ( ISearchResultDto )GetValue ( SelectedItemProperty ); }
            set { SetValue ( SelectedItemProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the selected item changed command.
        /// </summary>
        /// <value>The selected item changed command.</value>
        public ICommand SelectedItemChangedCommand
        {
            get { return ( ICommand )GetValue ( SelectedItemChangedCommandProperty ); }
            set { SetValue ( SelectedItemChangedCommandProperty, value ); }
        }

        #endregion

        #region Methods

        private static void SelectedItemChangedHandler ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var sc = ( d as SearchCommunicater );
            if ( sc != null && sc.SelectedItemChanged != null )
            {
                sc.SelectedItemChanged (
                    sc,
                    new SelectionChangedEventArgs (
                        new List<ISearchResultDto> { e.OldValue as ISearchResultDto }, new List<ISearchResultDto> { e.NewValue as ISearchResultDto } ) );
            }
        }

        #endregion
    }
}
