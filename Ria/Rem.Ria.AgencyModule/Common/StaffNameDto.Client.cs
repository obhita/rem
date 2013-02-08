using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffName class.
    /// </summary>
    public partial class StaffNameDto : ISearchResultDto
    {
        #region Public Properties

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return FirstName + " " + MiddleInitial + " " + LastName + " " + ProfessionalCredentialNote; }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        public string SelectedText
        {
            get { return FullName; }
        }

        #endregion
    }
}
