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
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate.Tool.hbm2ddl;
using Pillar.Common.Configuration;
using Rem.Infrastructure.Bootstrapper;
using Rem.Infrastructure.Domain;
using uNhAddIns.SessionEasier;

namespace ExportSchema
{
    public class Program
    {
        #region Constants and Fields

        private const string ExportOutputFilePropertyName = "ExportOutputFile";

        #endregion

        #region Methods

        private static void BuildCreate ( StringBuilder sqlCommandToRunBuilder, string databaseName, string sqlDataPath )
        {
            sqlCommandToRunBuilder.Append ( string.Format ( "CREATE DATABASE {0} ON PRIMARY", databaseName ) );
            sqlCommandToRunBuilder.Append ( Environment.NewLine );
            sqlCommandToRunBuilder.Append (
                string.Format (
                    @"( NAME = N'{0}_Data', FILENAME = N'{1}_Data.mdf' , SIZE = 167872KB , MAXSIZE = UNLIMITED, FILEGROWTH = 16384KB )",
                    databaseName,
                    Path.Combine ( sqlDataPath, databaseName ) ) );
            sqlCommandToRunBuilder.Append ( Environment.NewLine );
            sqlCommandToRunBuilder.Append ( " LOG ON" );
            sqlCommandToRunBuilder.Append ( Environment.NewLine );
            sqlCommandToRunBuilder.Append (
                string.Format (
                    @"( NAME = N'{0}_Log', FILENAME = N'{1}_Log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 16384KB )",
                    databaseName,
                    Path.Combine ( sqlDataPath, databaseName ) ) );
        }

        private static bool CheckDbExists ( SqlConnection sqlConnection, string databaseName )
        {
            bool databaseExists;
            try
            {
                var sqlCreateDbQuery = string.Format ( "SELECT * FROM master.dbo.sysdatabases WHERE name = '{0}'", databaseName );

                using ( var sqlCmd = new SqlCommand ( sqlCreateDbQuery, sqlConnection ) )
                {
                    var exists = sqlCmd.ExecuteScalar ();
                    databaseExists = exists != null;
                }
            }
            catch ( Exception )
            {
                databaseExists = false;
            }
            return databaseExists;
        }

        private static void CreateDatabase ( SqlConnection sqlConnection, string databaseName, string sqlDataPath )
        {
            var sqlCommandToRunBuilder = new StringBuilder ();
            BuildCreate ( sqlCommandToRunBuilder, databaseName, sqlDataPath );
            Console.WriteLine ( "Creating Database..." );
            using ( var sqlDropCreateDatabaseCommand = new SqlCommand ( sqlCommandToRunBuilder.ToString (), sqlConnection ) )
            {
                sqlDropCreateDatabaseCommand.ExecuteNonQuery ();
            }
        }

        private static string GenerateSchemaCreationSql ()
        {
            var moduleNames = GetModuleNames ();
            var sb = new StringBuilder ();

            foreach ( var moduleName in moduleNames )
            {
                var sql = string.Format (
                    "Create Schema {0} {1}go {2}",
                    moduleName,
                    Environment.NewLine,
                    Environment.NewLine );
                sb.Append ( sql );
            }

            return sb + Environment.NewLine;
        }

        private static string GetDbFilePath ( SqlConnection sqlConnection, string databaseName )
        {
            var path = string.Empty;
            try
            {
                var datafilepathQuery =
                    string.Format (
                        @"select physical_name from 
              master.sys.databases dbs inner join master.sys.master_files files 
                on dbs.database_id = files.database_id
                where dbs.name = '{0}' and type_desc = 'ROWS'",
                        databaseName );

                using ( var sqlCmd = new SqlCommand ( datafilepathQuery, sqlConnection ) )
                {
                    var datafilepath = sqlCmd.ExecuteScalar ();
                    path = datafilepath.ToString ();
                    path = path.Substring ( 0, path.LastIndexOf ( '\\' ) );
                }
            }
            catch
            {
            }
            return path;
        }

        private static IEnumerable<string> GetModuleNames ()
        {
            var types = new List<Type> ();

            AssemblyLocator assemblyLocator = new AssemblyLocator();

            var domainAssemblies = assemblyLocator.LocateDomainAssemblies ();

            foreach (var assembly in domainAssemblies)
            {
                types.AddRange ( assembly.GetTypes () );
            }

            IList<string> nameList = new List<string> ();
            foreach ( var type in types )
            {
                var namespaces = type.Namespace.Split ( '.' );
                var nspace = namespaces[namespaces.Length - 1];
                if ( nspace.EndsWith ( "Module" ) && !nameList.Contains ( nspace ) )
                {
                    nameList.Add ( nspace );
                }
            }

            return nameList.ToArray ();
        }

