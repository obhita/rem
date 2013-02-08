using System.ServiceModel;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// WCF Service to get C32 based on Patient Key
    /// </summary>
    [ServiceContract]
    public interface IC32Service
    {
        /// <summary>
        /// Gets the C32.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>A C32 string.</returns>
        [OperationContract]
        string GetC32(long patientKey);
    }
}
