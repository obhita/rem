using System.Windows.Input;
using Castle.DynamicProxy;
using Pillar.Common.Commands;
using Pillar.Common.Interceptors;
using Pillar.Security.AccessControl;

namespace Pillar.Security
{
    /// <summary>
    /// Interceptor that checks for security access when attempting to Execute or CanExectute a rule.
    /// </summary>
    public class CheckAccessInterceptor : IIntercept<ICommand>, IInterceptor
    {
        private readonly IAccessControlManager _accessControlManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckAccessInterceptor"/> class.
        /// </summary>
        /// <param name="accessControlManager">The access control manager.</param>
        public CheckAccessInterceptor(IAccessControlManager accessControlManager)
        {
            _accessControlManager = accessControlManager;
        }

        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        public void Intercept ( IInvocation invocation )
        {
            bool canExecute;
            if((invocation.Method.Name == "Execute" || invocation.Method.Name == "CanExecute") && invocation.Proxy is IFrameworkCommandInfo)
            {
                var frameworkCommandInfo = invocation.Proxy as IFrameworkCommandInfo;
                var resourceRequest = new ResourceRequest { frameworkCommandInfo.Owner.GetType ().FullName, frameworkCommandInfo.Name };
                canExecute = _accessControlManager.CanAccess ( resourceRequest );
            }
            else
            {
                canExecute = true;
            }
            if(canExecute)
            {
                invocation.Proceed ();
            }
            else if (invocation.Method.Name == "CanExecute")
            {
                invocation.ReturnValue = false;
            }
        }

        /// <summary>
        /// Gets the interceptor options.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="frameworkCommandInfo">The framework command info.</param>
        /// <returns>Null because has no options.</returns>
        public object GetInterceptorOptions<TOwner> ( IFrameworkCommandInfo frameworkCommandInfo )
        {
            return null;
        }
    }
}
