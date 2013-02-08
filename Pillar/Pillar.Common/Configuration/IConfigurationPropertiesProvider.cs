namespace Pillar.Common.Configuration
{
    /// <summary>
    /// Interface for providing configuration properties.
    /// </summary>
    public interface IConfigurationPropertiesProvider
    {
        #region Public Methods

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A <see cref="System.String"/></returns>
        string GetProperty ( string propertyName );

        /// <summary>
        /// Gets the property as an int.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A <see cref="System.Int32"/></returns>
        int GetPropertyInt ( string propertyName );

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        void SetProperty ( string propertyName, string propertyValue );

        #endregion
    }
}
