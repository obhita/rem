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

namespace Pillar.Security.AccessControl
{
    /// <summary>
    /// The <see cref="IPermissionDescriptor"/> defines the protected resources throughout 
    /// a system or module.
    /// </summary>
    /// <remarks>
    /// Implement an <see cref="IPermissionDescriptor"/> in order to protected resources throughout a 
    /// system or module.
    /// <code>
    ///    public class ClientModulePermissionDescriptor : IPermissionDescriptor
    ///    {
    ///        // Start by defining the permissions that you will use in this module/system.
    ///        public readonly static Permission EditClientPermission   = new Permission { Name = "clientmodule/editclient" };
    ///        public readonly static Permission CreateClientPermission = new Permission { Name = "clientmodule/createclient" };
    ///        public readonly static Permission DeleteClientPermission = new Permission { Name = "clientmodule/deleteclient" };
    ///    
    ///        // Then create a ResourceListBuilder to map permissions to the resources. 
    ///        // This will mean that a user will need to have the given permission in order to access
    ///        // the given resource.
    ///        private readonly ResourceList _resources = new ResourceListBuilder ()
    ///            .AddResource&lt;EditClientRequest&gt;   ( EditClientPermission   )
    ///            .AddResource&lt;CreateClientRequest&gt; ( CreateClientPermission )
    ///            .AddResource&lt;DeleteClientRequest&gt; ( DeleteClientPermission );
    ///    
    ///        public ResourceList Resources
    ///        {
    ///            get { return _resources; }
    ///        }
    ///    }
    /// </code>
    /// After creating a permission descriptor it is necessary to register it with the <see cref="IAccessControlManager"/>.
    /// <code>
    /// var accessControlManager = <see cref="Pillar.Common.InversionOfControl.IoC"/>.CurrentContainer.Resolve&lt;IAccessControlManager&gt; ();
    /// accessControlManager.RegisterPermissionDescriptor ( new ClientModulePermissionDescriptor () );
    /// </code>
    /// </remarks>
    public interface IPermissionDescriptor
    {
        #region Public Properties

        /// <summary>
        /// Gets the resources.
        /// </summary>
        ResourceList Resources { get; }

        #endregion
    }
}
