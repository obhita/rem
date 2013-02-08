using System;

namespace Pillar.Domain
{
    /// <summary>
    /// ComponentNamingStrategyAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct )]
    public sealed class ComponentNamingStrategyAttribute : Attribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentNamingStrategyAttribute"/> class.
        /// </summary>
        /// <param name="componentNamingStrategyType">Type of the component naming strategy.</param>
        public ComponentNamingStrategyAttribute ( Type componentNamingStrategyType )
        {
            if ( !typeof( IComponentNamingStrategy ).IsAssignableFrom ( componentNamingStrategyType ) )
            {
                throw new ArgumentException ( "Type must implement IComponentNamingStrategy", "componentNamingStrategyType" );
            }
            ComponentNamingStrategy = Activator.CreateInstance ( componentNamingStrategyType ) as IComponentNamingStrategy;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the component naming strategy.
        /// </summary>
        public IComponentNamingStrategy ComponentNamingStrategy { get; private set; }

        #endregion
    }
}
