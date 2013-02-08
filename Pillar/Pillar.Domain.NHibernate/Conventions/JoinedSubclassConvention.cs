using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// JoinedSubclassConvention class.
    /// </summary>
    public class JoinedSubclassConvention : IJoinedSubclassConvention
    {
        #region Public Methods

        /// <summary>
        /// Adds Module namespace to schema.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IJoinedSubclassInstance instance )
        {
            var namespaces = instance.EntityType.Namespace.Split ( ".".ToCharArray () );

            foreach ( var ns in namespaces )
            {
                if ( ns.Contains ( "Module" ) )
                {
                    instance.Schema ( ns );
                    break;
                }
            }

            var key = instance.EntityType.BaseType.Name + "Key";

            instance.Key.Column ( key );
        }

        #endregion
    }
}
