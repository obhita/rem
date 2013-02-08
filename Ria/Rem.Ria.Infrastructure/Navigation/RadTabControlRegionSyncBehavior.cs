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
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Telerik.Windows.Controls;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// Behavior to handle syncing the region with the <see cref="RadTabControl"/>
    /// </summary>
    public class RadTabControlRegionSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        #region Constants and Fields

        /// <summary>
        /// Key for this behavior
        /// </summary>
        public const string BehaviorKey = "RadTabControlRegionSyncBehavior";

        /// <summary>
        /// HeaderTemplateSelector dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateSelectorProperty =
            DependencyProperty.RegisterAttached (
                "HeaderTemplateSelector",
                typeof( DataTemplateSelector ),
                typeof( RadTabControlRegionSyncBehavior ),
                new PropertyMetadata ( null ) );

        private static readonly DependencyProperty IsGeneratedProperty =
            DependencyProperty.RegisterAttached ( "IsGenerated", typeof( bool ), typeof( TabControlRegionSyncBehavior ), null );

        private RadTabControl _hostControl;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value>
        public DependencyObject HostControl
        {
            get { return _hostControl; }

            set
            {
                var newValue = value as RadTabControl;
                if ( newValue == null )
                {
                    throw new InvalidOperationException ( "Host Control must be a RadTabControl." );
                }

                if ( IsAttached )
                {
                    throw new InvalidOperationException ( "Host Control cannot be set after attached." );
                }

                _hostControl = newValue;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the header template selector.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns><see cref="DataTemplateSelector"/> for obj.</returns>
        public static DataTemplateSelector GetHeaderTemplateSelector ( DependencyObject obj )
        {
            return ( DataTemplateSelector )obj.GetValue ( HeaderTemplateSelectorProperty );
        }

        /// <summary>
        /// Sets the header template selector.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetHeaderTemplateSelector ( DependencyObject obj, DataTemplateSelector value )
        {
            obj.SetValue ( HeaderTemplateSelectorProperty, value );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Undoes the effects of the <see cref="PrepareContainerForItem"/> method.
        /// </summary>
        /// <param name="tabItem">The container element for the item.</param>
        protected virtual void ClearContainerForItem ( RadTabItem tabItem )
        {
            if ( tabItem == null )
            {
                throw new ArgumentNullException ( "tabItem" );
            }
            if ( ( bool )tabItem.GetValue ( IsGeneratedProperty ) )
            {
                tabItem.Content = null;
            }
        }

        /// <summary>
        /// Gets the item contained in the <see cref="RadTabItem"/>.
        /// </summary>
        /// <param name="tabItem">The container item.</param>
        /// <returns>The item contained in the <paramref name="tabItem"/> if it was generated automatically by the behavior; otherwise <paramref name="tabItem"/>.</returns>
        protected virtual object GetContainedItem ( RadTabItem tabItem )
        {
            if ( tabItem == null )
            {
                throw new ArgumentNullException ( "tabItem" );
            }
            if ( ( bool )tabItem.GetValue ( IsGeneratedProperty ) )
            {
                return tabItem.Content;
            }

            return tabItem;
        }

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <param name="item">The item to get the container for.</param>
        /// <param name="itemCollection">The parent's <see cref="ItemCollection"/>.</param>
        /// <returns>The element that is used to display the given item.</returns>
        protected virtual RadTabItem GetContainerForItem ( object item, ItemCollection itemCollection )
        {
            if ( itemCollection == null )
            {
                throw new ArgumentNullException ( "itemCollection" );
            }
            var container = item as RadTabItem;
            if ( container != null && ( ( bool )container.GetValue ( IsGeneratedProperty ) ) == false )
            {
                return container;
            }

            return itemCollection.Cast<RadTabItem> ()
                .Where ( tabItem => ( bool )tabItem.GetValue ( IsGeneratedProperty ) )
                .FirstOrDefault ( tabItem => tabItem.Content == item );
        }

        /// <summary>
        /// Override this method to perform the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach ()
        {
            if ( _hostControl == null )
            {
                throw new InvalidOperationException ( "Host control cannot be Null." );
            }

            SynchronizeItems ();

            _hostControl.SelectionChanged += OnSelectionChanged;
            Region.ActiveViews.CollectionChanged += OnActiveViewsChanged;
            Region.Views.CollectionChanged += OnViewsChanged;
        }

        /// <summary>
        /// Override to change how RadTabItem's are prepared for items.
        /// </summary>
        /// <param name="item">The item to wrap in a RadTabItem</param>
        /// <param name="parent">The parent <see cref="DependencyObject"/></param>
        /// <returns>A tab item that wraps the supplied <paramref name="item"/></returns>
        protected virtual RadTabItem PrepareContainerForItem ( object item, DependencyObject parent )
        {
            var container = item as RadTabItem;
            if ( container == null )
            {
                var dataContext = GetDataContext ( item );
                container = new RadTabItem
                    {
                        Content = item,
                        Style = _hostControl.ItemContainerStyle,
                        DataContext = dataContext,
                        Header = dataContext 
                    };

                var headerTemplateSelector = GetHeaderTemplateSelector ( parent );
                if ( headerTemplateSelector != null )
                {
                    container.HeaderTemplate = headerTemplateSelector.SelectTemplate ( dataContext, parent );
                }

                container.SetValue ( IsGeneratedProperty, true );
            }

            return container;
        }

        private static object GetDataContext ( object item )
        {
            var frameworkElement = item as FrameworkElement;
            return frameworkElement == null ? item : frameworkElement.DataContext;
        }

        private void OnActiveViewsChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Add )
            {
                _hostControl.SelectedItem = GetContainerForItem ( e.NewItems[0], _hostControl.Items );
            }
            else if ( e.Action == NotifyCollectionChangedAction.Remove
                      && _hostControl.SelectedItem != null
                      && e.OldItems.Contains ( GetContainedItem ( ( RadTabItem )_hostControl.SelectedItem ) ) )
            {
                _hostControl.SelectedItem = null;
            }
        }

        private void OnSelectionChanged ( object sender, RadSelectionChangedEventArgs e )
        {
            // e.OriginalSource == null, that's why we use sender.
            if ( _hostControl == sender )
            {
                foreach ( RadTabItem tabItem in e.RemovedItems )
                {
                    var item = GetContainedItem ( tabItem );

                    // check if the view is in both Views and ActiveViews collections (there may be out of sync)
                    if ( Region.Views.Contains ( item ) && Region.ActiveViews.Contains ( item ) )
                    {
                        Region.Deactivate ( item );
                    }
                }

                foreach ( RadTabItem tabItem in e.AddedItems )
                {
                    var item = GetContainedItem ( tabItem );
                    if (!Region.ActiveViews.Contains(item) && Region.Views.Contains(item))
                    {
                        Region.Activate ( item );
                    }
                }
            }
        }

        private void OnViewsChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( e.Action == NotifyCollectionChangedAction.Add )
            {
                var startingIndex = e.NewStartingIndex;
                foreach ( var newItem in e.NewItems )
                {
                    var tabItem = PrepareContainerForItem ( newItem, _hostControl );
                    _hostControl.Items.Insert ( startingIndex, tabItem );
                }
            }
            else if ( e.Action == NotifyCollectionChangedAction.Remove )
            {
                foreach ( var oldItem in e.OldItems )
                {
                    var tabItem = GetContainerForItem ( oldItem, _hostControl.Items );
                    _hostControl.Items.Remove ( tabItem );
                    ClearContainerForItem ( tabItem );
                }
            }
        }

        private void SynchronizeItems ()
        {
            var existingItems = new List<object> ();
            if ( _hostControl.Items.Count > 0 )
            {
                // Control must be empty before "Binding" to a region
                existingItems.AddRange ( _hostControl.Items );
            }

            foreach ( var view in Region.Views )
            {
                var tabItem = PrepareContainerForItem ( view, _hostControl );
                _hostControl.Items.Add ( tabItem );
            }

            foreach ( var existingItem in existingItems )
            {
                Region.Add ( existingItem );
            }
        }

        #endregion
    }
}