        private static void Main ()
        {
            var appConfig = new AppSettingsConfiguration ();
            var connectionStringBuilder = new SqlConnectionStringBuilder ();

            connectionStringBuilder.DataSource = appConfig.GetProperty ( "DatabaseServerName" );
            connectionStringBuilder.IntegratedSecurity = true;

            var dbCreated = false;
            var databaseName = appConfig.GetProperty ( "DatabaseCatalogName" );
            var sqlDataPath = String.Empty; // appConfig.GetProperty("SqlDataPath");

            using ( var sqlConnnection = new SqlConnection ( connectionStringBuilder.ConnectionString ) )
            {
                sqlConnnection.Open ();

                sqlDataPath = GetDbFilePath ( sqlConnnection, "master" );

                if ( !CheckDbExists ( sqlConnnection, databaseName ) )
                {
                    CreateDatabase ( sqlConnnection, databaseName, sqlDataPath );
                    dbCreated = true;
                }
            }
            var appSettingsConfiguration = new AppSettingsConfiguration ();
            Console.WriteLine ( "Creating schema script...." );
            var msSql2008PersistenceConfigurerProvider = new MsSql2008PersistenceConfigurerProvider ( appSettingsConfiguration );
#if DEBUG
            FluentConfigurationSerializer.IsEnabled = false;
#endif
            IConfigurationProvider fluentConfigurationProvider = new FluentConfigurationProvider ( msSql2008PersistenceConfigurerProvider, new AssemblyLocator() );
            var configuration = fluentConfigurationProvider.Configure ().GetEnumerator ().Current;
            ISessionFactoryProvider sessionFactoryProvider = new SessionFactoryProvider ( fluentConfigurationProvider );
            var sessionFactory = sessionFactoryProvider.GetFactory ( null );

            var exportFileName = appConfig.GetProperty ( ExportOutputFilePropertyName );

            var sb = new StringBuilder ();

            var headerComment = string.Format (
                "{0}{0}{1}{0}{2}{0}{3}{0}",
                Environment.NewLine,
                "/***********************************************************",
                "Begin: SQL Scripts to create RemGenDatabase database objects",
                "***********************************************************/"
                );
            sb.Append ( headerComment );

            var sql = GenerateSchemaCreationSql ();

            using ( var session = sessionFactory.OpenSession () )
            {
                TextWriter textWriter = new StringWriter ();

                new SchemaExport ( configuration ).Execute (
                    true,
                    false,
                    false,
                    session.Connection,
                    textWriter );
                sql += textWriter.ToString ();
            }

            sb.Append ( sql );
            sb.Append ( Environment.NewLine );
            sb.Append ( "/***********************************************************" );
            sb.Append ( Environment.NewLine );
            sb.Append ( "End: SQL Scripts to create RemGenDatabase database objects" );
            sb.Append ( Environment.NewLine );
            sb.Append ( "***********************************************************/" );
            sb.Append ( Environment.NewLine );
            sb.Append ( Environment.NewLine );

            Debug.Write ( sb.ToString () );
            Console.WriteLine ( sb.ToString () );

            using ( var fs = new FileStream ( exportFileName, FileMode.OpenOrCreate, FileAccess.Write ) )
            {
                using ( var streamWriter = new StreamWriter ( fs ) )
                {
                    streamWriter.Write ( sb.ToString () );
                }
            }
            Console.WriteLine ( "Deploy to Database (Y/N)?" );
            var answer = Console.ReadLine ();
            if ( string.Compare ( answer, "yes", true ) == 0 || string.Compare ( answer, "y", true ) == 0 )
            {
                if ( !dbCreated )
                {
                    Console.WriteLine ( "Dropping Database..." );
                    using ( var sqlConnection = new SqlConnection ( connectionStringBuilder.ConnectionString ) )
                    {
                        sqlConnection.Open ();
                        using (
                            var sqlDropDatabaseCommand =
                                new SqlCommand (
                                    string.Format (
                                        "USE {0} ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE USE master DROP DATABASE {0}",
                                        databaseName ),
                                    sqlConnection ) )
                        {
                            sqlDropDatabaseCommand.ExecuteNonQuery ();
                        }
                    }
                }
                using ( var sqlConnection = new SqlConnection ( connectionStringBuilder.ConnectionString ) )
                {
                    sqlConnection.Open ();
                    if ( !dbCreated )
                    {
                        CreateDatabase ( sqlConnection, databaseName, sqlDataPath );
                    }
                }
                //connectionStringBuilder.InitialCatalog = databaseName;
                using ( var sqlConnection = new SqlConnection ( connectionStringBuilder.ConnectionString ) )
                {
                    sqlConnection.Open ();
                    sqlConnection.ChangeDatabase ( databaseName );
                    Console.WriteLine ( "Running schema script..." );
                    var fullScript = sb.ToString ();
                    var goSplit = fullScript.Split (
                        new[] { string.Format ( "go {0}", Environment.NewLine ) }, StringSplitOptions.RemoveEmptyEntries );
                    foreach ( var sqlstring in goSplit )
                    {
                        var sqlToRun = sqlstring;
                        if ( sqlstring == goSplit[0] )
                        {
                            sqlToRun = sqlstring.Replace ( headerComment, "" );
                        }
                        using ( var createSchemaCommand = new SqlCommand ( sqlToRun, sqlConnection ) )
                        {
                            createSchemaCommand.ExecuteNonQuery ();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
