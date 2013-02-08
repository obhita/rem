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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using DragEventArgs = Microsoft.Windows.DragEventArgs;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// This behavior enable casting on drop target whose binding data type is differnect from the binding data type of drag source.
    /// Only works when Drag Source's AllowedEffects is All or Scroll.
    /// This behavior only make sense for <see cref="DragDropTarget{TItemsControlType,TItemContainerType}"/> whose containd control bound to <see cref="IList{T}"/>.
    /// </summary>
    public class DragDropTargetDropCastBehavior : Behavior<ContentControl>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for CastMethodNameProperty Property.
        /// </summary>
        public static readonly DependencyProperty CastMethodNameProperty =
            DependencyProperty.Register (
                "CastMethodName",
                typeof( string ),
                typeof( DragDropTargetDropCastBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for CastMethodSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty CastMethodSourceProperty =
            DependencyProperty.Register (
                "CastMethodSource",
                typeof( object ),
                typeof( DragDropTargetDropCastBehavior ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for DropDataSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty DropDataSourceProperty =
            DependencyProperty.Register (
                "DropDataSource",
                typeof( IList ),
                typeof( DragDropTargetDropCastBehavior ),
                new PropertyMetadata ( null ) );

        private EventInfo _dropEventInfo;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the cast method.
        /// </summary>
        /// <value>The name of the cast method.</value>
        public string CastMethodName
        {
            get { return ( string )GetValue ( CastMethodNameProperty ); }
            set { SetValue ( CastMethodNameProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the cast method source.
        /// </summary>
        /// <value>The cast method source.</value>
        public object CastMethodSource
        {
            get { return GetValue ( CastMethodSourceProperty ); }
            set { SetValue ( CastMethodSourceProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the drop data source.
        /// </summary>
        /// <value>The drop data source.</value>
        public IList DropDataSource
        {
            get { return ( IList )GetValue ( DropDataSourceProperty ); }
            set { SetValue ( DropDataSourceProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            _dropEventInfo = AssociatedObject.GetType ().GetEvent ( "Drop" );

            if ( _dropEventInfo != null )
            {
                _dropEventInfo.AddEventHandler (
                    AssociatedObject,
                    Delegate.CreateDelegate (
                        _dropEventInfo.EventHandlerType,
                        this,
                        "AssociatedObject_Drop" ) );
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();

            if ( _dropEventInfo != null )
            {
                _dropEventInfo.RemoveEventHandler (
                    AssociatedObject,
                    Delegate.CreateDelegate (
                        _dropEventInfo.EventHandlerType,
                        this,
                        "AssociatedObject_Drop" ) );
            }
        }

        private void AssociatedObject_Drop ( object sender, DragEventArgs e )
        {
            // You can only get here when (e.AllowedEffects & DragDropEffects.Scroll) == DragDropEffects.Scroll
            if ( DropDataSource != null && CastMethodSource != null && CastMethodName != null )
            {
                // Retrieve the dropped data in the first available format.
                var data = e.Data.GetData ( e.Data.GetFormats ()[0] );

                var dragEventArgs = data as ItemDragEventArgs;
                if ( dragEventArgs != null )
                {
                    var selectionCollection = dragEventArgs.Data as SelectionCollection;
                    if ( selectionCollection != null )
                    {
                        var dtos =
                            selectionCollection.Select ( selection => selection.Item ).OfType<object> ();
                        var methodInfo = CastMethodSource.GetType ().GetMethod ( CastMethodName );

                        if ( dtos.Any () && methodInfo != null )
                        {
                            foreach ( var dto in dtos )
                            {
                                var targetDto = methodInfo.Invoke ( CastMethodSource, new[] { dto } );
                                if ( targetDto != null )
                                {
                                    DropDataSource.Add ( targetDto );
                                }
                            }
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
