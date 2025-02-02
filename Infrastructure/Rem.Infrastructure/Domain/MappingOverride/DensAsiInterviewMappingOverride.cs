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

using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Clinical.DensAsiModule;

namespace Rem.Infrastructure.Domain.MappingOverride
{
    /// <summary>
    /// A concrete class to override <see cref="T:Rem.Domain.Clinical.DensAsiModule.DensAsiInterview">DensAsiInterview</see> auto mapping.
    /// </summary>
    public class DensAsiInterviewMappingOverride : IAutoMappingOverride<DensAsiInterview>
    {
        /// <summary>
        /// Overrides the specified mapping.
        /// </summary>
        /// <param name="mapping">The mapping.</param>
        public void Override(AutoMapping<DensAsiInterview> mapping)
        {
            mapping.HasOne ( p => p.DensAsiClosure )
                .MapChildPart ();

            mapping.HasOne(p => p.DensAsiPatientProfile)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiDsmIv)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiClosure)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiDrugAlcoholUse)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiEmploymentStatus)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiFamilySocialRelationships)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiLegalStatus)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiMedicalStatus)
                .MapChildPart();

            mapping.HasOne(p => p.DensAsiPsychiatricStatus)
                .MapChildPart();
        }
    }
}