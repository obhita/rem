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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Windows.Input;
using Pillar.Common.Commands;
using Pillar.FluentRuleEngine;

namespace Rem.Ria.Infrastructure.Commands
{
    /// <summary>
    /// Static Class for creating <see cref="CommandFactoryHelper{TOwner}"/>
    /// </summary>
    public static class CommandFactoryHelper
    {
        #region Public Methods

        /// <summary>
        /// Creates the helper.
        /// </summary>
        /// <typeparam name="T">Type of object creating Helper for.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="commandFactory">The command factory.</param>
        /// <returns>A <see cref="CommandFactoryHelper{TOwner}"/></returns>
        public static CommandFactoryHelper<T> CreateHelper<T> ( T owner, ICommandFactory commandFactory )
        {
            return new CommandFactoryHelper<T> ( owner, commandFactory );
        }

        #endregion
    }

    /// <summary>
    /// Helper for using <see cref="Pillar.Common.Commands.ICommandFactory"/>
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    [SuppressMessage ( "StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here." )]
    public class CommandFactoryHelper<TOwner> : ICommandFactoryHelper
    {
        #region Constants and Fields

        private readonly ICommandFactory _commandFactory;
        private readonly TOwner _owner;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFactoryHelper&lt;TOwner&gt;"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="commandFactory">The command factory.</param>
        public CommandFactoryHelper ( TOwner owner, ICommandFactory commandFactory )
        {
            _owner = owner;
            _commandFactory = commandFactory;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the command factory.
        /// </summary>
        public ICommandFactory CommandFactory
        {
            get { return _commandFactory; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Build command.</returns>
        public VirtualDelegateCommand BuildDelegateCommand (
            Expression<Func<ICommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null )
        {
            return _commandFactory.Build<VirtualDelegateCommand, TOwner> ( _owner, propertyExpression, executeMethod, canExecuteMethod );
        }

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Build command.</returns>
        public VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> propertyExpression, Action<TParameter> executeMethod, Func<TParameter, bool> canExecuteMethod = null )
        {
            return _commandFactory.Build<VirtualDelegateCommand<TParameter>, TOwner, TParameter> (
                _owner, propertyExpression, executeMethod, canExecuteMethod );
        }

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="memberNameFunc">The member name func.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        public VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> comandPropertyExpression,
            Func<TParameter, string> memberNameFunc,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null )
        {
            return _commandFactory.Build<VirtualDelegateCommand<TParameter>, TOwner, TParameter> (
                _owner, comandPropertyExpression, memberNameFunc, executeMethod, canExecuteMethod );
        }

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        public VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> comandPropertyExpression,
            Expression<Func<object>> propertyExpressionForRulesToRun,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null )
        {
            return _commandFactory.Build<VirtualDelegateCommand<TParameter>, TOwner, TParameter> (
                _owner, comandPropertyExpression, propertyExpressionForRulesToRun, executeMethod, canExecuteMethod );
        }

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        public VirtualDelegateCommand BuildDelegateCommand (
            Expression<Func<ICommand>> comandPropertyExpression,
            Expression<Func<object>> propertyExpressionForRulesToRun,
            Action executeMethod,
            Func<bool> canExecuteMethod = null )
        {
            return _commandFactory.Build<VirtualDelegateCommand, TOwner> (
                _owner, comandPropertyExpression, propertyExpressionForRulesToRun, executeMethod, canExecuteMethod );
        }

        #endregion
    }
}
