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
using Rem.Infrastructure.Bootstrapper;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Rem.Infrastructure.Domain.Repository
{
    /// <summary>
    /// Registers all lookup values.
    /// </summary>
    public class RepositoryRegistry : Registry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryRegistry"/> class.
        /// </summary>
        public RepositoryRegistry()
        {
            Scan ( x =>
            {
                x.AssemblyContainingType ( GetType () );
                x.With ( new RepositoryRegistrationConvention () );
            } );

            RegisterAllLookupValueByRelatedKeysFetchers ();
        }

        private void RegisterAllLookupValueByRelatedKeysFetchers()
        {
            For<ILookupValueByRelatedKeysFetcher>()
                .Add(p => p.GetInstance<DetailedEthnicityByRaceFetcher>())
                .Named("DetailedEthnicity");
        }

        /// <summary>
        /// Registers all repository interfaces at the boot time.
        /// </summary>
        public class RepositoryRegistrationConvention : IRegistrationConvention
        {
            private static readonly IEnumerable<Type> RepositoryInterfaces;

            static RepositoryRegistrationConvention()
            {
                // essentially, cache-up, at boot-time, all repository interfaces, so we don't have to perform
                // this expensive operation each time the convention is invoked
                var repositoryAssemblies = new List<Assembly>
                                               {
                                                   Assembly.Load ( "Pillar.Common" ) // TODO: This is only here for the metadata repository.  Should be removed. Metadata actually is another bounded context.
                                               };

                repositoryAssemblies.AddRange ( new AssemblyLocator().LocateDomainAssemblies () );

                RepositoryInterfaces = repositoryAssemblies.SelectMany ( x => x.GetTypes () )
                    .Where ( p =>
                    {
                        if ( p != null && !string.IsNullOrWhiteSpace ( p.Namespace ) )
                        {
                            return p.Name.EndsWith ( "Repository" );
                        }
                        return false;
                    } );
            }

            #region Implementation of IRegistrationConvention

            /// <summary>
            /// Registers the specified type.
            /// </summary>
            /// <param name="type">The specified type.</param>
            /// <param name="registry">The registry.</param>
            public void Process ( Type type, Registry registry )
            {
                if ( type.IsAbstract || !type.IsClass || !type.Name.EndsWith ( "Repository" ) )
                {
                    return;
                }

                Type interfaceType;

                try
                {
                    interfaceType = RepositoryInterfaces.Single ( p => p.Name == "I" + type.Name );
                }
                catch ( ArgumentNullException )
                {
                    interfaceType = null;
                }
                catch ( InvalidOperationException )
                {
                    interfaceType = null;
                }

                if ( interfaceType == null )
                {
                    // todo log
                    throw new Exception ( "Could not find the corresponding interface for Repository:" + type.Name );
                }

                registry.AddType ( interfaceType, type );
            }

            #endregion
        }
    }
}
