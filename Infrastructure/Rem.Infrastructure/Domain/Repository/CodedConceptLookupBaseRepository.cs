using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using NHibernate.Criterion;
using Rem.Domain.Core.CommonModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.CommonModule.CodedConceptLookupBase">CodedConceptLookupBase</see>.
    /// </summary>
    public class CodedConceptLookupBaseRepository : LookupValueRepository, ICodedConceptLookupBaseRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodedConceptLookupBaseRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public CodedConceptLookupBaseRepository ( ISessionProvider sessionProvider )
            : base ( sessionProvider )
        {
        }

        #region ICodedConceptLookupBaseRepository Members

        /// <summary>
        /// Gets the lookup by coded concept code.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>Coded Concept Lookup Base</returns>
        public CodedConceptLookupBase GetLookupByCodedConceptCode ( Type type, string codedConceptCode )
        {
            IList result = Session.CreateCriteria ( type )
                .Add ( Restrictions.Eq ( "CodedConceptCode", codedConceptCode ) )
                .SetMaxResults ( 1 )
                .List ();

            bool found = result != null && result.Count > 0;

            CodedConceptLookupBase lookupByWellKnownName = found ? result[0] as CodedConceptLookupBase : null;

            return lookupByWellKnownName;
        }

        /// <summary>
        /// Gets the lookup by coded concept code.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>Coded Concept Lookup Base</returns>
        public TLookup GetLookupByCodedConceptCode<TLookup> ( string codedConceptCode ) where TLookup : CodedConceptLookupBase
        {
            IList result = Session.CreateCriteria ( typeof( TLookup ) )
                .Add ( Restrictions.Eq ( "CodedConceptCode", codedConceptCode ) )
                .SetMaxResults ( 1 )
                .List ();

            bool found = result != null && result.Count > 0;

            return found ? result[0] as TLookup : null;
        }

        #endregion
    }
}
