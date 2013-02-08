using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Commands;
using Rem.Ria.Infrastructure.View.CustomControls;
using Telerik.Windows.Controls;
using PrismDelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// AddEditableItemsControlItemBehavior class
    /// </summary>
    public class AddEditableItemsControlItemBehavior : Behavior<RadButton>
    {
        #region Constructure and field

        /// <summary>
        /// EditableItemsControlContainer Property
        /// </summary>
        public static readonly DependencyProperty EditableItemsControlContainerProperty =
            DependencyProperty.Register (
                "EditableItemsControlContainer",
                typeof( FrameworkElement ),
                typeof( AddEditableItemsControlItemBehavior ),
                new PropertyMetadata ( EditableItemsControlContainerChanged ) );

        /// <summary>
        /// SameCommand Property
        /// </summary>
        public static readonly DependencyProperty SameCommandProperty =
            DependencyProperty.Register (
                "SaveCommand",
                typeof( ICommand ),
                typeof( AddEditableItemsControlItemBehavior ),
                new PropertyMetadata ( SameCommandChanged ) );
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the editable items control container.
        /// </summary>
        /// <value>
        /// The editable items control container.
        /// </value>
        public FrameworkElement EditableItemsControlContainer
        {
            get { return (FrameworkElement)GetValue(EditableItemsControlContainerProperty); }
            set { SetValue(EditableItemsControlContainerProperty, value); }
        }

        /// <summary>
        /// Gets or sets the save command.
        /// </summary>
        /// <value>
        /// The save command.
        /// </value>
        public ICommand SaveCommand
        {
            get { return ( ICommand )GetValue ( SameCommandProperty ); }
            set { SetValue ( SameCommandProperty, value ); }
        }

        #endregion

        #region Protected method

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            SetCommand ();
        }
        
        #endregion

        #region Method

        private void SaveAddingItemCommand ()
        {
            var editablItemsControls = GetChildren<EditableItemsControl> ( EditableItemsControlContainer );

            foreach ( var editableItemsControl in editablItemsControls )
            {
                if ( editableItemsControl.IsAddingDirty )
                {
                    editableItemsControl.AddAddingItemCommand.Execute ( null );
                }
            }
        }

        private void SetCommand ()
        {
            if ( EditableItemsControlContainer == null || SaveCommand == null )
            {
                return;
            }

            var compositeCommand = new CompositeCommand ();

            compositeCommand.RegisterCommand ( new PrismDelegateCommand ( SaveAddingItemCommand ) );

            compositeCommand.RegisterCommand ( SaveCommand );

            AssociatedObject.Command = compositeCommand;
        }

        private static void EditableItemsControlContainerChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BehaviorSetCommand ( d );
        }

        private static void SameCommandChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            BehaviorSetCommand ( d );
        }

        private static void BehaviorSetCommand ( DependencyObject d )
        {
            var behavior = d as AddEditableItemsControlItemBehavior;
            if ( behavior != null )
            {
                behavior.SetCommand ();
            }
        }

        private IEnumerable<T> GetChildren<T> ( DependencyObject d ) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount ( d );
            for ( int i = 0; i < count; i++ )
            {
                var c = VisualTreeHelper.GetChild ( d, i );
                if ( c is T )
                {
                    yield return ( T )c;
                }
                foreach ( var c1 in GetChildren<T> ( c ) )
                {
                    yield return c1;
                }
            }
        }

        #endregion
    }
}
