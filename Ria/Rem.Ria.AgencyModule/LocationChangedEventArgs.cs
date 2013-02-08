namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// LocationChangedEventArgs class.
    /// </summary>
    public class LocationChangedEventArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the location key.
        /// </summary>
        /// <value>The location key.</value>
        public long LocationKey { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public object Sender { get; set; }

        #endregion
    }
}
