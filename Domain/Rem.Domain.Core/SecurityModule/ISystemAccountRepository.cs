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
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// ISystemAccountRepository interface defines basic repository services for the <see cref="T:Rem.Domain.Core.SecurityModule.SystemAccount">SystemAccount</see>.
    /// </summary>
    public interface ISystemAccountRepository : IRepository<SystemAccount>
    {
        /// <summary>
        /// Finds the system accounts by keyword.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>A Tuple&lt;int, int, List&lt;SystemAccount&gt;&gt;.</returns>
        Tuple<int, int, List<SystemAccount>> FindSystemAccountsByKeyword ( string searchCriteria, int pageIndex, int pageSize );

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
        /// <returns>A Tuple&lt;int, int, List&lt;SystemAccount&gt;&gt;.</returns>
        Tuple<int, int, List<SystemAccount>> FindSystemAccountsByAdvancedSearch ( string firstName,
                                                                                  string middleName,
                                                                                  string lastName,
                                                                                  string accountName,
                                                                                  long? locationKey,
                                                                                  bool? activeIndicator,
                                                                                  int pageIndex,
                                                                                  int pageSize);


        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A SystemAccount.</returns>
        SystemAccount GetByIdentifier ( string identifier );
    }
}