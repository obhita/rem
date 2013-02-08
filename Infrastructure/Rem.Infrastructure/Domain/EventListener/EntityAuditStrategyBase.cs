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
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate.Persister.Entity;
using NHibernate.Type;
using Pillar.Domain;

namespace Rem.Infrastructure.Domain.EventListener
{
    internal abstract class EntityAuditStrategyBase : IEntityAuditStrategy
    {
        public abstract string GetAuditNoteForComponentProperty ( AbstractEntityPersister persister, ComponentType componentType, int propertyIndexToAudit );

        public abstract string GetAuditNoteForNonComponentProperty ( string columnName, int propertyIndexToAudit );

        internal string GetAuditNoteForComponent(AbstractEntityPersister persister, ComponentType componentType, string componentPropertyNameChain, IComponentAuditStrategy propertyAuditStrategy)
        {
            var noteBulder = new StringBuilder();

            Type componentDotNetType = componentType.ReturnedClass;

            foreach (PropertyInfo propertyInfo in componentDotNetType.GetProperties())
            {
                if (propertyAuditStrategy.IsExcludedFromAudit(propertyInfo))
                {
                    continue;
                }

                var ignoreMappingTypeAttributes = propertyInfo.GetCustomAttributes(typeof(IgnoreMappingAttribute), false);
                if (ignoreMappingTypeAttributes.Length != 0)
                {
                    continue;
                }

                string propertyName = propertyInfo.Name;
                var wholeComponentPropertyNameChain = string.Format("{0}.{1}", componentPropertyNameChain, propertyName);

                var subComponentType =
                    componentType.Subtypes.FirstOrDefault(p => p.ReturnedClass.FullName == propertyInfo.PropertyType.FullName && p.IsComponentType)
                    as
                    ComponentType;

                string note;

                if (subComponentType == null)
                {
                    string columnName = string.Join(",", persister.GetPropertyColumnNames(wholeComponentPropertyNameChain));

                    note = propertyAuditStrategy.GetAuditNoteForNonComponentProperty(propertyInfo, columnName);
                }
                else
                {
                    note = GetAuditNoteForComponent (
                        persister,
                        subComponentType,
                        wholeComponentPropertyNameChain,
                        propertyAuditStrategy.GetComponentPropertyAuditStrategy ( propertyInfo ) );
                }

                if (!string.IsNullOrWhiteSpace(note))
                {
                    noteBulder.AppendLine ( note );
                }
            }

            return noteBulder.ToString().Trim();
        }
    }
}