using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for LocationDisplayName class.
    /// </summary>
    public partial class LocationDisplayNameDto : ISearchResultDto
    {
        #region Public Properties

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        public string SelectedText
        {
            get { return DisplayName; }
        }

        #endregion
    }
}
