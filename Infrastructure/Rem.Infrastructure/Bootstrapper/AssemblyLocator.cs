using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Pillar.Common.Bootstrapper;

namespace Rem.Infrastructure.Bootstrapper
{
    /// <summary>
    /// Class for locating assemblies.
    /// </summary>
    public class AssemblyLocator : IAssemblyLocator
    {
        /// <summary>
        /// Locates the domain assemblies.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Assembly}"/></returns>
        public IEnumerable<Assembly> LocateDomainAssemblies ()
        {
            var assemblyList = new List<Assembly>
                {
                    Assembly.GetAssembly ( typeof( Rem.Domain.Core.CommonModule.LookupBase ) ),
                    Assembly.GetAssembly ( typeof( Rem.Domain.Clinical.PatientModule.Patient ) ),

                    Assembly.GetAssembly ( typeof( Rem.Domain.Billing.BillingOfficeModule.BillingOffice ) ) // Temporally remove Billing to avoid NHibernate mapping
                };

            return assemblyList;
        }

        /// <summary>
        /// Locates the infrastructure assemblies.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Assembly}"/></returns>
        public IEnumerable<Assembly> LocateInfrastructureAssemblies()
        {
            var assemblyList = new List<Assembly>
                {
                    Assembly.GetAssembly ( typeof( Rem.Infrastructure.Domain.Repository.PatientRepository ) )
                };

            return assemblyList;
        }

        /// <summary>
        /// Locates the web service assemblies.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{Assembly}"/></returns>
        public IEnumerable<Assembly> LocateWebServiceAssemblies()
        {
            var regex = new Regex("Rem.Ria.*.Web");
            var assemblies = from assembly in AppDomain.CurrentDomain.GetAssemblies()
                             where regex.IsMatch(assembly.FullName)
                             select assembly;

            return assemblies;
        }
    }
}
