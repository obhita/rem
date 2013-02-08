using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// Telerik GridViewRow Style Selector
    /// </summary>
    public class RowStyleSelector : StyleSelector
    {
        /// <summary>
        /// <para> When overriden this method is used for determining the Style of
        /// items.
        /// </para>      
        /// </summary>
        /// <param name="item">The item for whoose container is wanted.</param>
        /// <param name="container">The container for which a Style is selected.</param>
        /// <returns>
        /// The Style for the given container.
        /// </returns>
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (((GridViewRow)container).GridViewDataControl.Items.IndexOf(item) == 0)
            {
                Style style = new Style(typeof(GridViewRow));
                Setter setter = new Setter(GridViewRow.DetailsVisibilityProperty, Visibility.Visible);
                style.Setters.Add(setter);
                return style;
            }

            return new Style(typeof(GridViewRow));
        }
    }
}
