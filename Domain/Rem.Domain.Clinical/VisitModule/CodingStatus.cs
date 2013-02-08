namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// The coding status of a visit.
    /// </summary>
    public enum CodingStatus
    {
        /// <summary>
        /// Means that critical billing information related to a visit has been changed and that the visit must be resent to billing.
        /// </summary>
        ChangedAndPendingReview,

        /// <summary>
        /// Means that an error occured when the billing system received the coding context.
        /// </summary>
        HasError,
        
        /// <summary>
        /// Means that the coding context needs to be reviewed by the certified coder.
        /// </summary>
        PendingReview,

        /// <summary>
        /// Means that the visit has been sent to billing and has been successfully processed.
        /// </summary>
        ReceivedByBilling,

        /// <summary>
        /// Means that the Visit has been reviewed and that the information has been sent to the billing system.
        /// </summary>
        ReviewedAndSentToBilling
    }
}