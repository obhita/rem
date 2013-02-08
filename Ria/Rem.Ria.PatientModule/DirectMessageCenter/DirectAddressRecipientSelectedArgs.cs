namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// DirectAddress Recipient Selected Event Arguments
    /// </summary>
    public class DirectAddressRecipientSelectedArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        #endregion
    }
}
