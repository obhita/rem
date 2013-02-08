using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Marker interface to mark a request is query. If a request is query only, don't use DTC.
    /// </summary>
    public interface IQueryRequest
    {
    }
}
