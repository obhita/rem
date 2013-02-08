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
using System.Linq.Expressions;
using NHibernate.Criterion;
using Pillar.Domain;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.IKeywordsSearchService"> IKeywordsSearchService </see> defines utilities for keyword search with paging support.
    /// </summary>
    public interface IKeywordsSearchService
    {
        /// <summary>
        /// Finds the paged entity list by keywords.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="propertiesToSearch">The properties to search.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>A PagedEntityList&lt;TEntity&gt;</returns>
        PagedEntityList<TEnity> FindPagedEntityListByKeywords<TEnity>(
            string searchCriteria,
            IList<Expression<Func<TEnity, object>>> propertiesToSearch,
            int pageIndex, 
            int pageSize,
            IList<Order> orders,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null) 
            where TEnity : class, IEntity;

        /// <summary>
        /// Finds the paged entity list by keywords.
        /// </summary>
        /// <typeparam name="TEnity">The type of the enity.</typeparam>
        /// <param name="enityType">Type of the enity.</param>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="propertiesToSearch">The properties to search.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orders">The orders.</param>
        /// <param name="eagerLoadingAssociations">The eager loading associations.</param>
        /// <returns>A PagedEntityList&lt;TEntity&gt;</returns>
        PagedEntityList<TEnity> FindPagedEntityListByKeywords<TEnity>(
            Type enityType, 
            string searchCriteria,
            IList<Expression<Func<TEnity, object>>> propertiesToSearch,
            int pageIndex, 
            int pageSize,
            IList<Order> orders = null,
            IList<Expression<Func<TEnity, IEnumerable>>> eagerLoadingAssociations = null) 
            where TEnity : class, IEntity;
    }
}