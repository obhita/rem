using System;
using Microsoft.Practices.Unity;

namespace Rem.Ria.Infrastructure.Common.Extension
{
    /// <summary>
    /// Extension Methods for IUnityContainer  
    /// </summary>
    public static class UnityContainerExtensions
    {
        #region Public Methods

        /// <summary>
        /// Simplifies the registration of a type with the container.
        /// </summary>
        /// <typeparam name="T">The UnityContainer<see cref="IUnityContainer"/></typeparam>
        /// <param name="container">The container.</param>
        /// <returns>Returns the same calling instance</returns>
        public static IUnityContainer SimplyRegisterType<T> ( this IUnityContainer container )
            where T : class
        {
            Type type = typeof( T );
            container.RegisterType<object, T> ( type.Name );
            return container;
        }

        #endregion
    }
}
