using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// PrimaryKeyIdConvention class.
    /// </summary>
    public class PrimaryKeyIdConvention : IIdConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies primary key name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IIdentityInstance instance )
        {
            var key = instance.EntityType.Name;

            if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
            {
                key = key + "Lkp";
            }

            instance.Column ( key + "Key" );
            instance.Access.CamelCaseField ( CamelCasePrefix.Underscore );
            instance.GeneratedBy.Custom ( typeof( CustomTableHiLoGenerator ) );
        }

        #endregion
    }
}
