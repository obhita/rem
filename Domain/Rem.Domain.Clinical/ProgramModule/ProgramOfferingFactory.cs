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
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// The ProgramOfferingFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.ProgramModule.ProgramOffering">ProgramOffering</see>.
    /// </summary>
    public class ProgramOfferingFactory : IProgramOfferingFactory
    {
        private readonly IProgramOfferingRepository _programOfferingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramOfferingFactory"/> class.
        /// </summary>
        /// <param name="programOfferingRepository">The program offering repository.</param>
        public ProgramOfferingFactory ( IProgramOfferingRepository programOfferingRepository )
        {
            _programOfferingRepository = programOfferingRepository;
        }

        #region IProgramOfferingFactory Members

        /// <summary>
        /// Creates the program offering.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="location">The location.</param>
        /// <param name="startDate">The start date.</param>
        /// <returns>
        /// A ProgramOffering.
        /// </returns>
        public ProgramOffering CreateProgramOffering ( Program program, Location location, DateTime startDate )
        {
            var programOffering = new ProgramOffering ( program, location, startDate );
            _programOfferingRepository.MakePersistent ( programOffering );
            return programOffering;
        }

        /// <summary>
        /// Destroys the program offering.
        /// </summary>
        /// <param name="programOffering">The program offering.</param>
        public void DestroyProgramOffering ( ProgramOffering programOffering )
        {
            Check.IsNotNull ( programOffering, "Program is required" );

            DomainRuleEngine.CreateRuleEngine ( programOffering, () => DestroyProgramOffering )
                .Execute(() => _programOfferingRepository.MakeTransient(programOffering));
        }

        #endregion
    }
}