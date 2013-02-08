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
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using Rem.Domain.Clinical.ProgramModule;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Rem.Domain.Clinical.ProgramModule.Program">Program</see>.
    /// </summary>
    public class ProgramRepository : NHibernateRepositoryBase<Program>, IProgramRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRepository"/> class.
        /// </summary>
        /// <param name="sessionProvider">The session provider.</param>
        public ProgramRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        #region IProgramSetupRepository Members

        /// <summary>
        /// Gets a Program by key.
        /// </summary>
        /// <param name="key">The entity key.</param>
        /// <returns>A Program object.</returns>
        public Program GetByKey(long key)
        {
            return Helper.GetEntityByKey(key);
        }

        /// <summary>
        /// Saves a Program.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <returns>A Program object.</returns>
        public Program MakePersistent(Program entity)
        {
            return Helper.MakePersistent(entity);
        }

        /// <summary>
        /// Deletes a Program.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        public void MakeTransient(Program entity)
        {
            Helper.MakeTransient(entity);
        }

        /// <summary>
        /// Gets all programs by agency key.
        /// </summary>
        /// <param name="agencyKey">The agency key.</param>
        /// <returns>
        /// An IList&lt;Program&gt;.
        /// </returns>
        public IList<Program> GetAllProgramsByAgencyKey( long agencyKey )
        {
            var programs = Session.Query<Program>().Where(p => p.Agency.Key == agencyKey);
            return programs.ToList();
        }

        /// <summary>
        /// Counts the number of programs with the given name.
        /// </summary>
        /// <param name="programName">Name of the program.</param>
        /// <returns>
        /// An integer
        /// </returns>
        public int CountProgramsWithName ( string programName )
        {
            return Session.Query<Program> ().Count ( p => p.Name == programName );
        }

        #endregion
    }
}