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
using System.Collections.Generic;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using StructureMap;
using IContainer = Pillar.Common.InversionOfControl.IContainer;

namespace Pillar.IoC.StructureMap
{
    /// <summary>
    /// The Container is a StructureMap implementation for <see cref="IContainer"/>.
    /// </summary>
    public class Container : Common.InversionOfControl.IContainer
    {
        #region Constants and Fields

        private readonly Dictionary<Lifestyle, InstanceScope> _lifeStyleMappings = new Dictionary<Lifestyle, InstanceScope>
            {
                { Lifestyle.Singleton, InstanceScope.Singleton },
                { Lifestyle.Transient, InstanceScope.Transient }
            };
        private readonly global::StructureMap.IContainer _structureMapContainer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class with a default SturctureMap container.
        /// </summary>
        public Container ()
            : this ( new global::StructureMap.Container () )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="structureMapContainer">The StructureMap container.</param>
        public Container ( global::StructureMap.IContainer structureMapContainer )
        {
            Check.IsNotNull(structureMapContainer, "structureMapContainer is required.");
            _structureMapContainer = structureMapContainer;
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
            _structureMapContainer.Configure ( x => x.For ( componentType ).Use ( instance ) );
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
            _structureMapContainer.Configure ( x => x.For ( componentType ).Use ( instance ).Named ( registrationName ) );
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
            _structureMapContainer.Configure ( x => x.For ( componentType ).LifecycleIs ( _lifeStyleMappings[lifestyle] ).Use ( implementationType ) );
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
            _structureMapContainer.Configure (
                x => x.For ( componentType ).LifecycleIs ( _lifeStyleMappings[lifestyle] ).Use ( implementationType ).Named ( registrationName ) );
        }

        /// <summary>
        /// Releases the specified component from the container.
        /// </summary>
        /// <param name="component">The component to release.</param>
        public void Release ( object component )
        {
        }

        /// <summary>
        /// Resolves an instance of the requested type from the container.
        /// Throws exception if no registration is found in the container.
        /// </summary>
        /// <param name="componentType">The <see cref="Type"/> of object to get from the container.</param>
        /// <returns>The retrieved object.</returns>
        public object Resolve ( Type componentType )
        {
            return _structureMapContainer.GetInstance ( componentType );
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
            return _structureMapContainer.GetInstance ( componentType, registrationName );
        }

        /// <summary>
        /// Get all instances of the given <paramref name="componentType"/> currently
        /// registered in the container.
        /// </summary>
        /// <param name="componentType">Type of object requested.</param>
        /// <returns>A list of instances of the requested <paramref name="componentType"/>.</returns>
        public IList ResolveAll(Type componentType)
        {
            return _structureMapContainer.GetAllInstances ( componentType );
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
            if (componentType.IsAbstract || componentType.IsInterface)
            {
                return _structureMapContainer.TryGetInstance(componentType);
            }

            return _structureMapContainer.GetInstance(componentType);
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
            return _structureMapContainer.TryGetInstance ( componentType, registrationName );
        }

        #endregion
    }
}
