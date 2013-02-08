using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Substance Problem Type class.
    /// </summary>
    [DataContract]
    public class SubstanceProblemTypeDto : TedsLookupBaseDto
    {
        #region Constants and Fields

        private bool? _singleDrugCategoryIndicator;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [DataMember]
        public bool? SingleDrugCategoryIndicator
        {
            get { return _singleDrugCategoryIndicator; }

            set { ApplyPropertyChange(ref _singleDrugCategoryIndicator, () => SingleDrugCategoryIndicator, value); }
        }

        #endregion
    }
}
