namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Data transfer object for SystemAccount class.
    /// </summary>
    /// <content>
    /// Contains client functionality for the SystemAccountDto.
    /// </content>
    public partial class SystemAccountDto
    {
        #region Constants and Fields

        private bool _isLinked;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is linked.
        /// </summary>
        /// <value><c>true</c> if this instance is linked; otherwise, <c>false</c>.</value>
        public virtual bool IsLinked
        {
            get { return _isLinked; }
            set { ApplyPropertyChange ( ref _isLinked, () => IsLinked, value ); }
        }

        #endregion
    }
}
