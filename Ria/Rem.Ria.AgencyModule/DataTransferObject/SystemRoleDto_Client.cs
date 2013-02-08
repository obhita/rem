namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// System Role Dto class.
    /// </summary>
    public partial class SystemRoleDto
    {
        #region Constants and Fields

        private bool _isSelectable;
        private bool _isSelected;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selectable.
        /// </summary>
        /// <value><c>true</c> if this instance is selectable; otherwise, <c>false</c>.</value>
        public bool IsSelectable
        {
            get { return _isSelectable; }
            set { ApplyPropertyChange ( ref _isSelectable, () => IsSelectable, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected
        {
            get { return _isSelected; }
            set { ApplyPropertyChange ( ref _isSelected, () => IsSelected, value ); }
        }

        #endregion
    }
}
