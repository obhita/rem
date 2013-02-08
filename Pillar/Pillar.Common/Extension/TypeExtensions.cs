using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Pillar.Common.Extension
{
    /// <summary>
    /// Extensions for objects of type <see cref="Type">Type</see>
    /// </summary>
    public static class TypeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Gets whether property infos of a certain type are readonly.
        /// </summary>
        /// <param name="type">Type that contains <paramref name="writableProperties">Property Infos</paramref></param>
        /// <param name="writableProperties">Out array of <see cref="PropertyInfo">PropertyInfo</see></param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool AllPropertiesAreReadonly ( this Type type, out PropertyInfo[] writableProperties )
        {
            var properties = type.GetProperties ();
            var list = properties.Where ( propertyInfo => !propertyInfo.IsReadonly () ).ToList ();

            writableProperties = list.ToArray ();
            return list.Count == 0;
        }

        /// <summary>
        /// Gets the deep copy of an object.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="obj">Object to deep copy.</param>
        /// <returns>A copy of type T.</returns>
        public static T DeepCopy<T> ( this T obj )
        {
            T copy;

            var serializer = new DataContractSerializer ( typeof( T ) );

            using ( var stream = new MemoryStream () )
            {
                serializer.WriteObject ( stream, obj );
                stream.Position = 0;
                copy = ( T )serializer.ReadObject ( stream );
            }

            return copy;
        }

        /// <summary>
        /// Gets default value of type.
        /// </summary>
        /// <param name="type">Type to get default value for.</param>
        /// <returns>An <see cref="object">object</see> that is the default value of the <paramref name="type"/></returns>
        public static object GetDefault ( this Type type )
        {
            return type.IsNullable () ? null : Activator.CreateInstance ( type );
        }

        /// <summary>
        /// Gets a list of <see cref="PropertyInfo"/> where the property type is of type T.
        /// </summary>
        /// <typeparam name="T">The type of properties to find.</typeparam>
        /// <param name="type">The type to find properties on.</param>
        /// <returns>List of <see cref="PropertyInfo"/></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesOfType<T> ( this Type type )
        {
            return type.GetProperties ().Where ( pi => pi.PropertyType == typeof( T ) );
        }

        /// <summary>
        /// Gets whether type is nullable or not.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        [DebuggerStepThrough]
        public static bool IsNullable ( this Type type )
        {
            var isNullable = false;

            if ( type.IsValueType )
            {
                if ( type.IsGenericType && type.GetGenericTypeDefinition ().Equals ( typeof( Nullable<> ) ) )
                {
                    isNullable = true;
                }
            }
            else
            {
                isNullable = true;
            }

            return isNullable;
        }

        /// <summary>
        /// Gets whether type is a nullable enum.
        /// </summary>
        /// <param name="type">The type .</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsNullableEnum ( this Type type )
        {
            var isNullableEnum = type.IsGenericType && type.GetGenericTypeDefinition () == typeof( Nullable<> )
                                  && type.GetGenericArguments ()[0].IsEnum;
            return isNullableEnum;
        }

        /// <summary>
        /// Gets whether a type is a subclass of a generic type.
        /// </summary>
        /// <param name="toCheck">Type to check.</param>
        /// <param name="generic">Base Generic Type.</param>
        /// <returns>A <see cref="bool">Boolean</see>.</returns>
        public static bool IsSubclassOfBaseGeneric ( this Type toCheck, Type generic )
        {
            while ( toCheck != typeof( object ) )
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition () : toCheck;
                if ( generic == cur )
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        #endregion
    }
}
