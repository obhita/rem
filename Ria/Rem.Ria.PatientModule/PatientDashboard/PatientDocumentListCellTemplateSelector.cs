using System.Windows;
using Rem.Ria.PatientModule.Web.Common;
using Telerik.Windows.Controls;

namespace Rem.Ria.PatientModule.PatientDashboard
{
    /// <summary>
    /// Class for selecting task group list edit data template.
    /// </summary>
    public class PatientDocumentListCellTemplateSelector : DataTemplateSelector
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the edit template.
        /// </summary>
        /// <value>
        /// The edit template.
        /// </value>
        public DataTemplate EditTemplate { get; set; }

        /// <summary>
        /// Gets or sets the read only template.
        /// </summary>
        /// <value>
        /// The read only template.
        /// </value>
        public DataTemplate ReadOnlyTemplate { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, returns a DataTemplate based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>A <see cref="System.Windows.DataTemplate"/></returns>
        public override DataTemplate SelectTemplate ( object item, DependencyObject container )
        {
            if (item is PatientDocumentDto)
            {
                var patietDocumentDto = item as PatientDocumentDto;
                if (patietDocumentDto.Key == 0)
                {
                    return EditTemplate;
                }

                return ReadOnlyTemplate;
            }

            return null;
        }

        #endregion
    }
}
