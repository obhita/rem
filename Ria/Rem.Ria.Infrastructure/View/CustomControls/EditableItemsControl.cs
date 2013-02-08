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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Pillar.Common.Utility;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// EditableItemsControl class.
    /// </summary>
    public class EditableItemsControl : ItemsControl
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AddButtonMarginProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddButtonMarginProperty =
            DependencyProperty.Register (
                "AddButtonMargin",
                typeof( Thickness ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( new Thickness ( 0, 0, 0, 0 ) ) );

        /// <summary>
        /// Dependency Property for AddButtonVerticalAlignmentProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddButtonVerticalAlignmentProperty =
            DependencyProperty.Register (
                "AddButtonVerticalAlignment",
                typeof( VerticalAlignment ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( VerticalAlignment.Bottom ) );

        /// <summary>
        /// Dependency Property for AddingItemProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddingItemProperty =
            DependencyProperty.Register (
                "AddingItem",
                typeof( object ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null, AddingItemChanged ) );

        /// <summary>
        /// Dependency Property for AddingItemTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddingItemTemplateProperty =
            DependencyProperty.Register (
                "AddingItemTemplate",
                typeof( DataTemplate ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for AddingItemTypeProperty Property.
        /// </summary>
        public static readonly DependencyProperty AddingItemTypeProperty =
            DependencyProperty.Register (
                "AddingItemType",
                typeof( Type ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null, AddingItemTypeChanged ) );

        /// <summary>
        /// Dependency Property for CornerRadiusProperty Property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register (
                "CornerRadius",
                typeof( CornerRadius ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for HasItemsProperty Property.
        /// </summary>
        public static readonly DependencyProperty HasItemsProperty =
            DependencyProperty.Register (
                "HasItems",
                typeof( bool ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for IsAddRowVisibleProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsAddRowVisibleProperty =
            DependencyProperty.Register (
                "IsAddRowVisible",
                typeof( bool ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( true ) );

        /// <summary>
        /// Dependency Property for IsAddingDirtyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsAddingDirtyProperty =
            DependencyProperty.Register (
                "IsAddingDirty",
                typeof( bool ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( true, IsAddingDirtyChanged ) );

        /// <summary>
        /// Dependency Property for IsReadOnlyProperty Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register (
                "IsReadOnly",
                typeof( bool ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for ItemContainerStyleProperty Property.
        /// </summary>
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register (
                "ItemContainerStyle",
                typeof( Style ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null ) );

        /// <summary>
        /// Dependency Property for ItemTemplateProperty Property.
        /// </summary>
        public static new readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register (
                "ItemTemplate",
                typeof( DataTemplate ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( null, ItemTemplateChanged ) );

        /// <summary>
        /// Dependency Property for ShowAddRowOnTopProperty Property.
        /// </summary>
        public static readonly DependencyProperty ShowAddRowOnTopProperty =
            DependencyProperty.Register (
                "ShowAddRowOnTop",
                typeof( bool ),
                typeof( EditableItemsControl ),
                new PropertyMetadata ( false ) );

        private IEditableDtoWrapper _addingItemWrapper;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditableItemsControl"/> class.
        /// </summary>
        public EditableItemsControl ()
        {
            DefaultStyleKey = typeof( EditableItemsControl );

            AddAddingItemCommand = new DelegateCommand ( ExecuteAddAddingItemCommand, CanExecuteAddingItemCommand );
            ClearAddingItemCommand = new DelegateCommand ( ExecuteClearAddingItemCommand, CanExecuteAddingItemCommand );
            RemoveItemCommand = new DelegateCommand<object> ( ExecuteRemoveItemCommand );
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [adding item is dirty].
        /// </summary>
        public event EventHandler AddingItemIsDirty;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the add adding item command.
        /// </summary>
        public ICommand AddAddingItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the add button margin.
        /// </summary>
        /// <value>The add button margin.</value>
        public Thickness AddButtonMargin
        {
            get { return ( Thickness )GetValue ( AddButtonMarginProperty ); }
            set { SetValue ( AddButtonMarginProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the add button vertical alignment.
        /// </summary>
        /// <value>The add button vertical alignment.</value>
        public VerticalAlignment AddButtonVerticalAlignment
        {
            get { return ( VerticalAlignment )GetValue ( AddButtonVerticalAlignmentProperty ); }
            set { SetValue ( AddButtonVerticalAlignmentProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the adding item.
        /// </summary>
        /// <value>The adding item.</value>
        public object AddingItem
        {
            get { return GetValue ( AddingItemProperty ); }
            set { SetValue ( AddingItemProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the adding item template.
        /// </summary>
        /// <value>The adding item template.</value>
        public DataTemplate AddingItemTemplate
        {
            get { return ( DataTemplate )GetValue ( AddingItemTemplateProperty ); }
            set { SetValue ( AddingItemTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the type of the adding item.
        /// </summary>
        /// <value>The type of the adding item.</value>
        public Type AddingItemType
        {
            get { return ( Type )GetValue ( AddingItemTypeProperty ); }
            set { SetValue ( AddingItemTypeProperty, value ); }
        }

        /// <summary>
        /// Gets the clear adding item command.
        /// </summary>
        public ICommand ClearAddingItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public CornerRadius CornerRadius
        {
            get { return ( CornerRadius )GetValue ( CornerRadiusProperty ); }
            set { SetValue ( CornerRadiusProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has items.
        /// </summary>
        /// <value><c>true</c> if this instance has items; otherwise, <c>false</c>.</value>
        public bool HasItems
        {
            get { return ( bool )GetValue ( HasItemsProperty ); }
            set { SetValue ( HasItemsProperty, value ); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is add row visible.
        /// </summary>
        public bool IsAddRowVisible
        {
            get { return ( bool )GetValue ( IsAddRowVisibleProperty ); }
            private set { SetValue ( IsAddRowVisibleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is adding dirty.
        /// </summary>
        /// <value><c>true</c> if this instance is adding dirty; otherwise, <c>false</c>.</value>
        public bool IsAddingDirty
        {
            get { return ( bool )GetValue ( IsAddingDirtyProperty ); }
            set { SetValue ( IsAddingDirtyProperty, value ); }
        }

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
        /// Gets or sets the item container style.
        /// </summary>
        /// <value>The item container style.</value>
        public Style ItemContainerStyle
        {
            get { return ( Style )GetValue ( ItemContainerStyleProperty ); }
            set { SetValue ( ItemContainerStyleProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.DataTemplate"/> used to display each item.
        /// </summary>
        /// <value>The item template.</value>
        /// <returns>The template that specifies the visualization of the data objects. The default is null.</returns>
        public new DataTemplate ItemTemplate
        {
            get { return ( DataTemplate )GetValue ( ItemTemplateProperty ); }
            set { SetValue ( ItemTemplateProperty, value ); }
        }

        /// <summary>
        /// Gets the remove item command.
        /// </summary>
        public ICommand RemoveItemCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show add row on top].
        /// </summary>
        /// <value><c>true</c> if [show add row on top]; otherwise, <c>false</c>.</value>
        public bool ShowAddRowOnTop
        {
            get { return ( bool )GetValue ( ShowAddRowOnTopProperty ); }
            set { SetValue ( ShowAddRowOnTopProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates or identifies the element that is used to display the given item.
        /// </summary>
        /// <returns>The element that is used to display the given item.</returns>
        protected override DependencyObject GetContainerForItemOverride ()
        {
            var item = new EditableItemsControlItem
                { Parent = this, VerticalAlignment = VerticalContentAlignment, HorizontalAlignment = HorizontalContentAlignment };

            var readOnlyBinding = new Binding
                {
                    Source = this,
                    Path = new PropertyPath ( "IsReadOnly" ),
                    Mode = BindingMode.TwoWay
                };
            item.SetBinding ( EditableItemsControlItem.IsReadOnlyProperty, readOnlyBinding );

            if ( ItemContainerStyle != null )
            {
                item.Style = ItemContainerStyle;
            }
            return item;
        }

        /// <summary>
        /// Called when the value of the <see cref="P:System.Windows.Controls.ItemsControl.Items"/> property changes.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> that contains the event data</param>
        protected override void OnItemsChanged ( NotifyCollectionChangedEventArgs e )
        {
            base.OnItemsChanged ( e );
            IsAddRowVisible = Items.Count <= 0 || ShowAddRowOnTop;
            HasItems = Items.Count > 0;
        }

        private static void AddingItemChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var eic = d as EditableItemsControl;
            if ( eic != null )
            {
                if ( e.OldValue != null )
                {
                    eic.UnSubsribeAddingItemChanged ();
                }
                if ( e.NewValue != null )
                {
                    eic.SubsribeAddingItemChanged ();
                }
                ( eic.AddAddingItemCommand as DelegateCommand ).RaiseCanExecuteChanged ();
                ( eic.ClearAddingItemCommand as DelegateCommand ).RaiseCanExecuteChanged ();
            }
        }

        private static void AddingItemTypeChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var eic = d as EditableItemsControl;
            if ( eic != null )
            {
                if ( eic.AddingItemType != null )
                {
                    eic.AddingItem = Activator.CreateInstance ( eic.AddingItemType );
                }
            }
        }

        private static void IsAddingDirtyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var eic = d as EditableItemsControl;
            if ( eic != null )
            {
                if ( ( bool )e.NewValue && eic.AddingItemIsDirty != null )
                {
                    eic.AddingItemIsDirty ( eic, new EventArgs () );
                }
            }
        }

        private static void ItemTemplateChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var eic = d as EditableItemsControl;
            if ( eic != null )
            {
                eic.SetBaseItemTemplate ();
                if ( eic.ReadLocalValue ( AddingItemTemplateProperty ) == DependencyProperty.UnsetValue )
                {
                    eic.AddingItemTemplate = eic.ItemTemplate;
                }
            }
        }

        private bool CanExecuteAddingItemCommand ()
        {
            return AddingItem != null;
        }

        private void ExecuteAddAddingItemCommand ()
        {
            var collection = ( ItemsSource as IList );
            if ( collection == null && ItemsSource is ICollectionView )
            {
                collection = ( ItemsSource as ICollectionView ).SourceCollection as IList;
            }
            if ( collection != null && IsAddingDirty )
            {
                collection.Add ( AddingItem );
                AddingItem = Activator.CreateInstance ( AddingItem.GetType () );
            }
        }

        private void ExecuteClearAddingItemCommand ()
        {
            var publicProperties = AddingItem.GetType ().GetProperties ();
            foreach ( var publicProperty in publicProperties )
            {
                if ( publicProperty.CanWrite && publicProperty.GetSetMethod ( false ) != null )
                {
                    if ( publicProperty.PropertyType != typeof( string ) && typeof( IList ).IsAssignableFrom ( publicProperty.PropertyType ) )
                    {
                        var list = ( publicProperty.GetValue ( AddingItem, null ) as IList );
                        list.Clear ();
                    }
                    else
                    {
                        publicProperty.SetValue (
                            AddingItem,
                            publicProperty.DeclaringType.IsValueType
                                ? Activator.CreateInstance ( publicProperty.DeclaringType )
                                : null,
                            null );
                    }
                }
            }
        }

        private void ExecuteRemoveItemCommand ( object item )
        {
            if ( item == AddingItem )
            {
                AddingItem = Activator.CreateInstance ( AddingItem.GetType () );
            }
            else
            {
                var collection = ( ItemsSource as IList );
                if ( collection == null && ItemsSource is ICollectionView )
                {
                    collection = ( ItemsSource as ICollectionView ).SourceCollection as IList;
                }
                if ( collection != null )
                {
                    collection.Remove ( item );
                }
            }
        }

        private void SetBaseItemTemplate ()
        {
            base.ItemTemplate = ItemTemplate;
        }

        private void SubsribeAddingItemChanged ()
        {
            if ( AddingItem is IEditableDtoWrapper )
            {
                _addingItemWrapper = AddingItem as IEditableDtoWrapper;
            }
            else
            {
                var abstractdto = AddingItem as AbstractDataTransferObject;
                _addingItemWrapper = abstractdto != null ? new EditableDtoWrapper { EditableDto = abstractdto } : null;
            }
            var addingDirtyBinding = new Binding ( PropertyUtil.ExtractPropertyName<EditableDtoWrapper, bool> ( p => p.IsDirty ) );
            addingDirtyBinding.Source = _addingItemWrapper;
            addingDirtyBinding.Mode = BindingMode.OneWay;
            SetBinding ( IsAddingDirtyProperty, addingDirtyBinding );
        }

        private void UnSubsribeAddingItemChanged ()
        {
            if ( _addingItemWrapper != null )
            {
                _addingItemWrapper.Dispose ();
            }
        }

        #endregion
    }
}
