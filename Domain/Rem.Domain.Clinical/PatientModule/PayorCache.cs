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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Payor defines payor profile such as payor's name and address.
    /// </summary>
    public class PayorCache : AuditableAggregateRootBase
    {
        private readonly IList<PayorCoverageCache> _payorCoverages;
        private BillingOfficeCache _billingOfficeCache;
        private string _name;
        private Address _address;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCache"/> class.
        /// </summary>
        protected internal PayorCache ()
        {
            _payorCoverages = new List<PayorCoverageCache> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayorCache"/> class.
        /// </summary>
        /// <param name="billingOfficeCache">
        /// The billing office cache.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="address">
        /// The address.
        /// </param>
        protected internal PayorCache ( BillingOfficeCache billingOfficeCache, string name, Address address )
        {
            Check.IsNotNull ( billingOfficeCache, () => BillingOfficeCache );
            Check.IsNotNull ( name, () => Name );

            _billingOfficeCache = billingOfficeCache;
            _name = name;
            _address = address;
        }

        /// <summary>
        /// Gets the billing office cache.
        /// </summary>
        [NotNull]
        public virtual BillingOfficeCache BillingOfficeCache
        {
            get { return _billingOfficeCache; }
            private set { ApplyPropertyChange ( ref _billingOfficeCache, () => BillingOfficeCache, value ); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public virtual Address Address
        {
            get { return _address; }
            private set { ApplyPropertyChange ( ref _address, () => Address, value ); }
        }

        /// <summary>
        /// Gets the payor coverages.
        /// </summary>
        public virtual IEnumerable<PayorCoverageCache> PayorCoverages
        {
            get { return _payorCoverages.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Revises the billing office cache.
        /// </summary>
        /// <param name="billingOfficeCache">
        /// The billing office cache.
        /// </param>
        public virtual void ReviseBillingOfficeCache ( BillingOfficeCache billingOfficeCache )
        {
            BillingOfficeCache = billingOfficeCache;
        }

        /// <summary>
        /// Revises the name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public virtual void ReviseName ( string name )
        {
            Name = name;
        }

        /// <summary>
        /// Revises the address.
        /// </summary>
        /// <param name="address">
        /// The address.
        /// </param>
        public virtual void ReviseAddress ( Address address )
        {
            Address = address;
        }

        /// <summary>
        /// Adds the payor coverage.
        /// </summary>
        /// <param name="payorCoverage">
        /// The payor coverage.
        /// </param>
        public virtual void AddPayorCoverage ( PayorCoverageCache payorCoverage )
        {
            Check.IsNotNull ( payorCoverage, "Payor Coverage is required." );
            payorCoverage.RevisePayorCache ( this );
            _payorCoverages.Add ( payorCoverage );
            NotifyItemAdded ( () => PayorCoverages, payorCoverage );
        }

        /// <summary>
        /// Removes the payor coverage.
        /// </summary>
        /// <param name="payorCoverage">
        /// The payor coverage.
        /// </param>
        public virtual void RemovePayorCoverage ( PayorCoverageCache payorCoverage )
        {
            _payorCoverages.Delete ( payorCoverage );
            NotifyItemRemoved ( () => PayorCoverages, payorCoverage );
        }
    }
}
