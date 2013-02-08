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

using System;
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.VisitModule.Event;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// Represents the information that is managed for a visit when a certified coder
    /// is reviewing the diagnosis and procedure codes for a visit.
    /// </summary>
    public class CodingContext : AuditableAggregateRootBase
    {
        private readonly IList<Procedure> _procedures;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingContext"/> class.
        /// </summary>
        protected internal CodingContext ( )
        {
            _procedures = new List<Procedure>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodingContext"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="procedures">The procedures.</param>
        protected internal CodingContext(Visit visit, IList<Procedure> procedures )
        {
            Check.IsNotNull(visit, "Visit is required.");
            Visit = visit;

            _procedures = new List<Procedure>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the staff representing the person who reviewed the coding of the visit.
        /// </summary>
        public virtual Staff CodedByStaff { get; private set; }

        /// <summary>
        /// Gets the date that the visit coding was reviewed.
        /// </summary>
        public virtual DateTime CodedDate { get; private set; }

        /// <summary>
        /// Gets the coding status.
        /// </summary>
        public virtual CodingStatus CodingStatus { get; private set; }

        /// <summary>
        /// Gets the error note.
        /// </summary>
        public virtual string ErrorNote { get; private set; }
        
        /// <summary>
        /// Gets the visit.
        /// </summary>
        public virtual Visit Visit { get; private set; }

        /// <summary>
        /// Gets the procedures.
        /// </summary>
        public virtual IEnumerable<Procedure> Procedures
        {
            get { return _procedures.ToList().AsReadOnly(); }
            private set { }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reports an error message and sets the <see cref="CodingContext.CodingStatus"/> to HasError.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        public virtual void ReportError ( string errorMessage )
        {
            Check.IsNotNullOrWhitespace ( errorMessage, () => ErrorNote );
            ErrorNote = errorMessage;
            CodingStatus = CodingStatus.HasError;
        }

        /// <summary>
        /// Reviews the visit.
        /// </summary>
        public virtual void ReviewVisit()
        {
            // Do something

            // Then raise a domain event
            DomainEvent.Raise(new VisitReviewedEvent { VisitKey = Visit.Key });
        }

        #endregion
    }
}
