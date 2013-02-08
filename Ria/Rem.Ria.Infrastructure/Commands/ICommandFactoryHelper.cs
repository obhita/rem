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
using System.Linq.Expressions;
using System.Windows.Input;

namespace Rem.Ria.Infrastructure.Commands
{
    /// <summary>
    /// Interface for command factory helper/&gt;
    /// </summary>
    public interface ICommandFactoryHelper
    {
        #region Public Methods

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        VirtualDelegateCommand BuildDelegateCommand (
            Expression<Func<ICommand>> comandPropertyExpression,
            Expression<Func<object>> propertyExpressionForRulesToRun,
            Action executeMethod,
            Func<bool> canExecuteMethod = null );

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="propertyExpressionForRulesToRun">The property expression for rules to run.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> comandPropertyExpression,
            Expression<Func<object>> propertyExpressionForRulesToRun,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null );

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="comandPropertyExpression">The comand property expression.</param>
        /// <param name="memberNameFunc">The member name func.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Built command.</returns>
        VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> comandPropertyExpression,
            Func<TParameter, string> memberNameFunc,
            Action<TParameter> executeMethod,
            Func<TParameter, bool> canExecuteMethod = null );

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Build command.</returns>
        VirtualDelegateCommand BuildDelegateCommand (
            Expression<Func<ICommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null );

        /// <summary>
        /// Builds the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the parameter.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        /// <returns>Build command.</returns>
        VirtualDelegateCommand<TParameter> BuildDelegateCommand<TParameter> (
            Expression<Func<ICommand>> propertyExpression, Action<TParameter> executeMethod, Func<TParameter, bool> canExecuteMethod = null );

        #endregion
    }
}
