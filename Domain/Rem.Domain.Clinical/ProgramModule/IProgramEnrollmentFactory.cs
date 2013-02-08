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
using Rem.Domain.Clinical.ClinicalCaseModule;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Clinical.ProgramModule
{
    /// <summary>
    /// IProgramEnrollmentFactory interface defines <see cref="T:Rem.Domain.Clinical.ProgramModule.ProgramEnrollment">ProgramEnrollment</see> creation and destruction services.
    /// </summary>
    public interface IProgramEnrollmentFactory
    {
        /// <summary>
        /// Creates the program enrollment.
        /// </summary>
        /// <param name="programOffering">The program offering.</param>
        /// <param name="clinicalCase">The clinical case.</param>
        /// <param name="enrollmentDate">The enrollment date.</param>
        /// <param name="enrollingStaff">The enrolling staff.</param>
        /// <returns>A ProgramEnrollment.</returns>
        ProgramEnrollment CreateProgramEnrollment(ProgramOffering programOffering, ClinicalCase clinicalCase, DateTime enrollmentDate, Staff enrollingStaff);

        /// <summary>
        /// Destroys the program enrollment.
        /// </summary>
        /// <param name="programEnrollment">The program enrollment.</param>
        void DestroyProgramEnrollment(ProgramEnrollment programEnrollment);
    }
}
