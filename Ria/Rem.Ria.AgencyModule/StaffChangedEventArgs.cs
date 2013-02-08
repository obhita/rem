namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// StaffChangedEventArgs class.
    /// </summary>
    public class StaffChangedEventArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public object Sender { get; set; }

        /// <summary>
        /// Gets or sets the staff key.
        /// </summary>
        /// <value>The staff key.</value>
        public long StaffKey { get; set; }

        #endregion
    }
}
