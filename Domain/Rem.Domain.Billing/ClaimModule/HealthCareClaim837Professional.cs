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
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Billing.ClaimModule
{
    /// <summary>
    ///   The Claim defines an entity that encapsulates the Health Care Claim information as a file.
    /// </summary>
    public class HealthCareClaim837Professional : AuditableAggregateRootBase
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="HealthCareClaim837Professional" /> class.
        /// </summary>
        protected internal HealthCareClaim837Professional ()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="HealthCareClaim837Professional" /> class.
        /// </summary>
        /// <param name="claimBatch"> The claim batch. </param>
        /// <param name="document"> The document. </param>
        protected internal HealthCareClaim837Professional ( ClaimBatch claimBatch, byte[] document )
        {
            Check.IsNotNull ( claimBatch, () => ClaimBatch );
            Check.IsNotNull ( document, () => Document );
            ClaimBatch = claimBatch;
            Document = document;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets the claim batch. This claim batch's key is part of the <see cref="Document" /> .
        /// </summary>
        public virtual ClaimBatch ClaimBatch { get; private set; }

        /// <summary>
        ///   Gets the Health Care Claim 837 Professional document. This contains the <see cref="ClaimBatch" /> 's key.
        /// </summary>
        public virtual byte[] Document { get; private set; }

        #endregion
    }
}
