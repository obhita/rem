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
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Cfg;
using Pillar.Common.Bootstrapper;
using Pillar.Common.InversionOfControl;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.FluentConfigurationSerializer"> FluentConfigurationSerializer </see> contains fluent configuraton serializer utilities.
    /// </summary>
    public static class FluentConfigurationSerializer
    {
        private const string SerializedConfiguration = @"c:\\temp\configuration.serialized";

        /// <summary>
        /// Initializes static members of the <see cref="FluentConfigurationSerializer"/> class.
        /// </summary>
        static FluentConfigurationSerializer()
        {
            IsEnabled = true;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool IsEnabled { get; set; }

     
        internal static bool IsConfigurationFileValid
        {
            get
            {
                if(!IsEnabled)
                {
                    return false;
                }

                var isValid = true;

                var configInfoLastWriteTime = new FileInfo(SerializedConfiguration).LastWriteTime;

                var assemblyLocator = IoC.CurrentContainer.Resolve<IAssemblyLocator> ();

                // First check domain assemblies
                var domainAssemblies = assemblyLocator.LocateDomainAssemblies ();

                foreach ( var domainAssembly in domainAssemblies )
                {
                    if (IsConfigInfoWrittenEarlier(configInfoLastWriteTime, domainAssembly))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    // Then check Pillar NHibernate Assebmly
                    var conventionAssembly = Assembly.GetAssembly ( typeof( Pillar.Domain.NHibernate.AutomappingConfiguration ) );

                    if (IsConfigInfoWrittenEarlier(configInfoLastWriteTime, conventionAssembly))
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    // Then check infrastructure assemblies
                    var infrastructureAssemblies = assemblyLocator.LocateInfrastructureAssemblies ();
                    foreach ( var infrastructureAssembly in infrastructureAssemblies )
                    {
                        if (IsConfigInfoWrittenEarlier(configInfoLastWriteTime, infrastructureAssembly))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                return isValid;
            }
        }

        private static bool IsConfigInfoWrittenEarlier(DateTime configInfoLastWriteTime, Assembly assembly)
        {
            // The CodeBase is a URL to the place where the file was found, while the Location is the path from where it was actually loaded.
            var assemblyFilePath = assembly.CodeBase.Substring("file:///".Length); // Get ride of "file:///"
            var assInfo = new FileInfo(assemblyFilePath); 
            if (configInfoLastWriteTime < assInfo.LastWriteTime)
            {
                return true;
            }

            return false;
        }

        internal static void SaveConfigurationToFile(NHibernate.Cfg.Configuration configuration)
        {
            if (IsEnabled)
            {
                using ( var file = File.Open ( SerializedConfiguration, FileMode.Create ) )
                {
                    var bf = new BinaryFormatter ();
                    bf.Serialize ( file, configuration );
                }
            }
        }

        internal static NHibernate.Cfg.Configuration LoadConfigurationFromFile()
        {
            if (!IsEnabled || IsConfigurationFileValid == false)
            {
                return null;
            }
            try
            {
                using (var file = File.OpenRead(SerializedConfiguration))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as NHibernate.Cfg.Configuration;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
