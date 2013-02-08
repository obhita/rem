using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Pillar.Common.Extension
{
    /// <summary>
    /// Extension methos for <see cref="PropertyInfo">PropertyInfo</see> type.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets whether <see cref="PropertyInfo">propertyInfo</see> is an Auto Property.
        /// </summary>
        /// <param name="propertyInfo">Property Info to check.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsAutoProperty ( this PropertyInfo propertyInfo )
        {
            var isAutoProperty = false;

            var getMethod = propertyInfo.GetGetMethod ( true );
            var setMethod = propertyInfo.GetSetMethod ( true );

            if ( getMethod != null && setMethod != null )
            {
                var getMethodIsCompilerGenerated = getMethod.GetCustomAttributes ( typeof( CompilerGeneratedAttribute ), true ).Any ();
                var setMethodIsCompilerGenerated = setMethod.GetCustomAttributes ( typeof( CompilerGeneratedAttribute ), true ).Any ();

                if ( getMethodIsCompilerGenerated && setMethodIsCompilerGenerated )
                {
                    isAutoProperty = true;
                }
            }

            return isAutoProperty;
        }

        /// <summary>
        /// Gets Whether property info is settable.
        /// </summary>
        /// <param name="propertyInfo">Property info to check.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsReadonly ( this PropertyInfo propertyInfo )
        {
            var setter = propertyInfo.GetSetMethod ( false );
            return setter == null;
        }

        #endregion
    }
}
