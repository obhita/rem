namespace Rem.Ria.AgencyModule.Web.Common
{
    /// <summary>
    /// Client-side specific version of the DataTransferObject Staff, representing the Staff Domain Entity.
    /// </summary>
    public partial class StaffDto
    {
        #region Constants and Fields

        private bool _isNewSystemAccountMode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is linked system account mode.
        /// </summary>
        /// <value><c>true</c> if this instance is linked system account mode; otherwise, <c>false</c>.</value>
        public virtual bool IsLinkedSystemAccountMode
        {
            get { return !_isNewSystemAccountMode; }

            set
            {
                ApplyPropertyChange ( ref _isNewSystemAccountMode, () => IsNewSystemAccountMode, !value );
                RaisePropertyChanged ( () => IsLinkedSystemAccountMode );
            }
        }

        /// <summary>
        /// Gets or sets the is linked to system account.
        /// </summary>
        /// <value>The is linked to system account.</value>
        public virtual bool? IsLinkedToSystemAccount
        {
            get { return SystemAccount != null && SystemAccount.Key != 0; }

            set
            {
                RaisePropertyChanged ( () => IsLinkedToSystemAccount );
                RaisePropertyChanged ( () => IsNotLinkedToSystemAccount );
            }
        }

        /// <summary>
        /// Gets or sets the is new system account mode.
        /// </summary>
        /// <value>The is new system account mode.</value>
        public virtual bool? IsNewSystemAccountMode
        {
            get { return _isNewSystemAccountMode; }

            set
            {
                ApplyPropertyChange ( ref _isNewSystemAccountMode, () => IsNewSystemAccountMode, value.Value );
                RaisePropertyChanged ( () => IsLinkedSystemAccountMode );
            }
        }

        /// <summary>
        /// Gets the is not linked to system account.
        /// </summary>
        public virtual bool? IsNotLinkedToSystemAccount
        {
            get { return !IsLinkedToSystemAccount; }
        }

        #endregion
    }
}
