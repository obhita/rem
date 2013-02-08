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

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// This behavior supports management of RadGridView sorting properties.
    /// </summary>
    public class RadGridViewPagedSortBehavior : Behavior<RadGridView>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for SortByProperty Property.
        /// </summary>
        public static readonly DependencyProperty SortByProperty =
            DependencyProperty.Register (
                "SortBy",
                typeof( string ),
                typeof( RadGridViewPagedSortBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SortCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty SortCommandProperty =
            DependencyProperty.Register (
                "SortCommand",
                typeof( ICommand ),
                typeof( RadGridViewPagedSortBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for SortDirectionProperty Property.
        /// </summary>
        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.Register (
                "SortDirection",
                typeof( ListSortDirection ),
                typeof( RadGridViewPagedSortBehavior ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>The sort by.</value>
        public string SortBy
        {
            get { return ( string )GetValue ( SortByProperty ); }
            set { SetValue ( SortByProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the sort command.
        /// </summary>
        /// <value>The sort command.</value>
        public ICommand SortCommand
        {
            get { return ( ICommand )GetValue ( SortCommandProperty ); }
            set { SetValue ( SortCommandProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>The sort direction.</value>
        public ListSortDirection SortDirection
        {
            get { return ( ListSortDirection )GetValue ( SortDirectionProperty ); }
            set { SetValue ( SortDirectionProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.Sorting += AssociatedObject_Sorting;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.Sorting -= AssociatedObject_Sorting;
        }

        private void AssociatedObject_Sorting ( object sender, GridViewSortingEventArgs e )
        {
            e.Cancel = true;
            var sortByPropertyName = ( ( GridViewDataColumn )e.Column ).DataMemberBinding.Path.Path;

            switch ( e.OldSortingState )
            {
                case SortingState.None:
                    e.NewSortingState = SortingState.Ascending;
                    SortDirection = ListSortDirection.Ascending;
                    SortBy = sortByPropertyName;
                    break;
                case SortingState.Ascending:
                    e.NewSortingState = SortingState.Descending;
                    SortDirection = ListSortDirection.Descending;
                    SortBy = sortByPropertyName;
                    break;
                case SortingState.Descending:
                    SortDirection = ListSortDirection.Ascending;
                    SortBy = sortByPropertyName;
                    break;
                default:
                    break;
            }

            // Sort executed server-side.
            SortCommand.Execute ( null );
        }

        #endregion
    }
}
