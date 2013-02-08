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
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Linq;
using Pillar.Common.Extension;
using Rem.Domain.Core.SecurityModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Core.SecurityModule.SystemAccount">SystemAccount</see>.
    /// </summary>
    public class SystemAccountRepository : NHibernateRepositoryBase<SystemAccount>, ISystemAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccountRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public SystemAccountRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region IRepository<SystemAccount> Members

        /// <summary>
        /// Gets the SystemAccount by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A SystemAccount.</returns>
        public SystemAccount GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a SystemAccount.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A SystemAccount.</returns>
        public SystemAccount MakePersistent(SystemAccount entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a SystemAccount.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient(SystemAccount entity)
        {
            Helper.MakeTransient(entity);
        }

        #endregion

        /// <summary>
        /// Gets the SystemAccount by identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>
        /// A SystemAccount.
        /// </returns>
        public SystemAccount GetByIdentifier(string identifier)
        {
            var accounts = ( from systemAccount in Session.Query<SystemAccount> ()
                             where systemAccount.Identifier.Equals ( identifier )
                             select systemAccount ).ToList ();
                          
            var account = accounts.SingleOrDefault();
        
            return account;
        }

        private System.Tuple<int, int, List<SystemAccount>> GetResultTuple(IList<SystemAccount> accounts, int pageIndex, int pageSize)
        {
            var totalAccountsCount = accounts.Count;
            var skippedAccountsCount = pageIndex * pageSize;
            if (skippedAccountsCount >= totalAccountsCount)
            {
                pageIndex = (int)Math.Floor(((decimal)totalAccountsCount) / ((decimal)pageSize)) - 1;
                skippedAccountsCount = pageIndex * pageSize;
            }
            var pagedAccountsList = accounts.Skip(skippedAccountsCount).Take(pageSize).ToList();

            var resultTuple = Tuple.Create(totalAccountsCount, pageIndex, pagedAccountsList);

            return resultTuple;
        }

        /// <summary>
        /// Finds the system accounts by keyword.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A Tuple&lt;int, int, List&lt;SystemAccount&gt;&gt;.
        /// </returns>
        public System.Tuple<int, int, List<SystemAccount>> FindSystemAccountsByKeyword(string searchCriteria, int pageIndex, int pageSize)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria))
            {
                throw new ArgumentException("Search Criteria should not be empty", "searchCriteria");
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException("Invalid page index", "pageIndex");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("Invalid page size", "pageSize");
            }
            var keywords = searchCriteria.SplitIntoDistinctWords();
            var disjunction = Restrictions.Disjunction();

            foreach ( var keyword in keywords )
            {
                disjunction.Add(Restrictions.Like("sa.Identifier", keyword, MatchMode.Anywhere));
            }

            var systemAccountQuery = Session.CreateCriteria<SystemAccount> ( "sa" )
                .Add ( disjunction );

            var accounts = systemAccountQuery.List<SystemAccount>();
            var resultTuple = GetResultTuple(accounts, pageIndex, pageSize);

            return resultTuple;
        }

        /// <summary>
        /// Finds the system accounts by advanced search.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="middleName">Name of the middle.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="locationKey">The location key.</param>
        /// <param name="activeIndicator">The active indicator.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// A Tuple&lt;int, int, List&lt;SystemAccount&gt;&gt;.
        /// </returns>
        public System.Tuple<int, int, List<SystemAccount>> FindSystemAccountsByAdvancedSearch(string firstName, 
            string middleName,
            string lastName, 
            string accountName, 
            long? locationKey, 
            bool? activeIndicator, 
            int pageIndex, 
            int pageSize)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(middleName)
                && string.IsNullOrWhiteSpace(lastName)
                && string.IsNullOrWhiteSpace(accountName) && !locationKey.HasValue && !activeIndicator.HasValue)
            {
                throw new ArgumentException("No search criteria were specified.");
            }

            if (pageIndex < 0)
            {
                throw new ArgumentException("Invalid page index", "pageIndex");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("Invalid page size", "pageSize");
            }

            var systemAccountQuery = Session.CreateCriteria(typeof(SystemAccount), "sa");
            if (!string.IsNullOrEmpty(accountName))
            {
                systemAccountQuery.Add(Restrictions.Like("sa.Identifier", accountName.Replace('*', '%')));
            }
            if (activeIndicator.HasValue)
            {
                systemAccountQuery.Add(Restrictions.Eq("sa.EnabledIndicator", activeIndicator.Value));
            }
            var accounts = systemAccountQuery.List<SystemAccount>();
            var resultTuple = GetResultTuple(accounts, pageIndex, pageSize);

            return resultTuple;
        }
    }
}
