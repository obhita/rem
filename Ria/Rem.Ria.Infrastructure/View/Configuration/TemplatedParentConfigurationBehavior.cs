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
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using Rem.Ria.Infrastructure.Common.Extension;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Class for behaviing templated parent configuration.
    /// </summary>
    public class TemplatedParentConfigurationBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        private static readonly DependencyProperty DataContextProxyProperty =
            DependencyProperty.Register (
                "DataContextProxy",
                typeof( object ),
                typeof( TemplatedParentConfigurationBehavior ),
                new PropertyMetadata ( DataContextProxyChanged ) );

        private bool _applied;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatedParentConfigurationBehavior"/> class.
        /// </summary>
        public TemplatedParentConfigurationBehavior ()
        {
            TemplateParentLevel = 1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the property for metadata.
        /// </summary>
        /// <value>The property for metadata.</value>
        public string PropertyForMetadata { get; set; }

        /// <summary>
        /// Gets or sets the template parent level.
        /// </summary>
        /// <value>The template parent level.</value>
        public int TemplateParentLevel { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            var binding = new Binding ();
            BindingOperations.SetBinding ( this, DataContextProxyProperty, binding );
        }

        private static void DataContextProxyChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var templatedParentConfigurationBehavior = d as TemplatedParentConfigurationBehavior;
            if ( templatedParentConfigurationBehavior != null )
            {
                templatedParentConfigurationBehavior.CreateConfigurationBehavior ();
            }
        }

        private void CreateConfigurationBehavior ()
        {
            if ( !string.IsNullOrWhiteSpace ( PropertyForMetadata ) && !_applied )
            {
                var element = AssociatedObject;
                for ( var i = 0; i < TemplateParentLevel; i++ )
                {
                    if ( element == null )
                    {
                        break;
                    }
                    element = element.GetTemplatedParent () as FrameworkElement;
                }
                if ( element != null )
                {
                    var dependencyPropertyName = PropertyForMetadata + "Property";
                    var type = element.GetType ();
                    var field = type.GetField (
                        dependencyPropertyName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy );
                    if ( field != null )
                    {
                        var dp = ( DependencyProperty )field.GetValue ( element );
                        if ( dp != null )
                        {
                            var bindingExpression = element.GetBindingExpression ( dp );
                            if ( bindingExpression != null )
                            {
                                _applied = true;
                                AssociatedObject.OverrideBehavior (
                                    new ConfigurationBehavior
                                        {
                                            Metadata = bindingExpression.ParentBinding.Path.Path, DataContextProxy = element.DataContext
                                        } );
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
