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
using System.Windows;
using Pillar.Common.Utility;

namespace Pillar.Common.InversionOfControl
{
    /// <summary>
    /// This class provides the ambient container for this application. If your
    /// framework defines such an ambient container, use IoC.CurrentContainer
    /// to get it.
    /// Follow these two steps to use this class:
    /// 1. Call SetContainerProvider() to set the container provider;
    /// 2. Call Bootstrap() to register dependencies in all Pillar libraries.
    /// </summary>
    public static class IoC
    {
        private static ContainerProvider _containerProvider;
 
        /// <summary>
        /// The current ambient container.
        /// </summary>
        public static IContainer CurrentContainer
        {
            get
            {
                return _containerProvider == null ? null : _containerProvider();
            }
        }

        /// <summary>
        /// Set the delegate that is used to retrieve the current container.
        /// </summary>
        /// <param name="newProvider">Delegate that, when called, will return
        /// the current ambient container.</param>
        public static void SetContainerProvider(ContainerProvider newProvider)
        {
            _containerProvider = newProvider;
        }

        /// <summary>
        /// Bootstraps to register dependencies in all Pillar libraries.
        /// </summary>
        public static void Bootstrap()
        {
            Check.IsNotNull(CurrentContainer, "Container is not set yet.");

            const string PillarAssemblyNamePrefix = "Pillar.";

#if !SILVERLIGHT
            var pillarAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.GetName().Name.StartsWith(PillarAssemblyNamePrefix));
#else
            var pillarAssemblies = Deployment.Current.Parts.Select (
                ap =>
                Application.GetResourceStream ( new Uri ( ap.Source, UriKind.Relative ) ) ).Select ( sri => new AssemblyPart ().Load ( sri.Stream ) )
                .Where ( p => new AssemblyName(p.FullName).Name.StartsWith ( PillarAssemblyNamePrefix ) );
#endif

            foreach (var pillarAssembly in pillarAssemblies)
            {
                var registryTypes = pillarAssembly.GetTypes().Where(p => typeof(IRegistry).IsAssignableFrom(p) && p.IsClass);
                foreach (var registryType in registryTypes)
                {
                    var registry = (IRegistry)Activator.CreateInstance(registryType);
                    registry.Register();
                }
            }
        }
    }
}
