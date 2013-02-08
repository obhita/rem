using NServiceBus;

namespace Rem.Domain.ClinicalBilling.ContextMap.Messages
{
    /// <summary>
    /// The interface defines a patient renamed message.
    /// </summary>
    public interface IPatientRenamedMessage : IMessage
    {
        /// <summary>
        /// Gets or sets the patient key.
        /// </summary>
        /// <value>
        /// The patient key.
        /// </value>
        long PatientKey { get; set; }
    }
}
