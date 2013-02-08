using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pillar.Common.Utility
{
    /// <summary>
    /// Walks the logical tree of an object.
    /// </summary>
    public class LogicalTreeWalker
    {
        #region Constants and Fields

        private readonly Dictionary<Type, Action<object, object, PropertyInfo>> _actions;
        private readonly HashSet<object> _checkedObjects;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicalTreeWalker"/> class.
        /// </summary>
        public LogicalTreeWalker ()
        {
            _checkedObjects = new HashSet<object> ();
            _actions = new Dictionary<Type, Action<object, object, PropertyInfo>> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Walks the specified root.
        /// </summary>
        /// <typeparam name="T">Type of objects to recurse and apply actions on.</typeparam>
        /// <param name="root">The root object.</param>
        /// <param name="action">The action.</param>
        public static void Walk<T> ( object root, Action<T> action ) where T : class
        {
            var walker = new LogicalTreeWalker ();
            walker.RegisterAction ( action );
            walker.Walk<T> ( root );
        }

        /// <summary>
        /// Registers an action for when walking the tree.
        /// </summary>
        /// <typeparam name="T">Type of object to call action on.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="LogicalTreeWalker"/></returns>
        public LogicalTreeWalker RegisterAction<T> ( Action<T> action )
        {
            return RegisterAction<T> ( ( tobj, tPobj, propertyInfo ) => action ( tobj ) );
        }

        /// <summary>
        /// Registers an action for when walking the tree.
        /// </summary>
        /// <typeparam name="T">Type of object to call action on.</typeparam>
        /// <param name="action">The action.</param>
        /// <returns>A <see cref="LogicalTreeWalker"/></returns>
        public LogicalTreeWalker RegisterAction<T> ( Action<T, object, PropertyInfo> action )
        {
            if ( _actions.ContainsKey ( typeof( T ) ) )
            {
                _actions[typeof( T )] = ( obj, pobj, propertyInfo ) => action ( ( T )obj, ( T )pobj, propertyInfo );
            }
            else
            {
                _actions.Add ( typeof( T ), ( obj, pobj, propertyInfo ) => action ( ( T )obj, pobj, propertyInfo ) );
            }
            return this;
        }

        /// <summary>
        /// Walks the specified root.
        /// </summary>
        /// <typeparam name="T">Type of objects to recurse and apply actions on.</typeparam>
        /// <param name="root">The root object.</param>
        public void Walk<T> ( object root ) where T : class
        {
            _checkedObjects.Clear ();
            WalkerHelper<T> ( root, null, null );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the actions.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="propertyInfo">The property info.</param>
        private void CheckActions ( object item, object parentItem, PropertyInfo propertyInfo )
        {
            if ( item != null )
            {
                foreach ( var key in _actions.Keys )
                {
                    if ( key.IsAssignableFrom ( item.GetType () ) )
                    {
                        _actions[key] ( item, parentItem, propertyInfo );
                    }
                }
            }
        }

        /// <summary>
        /// Helper for recursively walking tree.
        /// </summary>
        /// <typeparam name="T">The type to walk.</typeparam>
        /// <param name="curentItem">The curent item.</param>
        /// <param name="parentItem">The parent item.</param>
        /// <param name="propertyInfo">The property info.</param>
        private void WalkerHelper<T> ( object curentItem, object parentItem, PropertyInfo propertyInfo ) where T : class
        {
            if ( curentItem != null )
            {
                _checkedObjects.Add ( curentItem );
                CheckActions ( curentItem, parentItem, propertyInfo );
                var publicProperties = curentItem.GetType ().GetProperties ();
                foreach ( var publicProperty in publicProperties )
                {
                    if ( typeof( T ).IsAssignableFrom ( publicProperty.PropertyType ) )
                    {
                        var tobjectToCheck = publicProperty.GetValue ( curentItem, null );
                        var objectToCheck = ( T )tobjectToCheck;
                        if ( !_checkedObjects.Contains ( objectToCheck ) )
                        {
                            WalkerHelper<T> ( objectToCheck, curentItem, publicProperty );
                        }
                    }
                    MethodInfo getMethod;
                    if ( publicProperty.PropertyType != typeof( string ) && typeof( IEnumerable ).IsAssignableFrom ( publicProperty.PropertyType )
                         && ( getMethod = publicProperty.GetGetMethod ( false ) ) != null && getMethod.GetParameters ().Count () == 0 )
                    {
                        var enumerableObjects = publicProperty.GetValue ( curentItem, null ) as IEnumerable;
                        if ( enumerableObjects != null )
                        {
                            CheckActions ( enumerableObjects, curentItem, publicProperty );

                            foreach ( var enumerableObject in enumerableObjects )
                            {
                                if ( typeof( T ).IsAssignableFrom ( enumerableObject.GetType () ) )
                                {
                                    var abstractDataTransferObject = ( T )enumerableObject;
                                    if ( !_checkedObjects.Contains ( abstractDataTransferObject ) )
                                    {
                                        WalkerHelper<T> ( abstractDataTransferObject, curentItem, publicProperty );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
