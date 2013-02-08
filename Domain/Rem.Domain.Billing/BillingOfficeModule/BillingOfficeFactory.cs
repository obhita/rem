#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using Pillar.Common.Utility;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Billing.BillingOfficeModule
{
    /// <summary>
    /// This class implements lifetime management of the <see cref="BillingOffice">BillingOffice</see>.
    /// </summary>
    public class BillingOfficeFactory : IBillingOfficeFactory
    {
        #region Constants and Fields

        private readonly IBillingOfficeRepository _billingOfficeRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficeFactory"/> class.
        /// </summary>
        /// <param name="billingOfficeRepository">The billing office repository.</param>
        public BillingOfficeFactory ( IBillingOfficeRepository billingOfficeRepository )
        {
            _billingOfficeRepository = billingOfficeRepository;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the billing office.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="administratorStaff">The administrator staff.</param>
        /// <param name="electronicTransmitterIdentificationNumber">The electronic transmitter identification number.</param>
        /// <param name="profile">The profile.</param>
        /// <returns>The Billing Office.</returns>
        public BillingOffice CreateBillingOffice (
            Agency agency, Staff administratorStaff, string electronicTransmitterIdentificationNumber, BillingOfficeProfile profile )
        {
            Check.IsNotNull ( agency, "Agency is required." );
            Check.IsNotNull ( administratorStaff, "Administrator Staff is required." );
            Check.IsNotNull ( electronicTransmitterIdentificationNumber, "Electronic Transmitter Identification Number is required." );

            var billingOffice = new BillingOffice ( agency, administratorStaff, electronicTransmitterIdentificationNumber, profile );

            _billingOfficeRepository.MakePersistent ( billingOffice );

            return billingOffice;
        }

        #endregion
    }
}
