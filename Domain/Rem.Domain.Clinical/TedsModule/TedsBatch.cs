using System;
using System.Collections.Generic;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines TEDS batch configurations such as start date and end date, that are shred by admission and discharge batch.
    /// </summary>
    public class TedsBatch : AuditableAggregateRootBase
    {
        private readonly IList<TedsAdmissionBatch> _tedsAdmissionBatches;
        private readonly IList<TedsDischargeBatch> _tedsDischargeBatches;

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsBatch"/> class.
        /// </summary>
        protected internal TedsBatch ()
        {
            _tedsAdmissionBatches = new List<TedsAdmissionBatch> ();
            _tedsDischargeBatches = new List<TedsDischargeBatch> ();
        }


        /// <summary>
        /// Gets the agency.
        /// </summary>
        public virtual Agency Agency { get; private set; }

        /// <summary>
        /// Gets the submission date range.
        /// </summary>
        public virtual DateRange SubmissionDateRange { get; private set; }

        /// <summary>
        /// Gets the extracted staff.
        /// </summary>
        public virtual Staff ExtractedStaff { get; private set; }

        /// <summary>
        /// Gets the teds batch status.
        /// </summary>
        public virtual TedsBatchStatus TedsBatchStatus { get; private set; }

        /// <summary>
        /// Gets the report date.
        /// </summary>
        public virtual DateTime? ReportDate { get; private set; }

        /// <summary>
        /// Gets the teds admission batches.
        /// </summary>
        public virtual IList<TedsAdmissionBatch> TedsAdmissionBatches
        {
            get { return _tedsAdmissionBatches; }
            private set { }
        }

        /// <summary>
        /// Gets the teds discharge batches.
        /// </summary>
         public virtual IList<TedsDischargeBatch> TedsDischargeBatches
        {
            get { return _tedsDischargeBatches; }
            private set { }
        }
    }
}
