using System;
using System.Linq.Expressions;
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Extension methods for <see cref="ICommandFactory"/>
    /// </summary>
    public static class CommandFactoryExtensions
    {
        /// <summary>
        /// Builds the specified command factory.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>
        /// The built command.
        /// </returns>
        public static TCommand Build<TCommand, TOwner>(this ICommandFactory commandFactory, 
            TOwner owner, 
            Expression<Func<ICommand>> comandPropertyExpression, 
            Expression<Func<object>> propertyExpressionForRulesToRun, 
            Action executeMethod,
            Func<bool> canExecuteMethod = null)
            where TCommand : class, ICommand
        {
            var commandName = PropertyUtil.ExtractPropertyName(comandPropertyExpression);
            var propertyNameForRule = PropertyUtil.ExtractPropertyName(propertyExpressionForRulesToRun);

            var ruleCommandInfo = new RuleCommandInfo(owner,
                commandName,
                new PropertyChainContainsMemberRuleSelector<object>(propertyNameForRule));

            return canExecuteMethod != null ?
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod, canExecuteMethod) as TCommand) :
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod) as TCommand);
        }

        /// <summary>
        /// Builds the specified command factory.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>
        /// The build command.
        /// </returns>
        public static TCommand Build<TCommand, TOwner, TParameter>(this ICommandFactory commandFactory,
            TOwner owner,
            Expression<Func<ICommand>> comandPropertyExpression,
            Expression<Func<object>> propertyExpressionForRulesToRun,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null)
            where TCommand : class, ICommand
        {
            var commandName = PropertyUtil.ExtractPropertyName(comandPropertyExpression);
            var propertyNameForRule = PropertyUtil.ExtractPropertyName(propertyExpressionForRulesToRun);

            var ruleCommandInfo = new RuleCommandInfo(owner, 
                commandName,  
                new PropertyChainContainsMemberRuleSelector<object>(propertyNameForRule));

            return canExecuteMethod != null ?
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod, canExecuteMethod) as TCommand) :
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod) as TCommand);
        }

        /// <summary>
        /// Builds the specified command factory.
        /// </summary>
        /// <typeparam name="TCommand">The type of the command.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="commandFactory">The command factory.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="memberNameFunc">The member name func.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>
        /// The build command.
        /// </returns>
        public static TCommand Build<TCommand, TOwner, TParameter>(this ICommandFactory commandFactory, 
            TOwner owner,
            Expression<Func<ICommand>> comandPropertyExpression,
            Func<TParameter, string> memberNameFunc,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null)
            where TCommand : class, ICommand
        {
            var commandName = PropertyUtil.ExtractPropertyName(comandPropertyExpression);

            var ruleCommandInfo = new RuleCommandInfo (
                owner, commandName, new PropertyChainContainsMemberRuleSelector<TParameter> ( memberNameFunc ) );

            return canExecuteMethod != null ?
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod, canExecuteMethod) as TCommand) :
                commandFactory.Build<TCommand, TOwner>(ruleCommandInfo, Activator.CreateInstance(typeof(TCommand), executeMethod) as TCommand);
        }
    }
}
