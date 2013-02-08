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
using System.Collections;
using System.Linq;
using Microsoft.Practices.Unity;
using Pillar.Common.Extension;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;

namespace Pillar.IoC.Unity
{
    /// <summary>
    /// The Container is a Unity implementation for <see cref="IContainer"/>.
    /// </summary>
    public class Container : IContainer
    {
        #region Constants and Fields

        private readonly IUnityContainer _unityContainer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class with a default Unity container.
        /// </summary>
        public Container ()
            : this ( new UnityContainer () )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="unityContainer">The Unity container.</param>
        public Container ( IUnityContainer unityContainer )
        {
            Check.IsNotNull(unityContainer, "unityContainer is required.");
            _unityContainer = unityContainer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Registers an instance with the container.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Instance registration is much like setting a type as a singleton, except that instead
        /// of the container creating the instance the first time it is requested, the user
        /// creates the instance ahead of registration and adds that instance to the container.
        /// </para>
        /// </remarks>
        /// <param name="componentType">Type of instance to register (may be an implemented interface instead of the full type).</param>
        /// <param name="instance">Object to returned.</param>
        public void RegisterInstance ( Type componentType, object instance )
        {
            _unityContainer.RegisterInstance ( componentType, instance );
        }

        /// <summary>
        /// Registers an instance with the container.The registration is given a name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Instance registration is much like setting a type as a singleton, except that instead
        /// of the container creating the instance the first time it is requested, the user
        /// creates the instance ahead of registration and adds that instance to the container.
        /// </para>
        /// </remarks>
        /// <param name="componentType">Type of instance to register (may be an implemented interface instead of the full type).</param>
        /// <param name="instance">Instance to be registered in the container.</param>
        /// <param name="registrationName">The name of the registration.</param>
        public void RegisterInstance ( Type componentType, object instance, string registrationName )
        {
            _unityContainer.RegisterInstance(componentType, registrationName, instance);
        }

        /// <summary>
        /// Registers a type mapping with the container, where the created instances will use
        /// the given <see cref="Lifestyle"/>.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> that will be requested.</param>
        /// <param name="implementationType">The <see cref="Type"/> that will actually be returned.</param>
        /// <param name="lifestyle">The <see cref="Lifestyle"/> that controls the lifetime
        /// of the returned instance.</param>
        public void RegisterType ( Type componentType, Type implementationType, Lifestyle lifestyle )
        {
            LifetimeManager lifetimeManager = GetLifetimeManager ( lifestyle );
            _unityContainer.RegisterType ( componentType, implementationType, lifetimeManager );
        }

        /// <summary>
        /// Registers a type mapping with the container, where the created instances will use
        /// the given <see cref="Lifestyle"/>. The registration is given a name.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> that will be requested.</param>
        /// <param name="implementationType">The <see cref="Type"/> that will actually be returned.</param>
        /// <param name="lifestyle">The <see cref="Lifestyle"/> that controls the lifetime
        /// of the returned instance.</param>
        /// <param name="registrationName">The name of the registration.</param>
        public void RegisterType ( Type componentType, Type implementationType, Lifestyle lifestyle, string registrationName )
        {
            LifetimeManager lifetimeManager = GetLifetimeManager(lifestyle);
            _unityContainer.RegisterType(componentType, implementationType, registrationName, lifetimeManager);
        }

        /// <summary>
        /// Releases the specified component from the container.
        /// </summary>
        /// <param name="component">The component to release.</param>
        public void Release ( object component )
        {
            _unityContainer.Teardown ( component );
        }

        /// <summary>
        /// Resolves an instance of the requested type from the container.
        /// Throws exception if no registration is found in the container.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> of object to get from the container.</param>
        /// <returns>The retrieved object.</returns>
        public object Resolve ( Type componentType )
        {
            return _unityContainer.Resolve ( componentType );
        }

        /// <summary>
        /// Resolves an instance of the requested type with the given name from the container.
        /// Throws exception if no registration is found in the container.
        /// </summary>
        /// <param name="componentType"><see cref="Type"/> of object to get from the container.</param>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>The retrieved object.</returns>
        public object Resolve ( Type componentType, string registrationName )
        {
            return _unityContainer.Resolve ( componentType, registrationName );
        }

        /// <summary>
        /// Get all instances of the given <paramref name="componentType"/> currently
        /// registered in the container.
        /// </summary>
        /// <param name="componentType">Type of object requested.</param>
        /// <returns>A list of instances of the requested <paramref name="componentType"/>.</returns>
        public IList ResolveAll(Type componentType)
        {
            // Be aware that Unity does NOT return an instance for the default (unnamed) registration.
            return _unityContainer.ResolveAll(componentType).ToList();
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type from the container.
        /// No exception will be thrown if no registration is found in the container.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> of object to get from the container.</param>
        /// <returns>The retrieved object 
        /// or the default value for the requested type if no registration is found in the container.</returns>
        public object TryResolve ( Type componentType )
        {
            try
            {
                return _unityContainer.Resolve ( componentType );

            }
            catch ( ResolutionFailedException )
            {

                return componentType.GetDefault();
            }
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type with the given name from the container.
        /// No exception will be thrown if no registration is found in the container.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> of object to get from the container.</param>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>The retrieved object 
        /// or the default value for the requested type if no registration is found in the container.</returns>
        public object TryResolve ( Type componentType, string registrationName )
        {
            try
            {
                return _unityContainer.Resolve(componentType, registrationName);

            }
            catch (ResolutionFailedException)
            {

                return componentType.GetDefault();
            }
        }

        #endregion

        #region Methods

        private LifetimeManager GetLifetimeManager ( Lifestyle lifestyle )
        {
            if ( lifestyle == Lifestyle.Transient )
            {
                return new TransientLifetimeManager ();
            }

            return new ContainerControlledLifetimeManager ();
        }

        #endregion
    }
}
