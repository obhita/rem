using System.Collections.Generic;
using NHibernate.Dialect;
using NHibernate.Id;
using NHibernate.Type;

namespace Pillar.Domain.NHibernate
{
    /// <summary>
    /// Class for generating custom table hi lo.
    /// </summary>
    public class CustomTableHiLoGenerator : TableHiLoGenerator
    {
        #region Public Methods

        /// <summary>
        /// Configures the specified type.
        /// </summary>
        /// <param name="type">The type to configure.</param>
        /// <param name="parms">The parameters.</param>
        /// <param name="dialect">The dialect.</param>
        public override void Configure ( IType type, IDictionary<string, string> parms, Dialect dialect )
        {
            parms[TableParamName] = "HiValue";
            parms[ColumnParamName] = "HiValueKey";
            parms[MaxLo] = "1000";
            parms["schema"] = "CommonModule";

            base.Configure ( type, parms, dialect );
        }

        #endregion
    }
}
