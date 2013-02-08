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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// This behavior make sure that inactive dto is not draggable.
    /// This behavior only make sense for <see cref="DragDropTarget{TItemsControlType,TItemContainerType}"/>.
    /// </summary>
    public class DragDropTargetDraggableBehavior : Behavior<ContentControl>
    {
        #region Constants and Fields

        private EventInfo _itemDragStartingEvent;

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            _itemDragStartingEvent = AssociatedObject.GetType ().GetEvent ( "ItemDragStarting" );

            if ( _itemDragStartingEvent != null )
            {
                _itemDragStartingEvent.AddEventHandler (
                    AssociatedObject,
                    Delegate.CreateDelegate (
                        _itemDragStartingEvent.EventHandlerType,
                        this,
                        "AssociatedObject_ItemDragStarting" ) );
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();

            if ( _itemDragStartingEvent != null )
            {
                _itemDragStartingEvent.RemoveEventHandler (
                    AssociatedObject,
                    Delegate.CreateDelegate (
                        _itemDragStartingEvent.EventHandlerType,
                        this,
                        "AssociatedObject_ItemDragStarting" ) );
            }
        }

        private void AssociatedObject_ItemDragStarting ( object sender, ItemDragEventArgs e )
        {
            var canDrag = true;

            var selectionCollection = e.Data as SelectionCollection;
            if ( selectionCollection != null )
            {
                var dtos =
                    selectionCollection.Select ( selection => selection.Item ).OfType<IActiveDto> ();
                if ( dtos.Any () )
                {
                    foreach ( var dto in dtos )
                    {
                        if ( !dto.IsActive )
                        {
                            canDrag = false;
                            break;
                        }
                    }
                }
            }

            if ( !canDrag )
            {
                // Use both to disable the Drag based on the bound data
                e.Handled = true;
                e.Cancel = true;
            }
        }

        #endregion
    }
}
