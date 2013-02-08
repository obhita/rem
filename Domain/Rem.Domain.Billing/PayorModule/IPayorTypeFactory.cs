using Rem.Domain.Billing.BillingOfficeModule;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// Defines the interface of Payor type factory.
    /// </summary>
    public interface IPayorTypeFactory
    {
        /// <summary>
        /// Creates the type of the payor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="billingForm">The billing form.</param>
        /// <returns>A payor type.</returns>
        PayorType CreatePayorType(string name, BillingOffice billingOffice, BillingForm billingForm);


        /// <summary>
        /// Destroys the type of the payor.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        void DestroyPayorType(PayorType payorType);
    }
}
