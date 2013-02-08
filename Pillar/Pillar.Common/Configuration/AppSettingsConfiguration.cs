using System;
using System.Configuration;

namespace Pillar.Common.Configuration
{
    /// <summary>
    /// Implementation of the IConfigurationPropertiesProvider that uses the
    /// (web.config-based) .NET ConfigurationManager AppSettings as its store.
    /// </summary>
    public class AppSettingsConfiguration : IConfigurationPropertiesProvider
    {
        #region Public Methods

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public string GetProperty ( string propertyName )
        {
            if ( propertyName == null )
            {
                throw new ArgumentNullException ( "propertyName" );
            }

            // TODO: WebConfigurationManager should used instead.
            var appSetting = ConfigurationManager.AppSettings[propertyName];

            return appSetting;
        }

        /// <summary>
        /// Gets the property as an int.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A <see cref="System.Int32"/></returns>
        public int  GetPropertyInt ( string propertyName )
        {
            var propertyValue = GetProperty ( propertyName );
            var iValue = 0;
            try
            {
                iValue = int.Parse ( propertyValue );
            }
            catch ( FormatException fe )
            {
                throw new FormatException (
                    propertyName +
                    " in the system configuration is an invalid int format.",
                    fe );
            }
            catch(ArgumentNullException argumentNullException)
            {
                throw new ArgumentNullException(
                   propertyName +
                   " in the system configuration is empty or does not exist",
                   argumentNullException);
            }
            return iValue;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        public void SetProperty ( string propertyName, string propertyValue )
        {
            if ( propertyName == null )
            {
                throw new ArgumentNullException ( "propertyName" );
            }

            if ( propertyValue == null )
            {
                throw new ArgumentNullException ( "propertyValue" );
            }

            // TODO: WebConfigurationManager should used instead.
            ConfigurationManager.AppSettings.Set ( propertyName, propertyValue );
        }

        #endregion
    }
}
