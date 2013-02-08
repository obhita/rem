using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Generic FrameworkElement behavior deals with Drop.
    /// </summary>
    /// <typeparam name="T">T is  FrameworkElement.</typeparam>
    public class DropBehavior<T> : Behavior<T>
        where T : FrameworkElement
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AllowDropProperty Property.
        /// </summary>
        public static readonly DependencyProperty AllowDropProperty =
            DependencyProperty.Register (
                "AllowDrop",
                typeof( bool ),
                typeof( DropBehavior<T> ),
                new PropertyMetadata ( false ) );

        /// <summary>
        /// Dependency Property for DropQueryCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DropQueryCommandProperty =
            DependencyProperty.Register (
                "DropQueryCommand",
                typeof( ICommand ),
                typeof( DropBehavior<T> ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow drop].
        /// </summary>
        /// <value><c>true</c> if [allow drop]; otherwise, <c>false</c>.</value>
        public bool AllowDrop
        {
            get { return ( bool )GetValue ( AllowDropProperty ); }
            set { SetValue ( AllowDropProperty, value ); }
        }

        /// <summary>
        /// Use this command to tell drag drop manager whether drop is possible or not, send a command parameter of type DragDropQueryEventArgs
        /// </summary>
        /// <value>The drop query command.</value>
        public ICommand DropQueryCommand
        {
            get { return ( ICommand )GetValue ( DropQueryCommandProperty ); }
            set { SetValue ( DropQueryCommandProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            if (AllowDrop)
            {
                RadDragAndDropManager.SetAllowDrop(AssociatedObject, true);

                RadDragAndDropManager.AddDropQueryHandler(AssociatedObject, OnDropQuery);
                RadDragAndDropManager.AddDropInfoHandler(AssociatedObject, OnDropInfo);
            }
        }

        /// <summary>
        /// Called when [drop info].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.DragDrop.DragDropEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDropInfo ( object sender, DragDropEventArgs e )
        {
            e.Handled = true;
        }

        /// <summary>
        /// Called when [drop query].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.DragDrop.DragDropQueryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDropQuery ( object sender, DragDropQueryEventArgs e )
        {
            e.QueryResult = true;

            if (e.Options.Status == DragStatus.DropDestinationQuery)
            {
                if (DropQueryCommand != null)
                {
                    DropQueryCommand.Execute(e);
                }
            }

            e.Handled = true;
        }

        #endregion
    }
}