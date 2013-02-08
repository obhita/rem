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
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate.Linq;
using Pillar.Common.Bootstrapper;
using Pillar.Common.Configuration;
using Pillar.Common.InversionOfControl;
using Pillar.Domain;
using Pillar.Domain.NHibernate.Conventions;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Domain;
using StructureMap;
using uNhAddIns.SessionEasier;
using Environment = NHibernate.Cfg.Environment;
using IContainer = StructureMap.IContainer;

namespace GenerateLoadScripts
{
    internal class Program
    {
        #region Constants and Fields

        private static int _codedConceptCodeSize;
        private static int _createdBySize;
        private static int _defIndSize;
        private static int _descSize;
        private static int _effEndDtSize;
        private static int _effStDtSize;
        private static int _keySize;
        private static int _nameSize;
        private static int _shortNameSize;
        private static int _sortOrdSize;
        private static int _sysOwnSize;
        private static int _updatedBySize;
        private static int _versionSize;
        private static int _wellKnownNameSize;

        #endregion

        #region Methods

        private static void ConfigureServiceLocator ( IContainer container )
        {
            var structureMapPillarContainer = new Pillar.IoC.StructureMap.Container ( container );
            IoC.SetContainerProvider ( () => structureMapPillarContainer );
            container.Configure ( c => c.For<Pillar.Common.InversionOfControl.IContainer> ().Singleton ().Use ( IoC.CurrentContainer ) );
        }

        private static IContainer CreateAndConfigureApplicationDiContainer ()
        {
            var appContainer = new Container ();

            appContainer.Configure(c => c.For<IAssemblyLocator>().Singleton().Use<AssemblyLocator>());

            appContainer.Configure(c => c.For<IConfigurationPropertiesProvider>().Singleton().Use<AppSettingsConfiguration>());
            appContainer.Configure(c => c.For<IPersistenceConfigurerProvider>().Singleton().Use<MsSql2008PersistenceConfigurerProvider>());
            appContainer.Configure(c => c.For<IConfigurationProvider>().Singleton().Use<FluentConfigurationProvider>());

            appContainer.Configure ( c => c.For<IConfigurationPropertiesProvider> ().Singleton ().Use<AppSettingsConfiguration> () );
            appContainer.Configure ( c => c.For<IPersistenceConfigurerProvider> ().Singleton ().Use<MsSql2008PersistenceConfigurerProvider> () );
            appContainer.Configure ( c => c.For<IConfigurationProvider> ().Singleton ().Use<FluentConfigurationProvider> () );

            return appContainer;
        }

        private static Type GetRemType ( Type type )
        {
            var remType = type;

            //var assemblyLocator = IoC.CurrentContainer.Resolve<IAssemblyLocator> ();

            //if (!assemblyLocator.LocateDomainAssemblies().Contains(remType.Assembly))
            //{
            //    remType = remType.BaseType;
            //}

            return remType;
        }

        private static void Initialize ()
        {
            var appContainer = CreateAndConfigureApplicationDiContainer ();
            ConfigureServiceLocator ( appContainer );

            InitializeAndRegisterPersistenceStuff ( appContainer );
        }

        private static void InitializeAndRegisterPersistenceStuff ( IContainer container )
        {
            var configurationProvider = container.GetInstance<IConfigurationProvider> ();
            var configuration = configurationProvider.Configure ().GetEnumerator ().Current;
            configuration.SetProperty (
                Environment.CurrentSessionContextClass,
                "uNhAddIns.SessionEasier.Contexts.ThreadLocalSessionContext, uNhAddIns" );
            var sessionFactoryProvider = new SessionFactoryProvider ( configurationProvider );
            sessionFactoryProvider.Initialize ();

            container.Configure ( x => x.For<ISessionFactoryProvider> ().Singleton ().Use ( sessionFactoryProvider ) );
            container.Configure ( x => x.For<ISessionProvider> ().Use<SessionProvider> () );
        }

