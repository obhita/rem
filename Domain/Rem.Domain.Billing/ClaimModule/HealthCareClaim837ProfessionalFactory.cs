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

namespace Rem.Domain.Billing.ClaimModule
{
    /// <summary>
    ///    The HealthCareClaim837ProfessionalFactory implements lifetime management of the <see cref="HealthCareClaim837Professional">HealthCareClaim837Professional</see>.
    /// </summary>
    public class HealthCareClaim837ProfessionalFactory : IHealthCareClaim837ProfessionalFactory
    {
        #region Constants and Fields

        private readonly IHealthCareClaim837ProfessionalRepository _healthCareClaim837ProfessionalRepository;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="HealthCareClaim837ProfessionalFactory" /> class.
        /// </summary>
        /// <param name="healthCareClaim837ProfessionalRepository"> The HealthCareClaim837Professional repository. </param>
        public HealthCareClaim837ProfessionalFactory ( IHealthCareClaim837ProfessionalRepository healthCareClaim837ProfessionalRepository )
        {
            _healthCareClaim837ProfessionalRepository = healthCareClaim837ProfessionalRepository;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Creates the health care claim837 professional.
        /// </summary>
        /// <param name="claimBatch"> The claim batch. </param>
        /// <param name="document"> The document. </param>
        /// <returns> The Health Care Claim 837 Professional.</returns>
        public HealthCareClaim837Professional CreateHealthCareClaim837Professional ( ClaimBatch claimBatch, byte[] document )
        {
            Check.IsNotNull ( claimBatch, "Claim Batch is required." );
            Check.IsNotNull ( document, "Document is required." );

            var healthCareClaim837Professional = new HealthCareClaim837Professional ( claimBatch, document );

            _healthCareClaim837ProfessionalRepository.MakePersistent ( healthCareClaim837Professional );

            return healthCareClaim837Professional;
        }

        /// <summary>
        ///   Destroys the clinical case.
        /// </summary>
        /// <param name="healthCareClaim837Professional"> The HealthCareClaim837Professional. </param>
        public void DestroyClinicalCase ( HealthCareClaim837Professional healthCareClaim837Professional )
        {
            _healthCareClaim837ProfessionalRepository.MakeTransient ( healthCareClaim837Professional );
        }

        #endregion
    }
}
