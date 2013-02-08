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

using System.Linq;
using Rem.Domain.Billing.BillingOfficeModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    ///   This class defines basic repository services for the <see cref="Rem.Domain.Billing.BillingOfficeModule">BillingOffice</see> .
    /// </summary>
    public class BillingOfficeRepository : NHibernateRepositoryBase<BillingOffice>, IBillingOfficeRepository
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="BillingOfficeRepository" /> class.
        /// </summary>
        /// <param name="sessionProvider"> The session provider. </param>
        public BillingOfficeRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Gets the by agency key.
        /// </summary>
        /// <param name="agencyKey"> The agency key. </param>
        /// <returns> A billing office. </returns>
        public BillingOffice GetByAgencyKey ( long agencyKey )
        {
            var billingOffice =
                Session.QueryOver<BillingOffice> ()
                    .Where ( p => p.Agency.Key == agencyKey )
                    .List ()
                    .FirstOrDefault ();
            return billingOffice;
        }

        /// <summary>
        ///   Gets the by key.
        /// </summary>
        /// <param name="key"> The key. </param>
        /// <returns> A <see cref="BillingOffice" /> BillingOffice. </returns>
        public BillingOffice GetByKey ( long key )
        {
            return Helper.GetEntityByKey ( key );
        }

        /// <summary>
        ///   Makes a BillingOffice persistent.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns> A <see cref="BillingOffice" /> BillingOffice. </returns>
        public BillingOffice MakePersistent ( BillingOffice entity )
        {
            return Helper.MakePersistent ( entity );
        }

        /// <summary>
        ///   Makes a BillingOffice transient.
        /// </summary>
        /// <param name="entity"> The entity. </param>
        public void MakeTransient ( BillingOffice entity )
        {
            Helper.MakeTransient ( entity );
        }

        #endregion
    }
}
