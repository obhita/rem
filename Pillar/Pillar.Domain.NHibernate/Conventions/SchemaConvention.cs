using System;
using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Pillar.Domain.NHibernate.Conventions
{
    /// <summary>
    /// SchemaConvention class.
    /// </summary>
    public class SchemaConvention : IClassConvention
    {
        #region Public Methods

        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <param name="type">The type to get module name for.</param>
        /// <returns>The module name.</returns>
        public static string GetModuleName ( Type type )
        {
            var nameSpace = type.Namespace;
            var namespaces = nameSpace.Split ( ".".ToCharArray () );
            return namespaces.FirstOrDefault ( ns => ns.Contains ( "Module" ) );
        }

        /// <summary>
        /// Applies the schema name for the module.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void Apply ( IClassInstance instance )
        {
            //Derive Schema
            var type = instance.EntityType;
            var moduleName = GetModuleName ( type );

            instance.Schema ( moduleName );
        }

        #endregion
    }
}
