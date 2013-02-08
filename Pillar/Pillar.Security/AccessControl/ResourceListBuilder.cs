#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

namespace Pillar.Security.AccessControl
{
    /// <summary>
    /// The <see cref="ResourceListBuilder"/> makes it really easy to create a <see cref="ResourceList"/>.
    /// </summary>
    public class ResourceListBuilder
    {
        #region Constants and Fields

        private readonly ResourceList _resourceList = new ResourceList ();

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new <see cref="Resource"/> to the <see cref="ResourceList"/>.
        /// </summary>
        /// <param name="name">
        /// The resource name.
        /// </param>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <param name="buildResourceList">
        /// Defines a new <see cref="ResourceListBuilder"/> 
        /// for building sub-resources.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceListBuilder"/> allowing for a fluent API.
        /// </returns>
        public ResourceListBuilder AddResource (
            string name, Permission permission, Func<ResourceListBuilder, ResourceList> buildResourceList = null )
        {
            var resource = new Resource { Name = name, Permission = permission };
            _resourceList.Add ( resource );

            if ( buildResourceList != null )
            {
                var resourceListBuilder = new ResourceListBuilder ();
                ResourceList resourceList = buildResourceList ( resourceListBuilder );
                resource.Resources = resourceList;
            }

            return this;
        }

        /// <summary>
        /// Adds a new <see cref="Resource"/> to the <see cref="ResourceList"/>.
        /// </summary>
        /// <typeparam name="TResource">
        /// The resource type.
        /// </typeparam>
        /// <param name="permission">
        /// The permission.
        /// </param>
        /// <param name="buildResourceList">
        /// Defines a new <see cref="ResourceListBuilder"/> 
        /// for building sub-resources.
        /// </param>
        /// <returns>
        /// A <see cref="ResourceListBuilder"/> allowing for a fluent API.
        /// </returns>
        public ResourceListBuilder AddResource<TResource> ( Permission permission, Func<ResourceListBuilder, ResourceList> buildResourceList = null )
            where TResource : class
        {
            return AddResource ( typeof( TResource ).FullName, permission, buildResourceList );
        }

        #endregion

        #region Operators

        /// <summary>
        /// Performs an implicit conversion from <see cref="Pillar.Security.AccessControl.ResourceListBuilder"/> 
        /// to <see cref="Pillar.Security.AccessControl.ResourceList"/>.
        /// </summary>
        /// <param name="resourceListBuilder">
        /// The resource list builder.
        /// </param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator ResourceList ( ResourceListBuilder resourceListBuilder )
        {
            return resourceListBuilder._resourceList;
        }

        #endregion
    }
}
