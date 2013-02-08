using Pillar.Common.Commands;

namespace Pillar.Common.Interceptors
{
    /// <summary>
    /// Marker Interface for interceptor of a certain type.
    /// </summary>
    /// <typeparam name="T">Type of object I Intercept.</typeparam>
    public interface IIntercept<out T>
    {
        /// <summary>
        /// Gets the interceptor options.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="frameworkCommandInfo">The framework command info.</param>
        /// <returns>Object that contains options for interceptor or <c>Null</c> if has no options.</returns>
        object GetInterceptorOptions<TOwner> ( IFrameworkCommandInfo frameworkCommandInfo );
    }
}
