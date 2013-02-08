using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using Castle.DynamicProxy;
using Pillar.Common.Interceptors;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;

namespace Pillar.Common.Commands
{
    /// <summary>
    /// Factory for creating commands.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        #region Constants and Fields

        private readonly IEnumerable<IIntercept<ICommand>> _commandInterceptors;
        private readonly ProxyGenerator _proxyGenerator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFactory"/> class.
        /// </summary>
        /// <param name="container">The Pillar IoC container.</param>
        /// <param name="proxyGenerator">The proxy generator.</param>
        public CommandFactory ( IContainer container, ProxyGenerator proxyGenerator )
        {
            _commandInterceptors = container.ResolveAll<IIntercept<ICommand>> ();
            _proxyGenerator = proxyGenerator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Build command.</returns>
        public TCommand Build<TCommand, TOwner> (
            TOwner owner, Expression<Func<ICommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null )
            where TCommand : class, ICommand
        {
            var commandName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            var frameworkCommandInfo = new FrameworkCommandInfo ( owner, commandName );

            return canExecuteMethod != null
                       ? Build<TCommand, TOwner> (
                           frameworkCommandInfo, Activator.CreateInstance ( typeof( TCommand ), executeMethod, canExecuteMethod ) as TCommand )
                       : Build<TCommand, TOwner> (
                           frameworkCommandInfo, Activator.CreateInstance ( typeof( TCommand ), executeMethod ) as TCommand );
        }

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        public TCommand Build<TCommand, TOwner, TParameter> (
            TOwner owner,
            Expression<Func<ICommand>> propertyExpression,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null )
            where TCommand : class, ICommand
        {
            var commandName = PropertyUtil.ExtractPropertyName ( propertyExpression );

            var frameworkCommandInfo = new FrameworkCommandInfo ( owner, commandName );

            return canExecuteMethod != null
                       ? Build<TCommand, TOwner> (
                           frameworkCommandInfo, Activator.CreateInstance ( typeof( TCommand ), executeMethod, canExecuteMethod ) as TCommand )
                       : Build<TCommand, TOwner> (
                           frameworkCommandInfo, Activator.CreateInstance ( typeof( TCommand ), executeMethod ) as TCommand );
        }

        /// <summary>
        /// Builds the specified framework command info.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="frameworkCommandInfo">The framework command info.</param>
        /// <param name="commandInstance">The command instance.</param>
        /// <returns>Built command.</returns>
        public TCommand Build<TCommand, TOwner> ( IFrameworkCommandInfo frameworkCommandInfo, TCommand commandInstance )
            where TCommand : class, ICommand
        {
            var options = new ProxyGenerationOptions ();
            options.AddMixinInstance ( frameworkCommandInfo );

            foreach ( var interceptor in _commandInterceptors )
            {
                var mixin = interceptor.GetInterceptorOptions<TOwner> ( frameworkCommandInfo );
                if ( mixin != null )
                {
                    options.AddMixinInstance ( mixin );
                }
            }

            return _proxyGenerator.CreateClassProxyWithTarget ( commandInstance, options, _commandInterceptors.OfType<IInterceptor> ().ToArray () );
        }

        #endregion
    }
}
