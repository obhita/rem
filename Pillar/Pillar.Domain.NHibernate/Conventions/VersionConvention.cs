using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// VersionConvention class.
    /// </summary>
    public class VersionConvention : IVersionConvention
    {
        #region Public Methods

        /// <summary>
        /// Applies the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IVersionInstance instance )
        {
            instance.Not.Nullable ();
        }

        #endregion
    }
}
