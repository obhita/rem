using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// ICodedConceptLookupBaseRepository class
    /// </summary>
    public interface ICodedConceptLookupBaseRepository : ILookupValueRepository
    {
        /// <summary>
        /// Gets the lookup by coded concept code.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>Coded Concept Lookup Base</returns>
        CodedConceptLookupBase GetLookupByCodedConceptCode(Type type, string codedConceptCode);

        /// <summary>
        /// Gets the lookup by coded concept code.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="codedConceptCode">The coded concept code.</param>
        /// <returns>Coded Concept Lookup Base</returns>
        TLookup GetLookupByCodedConceptCode<TLookup>(string codedConceptCode) where TLookup : CodedConceptLookupBase;
    }
}
