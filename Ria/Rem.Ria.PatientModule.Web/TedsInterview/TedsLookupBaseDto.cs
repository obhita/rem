using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// The <see cref="TedsLookupBaseDto"/> is a single value from a 'range of values', also known as 'lookup'.
    /// </summary>
    [DataContract]
    public class TedsLookupBaseDto : LookupValueDto
    {
        #region Constants and Fields

        private string _code;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [DataMember]
        public string Code
        {
            get { return _code; }

            set { ApplyPropertyChange(ref _code, () => Code, value); }
        }

        #endregion
    }
}
