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

using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.SbirtModule
{
    /// <summary>
    /// IDast10Factory interface defines <see cref="T:Rem.Domain.Clinical.SbirtModule.Dast10">Audit</see> creation and destruction services.
    /// </summary>
    public interface IDast10Factory : IActivityFactory
    {
        /// <summary>
        /// If the visit does not have a Dast10, then it returns a newly created Dast10,
        /// else, it returns an existing one from the visit.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <returns>A Dast10.</returns>
        Dast10 CreateDast10 ( Visit visit );

        /// <summary>
        /// Destroys the Dast10.
        /// </summary>
        /// <param name="dast10">The Dast10.</param>
        void DestroyDast10 ( Dast10 dast10 );
    }
}