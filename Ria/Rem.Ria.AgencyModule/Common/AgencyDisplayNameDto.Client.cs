using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for AgencyDisplayName class.
    /// </summary>
    public partial class AgencyDisplayNameDto : ISearchResultDto
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
