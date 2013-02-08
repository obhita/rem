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

using System.Windows;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View
{
    /// <summary>
    /// Proxy for binding setter.
    /// </summary>
    public class BindingSetterProxy : FrameworkElement
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for TargetProperty Property.
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register ( "Target", typeof( object ), typeof( BindingSetterProxy ), new PropertyMetadata ( null, TargetChanged ) );

        /// <summary>
        /// Dependency Property for ValueProperty Property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register ( "Value", typeof( object ), typeof( BindingSetterProxy ), new PropertyMetadata ( null, ValueChanged ) );

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public object Target
        {
            get { return GetValue ( TargetProperty ); }
            set { SetValue ( TargetProperty, value ); }
        }

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

        private static void TargetChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var bsp = d as BindingSetterProxy;
            if ( bsp != null )
            {
                var b = bsp.GetBindingExpression ( TargetProperty );
                bsp.Target = bsp.Value;
                if ( b != null )
                {
                    b.UpdateSource ();
                }
            }
        }

        private static void ValueChanged ( DependencyObject d, DependencyPropertyChangedEventArgs e )
        {
            var bsp = d as BindingSetterProxy;
            if ( bsp != null )
            {
                bsp.Target = bsp.Value;
            }
        }

        #endregion
    }
}
