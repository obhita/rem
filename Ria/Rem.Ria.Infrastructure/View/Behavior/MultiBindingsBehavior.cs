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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for behaviing multi bindings.
    /// </summary>
    public class MultiBindingsBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        /// <summary>
        /// DataContextPiggyBack Attached Dependency Property, used as a mechanism for exposing
        /// DataContext changed events
        /// </summary>
        public static readonly DependencyProperty DataContextPiggyBackProperty =
            DependencyProperty.RegisterAttached (
                "DataContextPiggyBack",
                typeof( object ),
                typeof( MultiBindingsBehavior ),
                new PropertyMetadata ( null, OnDataContextPiggyBackChanged ) );

        /// <summary>
        /// Dependency Property for MultiBindingsProperty Property.
        /// </summary>
        public static readonly DependencyProperty MultiBindingsProperty =
            DependencyProperty.Register (
                "MultiBindings",
                typeof( ObservableCollection<MultiBinding> ),
                typeof( MultiBindingsBehavior ),
                new PropertyMetadata ( null, MultiBindingsChanged ) );

        /// <summary>
        /// Dependency Property for NameScopeRootProperty Property.
        /// </summary>
        public static readonly DependencyProperty NameScopeRootProperty =
            DependencyProperty.Register (
                "NameScopeRoot",
                typeof( FrameworkElement ),
                typeof( MultiBindingsBehavior ),
                new PropertyMetadata ( null, NameScopeRootChanged ) );

        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached (
                "Behavior",
                typeof( MultiBindingsBehavior ),
                typeof( MultiBindingsBehavior ),
                new PropertyMetadata ( null ) );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiBindingsBehavior"/> class.
        /// </summary>
        public MultiBindingsBehavior ()
        {
            MultiBindings = new ObservableCollection<MultiBinding> ();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the multi bindings.
        /// </summary>
        /// <value>The multi bindings.</value>
        public ObservableCollection<MultiBinding> MultiBindings
        {
            get { return ( ObservableCollection<MultiBinding> )GetValue ( MultiBindingsProperty ); }
            set { SetValue ( MultiBindingsProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name scope root.
        /// </summary>
        /// <value>The name scope root.</value>
        public FrameworkElement NameScopeRoot
        {
            get { return ( FrameworkElement )GetValue ( NameScopeRootProperty ); }
            set { SetValue ( NameScopeRootProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the data context piggy back.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <returns>A <see cref="System.Object"/></returns>
        public static object GetDataContextPiggyBack ( DependencyObject d )
        {
            return d.GetValue ( DataContextPiggyBackProperty );
        }

        /// <summary>
        /// Sets the data context piggy back.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="value">The value.</param>
        public static void SetDataContextPiggyBack ( DependencyObject d, object value )
        {
            d.SetValue ( DataContextPiggyBackProperty, value );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            // bind the target elements DataContext, to our DataContext Piggy Back property
            // this allows us to get property changed events when the targetElement
            // DataContext changes
            AssociatedObject.SetBinding ( DataContextPiggyBackProperty, new Binding () );
            SetBehavior ( AssociatedObject, this );
            foreach ( var multiBinding in MultiBindings )
            {
                multiBinding.DataContext = AssociatedObject.DataContext;
                multiBinding.Init ( AssociatedObject, NameScopeRoot );
            }
        }

        private static MultiBindingsBehavior GetBehavior ( DependencyObject d )
        {
            return ( MultiBindingsBehavior )d.GetValue ( BehaviorProperty );
        }

        private static void MultiBindingsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = d as MultiBindingsBehavior;
            if ( behavior != null )
            {
                var oldCollection = e.OldValue as ObservableCollection<MultiBinding>;
                if ( oldCollection != null )
                {
                    oldCollection.CollectionChanged -= behavior.MultiBindingsCollectionChanged;
                }
                var newCollection = e.NewValue as ObservableCollection<MultiBinding>;
                if ( newCollection != null )
                {
                    newCollection.CollectionChanged += behavior.MultiBindingsCollectionChanged;
                }
            }
        }

        private static void NameScopeRootChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = d as MultiBindingsBehavior;
            if ( behavior != null )
            {
                foreach ( var multiBinding in behavior.MultiBindings )
                {
                    multiBinding.UpdateNameScope ( behavior.NameScopeRoot );
                }
            }
        }

        private static void OnDataContextPiggyBackChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var behavior = GetBehavior ( d );
            if ( behavior != null )
            {
                foreach ( var multiBinding in behavior.MultiBindings )
                {
                    multiBinding.DataContext = behavior.AssociatedObject.DataContext;
                    multiBinding.TryUpdateBinding ( true );
                }
            }
        }

        private static void SetBehavior ( DependencyObject d, MultiBindingsBehavior value )
        {
            d.SetValue ( BehaviorProperty, value );
        }

        private void MultiBindingsCollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            if ( AssociatedObject != null )
            {
                foreach ( MultiBinding multiBinding in e.NewItems )
                {
                    multiBinding.Init ( AssociatedObject, NameScopeRoot );
                }
            }
        }

        #endregion
    }
}
