using FluentNHibernate.Cfg.Db;
using Rem.Infrastructure.Domain;

namespace Rem.Infrastructure.Tests.Domain
{
    public class SqlitePersistenceConfigurerProvider : IPersistenceConfigurerProvider
    {
        private static readonly string ConnectionString =
          "Data Source=:memory:;Version=3;New=True;";
        //@"Data Source=c:\_Temp\Rem.s3db;Version=3;New=True;";

        #region IPersistenceConfigurerProvider Members

        public IPersistenceConfigurer Configure ()
        {
            IPersistenceConfigurer database = SQLiteConfiguration
                .Standard
                .AdoNetBatchSize ( 10 )
                .UseOuterJoin ()
                .QuerySubstitutions ( "true 1, false 0, yes 'Y', no 'N'" )
                .ConnectionString ( ConnectionString );

            return database;
        }

        #endregion
    }
}