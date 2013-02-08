using Rem.Domain.Billing.BillingOfficeModule;
using Rem.WellKnownNames.PayorModule;

namespace Rem.Domain.Billing.PayorModule
{
    /// <summary>
    /// The PayorFactory implements lifetime management of the <see cref="T:Rem.Domain.Billing.PayorModule.PayorType">PayorType</see>.
    /// </summary>
    public class PayorTypeFactory : IPayorTypeFactory
    {
        private readonly IPayorTypeRepository _payorTypeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorTypeFactory"/> class.
        /// </summary>
        /// <param name="payorTypeRepository">The payor type repository.</param>
        public PayorTypeFactory (IPayorTypeRepository payorTypeRepository)
        {
            _payorTypeRepository = payorTypeRepository;
        }

        /// <summary>
        /// Creates the type of the payor.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="billingOffice">The billing office.</param>
        /// <param name="billingForm">The billing form.</param>
        /// <returns>A payor type.</returns>
        public PayorType CreatePayorType ( string name, BillingOffice billingOffice, BillingForm billingForm )
        {
            var payorType = new PayorType ( name, billingOffice, billingForm );
            _payorTypeRepository.MakePersistent ( payorType );
            return payorType;
        }

        /// <summary>
        /// Destroys the type of the payor.
        /// </summary>
        /// <param name="payorType">Type of the payor.</param>
        public void DestroyPayorType ( PayorType payorType )
        {
            _payorTypeRepository.MakeTransient ( payorType );
        }
    }
}