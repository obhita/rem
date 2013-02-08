#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using System;
using Pillar.Common.Extension;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// GpraInterviewSectionAggregateNodeBase provides a base aggregrate node implementation.
    /// </summary>
    public abstract class GpraInterviewSectionAggregateNodeBase : IAggregateNode, IAuditable
    {
        private DateTimeOffset _createdTimestamp;
        private SystemAccount _createdBySystemAccount;
        private DateTimeOffset _updatedTimestamp;
        private SystemAccount _updatedBySystemAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewSectionAggregateNodeBase"/> class.
        /// </summary>
        protected GpraInterviewSectionAggregateNodeBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraInterviewSectionAggregateNodeBase"/> class.
        /// </summary>
        /// <param name="gpraInterview">The gpra interview.</param>
        protected GpraInterviewSectionAggregateNodeBase(GpraInterview gpraInterview)
        {
            GpraInterview = gpraInterview;
        }

        /// <summary>
        /// Gets or sets the Gpra Interview.
        /// </summary>
        /// <value>
        /// The Gpra Interview.
        /// </value>
        [NotNull]
        public virtual GpraInterview GpraInterview { get; protected internal set; }

        #region Implementation of IAuditable

        /// <summary>
        /// Gets the created timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset CreatedTimestamp
        {
            get { return _createdTimestamp; }
            private set { _createdTimestamp = value; }
        }

        /// <summary>
        /// Gets or sets the created by system account.
        /// </summary>
        /// <value>
        /// The created by system account.
        /// </value>
        [NotNull]
        public virtual SystemAccount CreatedBySystemAccount
        {
            get { return _createdBySystemAccount; }
            protected set { _createdBySystemAccount = value; }
        }

        /// <summary>
        /// Gets the updated timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset UpdatedTimestamp
        {
            get { return _updatedTimestamp; }
            private set { _updatedTimestamp = value; }
        }

        /// <summary>
        /// Gets or sets the updated by system account.
        /// </summary>
        /// <value>
        /// The updated by system account.
        /// </value>
        [NotNull]
        public virtual SystemAccount UpdatedBySystemAccount
        {
            get { return _updatedBySystemAccount; }
            protected set { _updatedBySystemAccount = value; }
        }

        #endregion

        /// <summary>
        /// Gets the aggregate root.
        /// </summary>
        public virtual IAggregateRoot AggregateRoot
        {
            get { return GpraInterview; }
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        [IgnoreMapping]
        public virtual long Key
        {
            get { return GpraInterview.Key; }
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return GetType().Name.SeparatePascalCaseWords ();
        }
    }
}