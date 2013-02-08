using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// TableNameConvention class.
    /// </summary>
    public class TableNameConvention : IClassConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            if ( typeof( ILookup ).IsAssignableFrom ( instance.EntityType ) )
            {
                instance.Table ( instance.EntityType.Name + "Lkp" );
            }
        }

        #endregion
    }
}
