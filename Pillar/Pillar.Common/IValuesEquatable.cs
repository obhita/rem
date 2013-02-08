namespace Pillar.Common
{
    /// <summary>
    /// Interface for object that is values equatable.
    /// </summary>
    public interface IValuesEquatable
    {
        #region Public Methods

        /// <summary>
        /// Checks if all the values of the object are equal.
        /// </summary>
        /// <param name="obj">The object to check equality with.</param>
        /// <returns>A bool indicating whether objects are equal.</returns>
        bool ValuesEqual ( object obj );

        #endregion
    }
}
