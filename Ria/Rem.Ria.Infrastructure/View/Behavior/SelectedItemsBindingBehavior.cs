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
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing selected items binding.
    /// </summary>
    public class SelectedItemsBindingBehavior : Behavior<ListBox>
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for SelectedItemsProperty Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register (
                "SelectedItems",
                typeof( IList ),
                typeof( SelectedItemsBindingBehavior ),
                new PropertyMetadata ( null, SelectedItemsChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the selected items.
        /// </summary>
        /// <value>The selected items.</value>
        public IList SelectedItems
        {
            get { return ( IList )GetValue ( SelectedItemsProperty ); }
            set { SetValue ( SelectedItemsProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        private static void SelectedItemsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = d as SelectedItemsBindingBehavior;
            if ( behavior != null )
            {
                if ( e.OldValue is INotifyCollectionChanged )
                {
                    ( e.OldValue as INotifyCollectionChanged ).CollectionChanged -= behavior.SelectedItems_CollectionChanged;
                }
                behavior.UpdateAssociatedObjectSelectedItems ();
                if ( e.NewValue is INotifyCollectionChanged )
                {
                    ( e.NewValue as INotifyCollectionChanged ).CollectionChanged += behavior.SelectedItems_CollectionChanged;
                }
            }
        }

        private void AssociatedObject_Loaded ( object sender, RoutedEventArgs e )
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            UpdateAssociatedObjectSelectedItems ();
        }

        private void AssociatedObject_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            if ( SelectedItems != null )
            {
                if ( e.AddedItems != null )
                {
                    foreach ( var item in e.AddedItems )
                    {
                        if ( !SelectedItems.Contains ( item ) )
                        {
                            SelectedItems.Add ( item );
                        }
                    }
                }
                if ( e.RemovedItems != null )
                {
                    foreach ( var item in e.RemovedItems )
                    {
                        if ( SelectedItems.Contains ( item ) )
                        {
                            SelectedItems.Remove ( item );
                        }
                    }
                }
            }
        }

        private void SelectedItems_CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( AssociatedObject != null )
            {
                if ( e.NewItems != null )
                {
                    foreach ( var item in e.NewItems )
                    {
                        if ( !AssociatedObject.SelectedItems.Contains ( item ) )
                        {
                            AssociatedObject.SelectedItems.Add ( item );
                        }
                    }
                }
                if ( e.OldItems != null )
                {
                    foreach ( var item in e.OldItems )
                    {
                        if ( AssociatedObject.SelectedItems.Contains ( item ) )
                        {
                            AssociatedObject.SelectedItems.Remove ( item );
                        }
                    }
                }
            }
        }

        private void UpdateAssociatedObjectSelectedItems ()
        {
            if ( AssociatedObject != null )
            {
                AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
                AssociatedObject.SelectedItems.Clear ();
                if ( SelectedItems != null )
                {
                    foreach ( var selectedItem in SelectedItems )
                    {
                        var i = AssociatedObject.SelectedItems.Add ( selectedItem );
                    }
                }
                AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            }
        }

        #endregion
    }
}