        private static void InitializeSizes ( IEnumerable<LookupBase> valueList )
        {
            _keySize = 0;
            _versionSize = 0;
            _createdBySize = 0;
            _updatedBySize = 0;
            _nameSize = 0;
            _descSize = 0;
            _wellKnownNameSize = 0;
            _shortNameSize = 0;
            _sortOrdSize = 0;
            _effStDtSize = 0;
            _sysOwnSize = 0;
            _defIndSize = 0;
            _codedConceptCodeSize = 0;

            foreach ( var lookupBase in valueList )
            {
                var keySize = lookupBase.Key.ToString ().Length;
                _keySize = keySize > _keySize ? keySize : _keySize;

                var versionSize = lookupBase.Version.ToString ().Length;
                _versionSize = versionSize > _versionSize ? versionSize : _versionSize;

                var createdBySize = lookupBase.CreatedBySystemAccount.Key.ToString ().Length;
                _createdBySize = createdBySize > _createdBySize ? createdBySize : _createdBySize;

                var updatedBySize = lookupBase.UpdatedBySystemAccount.Key.ToString ().Length;
                _updatedBySize = updatedBySize > _updatedBySize ? updatedBySize : _updatedBySize;

                var nameSize = ( "N'" + lookupBase.Name.Encode () + "'" ).Length;
                _nameSize = nameSize > _nameSize ? nameSize : _nameSize;

                var descString = lookupBase.Description == null ? "NULL" : "N'" + lookupBase.Description.Encode () + "'";
                var descSize = descString.Length;
                _descSize = descSize > _descSize ? descSize : _descSize;

                var wellKnownNameString = lookupBase.WellKnownName == null ? "NULL" : "N'" + lookupBase.WellKnownName.Encode () + "'";
                var wellKnownNamedSize = wellKnownNameString.Length;
                _wellKnownNameSize = wellKnownNamedSize > _wellKnownNameSize ? wellKnownNamedSize : _wellKnownNameSize;

                var shortNameString = lookupBase.ShortName == null ? "NULL" : "N'" + lookupBase.ShortName.Encode () + "'";
                var shortNamedSize = shortNameString.Length;
                _shortNameSize = shortNamedSize > _shortNameSize ? shortNamedSize : _shortNameSize;

                var sortOrdName = lookupBase.SortOrderNumber == null ? "NULL" : lookupBase.SortOrderNumber.ToString ();
                var sortOrdSize = sortOrdName.Length;
                _sortOrdSize = sortOrdSize > _sortOrdSize ? sortOrdSize : _sortOrdSize;

                var effStDtString = lookupBase.EffectiveDateRange.StartDate == null
                                        ? "NULL"
                                        : "'" + lookupBase.EffectiveDateRange.StartDate.Value.ToString ( "d" ) + "'";
                var effStDtSize = effStDtString.Length;
                _effStDtSize = effStDtSize > _effStDtSize ? effStDtSize : _effStDtSize;

                var effEdDtString = lookupBase.EffectiveDateRange.EndDate == null
                                        ? "NULL"
                                        : "'" + lookupBase.EffectiveDateRange.EndDate.Value.ToString ( "d" ) + "'";
                var effEndDtSize = effEdDtString.Length;
                _effEndDtSize = effEndDtSize > _effEndDtSize ? effEndDtSize : _effEndDtSize;

                var sysOwnSize = ( lookupBase.SystemOwnedIndicator ? 1 : 0 ).ToString ().Length;
                _sysOwnSize = sysOwnSize > _sysOwnSize ? sysOwnSize : _sysOwnSize;

                var defIndSize = ( lookupBase.DefaultIndicator ? 1 : 0 ).ToString ().Length;
                _defIndSize = defIndSize > _defIndSize ? defIndSize : _defIndSize;

                if ( lookupBase.GetType ().BaseType == typeof( CodedConceptLookupBase ) )
                {
                    var codedConceptLookupBase = ( lookupBase as CodedConceptLookupBase );
                    var codedConceptCodeString = codedConceptLookupBase.CodedConceptCode == null
                                                     ? "NULL"
                                                     : "N'" + codedConceptLookupBase.CodedConceptCode.Encode () + "'";
                    var codedConceptCodeSize = codedConceptCodeString.Length;
                    _codedConceptCodeSize = codedConceptCodeSize > _codedConceptCodeSize ? codedConceptCodeSize : _codedConceptCodeSize;
                }
            }
        }

