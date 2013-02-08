using System.Reflection;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.MappingModel.ClassBased;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// This is a hack.
    /// The reason for doing this hack is:
    /// We want to access the Components property of IComponentBaseInspector.
    /// But the implementation (ComponentBaseInspector) of IComponentBaseInspector throws exception when accessing the Components property.
    /// The ComponentBaseInspector has a mapping field. Lots of its properties (including Components) just reflect the state of mapping fields.
    /// So here we just directly use the mapping fields.
    /// </summary>
    public static class ComponentInstanceExtensions
    {
        #region Public Methods

        /// <summary>
        /// Converts to I component mapping.
        /// </summary>
        /// <param name="componentInstance">The component instance.</param>
        /// <returns>A <see cref="FluentNHibernate.MappingModel.ClassBased.IComponentMapping"/></returns>
        public static IComponentMapping ConvertToIComponentMapping ( this IComponentInstance componentInstance )
        {
            var componentBaseInspector = componentInstance as ComponentBaseInspector;

            var mappingField = componentBaseInspector.GetType ().GetField ( "mapping", BindingFlags.Instance | BindingFlags.NonPublic );

            var mapping = mappingField.GetValue ( componentBaseInspector ) as IComponentMapping;

            return mapping;
        }

        #endregion
    }
}
