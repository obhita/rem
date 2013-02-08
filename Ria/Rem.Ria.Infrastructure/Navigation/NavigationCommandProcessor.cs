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
using System.Collections.Generic;
using Microsoft.Practices.Prism.Regions;
using Pillar.Common.Utility;

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// Class for processing navigation command.
    /// </summary>
    public class NavigationCommandProcessor
    {
        #region Constants and Fields

        private readonly IDictionary<string, Action<NavigationContext>> _commandHandlers = new Dictionary<string, Action<NavigationContext>> ();
        private readonly string _commandNameParameter;
        private Action<NavigationContext> _defaultHandler;
        private bool _throwExceptionIfDefault;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationCommandProcessor"/> class.
        /// </summary>
        /// <param name="commandNameParameter">The command name parameter.</param>
        public NavigationCommandProcessor ( string commandNameParameter = "CommandName" )
        {
            _commandNameParameter = commandNameParameter;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the command handler.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Navigation.NavigationCommandProcessor"/></returns>
        public NavigationCommandProcessor AddCommandHandler ( string commandName, Action<NavigationContext> handler )
        {
            Check.IsNotNullOrWhitespace ( commandName, "Command Name is Required." );
            Check.IsNotNull ( handler, "Handler cannot be null." );

            if ( _commandHandlers.ContainsKey ( commandName ) )
            {
                _commandHandlers[commandName] = handler;
            }
            else
            {
                _commandHandlers.Add ( commandName, handler );
            }
            return this;
        }

        /// <summary>
        /// Adds the default command handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Navigation.NavigationCommandProcessor"/></returns>
        public NavigationCommandProcessor AddDefaultCommandHandler ( Action<NavigationContext> handler )
        {
            Check.IsNotNull ( handler, "Handler cannot be null." );

            _defaultHandler = handler;
            return this;
        }

        /// <summary>
        /// Processes the specified navigation context.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void Process ( NavigationContext navigationContext )
        {
            Check.IsNotNull ( navigationContext, "Navigation Context cannot be null." );

            var commandName = navigationContext.TryGetNavigationParameter<string> ( _commandNameParameter );
            if ( _commandHandlers.ContainsKey ( commandName ) )
            {
                _commandHandlers[commandName] ( navigationContext );
            }
            else
            {
                if ( _defaultHandler != null )
                {
                    _defaultHandler ( navigationContext );
                }
                if ( _throwExceptionIfDefault )
                {
                    throw new ArgumentException ( "Invalid Command Name:" + commandName );
                }
            }
        }

        /// <summary>
        /// Throws the exception if default.
        /// </summary>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.Navigation.NavigationCommandProcessor"/></returns>
        public NavigationCommandProcessor ThrowExceptionIfDefault ()
        {
            _throwExceptionIfDefault = true;
            return this;
        }

        #endregion
    }
}