        private static bool IsSupported ( Type type )
        {
            var remType = GetRemType ( type );

            var isSupported = false;

            // for now we can only generate lookup scripts for types that 
            // directly inherit from LookupBase/CodedConceptLookupBase 
            // and introduce no mapping properties of their own.
            if ( type.BaseType == typeof( LookupBase ) || type.BaseType == typeof( CodedConceptLookupBase ) )
            {
                var introduceNewMappingProperty = false;
                var props = remType.GetProperties ( BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance );
                foreach ( var propertyInfo in props )
                {
                    var ignoreMappingMemberAttributes = propertyInfo.GetCustomAttributes ( typeof( IgnoreMappingAttribute ), false );
                    if ( ignoreMappingMemberAttributes.Length == 0 )
                    {
                        introduceNewMappingProperty = true;
                        break;
                    }
                }

                isSupported = !introduceNewMappingProperty;
            }

            return isSupported;
        }

        private static void Main ()
        {
            Initialize ();

            var sqlFolderPath = AppDomain.CurrentDomain.BaseDirectory + @"\Sql\";
            if ( !Directory.Exists ( sqlFolderPath ) )
            {
                Directory.CreateDirectory ( sqlFolderPath );
            }

            var sessionProvider = IoC.CurrentContainer.Resolve<ISessionProvider> ();
            var session = sessionProvider.GetSession ();
            using ( session )
            {
                var query = from lkp in session.Query<LookupBase> ()
                            select lkp;
                var lookupList = query.ToList ();
                var lookupTypeList = ( from lkp in lookupList select GetRemType ( lkp.GetType () ) ).Distinct ().ToList ();

                foreach ( var lookupType in lookupTypeList )
                {
                    var type = lookupType;

                    if ( !IsSupported ( type ) )
                    {
                        Console.Error.WriteLine ( "** Unsupported type {0}. Skipping.", type );
                        continue;
                    }

                    var valueList = from lkp in lookupList
                                    where GetRemType ( lkp.GetType () ) == type
                                    select lkp;

                    WriteLoadScript ( sqlFolderPath, type, valueList );
                }
            }
        }

