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

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Configuration class.
    /// </summary>
    public static class Configuration
    {
        #region Constants and Fields

        /// <summary>
        /// Attached Property used to inform the Configuration Service to not recurse any further down the Tree for the attached DependencyObject.
        /// </summary>
        public static readonly DependencyProperty IHandleConfigurationProperty = DependencyProperty.RegisterAttached (
            "IHandleConfiguration", typeof( bool ), typeof( ConfigurationBehaviorService ), new PropertyMetadata ( false ) );

        /// <summary>
        /// Attached Property used to Overide the MetaData Value used by the ConfigurationBehavior.
        /// </summary>
        public static readonly DependencyProperty MetaDataOverideProperty = DependencyProperty.RegisterAttached (
            "MetaDataOveride", typeof( string ), typeof( Configuration ), new PropertyMetadata ( null ) );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the IHandleConfigurationValue of the Attached Property
        /// </summary>
        /// <param name="o">DependencyObject to get the value for.</param>
        /// <returns>Bool value indicating whether this Dependency Object handles its own Configuration.</returns>
        public static bool GetIHandleConfiguration ( DependencyObject o )
        {
            return ( bool )o.GetValue ( IHandleConfigurationProperty );
        }

        /// <summary>
        /// Gets the MetaDataOveride value of the Attached Property.
        /// </summary>
        /// <param name="o">DependencyObject to get the value for.</param>
        /// <returns>String value indicating an overide of the Metadata used by the Configuration Behavior.</returns>
        public static string GetMetaDataOveride ( DependencyObject o )
        {
            return ( string )o.GetValue ( MetaDataOverideProperty );
        }

        /// <summary>
        /// Sets the IHandleConfigurationValue of the Attached Property
        /// </summary>
        /// <param name="o">DependencyObject to set the value for.</param>
        /// <param name="value">Bool value to set.</param>
        public static void SetIHandleConfiguration ( DependencyObject o, bool value )
        {
            o.SetValue ( IHandleConfigurationProperty, value );
        }

        /// <summary>
        /// Gets the MetaDataOveride value of the Attached Property.
        /// </summary>
        /// <param name="o">DependencyObject to get the value for.</param>
        /// <param name="value">String value to set.</param>
        public static void SetMetaDataOveride ( DependencyObject o, string value )
        {
            o.SetValue ( MetaDataOverideProperty, value );
        }

        #endregion
    }
}
