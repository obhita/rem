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
using Agatha.Common;
using Agatha.Common.WCF;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Bootstrapper
{
    /// <summary>
    /// A helper class that contains functionalities relating to generic and known types.
    /// </summary>
    public class KnownTypeHelper
    {
        /// <summary>
        /// Registers the non generic requests and responses for an assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void RegisterNonGenericRequestsAndResponses(Assembly assembly)
        {
            var nonGenericTypes = (from t in assembly.GetTypes()
                                   where
                                       t.IsAbstract == false && t.IsGenericTypeDefinition == false
                                       && (t.IsSubclassOf(typeof(Request)) || t.IsSubclassOf(typeof(Response)))
                                   select t).ToArray();
            foreach (var nonGenericType in nonGenericTypes)
            {
                KnownTypeProvider.Register(nonGenericType);
            }
        }

        /// <summary>
        /// Gets the generics.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A Dictionary&lt;Type, Type&gt;.</returns>
        public static Dictionary<Type, Type> GetGenerics(Assembly assembly)
        {
            var generics = new Dictionary<Type, Type>();
            var genericTypes = from t in assembly.GetTypes()
                               where t.IsGenericTypeDefinition && t.IsAbstract == false
                               select t;

            foreach (var genericType in genericTypes)
            {
                if (genericType.GetGenericArguments().Length > 1)
                {
                    continue;
                }

                var argument = genericType.GetGenericArguments().Single();

                if (!argument.BaseType.IsSubclassOf(typeof(AbstractDataTransferObject)))
                {
                    continue;
                }

                var markerType = argument.GetGenericParameterConstraints().FirstOrDefault();
                if (markerType == null)
                {
                    throw new InvalidOperationException(
                        "generic parameter must be constraint to implement some marker interface in order to build closed generic types for every marker implementor");
                }

                generics.Add(genericType, markerType);
            }
            return generics;
        }

        /// <summary>
        /// Registers the generics.
        /// </summary>
        /// <param name="generics">The generics.</param>
        /// <param name="assembly">The assembly.</param>
        public static void RegisterGenerics(Dictionary<Type, Type> generics, Assembly assembly)
        {
            foreach (var genericType in generics.Keys)
            {
                var typesToApply = from t in assembly.GetTypes()
                                   where !t.IsGenericType && t.IsSubclassOf ( generics[genericType])
                                   select t;
                foreach (var type in typesToApply)
                {
                    Type genericType1 = genericType;
                    KnownTypeProvider.Register(genericType1.MakeGenericType(type));
                }
            }
        }

        /// <summary>
        /// Registers the known types from IKnownType providers.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public static void RegisterKnownTypesFromIKnownTypeProviders(Assembly assembly)
        {
            var knownTypeProviderTypes = from t in assembly.GetTypes ()
                                     where typeof ( IKnownTypeProvider ).IsAssignableFrom ( t ) && !t.IsInterface && !t.IsAbstract
                                     select t;
            foreach (var knownTypeProviderType in knownTypeProviderTypes)
            {
                var knownTypeProvider = Activator.CreateInstance ( knownTypeProviderType ) as IKnownTypeProvider;
                knownTypeProvider.RegisterTypes ();
            }
        }
    }
}