        private static void WriteLoadScript ( string sqlFolderPath, Type type, IEnumerable<LookupBase> valueList )
        {
            InitializeSizes ( valueList );

            var namespaceName = SchemaConvention.GetModuleName ( type );
            var typeName = type.Name;
            var fileName = string.Format ( "{0}_{1}Lkp.sql", namespaceName, typeName );

            var filePath = sqlFolderPath + fileName;

            var file = new FileInfo ( filePath );

            if ( file.Exists && file.IsReadOnly )
            {
                Console.Error.WriteLine ( string.Format ( "\t** Cannot update {0} because it is readonly.", fileName ) );
                return;
            }

            Console.Out.WriteLine ( "Writing {0}.", fileName );

            using ( var sw = file.CreateText () )
            {
                sw.WriteLine ();
                sw.WriteLine ( "print '------------------------------------------------------------------------------------------'" );
                sw.WriteLine ( string.Format ( "print '{0}'", fileName ) );
                sw.WriteLine ( "print '------------------------------------------------------------------------------------------'" );
                sw.WriteLine ();

                foreach ( var lookupBase in valueList )
                {
                    var primaryKeyName = string.Format ( "{0}LkpKey", typeName );
                    var columnNames =
                        "[" + primaryKeyName + "], " +
                        "[CreatedTimestamp], " +
                        "[CreatedBySystemAccountKey], " +
                        "[UpdatedTimestamp], " +
                        "[UpdatedBySystemAccountKey], " +
                        "[Version], " +
                        "[Name], " +
                        "[Description], " +
                        "[WellKnownName], " +
                        "[ShortName], " +
                        "[SortOrderNumber], " +
                        "[EffectiveStartDate], " +
                        "[EffectiveEndDate], " +
                        "[SystemOwnedIndicator], " +
                        "[DefaultIndicator] ";

                    var keyString = lookupBase.Key.ToString ().PadLeft ( _keySize );
                    var createByString = lookupBase.CreatedBySystemAccount.Key.ToString ().PadLeft ( _createdBySize );
                    var updatedByString = lookupBase.UpdatedBySystemAccount.Key.ToString ().PadLeft ( _updatedBySize );
                    var versionString = lookupBase.Version.ToString ().PadLeft ( _versionSize );

                    var nameString = lookupBase.Name.Encode ();
                    nameString = "N'" + nameString + "'";
                    nameString = nameString.PadLeft ( _nameSize );

                    var descString = lookupBase.Description == null ? "NULL" : "N'" + lookupBase.Description.Encode () + "'";
                    descString = descString.PadLeft ( _descSize );

                    var wellKnownNameString = lookupBase.WellKnownName == null ? "NULL" : "N'" + lookupBase.WellKnownName.Encode () + "'";
                    wellKnownNameString = wellKnownNameString.PadLeft ( _wellKnownNameSize );

                    var shortNameString = lookupBase.ShortName == null ? "NULL" : "N'" + lookupBase.ShortName.Encode () + "'";
                    shortNameString = shortNameString.PadLeft ( _shortNameSize );

                    var sortOrdName = lookupBase.SortOrderNumber == null ? "NULL" : lookupBase.SortOrderNumber.ToString ();
                    sortOrdName = sortOrdName.PadLeft ( _sortOrdSize );

                    var effStDtString = lookupBase.EffectiveDateRange.StartDate == null
                                            ? "NULL"
                                            : "'" + lookupBase.EffectiveDateRange.StartDate.Value.ToString ( "d" ) + "'";
                    effStDtString = effStDtString.PadLeft ( _effStDtSize );

                    var effEdDtString = lookupBase.EffectiveDateRange.EndDate == null
                                            ? "NULL"
                                            : "'" + lookupBase.EffectiveDateRange.EndDate.Value.ToString ( "d" ) + "'";
                    effEdDtString = effEdDtString.PadLeft ( _effEndDtSize );

                    var sysOwnString = ( lookupBase.SystemOwnedIndicator ? 1 : 0 ).ToString ().PadLeft ( _sysOwnSize );
                    var defIndString = ( lookupBase.DefaultIndicator ? 1 : 0 ).ToString ().PadLeft ( _defIndSize );

                    var columnValueFormat =
                        "{0}, " + // primaryKey
                        "current_timestamp, " + // CreatedTimestamp
                        "{1}, " + // CreatedBySystemAccountKey
                        "current_timestamp, " + // UpdatedTimestamp 
                        "{2}, " + // UpdatedSystemAccountKey
                        "{3}, " + // Version
                        "{4}, " + // Name
                        "{5}, " + // Description
                        "{6}, " + // WellKnownName
                        "{7}, " + // ShortName
                        "{8}, " + // SortOrderNumber
                        "{9}, " + // EffectiveStartDate
                        "{10}, " + // EffectiveEndDate
                        "{11}, " + // SystemOwnedIndicator
                        "{12}"; // DefaultIndicator

                    var columnValueArgs = new ArrayList
                        {
                            keyString,
                            createByString,
                            updatedByString,
                            versionString,
                            nameString,
                            descString,
                            wellKnownNameString,
                            shortNameString,
                            sortOrdName,
                            effStDtString,
                            effEdDtString,
                            sysOwnString,
                            defIndString
                        };

                    if ( type.BaseType == typeof( CodedConceptLookupBase ) )
                    {
                        columnNames = columnNames.TrimEnd () + ", " + "[CodedConceptCode]";
                        columnValueFormat = columnValueFormat + ", " +
                                            "{13}"; // CodedConceptCode
                        var codedConceptLookupBase = ( lookupBase as CodedConceptLookupBase );
                        var codedConceptCodeString = codedConceptLookupBase.CodedConceptCode == null
                                                         ? "NULL"
                                                         : "N'" + codedConceptLookupBase.CodedConceptCode.Encode () + "'";
                        codedConceptCodeString = codedConceptCodeString.PadLeft ( _codedConceptCodeSize );
                        columnValueArgs.Add ( codedConceptCodeString );
                    }

                    var columnValues = string.Format ( columnValueFormat, columnValueArgs.ToArray () );

                    var line = string.Format (
                        "INSERT INTO [{0}].[{1}Lkp] ( {2} ) VALUES ( {3} )", namespaceName, typeName, columnNames, columnValues );
                    sw.WriteLine ( line );
                }

                sw.WriteLine ();
                sw.WriteLine ( "GO" );
            }
        }

        #endregion
    }
}
