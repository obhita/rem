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

using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// MultiBinding class.
    /// </summary>
    [ContentProperty ( "Bindings" )]
    public class MultiBinding : Panel
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for BindingsProperty Property.
        /// </summary>
        public static readonly DependencyProperty BindingsProperty =
            DependencyProperty.Register (
                "Bindings",
                typeof( BindingCollection ),
                typeof( MultiBinding ),
                new PropertyMetadata ( null, BindingsChanged ) );

        /// <summary>
        /// Dependency Property for ConverterParameterProperty Property.
        /// </summary>
        public static readonly DependencyProperty ConverterParameterProperty =
            DependencyProperty.Register (
                "ConverterParameter",
                typeof( object ),
                typeof( MultiBinding ),
                new PropertyMetadata ( null, ConverterParameterChanged ) );

        /// <summary>
        /// Dependency Property for ValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register (
                "Value",
                typeof( object ),
                typeof( MultiBinding ),
                new PropertyMetadata ( null ) );

        private int _curBindingCount;
        private bool _isInitialized;
        private FrameworkElement _nameScope;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiBinding"/> class.
        /// </summary>
        public MultiBinding ()
        {
            Bindings = new BindingCollection ();
            _curBindingCount = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the bindings.
        /// </summary>
        /// <value>The bindings.</value>
        public BindingCollection Bindings
        {
            get { return ( BindingCollection )GetValue ( BindingsProperty ); }
            set { SetValue ( BindingsProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>The converter.</value>
        public IMultiValueConverter Converter { get; set; }

        /// <summary>
        /// Gets or sets the converter parameter.
        /// </summary>
        /// <value>The converter parameter.</value>
        public object ConverterParameter
        {
            get { return GetValue ( ConverterParameterProperty ); }
            set { SetValue ( ConverterParameterProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the string format.
        /// </summary>
        /// <value>The string format.</value>
        public string StringFormat { get; set; }

        /// <summary>
        /// Gets or sets the target property.
        /// </summary>
        /// <value>The target property.</value>
        [TypeConverter ( typeof( PropertyPathConverter ) )]
        public PropertyPath TargetProperty { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return GetValue ( ValueProperty ); }
            set { SetValue ( ValueProperty, value ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the bindings.
        /// </summary>
        /// <param name="bindings">The bindings.</param>
        public void CreateBindings ( IList bindings )
        {
            if ( bindings != null )
            {
                foreach ( Binding binding in bindings )
                {
                    var slave = new BindingSlave ( this );
                    Children.Add ( slave );
                    if ( _nameScope != null && !string.IsNullOrEmpty ( binding.ElementName ) )
                    {
                        var element = _nameScope.FindName ( binding.ElementName );
                        binding.ElementName = null;
                        binding.Source = element;
                    }
                    binding.TargetNullValue = null;
                    binding.FallbackValue = null;
                    slave.SetBinding ( BindingSlave.ValueProperty, binding );
                }
            }
        }

        /// <summary>
        /// Inits the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="nameScope">The name scope.</param>
        public void Init ( FrameworkElement target, FrameworkElement nameScope )
        {
            if ( target != null )
            {
                var parentBind = new Binding ();
                parentBind.Source = target;
                parentBind.Path = TargetProperty;
                parentBind.Mode = BindingMode.TwoWay;
                var value = GetValue ( ValueProperty );
                SetBinding ( ValueProperty, parentBind );
                SetValue ( ValueProperty, value );
                _isInitialized = true;
                UpdateNameScope ( nameScope );
            }
        }

        /// <summary>
        /// Updates the name scope.
        /// </summary>
        /// <param name="nameScope">The name scope.</param>
        public void UpdateNameScope ( FrameworkElement nameScope )
        {
            if ( _isInitialized )
            {
                _nameScope = nameScope;
                foreach ( BindingSlave slave in Children )
                {
                    var bindingExpression = slave.GetBindingExpression ( BindingSlave.ValueProperty );
                    if ( bindingExpression != null )
                    {
                        var oldbinding = bindingExpression.ParentBinding;
                        if ( _nameScope != null && !string.IsNullOrEmpty ( oldbinding.ElementName ) )
                        {
                            var element = _nameScope.FindName ( oldbinding.ElementName );
                            if ( element != null )
                            {
                                var binding = new Binding
                                    {
                                        BindsDirectlyToSource = oldbinding.BindsDirectlyToSource,
                                        Converter = oldbinding.Converter,
                                        ConverterCulture = oldbinding.ConverterCulture,
                                        ConverterParameter = oldbinding.ConverterParameter,
                                        FallbackValue = oldbinding.FallbackValue,
                                        Mode = oldbinding.Mode,
                                        NotifyOnValidationError = oldbinding.NotifyOnValidationError,
                                        Path = oldbinding.Path,
                                        StringFormat = oldbinding.StringFormat,
                                        TargetNullValue = oldbinding.TargetNullValue,
                                        UpdateSourceTrigger = oldbinding.UpdateSourceTrigger,
                                        ValidatesOnDataErrors = oldbinding.ValidatesOnDataErrors,
                                        ValidatesOnExceptions = oldbinding.ValidatesOnExceptions,
                                        ValidatesOnNotifyDataErrors = oldbinding.ValidatesOnNotifyDataErrors
                                    };
                                binding.Source = element;
                                slave.ClearValue ( BindingSlave.ValueProperty );
                                slave.SetBinding ( BindingSlave.ValueProperty, binding );
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries the update binding.
        /// </summary>
        /// <param name="force">If set to <c>true</c> [force].</param>
        internal void TryUpdateBinding ( bool force = false )
        {
            if ( _curBindingCount < Children.Count )
            {
                _curBindingCount++;
            }
            UpdateBinding ( force );
        }

        private static void BindingsChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var multiBinding = d as MultiBinding;
            if ( multiBinding != null )
            {
                var oldCollection = e.OldValue as BindingCollection;
                if ( oldCollection != null )
                {
                    oldCollection.CollectionChanged -= multiBinding.BindingsCollectionChanged;
                }
                var newCollection = e.NewValue as BindingCollection;
                if ( newCollection != null )
                {
                    newCollection.CollectionChanged += multiBinding.BindingsCollectionChanged;
                    multiBinding.CreateBindings ( newCollection );
                }
            }
        }

        private static void ConverterParameterChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var multiBinding = d as MultiBinding;
            if ( multiBinding != null )
            {
                if ( multiBinding.Children.Count > 0 )
                {
                    multiBinding.UpdateBinding ();
                }
            }
        }

        private void BindingsCollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            CreateBindings ( e.NewItems );
        }

        private void SlaveValueChanged ( BindingSlave slave )
        {
            TryUpdateBinding ();
        }

        private void UpdateBinding ( bool force = false )
        {
            if ( _curBindingCount == Children.Count || force )
            {
                object value = null;
                var values = Children.OfType<BindingSlave> ().Select ( s => s.Value ).ToArray ();
                if ( Converter != null )
                {
                    value = Converter.Convert ( values, typeof( object ), ConverterParameter, CultureInfo.CurrentUICulture );
                }
                if ( !string.IsNullOrWhiteSpace ( StringFormat ) )
                {
                    value = Converter == null
                                ? string.Format ( StringFormat, values )
                                : string.Format ( StringFormat, value );
                }
                SetValue ( ValueProperty, value );
            }
        }

        #endregion

        /// <summary>
        /// BindingSlave class.
        /// </summary>
        public class BindingSlave : FrameworkElement
        {
            #region Constants and Fields

            /// <summary>
            /// Dependency Property for ValueProperty Property.
            /// </summary>
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register (
                    "Value",
                    typeof( object ),
                    typeof( BindingSlave ),
                    new PropertyMetadata ( ValueChanged ) );

            private readonly MultiBinding _multiBinding;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="BindingSlave"/> class.
            /// </summary>
            /// <param name="multiBinding">The multi binding.</param>
            public BindingSlave ( MultiBinding multiBinding )
            {
                _multiBinding = multiBinding;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public object Value
            {
                get { return GetValue ( ValueProperty ); }
                set { SetValue ( ValueProperty, value ); }
            }

            #endregion

            #region Methods

            private static void ValueChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
            {
                var bindingSlave = d as BindingSlave;
                if ( bindingSlave != null )
                {
                    bindingSlave._multiBinding.SlaveValueChanged ( bindingSlave );
                }
            }

            #endregion
        }
    }
}
