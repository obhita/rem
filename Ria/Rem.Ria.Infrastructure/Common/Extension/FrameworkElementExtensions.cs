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
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Pillar.Common.Utility;
using Rem.Ria.Infrastructure.View.Behavior;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.Common.Extension
{
    /// <summary>
    /// FrameworkElementExtensions class.
    /// </summary>
    public static class FrameworkElementExtensions
    {
        #region Constants and Fields

        private const int ScrollPadding = 10;

        #endregion

        #region Public Methods

        /// <summary>
        /// Brings the into view.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="animate">If set to <c>true</c> [animate].</param>
        public static void BringIntoView ( this FrameworkElement frameworkElement, bool animate = false )
        {
            var parent = VisualTreeHelper.GetParent ( frameworkElement );
            while ( parent != null )
            {
                parent = VisualTreeHelper.GetParent ( parent );
                var scrollViewer = parent as ScrollViewer;
                if ( scrollViewer != null )
                {
                    frameworkElement.BringIntoViewForScrollViewer ( scrollViewer, animate );
                    break;
                }
            }
        }

        /// <summary>
        /// Brings the into view for scroll viewer.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="scrollViewer">The scroll viewer.</param>
        /// <param name="animate">If set to <c>true</c> [animate].</param>
        public static void BringIntoViewForScrollViewer ( this FrameworkElement frameworkElement, ScrollViewer scrollViewer, bool animate )
        {
            var transform = frameworkElement.TransformToVisual ( scrollViewer );
            var topPositionInScrollViewer = transform.Transform ( new Point ( 0, 0 ) );
            var bottomPositionInScrollViewer = transform.Transform ( new Point ( 0, frameworkElement.ActualHeight ) );

            double offsetToScrollTo = -1;

            if ( topPositionInScrollViewer.Y < 0 )
            {
                offsetToScrollTo = scrollViewer.VerticalOffset + topPositionInScrollViewer.Y - ScrollPadding;
            }
            else if ( bottomPositionInScrollViewer.Y > scrollViewer.ViewportHeight )
            {
                offsetToScrollTo = scrollViewer.VerticalOffset + bottomPositionInScrollViewer.Y - scrollViewer.ViewportHeight +
                                   ScrollPadding;
            }
            if ( offsetToScrollTo > 0 )
            {
                if ( animate )
                {
                    var storyboard = new Storyboard ();
                    storyboard.Children.Add (
                        new DoubleAnimation
                            {
                                From = scrollViewer.VerticalOffset,
                                To = offsetToScrollTo,
                                Duration = new Duration ( new TimeSpan ( 0, 0, 1 ) ),
                                EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut }
                            } );
                    var animationHelper = new ScrollViewerAnimationHelper { ScrollViewer = scrollViewer };
                    Storyboard.SetTarget ( storyboard.Children[0], animationHelper );
                    Storyboard.SetTargetProperty (
                        storyboard.Children[0], new PropertyPath ( PropertyUtil.ExtractPropertyName ( () => animationHelper.VerticalOffset ) ) );
                    storyboard.Begin ();
                }
                else
                {
                    scrollViewer.ScrollToVerticalOffset ( offsetToScrollTo );
                }
            }
        }

        /// <summary>
        /// Gets the behaviors.
        /// </summary>
        /// <typeparam name="T">The type of behavior</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>List of behaviors.</returns>
        public static IEnumerable<T> GetBehaviors<T> ( this DependencyObject dependencyObject ) where T : Behavior
        {
            var behaviors = Interaction.GetBehaviors ( dependencyObject );
            return behaviors.OfType<T> ();
        }

        /// <summary>
        /// This is needed because for some reason the TemplatedParent property of a frameworkelement is Internal in silverlight (it is public in WPF!).
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <returns>The temlplated parent or null.</returns>
        public static object GetTemplatedParent ( this FrameworkElement frameworkElement )
        {
            var tempBinding = new Binding { RelativeSource = new RelativeSource ( RelativeSourceMode.TemplatedParent ) };
            frameworkElement.SetBinding ( FrameworkElement.TagProperty, tempBinding );
            var value = frameworkElement.Tag;
            return value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="propertyNameToGet">The property name to get.</param>
        /// <param name="found">If set to <c>true</c> [found].</param>
        /// <returns>A <see cref="System.Object"/></returns>
        public static object GetValue ( this FrameworkElement frameworkElement, string propertyNameToGet, out bool found )
        {
            var type = frameworkElement.GetType ();
            var propertyInfo = type.GetProperty (
                propertyNameToGet,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.Instance );
            if ( propertyInfo != null )
            {
                found = true;
                return propertyInfo.GetValue ( frameworkElement, null );
            }
            found = false;
            return null;
        }

        /// <summary>
        /// Determines whether the specified dependency object has behavior.
        /// </summary>
        /// <typeparam name="T">The type of behavior.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns><c>true</c> if the specified dependency object has behavior; otherwise, <c>false</c>.</returns>
        public static bool HasBehavior<T> ( this DependencyObject dependencyObject ) where T : Behavior
        {
            return GetBehaviors<T> ( dependencyObject ).Count () > 0;
        }

        /// <summary>
        /// Determines whether [is not readonly] [the specified framework element].
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        public static void IsNotReadonly ( this FrameworkElement frameworkElement )
        {
            frameworkElement.SetReadOnly ( false );
        }

        /// <summary>
        /// Determines whether the specified framework element is readonly.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        public static void IsReadonly ( this FrameworkElement frameworkElement )
        {
            frameworkElement.SetReadOnly ( true );
        }

        /// <summary>
        /// Overrides the behavior.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="justRemove">If set to <c>true</c> [just remove].</param>
        public static void OverrideBehavior ( this DependencyObject dependencyObject, Behavior behavior, bool justRemove = false )
        {
            var behaviors = Interaction.GetBehaviors ( dependencyObject );
            var sameType = behaviors.FirstOrDefault ( b => b.GetType () == behavior.GetType () );
            if ( sameType != null )
            {
                behaviors.Remove ( sameType );
            }
            if ( !justRemove )
            {
                behaviors.Add ( behavior );
            }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="propertyNameToSet">The property name to set.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool SetValue ( this FrameworkElement frameworkElement, string propertyNameToSet, object value )
        {
            var ret = false;
            var type = frameworkElement.GetType ();
            var propertyInfo = type.GetProperty (
                propertyNameToSet,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.SetProperty | BindingFlags.Instance );
            if ( propertyInfo != null )
            {
                propertyInfo.SetValue ( frameworkElement, value, null );
                ret = true;
            }
            return ret;
        }

        #endregion

        #region Methods

        private static void SetReadOnly ( this FrameworkElement frameworkElement, bool isReadOnly )
        {
            if ( frameworkElement is Border )
            {
                if ( ( frameworkElement as Border ).Child != null && ( frameworkElement as Border ).Child is FrameworkElement )
                {
                    var fe = ( ( frameworkElement as Border ).Child as FrameworkElement );
                    fe.SetReadOnly ( isReadOnly );
                }
            }
            else
            {
                if ( frameworkElement is IReadOnly )
                {
                    ( frameworkElement as IReadOnly ).IsReadOnly = isReadOnly;
                }
                else
                {
                    frameworkElement.SetValue ( "IsReadOnly", isReadOnly );
                }
            }
        }

        #endregion
    }
}
