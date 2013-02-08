using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls.DragDrop;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Generic FrameworkElement behavior deals with Drag.
    /// </summary>
    /// <typeparam name="T">T is  FrameworkElement.</typeparam>
    public class DragBehavior<T> : Behavior<T> where T : FrameworkElement
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for AllowDragProperty Property.
        /// </summary>
        public static readonly DependencyProperty AllowDragProperty =
            DependencyProperty.Register(
                "AllowDrag",
                typeof(bool),
                typeof(DragBehavior<T>),
                new PropertyMetadata(false));

        /// <summary>
        /// Dependency Property for DragContentTemplateProperty Property.
        /// </summary>
        public static readonly DependencyProperty DragContentTemplateProperty =
            DependencyProperty.Register(
                "DragContentTemplate",
                typeof(DataTemplate),
                typeof(DragBehavior<T>),
                new PropertyMetadata(null));

        /// <summary>
        /// Dependency Property for DragQueryCommandProperty Property.
        /// </summary>
        public static readonly DependencyProperty DragQueryCommandProperty =
            DependencyProperty.Register(
                "DragQueryCommand",
                typeof(ICommand),
                typeof(DragBehavior<T>),
                new PropertyMetadata(null));

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow drag].
        /// </summary>
        /// <value><c>true</c> if [allow drag]; otherwise, <c>false</c>.</value>
        public bool AllowDrag
        {
            get { return (bool)GetValue(AllowDragProperty); }
            set { SetValue(AllowDragProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drag content template.
        /// </summary>
        /// <value>The drag content template.</value>
        public DataTemplate DragContentTemplate
        {
            get { return (DataTemplate)GetValue(DragContentTemplateProperty); }
            set { SetValue(DragContentTemplateProperty, value); }
        }

        /// <summary>
        /// Use this command to tell drag drop manager whether drag is possible or not, sends a command parameter of type DragDropQueryEventArgs
        /// </summary>
        /// <value>The drag query command.</value>
        public ICommand DragQueryCommand
        {
            get { return (ICommand)GetValue(DragQueryCommandProperty); }
            set { SetValue(DragQueryCommandProperty, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            if (AllowDrag)
            {
                RadDragAndDropManager.SetAllowDrag(AssociatedObject, true);

                RadDragAndDropManager.AddDragQueryHandler(AssociatedObject, OnDragQuery);
                RadDragAndDropManager.AddDragInfoHandler(AssociatedObject, OnDragInfo);
            }
        }

        /// <summary>
        /// Called when [drag info].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.DragDrop.DragDropEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDragInfo(object sender, DragDropEventArgs e)
        {
            e.Handled = true;

            if (e.Options.Status == DragStatus.DragInProgress)
            {
                var cue = RadDragAndDropManager.GenerateVisualCue();
                cue.Content = e.Options.Payload;
                if (DragContentTemplate != null)
                {
                    cue.ContentTemplate = DragContentTemplate;
                }
                e.Options.DragCue = cue;
            }
        }

        /// <summary>
        /// Called when [drag query].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.DragDrop.DragDropQueryEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDragQuery(object sender, DragDropQueryEventArgs e)
        {
            e.QueryResult = true;

            if (e.Options.Status == DragStatus.DragQuery)
            {
                var dataConext = ((e.Source) as FrameworkElement).DataContext;
                if (dataConext != null)
                {
                    e.Options.Payload = dataConext;

                    if ( DragQueryCommand != null )
                    {
                        DragQueryCommand.Execute ( e );
                    }
                }
                else
                {
                    e.QueryResult = false;
                }
            }

            e.Handled = true;
        }

        #endregion
    }
}