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
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using NLog;
using Rem.Ria.Infrastructure.Common.Extension;
using Rem.Ria.Infrastructure.View.CustomControls;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// ConfigurationBehaviorService class.
    /// </summary>
    public class ConfigurationBehaviorService
    {
        #region Constants and Fields

        private static readonly DependencyProperty HasBeenConfiguredProperty = DependencyProperty.RegisterAttached (
            "HasBeenConfigured", typeof( bool ), typeof( ConfigurationBehaviorService ), new PropertyMetadata ( false ) );

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger ();
        private readonly object _layoutUpdateSync = new object ();

        private readonly IDictionary<Type, DependencyProperty> _typeDependencyPropertyMap = new Dictionary<Type, DependencyProperty> ();
        private readonly IDictionary<Type, DependencyProperty> _typeTargetTypeMap = new Dictionary<Type, DependencyProperty> ();
        private FrameworkElement _curElement;

        private IDisposable _layoutSubsriber;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the ConfigurationBehaviorService class.
        /// Initializes core Type Dependency Maps and Type Target Maps
        /// </summary>
        public ConfigurationBehaviorService ()
        {
            //Core Controls
            _typeDependencyPropertyMap.Add ( typeof( TextBlock ), TextBlock.TextProperty );
            _typeDependencyPropertyMap.Add ( typeof( TextBox ), TextBox.TextProperty );
            _typeDependencyPropertyMap.Add ( typeof( ContentControl ), ContentControl.ContentProperty );
            _typeDependencyPropertyMap.Add ( typeof( DatePicker ), DatePicker.SelectedDateProperty );
            _typeDependencyPropertyMap.Add ( typeof( ToggleButton ), ToggleButton.IsCheckedProperty );
            _typeDependencyPropertyMap.Add ( typeof( ButtonBase ), ButtonBase.CommandProperty );
            _typeDependencyPropertyMap.Add ( typeof( ItemsControl ), ItemsControl.ItemsSourceProperty );
            _typeDependencyPropertyMap.Add ( typeof( Selector ), Selector.SelectedItemProperty );
            _typeDependencyPropertyMap.Add ( typeof( TimeSpanPicker ), TimeSpanPicker.TimeSpanProperty );

            //Custom Controls
            _typeDependencyPropertyMap.Add ( typeof( AddRemoveCommentControl ), AddRemoveCommentControl.TextProperty );
            _typeDependencyPropertyMap.Add ( typeof( BasicScreenQuestionControl ), BasicScreenQuestionControl.ValueProperty );
            _typeDependencyPropertyMap.Add ( typeof( NonResponseQuestionControl ), NonResponseQuestionControl.NonResponseDtoProperty );
            _typeDependencyPropertyMap.Add ( typeof( Editor ), FrameworkElement.DataContextProperty );
            _typeDependencyPropertyMap.Add ( typeof( EditableExpander ), EditableExpander.ContentProperty );
            _typeDependencyPropertyMap.Add ( typeof( QuestionItem ), QuestionItem.ValueProperty );

            _typeTargetTypeMap.Add ( typeof( Label ), Label.TargetProperty );
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a New Target Type to Dependency Property map or Replaces the Dependency Property of a Currently Mapped Type.
        /// </summary>
        /// <typeparam name="T">Type of Object.</typeparam>
        /// <param name="dp">Dependency Property to Map to Type that points to the Target.</param>
        public void RegisterTargetType<T> ( DependencyProperty dp ) where T : DependencyObject
        {
            RegisterTargetType ( typeof( T ), dp );
        }

        /// <summary>
        /// Adds a New Target Type to Dependency Property map or Replaces the Dependency Property of a Currently Mapped Type.
        /// </summary>
        /// <param name="type">Type of Object.</param>
        /// <param name="dp">Dependency Property to Map to Type that points to the Target.</param>
        /// <exception cref="ArgumentException">If type does not derive from DependencyObject.</exception>
        public void RegisterTargetType ( Type type, DependencyProperty dp )
        {
            if ( !typeof( DependencyObject ).IsAssignableFrom ( type ) )
            {
                throw new ArgumentException ( "Type must be a DependencyObject", "type" );
            }

            if ( _typeTargetTypeMap.ContainsKey ( type ) )
            {
                _typeTargetTypeMap[type] = dp;
            }
            else
            {
                _typeTargetTypeMap.Add ( type, dp );
            }
        }

        /// <summary>
        /// Adds a new Type to Dependency Property map or replaces the Dependency Property of a currently mapped type.
        /// </summary>
        /// <typeparam name="T">Type of object, must derive from DependencyObject.</typeparam>
        /// <param name="dp">DependencyProperty to map.</param>
        public void RegisterType<T> ( DependencyProperty dp ) where T : DependencyObject
        {
            RegisterType ( typeof( T ), dp );
        }

        /// <summary>
        /// Adds a new Type to Dependency Property map or replaces the Dependency Property of a currently mapped type.
        /// </summary>
        /// <param name="type">Type of object, must derive from DependencyObject.</param>
        /// <param name="dp">DependencyProperty to map.</param>
        /// <exception cref="ArgumentException">If type does not derive from DependencyObject.</exception>
        public void RegisterType ( Type type, DependencyProperty dp )
        {
            if ( !typeof( DependencyObject ).IsAssignableFrom ( type ) )
            {
                throw new ArgumentException ( "Type must be a DependencyObject", "type" );
            }

            if ( _typeDependencyPropertyMap.ContainsKey ( type ) )
            {
                _typeDependencyPropertyMap[type] = dp;
            }
            else
            {
                _typeDependencyPropertyMap.Add ( type, dp );
            }
        }

        /// <summary>
        /// Stops listening to Layout Changes of the Current Element Being Watched.
        /// </summary>
        public void Stop ()
        {
            if ( _layoutSubsriber != null )
            {
                _layoutSubsriber.Dispose ();
            }
            _curElement = null;
        }

        /// <summary>
        /// Removes the Type to Dependency Property Map
        /// </summary>
        /// <typeparam name="T">The Type to be removed from the mapping list.</typeparam>
        public void UnRegisterType<T> ()
        {
            UnRegisterType ( typeof( T ) );
        }

        /// <summary>
        /// Removes the Type to Dependency Property Map
        /// </summary>
        /// <param name="type">The Type to be removed from the mapping list.</param>
        public void UnRegisterType ( Type type )
        {
            _typeDependencyPropertyMap.Remove ( type );
        }

        /// <summary>
        /// Starts Listening to the elements LayoutUpdated Event in order to automatically apply Configuration.
        /// </summary>
        /// <param name="element">FrameworkElement to listen to.</param>
        public void Watch ( FrameworkElement element )
        {
            if ( _layoutSubsriber != null )
            {
                _layoutSubsriber.Dispose ();
            }

            var layoutUpdatedEvent =
                Observable.FromEventPattern<EventArgs> ( element, "LayoutUpdated" ).Sample ( TimeSpan.FromMilliseconds ( 700 ) );

            _layoutSubsriber = layoutUpdatedEvent.Subscribe ( LayoutUpdated );

            _curElement = element;
        }

        #endregion

        #region Methods

        private static IEnumerable<Type> GetBaseTypes ( Type target )
        {
            while ( target != null && target != typeof( UIElement ) )
            {
                yield return target;
                target = target.BaseType;
            }
        }

        private static bool GetHasBeenConfigured ( DependencyObject o )
        {
            return ( bool )o.GetValue ( HasBeenConfiguredProperty );
        }

        private static void SetHasBeenConfigured ( DependencyObject o, bool value )
        {
            o.SetValue ( HasBeenConfiguredProperty, value );
        }

        private void HandleConfiguration ( FrameworkElement frameworkElement )
        {
            var elementTypes = GetBaseTypes ( frameworkElement.GetType () );

            DependencyProperty dependencyProperty = null;
            foreach ( var elementType in elementTypes )
            {
                if ( _typeDependencyPropertyMap.ContainsKey ( elementType ) )
                {
                    dependencyProperty = _typeDependencyPropertyMap[elementType];
                }
            }

            if ( dependencyProperty != null )
            {
                frameworkElement.OverideConfigurationBehavior ( dependencyProperty );
            }
        }

        private void HandleTargetConfiguration ( FrameworkElement frameworkElement )
        {
            var targetElement = frameworkElement.GetValue ( _typeTargetTypeMap[frameworkElement.GetType ()] ) as FrameworkElement;

            if ( targetElement != null )
            {
                var elementTypes = GetBaseTypes ( targetElement.GetType () );

                DependencyProperty dependencyProperty = null;
                foreach ( var elementType in elementTypes )
                {
                    if ( _typeDependencyPropertyMap.ContainsKey ( elementType ) )
                    {
                        dependencyProperty = _typeDependencyPropertyMap[elementType];
                    }
                }

                if ( dependencyProperty != null )
                {
                    frameworkElement.OverideConfigurationBehavior ( targetElement, dependencyProperty );
                }
            }
        }

        private void LayoutUpdateWorker ()
        {
            lock ( _layoutUpdateSync )
            {
                var startTime = DateTime.Now;

                var children = new List<FrameworkElement> ();
                _curElement.FindVisualChildren ( ele => !Configuration.GetIHandleConfiguration ( ele ), ref children );

                foreach ( var child in children )
                {
                    if ( GetHasBeenConfigured ( child ) )
                    {
                        continue;
                    }

                    if ( _typeTargetTypeMap.ContainsKey ( child.GetType () ) )
                    {
                        HandleTargetConfiguration ( child );
                    }
                    else
                    {
                        HandleConfiguration ( child );
                    }

                    SetHasBeenConfigured ( child, true );
                }

                var ms = ( DateTime.Now - startTime ).TotalMilliseconds;
                Logger.Trace ( string.Format ( "LayoutUpdatedWorker took: {0} ms", ms ) );
            }
        }

        private void LayoutUpdated ( EventPattern<EventArgs> eventArgs )
        {
            _curElement.Dispatcher.BeginInvoke ( LayoutUpdateWorker );
        }

        #endregion
    }
}
