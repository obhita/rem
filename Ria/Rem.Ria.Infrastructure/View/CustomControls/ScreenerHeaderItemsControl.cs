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
using System.Windows.Data;
using System.Windows.Media;
using Pillar.Common.Utility;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// ScreenerHeaderItemsControl class.
    /// </summary>
    public class ScreenerHeaderItemsControl : ItemsControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( ScreenerHeaderItemsControl ),
                new PropertyMetadata(false));

        /// <summary>
        /// Dependency Property for ShowWhenReadOnly Property.
        /// </summary>
        public static readonly DependencyProperty ShowWhenReadOnlyProperty =
            DependencyProperty.Register(
            "ShowWhenReadOnly", 
            typeof(bool), 
            typeof(ScreenerHeaderItemsControl),
            new PropertyMetadata(false, ShowWhenReadOnlyChanged));

        private Grid _grid;
        private ItemsPresenter _itemsPresenter;
        private bool _templateApplied;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenerHeaderItemsControl"/> class.
        /// </summary>
        public ScreenerHeaderItemsControl ()
        {
            DefaultStyleKey = typeof( ScreenerHeaderItemsControl );

            SetVisibilityBinding();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get { return ( bool )GetValue ( IsReadOnlyProperty ); }
            set { SetValue ( IsReadOnlyProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible when in read only mode.
        /// </summary>
        /// <value><c>true</c> if this instance is visible when in read only mode; otherwise, <c>false</c>.</value>
        public bool ShowWhenReadOnly
        {
            get { return (bool)GetValue(ShowWhenReadOnlyProperty); }
            set { SetValue(ShowWhenReadOnlyProperty, value); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>. In simplest terms, this means the method is called just before a UI element displays in an application. For more information, see Remarks.
        /// </summary>
        public override void OnApplyTemplate ()
        {
            base.OnApplyTemplate ();
            _itemsPresenter = GetTemplateChild ( "ItemsPresenter" ) as ItemsPresenter;
            _templateApplied = true;
            LayoutUpdated += ApplyTemplateLayoutUpdated;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>The element that is used to display the given item.</returns>
        protected override DependencyObject GetContainerForItemOverride ()
        {
            return new ScreenerHeaderItem ();
        }

        /// <summary>
        /// Determines if the specified item is (or is eligible to be) its own container.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is (or is eligible to be) its own container; otherwise, false.</returns>
        protected override bool IsItemItsOwnContainerOverride ( object item )
        {
            return typeof( ScreenerHeaderItem ).IsAssignableFrom ( item.GetType () );
        }

        /// <summary>
        /// Called when the value of the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> that contains the event data</param>
        protected override void OnItemsChanged ( NotifyCollectionChangedEventArgs e )
        {
            base.OnItemsChanged ( e );
            if ( _templateApplied )
            {
                if ( e.Action == NotifyCollectionChangedAction.Add )
                {
                    if ( _grid != null )
                    {
                        CreateColumnsForItems ( _grid, e.NewItems.OfType<ScreenerHeaderItem> () );
                    }
                }
                else if ( e.Action == NotifyCollectionChangedAction.Remove )
                {
                    if ( _grid != null )
                    {
                        RemoveColumnsForItems ( _grid, e.OldItems.OfType<ScreenerHeaderItem> () );
                    }
                }
                else if ( e.Action == NotifyCollectionChangedAction.Replace )
                {
                    if ( _grid != null )
                    {
                        CreateColumnsForItems ( _grid, e.NewItems.OfType<ScreenerHeaderItem> () );
                        RemoveColumnsForItems ( _grid, e.OldItems.OfType<ScreenerHeaderItem> () );
                    }
                }
                else if ( e.Action == NotifyCollectionChangedAction.Reset )
                {
                    if ( _grid != null )
                    {
                        _grid.ColumnDefinitions.Clear ();
                        _grid.RowDefinitions.Clear ();
                        CreateColumnsForItems ( _grid, Items.OfType<ScreenerHeaderItem> () );
                    }
                }
            }
        }

        private static void ShowWhenReadOnlyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var screenerHeader = sender as ScreenerHeaderItemsControl;
            if (screenerHeader != null)
            {
                if (screenerHeader.ShowWhenReadOnly)
                {
                    screenerHeader.RemoveVisibilityBinding();
                }
                else
                {
                    screenerHeader.SetVisibilityBinding();
                }
            }
        }

        private static void CreateColumnsForItems ( Grid grid, IEnumerable<ScreenerHeaderItem> items )
        {
            foreach ( var screenerHeaderItem in items )
            {
                var columnCount = grid.ColumnDefinitions.Count;
                grid.ColumnDefinitions.Add ( new ColumnDefinition { Width = screenerHeaderItem.ColumnWidth } );
                Grid.SetColumn ( screenerHeaderItem, columnCount );
            }
        }

        private static void RemoveColumnsForItems ( Grid grid, IEnumerable<ScreenerHeaderItem> items )
        {
            foreach ( var screenerHeaderItem in items )
            {
                var column = Grid.GetColumn ( screenerHeaderItem );
                grid.ColumnDefinitions.RemoveAt ( column );
            }
        }

        private void ApplyTemplateLayoutUpdated ( object sender, EventArgs e )
        {
            LayoutUpdated -= ApplyTemplateLayoutUpdated;
            _grid = VisualTreeHelper.GetChild ( _itemsPresenter, 0 ) as Grid;
            if ( _grid != null )
            {
                _grid.ColumnDefinitions.Clear ();
                _grid.RowDefinitions.Clear ();
                CreateColumnsForItems ( _grid, Items.OfType<ScreenerHeaderItem> () );
            }
        }

        private void SetVisibilityBinding()
        {
            var visibilityBinding = new Binding();
            visibilityBinding.Source = this;
            visibilityBinding.Path = new PropertyPath(PropertyUtil.ExtractPropertyName(() => IsReadOnly));
            visibilityBinding.Converter = Application.Current.Resources["InvertedBooleanToVisibilityConverterInstance"] as IValueConverter;
            SetBinding(VisibilityProperty, visibilityBinding);
        }

        private void RemoveVisibilityBinding()
        {
            ClearValue(VisibilityProperty);
            Visibility = Visibility.Visible;
        }

        #endregion
    }
}
