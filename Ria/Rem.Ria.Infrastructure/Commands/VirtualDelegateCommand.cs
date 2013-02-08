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
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace Rem.Ria.Infrastructure.Commands
{
    /// <summary>
    /// Delegate command that exposes its ICommand interface as virtual methods.
    /// </summary>
    public class VirtualDelegateCommand : DelegateCommand, ICommand, IExecutionManager
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public VirtualDelegateCommand ( Action executeMethod )
            : base ( executeMethod )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand"/> class.
        /// </summary>
        /// <param name="executeMethod">The <see cref="T:System.Action"/> to invoke when <see cref="M:System.Windows.Input.ICommand.Execute(System.Object)"/> is called.</param>
        /// <param name="canExecuteMethod">The <see cref="T:System.Func`1"/> to invoke when <see cref="M:System.Windows.Input.ICommand.CanExecute(System.Object)"/> is called</param>
        public VirtualDelegateCommand ( Action executeMethod, Func<bool> canExecuteMethod )
            : base ( executeMethod, canExecuteMethod )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand"/> class.
        /// </summary>
        protected VirtualDelegateCommand ()
            : base ( () => { } )
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VirtualDelegateCommand"/> is executed.
        /// </summary>
        /// <value><c>true</c> if executed; otherwise, <c>false</c>.</value>
        public bool Executed { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true"/> if the command can execute,otherwise returns <see langword="false"/>.</returns>
        public new virtual bool CanExecute ()
        {
            return base.CanExecute ();
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public new virtual void Execute ()
        {
            base.Execute ();
            Executed = true;
        }

        #endregion

        #region Explicit Interface Methods

        bool ICommand.CanExecute ( object obj )
        {
            return CanExecute ();
        }

        void ICommand.Execute ( object obj )
        {
            Execute ();
        }

        #endregion
    }

    /// <summary>
    /// Delegate command that exposes its ICommand interface as virtual methods.
    /// </summary>
    /// <typeparam name="TParamter">The type of the paramter.</typeparam>
    [SuppressMessage ( "StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here." )]
    public class VirtualDelegateCommand<TParamter> : DelegateCommand<TParamter>, ICommand, IExecutionManager
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand&lt;TParamter&gt;"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        public VirtualDelegateCommand ( Action<TParamter> executeMethod )
            : base ( executeMethod )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand&lt;TParamter&gt;"/> class.
        /// </summary>
        /// <param name="executeMethod">The execute method.</param>
        /// <param name="canExecuteMethod">The can execute method.</param>
        public VirtualDelegateCommand ( Action<TParamter> executeMethod, Func<TParamter, bool> canExecuteMethod )
            : base ( executeMethod, canExecuteMethod )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualDelegateCommand&lt;TParamter&gt;"/> class.
        /// </summary>
        protected VirtualDelegateCommand ()
            : base ( o => { } )
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="VirtualDelegateCommand&lt;TParamter&gt;"/> is executed.
        /// </summary>
        /// <value><c>true</c> if executed; otherwise, <c>false</c>.</value>
        public bool Executed { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this instance can execute the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns><c>true</c> if this instance can execute the specified param; otherwise, <c>false</c>.</returns>
        public new virtual bool CanExecute ( TParamter param )
        {
            return base.CanExecute ( param );
        }

        /// <summary>
        /// Executes the specified param.
        /// </summary>
        /// <param name="param">The param.</param>
        public new virtual void Execute ( TParamter param )
        {
            base.Execute ( param );
            Executed = true;
        }

        #endregion

        #region Explicit Interface Methods

        bool ICommand.CanExecute ( object obj )
        {
            if ( obj == null || typeof( TParamter ).IsAssignableFrom ( obj.GetType () ) )
            {
                return CanExecute ( ( TParamter )obj );
            }
            return false;
        }

        void ICommand.Execute ( object obj )
        {
            Execute ( ( TParamter )obj );
        }

        #endregion
    }
}
