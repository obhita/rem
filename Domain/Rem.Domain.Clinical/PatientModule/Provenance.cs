using System;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.PatientModule
{
    /// <summary>
    /// Defines the provenance metadata of C32 import.
    /// </summary>
    public class Provenance : AuditableAggregateRootBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Provenance"/> class.
        /// </summary>
        protected internal Provenance ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Provenance"/> class.
        /// </summary>
        /// <param name="taggedDataElement">The tagged data element.</param>
        /// <param name="signedTimestamp">The signed timestamp.</param>
        public Provenance (TaggedDataElement taggedDataElement, DateTimeOffset signedTimestamp)
        {
            Check.IsNotNull ( taggedDataElement, () => TaggedDataElement );
            Check.IsNotNull ( signedTimestamp, () => SignedTimestamp );

            TaggedDataElement = taggedDataElement;
            SignedTimestamp = signedTimestamp;
        }

        /// <summary>
        /// Gets the tagged data element.
        /// </summary>
        [NotNull]
        public virtual TaggedDataElement TaggedDataElement { get; private set; }

        /// <summary>
        /// Gets the signed timestamp.
        /// </summary>
        [NotNull]
        public virtual DateTimeOffset SignedTimestamp { get; private set; }

        /// <summary>
        /// Gets the assigned author.
        /// </summary>
        public virtual AssignedAuthor AssignedAuthor { get; private set; }

        /// <summary>
        /// Gets the represented organization.
        /// </summary>
        public virtual RepresentedOrganization RepresentedOrganization { get; private set; }

        /// <summary>
        /// Revises the tagged data element.
        /// </summary>
        /// <param name="taggedDataElement">The tagged data element.</param>
        public virtual void ReviseTaggedDataElement ( TaggedDataElement taggedDataElement)
        {
            Check.IsNotNull(taggedDataElement, () => TaggedDataElement);
            
            TaggedDataElement = taggedDataElement;
        }

        /// <summary>
        /// Revises the signed timestamp.
        /// </summary>
        /// <param name="signedTimestamp">The signed timestamp.</param>
        public virtual void ReviseSignedTimestamp(DateTimeOffset signedTimestamp)
        {
            Check.IsNotNull(signedTimestamp, () => SignedTimestamp);

            SignedTimestamp = signedTimestamp;
        }

        /// <summary>
        /// Revises the assigned author.
        /// </summary>
        /// <param name="assignedAuthor">The assigned author.</param>
        public virtual void ReviseAssignedAuthor ( AssignedAuthor assignedAuthor)
        {
            AssignedAuthor = assignedAuthor;
        }

        /// <summary>
        /// Revises the represented organization.
        /// </summary>
        /// <param name="representedOrganization">The represented organization.</param>
        public virtual void ReviseRepresentedOrganization ( RepresentedOrganization representedOrganization)
        {
            RepresentedOrganization = representedOrganization;
        }
    }
}
