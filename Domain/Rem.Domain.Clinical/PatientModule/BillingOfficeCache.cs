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

using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Billing Office cache defines a billing office.  
    /// </summary>
    public class BillingOfficeCache : AuditableAggregateRootBase
    {
        private readonly IList<PayorCache> _payorCaches;
        private Agency _agency;

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficeCache"/> class.
        /// </summary>
        protected internal BillingOfficeCache ()
        {
            _payorCaches = new List<PayorCache> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingOfficeCache"/> class.
        /// </summary>
        /// <param name="agency">The agency.</param>
        protected internal BillingOfficeCache ( Agency agency )
        {
            Check.IsNotNull ( agency, () => Agency );
            _agency = agency;
        }

        /// <summary>
        /// Gets Agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency
        {
            get { return _agency; }
            private set { ApplyPropertyChange ( ref _agency, () => Agency, value ); }
        }

        /// <summary>
        /// Gets the payor caches.
        /// </summary>
        public virtual IEnumerable<PayorCache> PayorCaches
        {
            get { return _payorCaches.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Adds the payor cache.
        /// </summary>
        /// <param name="payorCache">The payor cache.</param>
        public virtual void AddPayorCache ( PayorCache payorCache )
        {
            Check.IsNotNull ( payorCache, "payorCache is required." );
            payorCache.ReviseBillingOfficeCache ( this );
            _payorCaches.Add ( payorCache );
            NotifyItemAdded ( () => PayorCaches, payorCache );
        }

        /// <summary>
        /// Removes the payor cache.
        /// </summary>
        /// <param name="payorCache">The payor cache.</param>
        public virtual void RemovePayorCache ( PayorCache payorCache )
        {
            _payorCaches.Delete ( payorCache );
            NotifyItemRemoved ( () => PayorCaches, payorCache );
        }
    }
}
