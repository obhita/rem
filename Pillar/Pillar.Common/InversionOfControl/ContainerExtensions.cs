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
using System.Linq;
using Pillar.Common.Utility;

namespace Pillar.Common.InversionOfControl
{
    /// <summary>
    /// The ContainerExtensions defines extension methods for the <see cref="IContainer"/>.
    /// </summary>
    public static class ContainerExtensions
    {
        #region Public Methods

        /// <summary>
        /// Register an instance with the container.
        /// </summary>
        /// <typeparam name="TComponent">Type of instance to register.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="instance">Object to returned.</param>
        public static void RegisterInstance<TComponent> ( this IContainer container, TComponent instance )
        {
            CheckNotNull ( container );

            container.RegisterInstance ( typeof( TComponent ), instance );
        }

        /// <summary>
        /// Registers an instance with the container. The registration is given a name.
        /// </summary>
        /// <typeparam name="TComponent">Type of instance to register.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="instance">Object to returned.</param>
        /// <param name="registrationName">The name of the registration.</param>
        public static void RegisterInstance<TComponent> ( this IContainer container, TComponent instance, string registrationName )
        {
            CheckNotNull ( container );

            container.RegisterInstance ( typeof( TComponent ), instance, registrationName );
        }

        /// <summary>
        /// Registers a type mapping with the container, where the created instances will use
        /// the given <see cref="Lifestyle"/>.
        /// </summary>
        /// <typeparam name="TComponent">The type that will be requested.</typeparam>
        /// <typeparam name="TImplementation">The type that will actually be returned.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="lifestyle">The <see cref="Lifestyle"/> that controls the lifetime
        /// of the returned instance.</param>
        public static void RegisterType<TComponent, TImplementation> ( this IContainer container, Lifestyle lifestyle )
            where TImplementation : TComponent
        {
            CheckNotNull ( container );

            container.RegisterType ( typeof( TComponent ), typeof( TImplementation ), lifestyle );
        }

        /// <summary>
        /// Registers a type mapping with the container, where the created instances will use
        /// the given <see cref="Lifestyle"/>. The registration is given a name.
        /// </summary>
        /// <typeparam name="TComponent">The type that will be requested.</typeparam>
        /// <typeparam name="TImplementation">The type that will actually be returned.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="lifestyle">The <see cref="Lifestyle"/> that controls the lifetime
        /// of the returned instance.</param>
        /// <param name="registrationName">The name of the registration.</param>
        public static void RegisterType<TComponent, TImplementation> ( this IContainer container, Lifestyle lifestyle, string registrationName )
            where TImplementation : TComponent
        {
            CheckNotNull ( container );

            container.RegisterType ( typeof( TComponent ), typeof( TImplementation ), lifestyle, registrationName );
        }

        /// <summary>
        /// Resolves an instance of the requested type from the container.
        /// Throws exception if no registration is found in the container.
        /// </summary>
        /// <typeparam name="TComponent">The type of object to get from the container.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The retrieved object.</returns>
        public static TComponent Resolve<TComponent> ( this IContainer container )
        {
            CheckNotNull ( container );

            return ( TComponent )container.Resolve ( typeof( TComponent ) );
        }

        /// <summary>
        /// Resolves an instance of the requested type with the given name from the container.
        /// Throws exception if no registration is found in the container.
        /// </summary>
        /// <typeparam name="TComponent">The type of object to get from the container.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>The retrieved object.</returns>
        public static TComponent Resolve<TComponent> ( this IContainer container, string registrationName )
        {
            CheckNotNull ( container );

            return ( TComponent )container.Resolve ( typeof( TComponent ), registrationName );
        }

        /// <summary>
        /// Get all instances of the given <typeparamref name="TComponentType"/> currently
        /// registered in the container.
        /// </summary>
        /// <typeparam name="TComponentType">Type of object requested.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>A list of instances of the requested <typeparamref name="TComponentType"/>.</returns>
        public static IList<TComponentType> ResolveAll<TComponentType>(this IContainer container)
        {
            CheckNotNull(container);

            return container.ResolveAll ( typeof( TComponentType ) ).Cast<TComponentType> ().ToList ();
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type from the container.
        /// No exception will be thrown if no registration is found in the container.
        /// </summary>
        /// <typeparam name="TComponent">The type of object to get from the container.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The retrieved object 
        /// or the default value for the requested type if no registration is found in the container.</returns>
        public static TComponent TryResolve<TComponent> ( this IContainer container )
        {
            CheckNotNull ( container );

            return ( TComponent )container.TryResolve ( typeof( TComponent ) );
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type with the given name from the container.
        /// No exception will be thrown if no registration is found in the container.
        /// </summary>
        /// <typeparam name="TComponent">The type of object to get from the container.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="registrationName">Name of the registration.</param>
        /// <returns>The retrieved object 
        /// or the default value for the requested type if no registration is found in the container.</returns>
        public static TComponent TryResolve<TComponent> ( this IContainer container, string registrationName )
        {
            CheckNotNull ( container );

            return ( TComponent )container.TryResolve ( typeof( TComponent ), registrationName );
        }

        #endregion

        #region Methods

        private static void CheckNotNull ( IContainer container )
        {
            Check.IsNotNull ( container, "container is required." );
        }

        #endregion
    }
}
