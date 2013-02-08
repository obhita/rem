using Pillar.Domain;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// ICodingContextRepository interface defines basic repository services for the 
    /// <see cref="T:Rem.Domain.Clinical.VisitModule.CodingContext">CodingContext</see>.
    /// </summary>
    public interface ICodingContextRepository : IRepository<CodingContext>
    {
        /// <summary>
        /// Gets the coding context by visit key.
        /// </summary>
        /// <param name="visitKey">The visit key.</param>
        /// <returns>A CodingContext instance.</returns>
        CodingContext GetByVisitKey ( long visitKey );
    }
}
