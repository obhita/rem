using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.DensAsiModule
{
    /// <summary>
    /// IDensAsiInterviewFactory interface defines DensAsiInterview creation and destruction services.
    /// </summary>
    public interface IDensAsiInterviewFactory : IActivityFactory
    {
        /// <summary>
        /// Creates the DensAsi interview.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A DensAsiInterview.</returns>
        DensAsiInterview CreateDensAsiInterview ( Visit visit );

        /// <summary>
        /// Destroys the DensAsi interview.
        /// </summary>
        /// <param name="densAsiInterview">The DensAsi interview.</param>
        void DestroyDensAsiInterview ( DensAsiInterview densAsiInterview );
    }
}