﻿namespace Rem.Ria.AgencyModule
{
    /// <summary>
    /// ProgramChangedEventArgs class.
    /// </summary>
    public class ProgramChangedEventArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the agency key.
        /// </summary>
        /// <value>The agency key.</value>
        public long AgencyKey { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        /// <value>The sender.</value>
        public object Sender { get; set; }

        #endregion
    }
}
