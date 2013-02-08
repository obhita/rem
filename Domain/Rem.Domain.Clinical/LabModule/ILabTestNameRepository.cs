using Pillar.Domain;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// ILabSpecimenTypeRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Clinical.LabModule.LabTestName">LabTestName</see>.
    /// </summary>
    public interface ILabTestNameRepository : IRepository<LabTestName>
    {
        /// <summary>
        /// Gets the by coded concept code.
        /// </summary>
        /// <param name="codedConceptCode">
        /// The coded concept code.
        /// </param>
        /// <returns>
        /// A LabTestName.
        /// </returns>
        LabTestName GetByCodedConceptCode ( string codedConceptCode );
    }
}
