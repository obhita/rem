using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behavior RAD grid view row tooltip.
    /// </summary>
    public class RadGridViewRowToolTipBehavior : Behavior<RadGridView>
    {
        /// <summary>
        /// Dependency Property for DataTemplateResouce Property
        /// </summary>
        public static readonly DependencyProperty ToolTipDataTemplateProperty =
           DependencyProperty.Register(
               "ToolTipDataTemplate",
               typeof(DataTemplate),
               typeof(RadGridViewRowToolTipBehavior),
               new PropertyMetadata(null));


        /// <summary>
        /// Gets or sets the tool tip data template.
        /// </summary>
        /// <value>
        /// The tool tip data template.
        /// </value>
        public DataTemplate ToolTipDataTemplate
        {
            get { return (DataTemplate) GetValue(ToolTipDataTemplateProperty); }
            set { SetValue(ToolTipDataTemplateProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.RowLoaded += AssociatedObjectRowLoaded;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.RowLoaded -= AssociatedObjectRowLoaded;
        }

        private void AssociatedObjectRowLoaded(object sender, RowLoadedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            if (row != null)
            {
                var toolTip = new ToolTip
                    {
                        Content = row.DataContext,
                        ContentTemplate = ToolTipDataTemplate,
                    };
                ToolTipService.SetToolTip(row, toolTip);
            }
        }
    }
}
