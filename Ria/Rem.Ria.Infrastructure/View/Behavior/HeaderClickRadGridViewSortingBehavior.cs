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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing header click RAD grid view sorting.
    /// </summary>
    public class HeaderClickRadGridViewSortingBehavior : RadGridViewPagedSortBehavior
    {
        #region Constants and Fields

        private MouseButtonEventHandler _handler;

        #endregion

        #region Methods

        /// <summary>
        /// Called when [attached].
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.CanUserSortColumns = false;
            _handler = MouseDownOnHeaderCell;
            AssociatedObject.AddHandler ( UIElement.MouseLeftButtonDownEvent, _handler, true );
        }

        /// <summary>
        /// Called when [detaching].
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.RemoveHandler ( UIElement.MouseLeftButtonDownEvent, _handler );
        }

        private void MouseDownOnHeaderCell ( object sender, MouseEventArgs args )
        {
            var cellClicked = ( ( FrameworkElement )args.OriginalSource ).ParentOfType<GridViewHeaderCell> ();

            if ( cellClicked == null )
            {
                return;
            }
            var column = ( GridViewDataColumn )cellClicked.Column;
            var sortByPropertyName = ( column ).DataMemberBinding.Path.Path;

            var oldSortDirection = SortDirection;

            if ( sortByPropertyName != SortBy )
            {
                oldSortDirection = ListSortDirection.Descending;
            }

            switch ( oldSortDirection )
            {
                case ListSortDirection.Ascending:
                    column.SortingState = SortingState.Descending;
                    SortDirection = ListSortDirection.Descending;
                    SortBy = sortByPropertyName;
                    break;
                case ListSortDirection.Descending:
                    column.SortingState = SortingState.Ascending;
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
