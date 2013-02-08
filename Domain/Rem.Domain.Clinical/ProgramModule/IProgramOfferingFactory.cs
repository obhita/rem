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
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// IProgramOfferingFactory interface defines <see cref="T:Rem.Domain.Clinical.ProgramModule.ProgramOffering">ProgramOffering</see> creation and destruction services.
    /// </summary>
    public interface IProgramOfferingFactory
    {
        /// <summary>
        /// Creates the program offering.
        /// </summary>
        /// <param name="program">The program.</param>
        /// <param name="location">The location.</param>
        /// <param name="startDate">The start date.</param>
        /// <returns>A ProgramOffering.</returns>
        ProgramOffering CreateProgramOffering(Program program, Location location, DateTime startDate);

        /// <summary>
        /// Destroys the program offering.
        /// </summary>
        /// <param name="programOffering">The program offering.</param>
        void DestroyProgramOffering(ProgramOffering programOffering);
    }
}