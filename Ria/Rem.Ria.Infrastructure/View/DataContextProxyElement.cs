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
using System.Windows;
using System.Windows.Data;

namespace Rem.Ria.Infrastructure.View
{
    /// <summary>
    /// DataContextProxyElement class.
    /// </summary>
    public class DataContextProxyElement : FrameworkElement
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for DataSourceProperty Property.
        /// </summary>
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register ( "DataSource", typeof( object ), typeof( DataContextProxyElement ), null );

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextProxyElement"/> class.
        /// </summary>
        public DataContextProxyElement ()
        {
            Loaded += DataContextProxy_Loaded;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the binding mode.
        /// </summary>
        /// <value>The binding mode.</value>
        public BindingMode BindingMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the binding property.
        /// </summary>
        /// <value>The name of the binding property.</value>
        public string BindingPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public object DataSource
        {
            get { return GetValue ( DataSourceProperty ); }
            set { SetValue ( DataSourceProperty, value ); }
        }

        #endregion

        #region Methods

        private void DataContextProxy_Loaded ( object sender, RoutedEventArgs e )
        {
            var binding = new Binding ();
            if ( !string.IsNullOrEmpty ( BindingPropertyName ) )
            {
                binding.Path = new PropertyPath ( BindingPropertyName );
            }
            binding.Source = DataContext;
            binding.Mode = BindingMode;
            SetBinding ( DataSourceProperty, binding );
        }

        #endregion
    }
}
