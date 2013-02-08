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
using Pillar.Common.Utility;
using Pillar.Domain.NHibernate.Extensions;
using Rem.Domain.Clinical.GpraModule;

namespace Rem.Infrastructure.Domain.MappingOverride
{
    internal static class GpraInterviewSectionMappingOverrideHelper
    {
        internal static void OverrideMapping<T>(AutoMapping<T> mapping) where T : GpraInterviewSectionAggregateNodeBase
        {
            var idColumnName = typeof(GpraInterview).Name + "Key";
            var idGeneratorPropertyName = PropertyUtil.ExtractPropertyName<T, GpraInterview>(p => p.GpraInterview);
            var foreignKeyName = string.Format("{0}_{1}_FK", typeof(T).Name, typeof(GpraInterview).Name);

            mapping.Id<long>(idColumnName)
                .GeneratedBy.Foreign(idGeneratorPropertyName)
                .Access.BackingField();

            mapping.HasOne(p => p.GpraInterview)
                .MapParentPart(foreignKeyName);
        }
    }
}