using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace Pillar.Common.Commands
{
    /// <summary>
    /// Interface for factory that builds commands.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Builds the specified command type.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>
        /// The <see cref="ICommand"/> that was built.
        /// </returns>
        TCommand Build<TCommand, TOwner>(TOwner owner, Expression<Func<ICommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null)
            where TCommand : class, ICommand;

        /// <summary>
        /// Builds the specified command type.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>
        /// The <see cref="ICommand"/> that was built.
        /// </returns>
        TCommand Build<TCommand, TOwner, TParameter>(
            TOwner owner,
            Expression<Func<ICommand>> propertyExpression,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null)
            where TCommand : class, ICommand;

        /// <summary>
        /// Builds the specified framework command info.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="frameworkCommandInfo">The framework command info.</param>
        /// <param name="commandInstance">The command instance.</param>
        /// <returns>
        /// Built command.
        /// </returns>
        TCommand Build<TCommand, TOwner>(IFrameworkCommandInfo frameworkCommandInfo, TCommand commandInstance)
            where TCommand : class, ICommand;
    }
}
