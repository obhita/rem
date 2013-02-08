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
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Rem.Domain.Core.CommonModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.CommonModule.LookupBase">LookupBase</see>.
    /// </summary>
    public class LookupValueRepository : NHibernateRepositoryBase<LookupBase>, ILookupValueRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupValueRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public LookupValueRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region ILookupValueRepository Members

        /// <summary>
        /// Gets all Lookup values for a Type.
        /// </summary>
        /// <param name="type">The lookup type.</param>
        /// <returns>
        /// A IList&lt;LookupBase&gt;
        /// </returns>
        public IList<LookupBase> GetAll ( Type type )
        {
            return Session.CreateCriteria ( type )
                .SetCacheable ( true )
                .SetCacheMode ( CacheMode.Normal )
                .List<LookupBase> ();
        }

        /// <summary>
        /// Gets a lookup by well known name.
        /// </summary>
        /// <param name="type">The lookup type.</param>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>
        /// A LookupBase object.
        /// </returns>
        public LookupBase GetLookupByWellKnownName ( Type type, string wellKnownName )
        {
            IList result = Session.CreateCriteria ( type )
                .Add ( Restrictions.Eq ( "WellKnownName", wellKnownName ) )
                .SetMaxResults ( 1 )
                .List ();

            bool found = result != null && result.Count > 0;

            LookupBase lookupByWellKnownName = found ? result[ 0 ] as LookupBase : null;

            return lookupByWellKnownName;
        }

        /// <summary>
        /// Gets a lookup by well known name.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="wellKnownName">Name of the well known.</param>
        /// <returns>A  lookup.</returns>
        public TLookup GetLookupByWellKnownName<TLookup> ( string wellKnownName ) where TLookup : LookupBase
        {
            IList result = Session.CreateCriteria ( typeof ( TLookup ) )
                .Add ( Restrictions.Eq ( "WellKnownName", wellKnownName ) )
                .SetMaxResults ( 1 )
                .List ();

            return result != null ? result[ 0 ] as TLookup : null;
        }

        /// <summary>
        /// Gets the name of the lookup by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns>A LookupBase</returns>
        public LookupBase GetLookupByName(Type type, string name)
        {
            IList result = Session.CreateCriteria(type)
                .Add(Restrictions.Eq("Name", name))
                .SetMaxResults(1)
                .List();

            bool found = result != null && result.Count > 0;

            LookupBase lookupByWellKnownName = found ? result[0] as LookupBase : null;

            return lookupByWellKnownName;
        }

        /// <summary>
        /// Gets the name of the lookup by.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>A Lookup</returns>
        public TLookup GetLookupByName<TLookup>(string name) where TLookup : LookupBase
        {
            IList result = Session.CreateCriteria(typeof(TLookup))
                .Add(Restrictions.Eq("Name", name))
                .SetMaxResults(1)
                .List();

            return result != null ? result[0] as TLookup : null;
        }


        /// <summary>
        /// Gets the lookup by key.
        /// </summary>
        /// <param name="type">The lookup type.</param>
        /// <param name="key">The lookup key.</param>
        /// <returns>
        /// A LookupBase object.
        /// </returns>
        public LookupBase GetLookupByKey ( Type type, long key )
        {
            object result = Session.Get ( type, key );

            return result != null ? result as LookupBase : null;
        }

        /// <summary>
        /// Gets the lookup by key.
        /// </summary>
        /// <typeparam name="TLookup">The type of the lookup.</typeparam>
        /// <param name="key">The lookup key.</param>
        /// <returns>A lookup object.</returns>
        public TLookup GetLookupByKey<TLookup> (long key) where TLookup : LookupBase
        {
            var result = Session.Get<TLookup> ( key );

            return result;
        }

        #endregion
    }
}