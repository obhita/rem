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
using System.Linq;
using System.Reflection;
using System.Text;
using NHibernate;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using NHibernate.Type;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.PatientModule;

namespace Rem.Infrastructure.Domain.EventListener
{
    /// <summary>
    /// This class provides utility methods for subclasses.
    /// </summary>
    public abstract class PatientAccessAuditableListenerBase
    {
        #region Constants and Fields

        private static readonly IList<string> AuditablePropertyList;
        private static readonly IList<string> EntityPropertyList;

        #endregion

        static PatientAccessAuditableListenerBase()
        {
            AuditablePropertyList = new List<string> ();
            PropertyInfo[] properties = typeof( IAuditable ).GetProperties ();
            foreach ( PropertyInfo propertyInfo in properties )
            {
                AuditablePropertyList.Add(propertyInfo.Name);
            }

            EntityPropertyList = new List<string> ();
            PropertyInfo[] entityProperties = typeof( IEntity ).GetProperties ();
            foreach ( PropertyInfo propertyInfo in entityProperties )
            {
                EntityPropertyList.Add ( propertyInfo.Name );
            }
        }

        internal void AuditPatientAccess(object entity, AbstractEvent @event, string patientAccessEventType, Func<string> getAuditNote)
        {
            var patientAccessAuditable = entity as IPatientAccessAuditable;

            var aggregateNode = entity as IAggregateNode;
            var aggregateRoot = entity as IAggregateRoot;

            if (aggregateNode != null)
            {
                aggregateRoot = aggregateNode.AggregateRoot;
                patientAccessAuditable = aggregateRoot as IPatientAccessAuditable;
            }

            if (patientAccessAuditable == null)
            {
                return;
            }

            string noteResult = getAuditNote();

            if ( string.IsNullOrWhiteSpace ( noteResult ) )
            {
                return;
            }

            Patient patient = patientAccessAuditable.AuditedPatient;

            ISession session = @event.Session.GetSession(EntityMode.Poco);

            PatientAccessEventType eventType = PatientAccessEventTypeHelper.GetPatientAccessEventTypeByWellKnownName(
                session, patientAccessEventType);

            var eventAccessEntry = new PatientAccessEvent(patient, eventType, patientAccessAuditable.AuditedContextDescription, noteResult)
                {
                    AggregateRootTypeName = aggregateRoot.GetType().FullName,
                    AggregateRootKey = aggregateRoot.Key,
                    AggregateNodeTypeName = aggregateNode == null ? null : aggregateNode.GetType().FullName
                };

            if (aggregateNode != null)
            {
                if ((aggregateNode as IEntity) != null)
                {
                    eventAccessEntry.AggregateNodeKey = (aggregateNode as IEntity).Key;
                }
            }

            session.Save(eventAccessEntry);

            session.Flush();
        }

        internal string GetAuditNoteForEntity(AbstractPostDatabaseOperationEvent @event, IEnumerable<int> propertyIndexesToAudit, IEntityAuditStrategy entityAuditStrategy)
        {
            var noteBulder = new StringBuilder();

            var persister = @event.Persister as AbstractEntityPersister;

            foreach (int propertyIndexToAudit in propertyIndexesToAudit)
            {
                var propertyType = @event.Persister.PropertyTypes[propertyIndexToAudit];
                if (propertyType is CollectionType)
                {
                    continue;
                }

                var componentType = propertyType as ComponentType;
                string note;

                if (componentType != null)
                {
                    note = entityAuditStrategy.GetAuditNoteForComponentProperty(persister, componentType, propertyIndexToAudit);
                }
                else
                {
                    string propertyName = @event.Persister.PropertyNames[propertyIndexToAudit];

                    if ((@event.Entity is IEntity && EntityPropertyList.Contains(propertyName))
                         || (@event.Entity is IAuditable && AuditablePropertyList.Contains(propertyName)))
                    {
                        continue;
                    }

                    string columnName = string.Join(",", persister.GetPropertyColumnNames(propertyName));

                    note = entityAuditStrategy.GetAuditNoteForNonComponentProperty(columnName, propertyIndexToAudit);
                }

                if (!string.IsNullOrWhiteSpace(note))
                {
                    noteBulder.AppendLine(note);
                }
            }

            string noteResult = noteBulder.ToString().Trim();

            return noteResult;
        }

        // e.g. componentPropertyNameChain = "Name.First" for patient first name
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
                    noteBulder.AppendLine();
                }
            }

            return noteBulder.ToString().Trim();
        }
    }
}
