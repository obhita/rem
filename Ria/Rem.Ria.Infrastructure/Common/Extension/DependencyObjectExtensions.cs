#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Rem.Ria.Infrastructure.Common.Extension
{
    /// <summary>
    /// DependencyObjectExtensions class.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        #region Public Methods

        /// <summary>
        /// Finds the ancestor.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="obj">The root object.</param>
        /// <returns>Found object or null.</returns>
        public static T FindAncestor<T> ( DependencyObject obj ) where T : DependencyObject
        {
            while ( obj != null )
            {
                var o = obj as T;
                if ( o != null )
                {
                    return o;
                }
                obj = VisualTreeHelper.GetParent ( obj );
            }
            return null;
        }

        /// <summary>
        /// Finds the ancestor.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="obj">The root object.</param>
        /// <returns>Found object or null.</returns>
        public static T FindAncestor<T> ( this UIElement obj ) where T : UIElement
        {
            return FindAncestor<T> ( ( DependencyObject )obj );
        }

        /// <summary>
        /// Finds the visual child.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="depObj">The dep obj.</param>
        /// <returns>The found child or null.</returns>
        public static T FindVisualChild<T> ( this DependencyObject depObj ) where T : DependencyObject
        {
            return FindVisualChild<T> ( depObj, o => true );
        }

        /// <summary>
        /// Finds the visual child.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="match">The predicate match.</param>
        /// <returns>The found child or null.</returns>
        public static T FindVisualChild<T> ( this DependencyObject depObj, Predicate<T> match ) where T : DependencyObject
        {
            if ( depObj != null )
            {
                for ( var i = 0; i < VisualTreeHelper.GetChildrenCount ( depObj ); i++ )
                {
                    var child = VisualTreeHelper.GetChild ( depObj, i );
                    if ( child != null && child is T && match ( child as T ) )
                    {
                        return ( T )child;
                    }

                    var childItem = FindVisualChild ( child, match );
                    if ( childItem != null )
                    {
                        return childItem;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the visual children.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="recurse">If set to <c>true</c> recurse.</param>
        /// <param name="children">The found children.</param>
        public static void FindVisualChildren<T> ( this DependencyObject parent, bool recurse, ref List<T> children ) where T : DependencyObject
        {
            FindVisualChildren ( parent, ele => recurse, ref children );
        }

        /// <summary>
        /// Finds the visual children.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="shouldRecurse">The should recurse predicate.</param>
        /// <param name="children">The found children.</param>
        public static void FindVisualChildren<T> ( this DependencyObject parent, Predicate<DependencyObject> shouldRecurse, ref List<T> children )
            where T : DependencyObject
        {
            foreach ( var child in GetChildren ( parent ) )
            {
                if ( child is T )
                {
                    children.Add ( ( T )child );
                }

                if ( shouldRecurse ( child ) )
                {
                    child.FindVisualChildren ( shouldRecurse, ref children );
                }
            }
        }

        /// <summary>
        /// Determines whether the specified obj is child.
        /// </summary>
        /// <param name="obj">The parent object.</param>
        /// <param name="childObj">The child obj.</param>
        /// <returns><c>true</c> if the specified obj is child; otherwise, <c>false</c>.</returns>
        public static bool IsChild ( this DependencyObject obj, object childObj )
        {
            var ret = false;
            if ( obj is Popup )
            {
                obj = ( obj as Popup ).Child;
            }
            else if ( obj is Telerik.Windows.Controls.Primitives.Popup )
            {
                obj = ( obj as Telerik.Windows.Controls.Primitives.Popup ).RealPopup.Child;
            }

            if(obj == null)
            {
                return false;
            }

            for ( var i = 0; i < VisualTreeHelper.GetChildrenCount ( obj ); i++ )
            {
                var child = VisualTreeHelper.GetChild ( obj, i );
                if ( child != null && child == childObj )
                {
                    ret = true;
                    break;
                }
                else if ( child != null )
                {
                    ret = IsChild ( child, childObj );
                    if ( ret )
                    {
                        break;
                    }
                }
            }
            return ret;
        }

        #endregion

        #region Methods

        private static IEnumerable<DependencyObject> GetChildren ( DependencyObject parent )
        {
            if (parent != null)
            {
                var childCount = VisualTreeHelper.GetChildrenCount ( parent );

                for ( var i = 0; i < childCount; i++ )
                {
                    yield return VisualTreeHelper.GetChild ( parent, i );
                }
            }
        }

        #endregion
    }
}
