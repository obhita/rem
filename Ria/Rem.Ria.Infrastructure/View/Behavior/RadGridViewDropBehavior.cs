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

using System;
using System.Collections;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DragDrop;
using Telerik.Windows.Controls.GridView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for RAD grid view drag drop behavior.
    /// </summary>
    public class RadGridViewDropBehavior : DropBehavior<RadGridView>
    {
        private bool _dropCompleted;

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.RemoveHandler(RadDragAndDropManager.DropQueryEvent, new EventHandler<DragDropQueryEventArgs>(OnDropQuery));
            AssociatedObject.AddHandler(RadDragAndDropManager.DropQueryEvent, new EventHandler<DragDropQueryEventArgs>(OnDropQuery), true);

            AssociatedObject.RemoveHandler(RadDragAndDropManager.DropInfoEvent, new EventHandler<DragDropEventArgs>(OnDropInfo));
            AssociatedObject.AddHandler(RadDragAndDropManager.DropInfoEvent, new EventHandler<DragDropEventArgs>(OnDropInfo), true); // GridViewHeaderCell/GridViewHeaderRow couldn't handle DropInfoEvent

            AssociatedObject.RowLoaded += AssociatedObject_RowLoaded;
        }

        private void AssociatedObject_RowLoaded(object sender, RowLoadedEventArgs e)
        {
            if (AllowDrop)
            {
                RadDragAndDropManager.SetAllowDrop(e.Row, true);
                RadDragAndDropManager.AddDropQueryHandler(e.Row, OnDropQuery);
                RadDragAndDropManager.AddDropInfoHandler(e.Row, OnDropInfo);
            }
        }

        /// <summary>
        /// Called when [drop info].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.DragDrop.DragDropEventArgs"/> instance containing the event data.</param>
        protected override void OnDropInfo(object sender, DragDropEventArgs e)
        {
            base.OnDropInfo(sender, e);

            if (e.Options.Status == DragStatus.DropComplete)
            {
                if ( sender is GridViewRow )
                {
                    var row = sender as GridViewRow;
                    var destinationGrid = e.GetElement<RadGridView> ( e.Options.CurrentDragPoint );
                    if ( destinationGrid != null )
                    {
                        var index = destinationGrid.ItemContainerGenerator.IndexFromContainer ( row );
                        var destinationSource = destinationGrid.ItemsSource as IList;
                        if ( index >= 0 && destinationSource != null )
                        {
                            destinationSource.Insert ( index, e.Options.Payload );
                            destinationGrid.SelectedItem = e.Options.Payload;

                            _dropCompleted = true;
                        }
                    }
                }
                else if ( sender is RadGridView )
                {
                    if (!_dropCompleted)
                    {
                        var destinationGrid = sender as RadGridView;
                        var destinationSource = destinationGrid.ItemsSource as IList;
                        if (destinationSource != null)
                        {
                            destinationSource.Add(e.Options.Payload);
                            destinationGrid.SelectedItem = e.Options.Payload;
                        }
                    }
                    else
                    {
                        _dropCompleted = false;
                    }
                }
            }
        }

        #endregion
    }
}
