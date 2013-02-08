using System.Text;
using Rem.Ria.Infrastructure.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for StaffSearchResult class.
    /// </summary>
    public partial class StaffSearchResultDto : ISearchResultDto
    {
        #region Public Properties

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public string FullName
        {
            get { return FormatFullName (); }
        }

        /// <summary>
        /// Gets the selected text.
        /// </summary>
        public string SelectedText
        {
            get { return FullName; }
        }

        #endregion

        #region Methods

        private string FormatFullName ()
        {
            //TODO:Format the name based on a preference; possibly something defined globally.
            var names = new[] { FirstName, MiddleName, LastName };
            var separator = " ";

            return StaffFullNameFormatter ( names, separator );
        }

        private string StaffFullNameFormatter ( string[] names, string separator )
        {
            var formattedName = new StringBuilder ();
            foreach ( var name in names )
            {
                formattedName.Append ( !string.IsNullOrWhiteSpace ( name ) ? name + separator : string.Empty );
            }
            return formattedName.ToString ();
        }

        #endregion
    }
}
