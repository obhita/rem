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
using NLog;

namespace Pillar.Security.AccessControl
{
    /// <summary>
    /// The <see cref="AccessControlManager"/> can grant or deny access for a <see cref="ResourceRequest"/>.
    /// </summary>
    public partial class AccessControlManager : IAccessControlManager
    {
        #region Constants and Fields

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private readonly ICurrentUserPermissionService _currentUserPermissionService;
        private readonly ResourceList _resourceList;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlManager"/> class.
        /// </summary>
        /// <param name="currentUserPermissionService">The current user permission service.</param>
        public AccessControlManager ( ICurrentUserPermissionService currentUserPermissionService )
        {
            _currentUserPermissionService = currentUserPermissionService;
            _resourceList = new ResourceList ();
            Initialize ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether access to the specified resource request should be granted.
        /// </summary>
        /// <param name="resourceRequest">The resource request.</param>
        /// <returns>
        ///   <c>true</c> if this instance can access the specified resource request; otherwise, <c>false</c>.
        /// </returns>
        public bool CanAccess ( ResourceRequest resourceRequest )
        {
            if ( resourceRequest == null )
            {
                throw new ArgumentException ( "resource request is required." );
            }

            if ( resourceRequest.ResourceHierarchy.Count == 0 )
            {
                throw new ArgumentException ( "Invalid resource request." );
            }

            Permission requiredPermission = null;
            var canAccess = false;
            var resourceList = _resourceList;
            foreach ( var resourceName in resourceRequest.ResourceHierarchy )
            {
                if ( resourceList == null )
                {
                    break;
                }

                var name = resourceName;
                var resource = resourceList.FirstOrDefault ( r => r.Name == name );
                if ( resource == null )
                {
                    break;
                }

                requiredPermission = resource.Permission;
                resourceList = resource.Resources;
            }

            if ( requiredPermission != null )
            {
                canAccess = _currentUserPermissionService.DoesUserHavePermission ( requiredPermission );
                Logger.Debug (
                    "Permission ({0}) {1} for resource request ({2}).",
                    requiredPermission,
                    canAccess ? "granted" : "denied",
                    resourceRequest );
            }
            else
            {
                Logger.Debug ( "No permission defined for resource request ({0}).", resourceRequest );
            }

            return canAccess;
        }

        /// <summary>
        /// Registers one or more <see cref="IPermissionDescriptor"/> instances.
        /// </summary>
        /// <param name="permissionDescriptors">The permission descriptors.</param>
        public void RegisterPermissionDescriptor ( params IPermissionDescriptor[] permissionDescriptors )
        {
            foreach ( var permissionDescriptor in permissionDescriptors )
            {
                Logger.Debug ( string.Format ( "Registering Permission Descriptor: {0}", permissionDescriptor.GetType ().FullName ) );
                foreach ( var resource in permissionDescriptor.Resources )
                {
                    Logger.Debug ( string.Format ( "Registering Resource: {0}", resource ) );
                    var resourceName = resource.Name;

                    var resourceAdded = _resourceList.FirstOrDefault ( r => r.Name == resourceName );
                    if ( resourceAdded != null )
                    {
                        throw new ArgumentException (
                            string.Format (
                                "Cannot add Resource ({0}) for Permission ({1}) because Resource ({0}) has already been added for Permission ({2}).",
                                resource.Name,
                                resource.Permission.Name,
                                resourceAdded.Permission.Name ) );
                    }

                    _resourceList.Add ( resource );
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Initialize method is a partial method.  This provides the Silverlight implementation with a hook
        /// to perform additional setup upon construction.
        /// </summary>
        partial void Initialize ();

        #endregion
    }
}
