using System;
using System.Collections.Generic;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// ILookupValueRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Core.CommonModule.LookupBase">LookupBase</see>.
    /// </summary>
    public interface ILookupValueRepository
    {
        /// <summary>
        /// Gets all for a Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A IList&lt;LookupBase&gt;</returns>
        IList<LookupBase> GetAll ( Type type );

        /// <summary>
        /// Gets the name of the lookup by well known.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>A LookupBase</returns>
        LookupBase GetLookupByWellKnownName ( Type type, string wellKnownName );

        /// <summary>
        /// Gets the name of the lookup by well known.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>A TLookup</returns>
        TLookup GetLookupByWellKnownName<TLookup> ( string wellKnownName ) where TLookup : LookupBase;

        /// <summary>
        /// Gets the lookup by name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns>A LookupBase</returns>
        LookupBase GetLookupByName(Type type, string name);

        /// <summary>
        /// Gets the lookup by name.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>A TLookup</returns>
        TLookup GetLookupByName<TLookup>(string name) where TLookup : LookupBase;

        /// <summary>
        /// Gets the lookup by key.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns>A LookupBase</returns>
        LookupBase GetLookupByKey ( Type type, long key );

        /// <summary>
        /// Gets the lookup by key.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>A TLookup</returns>
        TLookup GetLookupByKey<TLookup> ( long key ) where TLookup : LookupBase;
    }
}