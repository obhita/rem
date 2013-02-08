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
using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffLicense defines a license held by a staff.
    /// </summary>
    public class StaffLicense : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        private DateRange _effectiveDateRange;
        private License _license;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffLicense"/> class.
        /// </summary>
        protected internal StaffLicense ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffLicense"/> class.
        /// </summary>
        /// <param name="license">The license.</param>
        /// <param name="effectiveDateRange">The effective date range.</param>
        public StaffLicense(License license, DateRange effectiveDateRange)
        {
            Check.IsNotNull ( license, () => License );
            _license = license;
            _effectiveDateRange = effectiveDateRange;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the license.
        /// </summary>
        [NotNull]
        public virtual License License
        {
            get { return _license; }
            private set { ApplyPropertyChange ( ref _license, () => License, value ); }
        }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            private set { ApplyPropertyChange ( ref _effectiveDateRange, () => EffectiveDateRange, value ); }
        }

        #endregion
            
        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffLicense">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>
        public virtual bool ValuesEqual(StaffLicense staffLicense)
        {
            if (staffLicense == null)
            {
                return false;
            }

            var valuesEqual =
                Equals(_license.Key, staffLicense._license.Key) &&
                Equals(_effectiveDateRange, staffLicense._effectiveDateRange);
            return valuesEqual;
        }
    }
}