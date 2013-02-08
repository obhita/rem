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
    /// The ProgramFactory implements lifetime management of the <see cref="T:Rem.Domain.Clinical.ProgramModule.Program">Program</see>.
    /// </summary>
    public class ProgramFactory : IProgramFactory
    {
        private readonly IProgramRepository _programRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramFactory"/> class.
        /// </summary>
        /// <param name="programRepository">The program repository.</param>
        public ProgramFactory ( IProgramRepository programRepository )
        {
            _programRepository = programRepository;
        }

        #region IProgramFactory Members

        /// <summary>
        /// Creates the program.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="name">The name.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="programCharacteristics">The program characteristics.</param>
        /// <returns>
        /// A Program.
        /// </returns>
        public Program CreateProgram ( Agency agency, string name, DateTime startDate, ProgramCharacteristics programCharacteristics )
        {
            var program = new Program ( agency, name, startDate, programCharacteristics );
            _programRepository.MakePersistent ( program );
            return program;
        }

        /// <summary>
        /// Destroys the program.
        /// </summary>
        /// <param name="program">The program.</param>
        public void DestroyProgram ( Program program )
        {
            Check.IsNotNull ( program, "Program is required" );

            DomainRuleEngine.CreateRuleEngine ( program, () => DestroyProgram )
                .Execute ( () => _programRepository.MakeTransient ( program ) );
        }

        #endregion
    }
}