namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Defines the provenance interface.
    /// </summary>
    public interface IHasProvenance
    {
        /// <summary>
        /// Gets the provenance.
        /// </summary>
        Provenance Provenance { get; }
    }
}
