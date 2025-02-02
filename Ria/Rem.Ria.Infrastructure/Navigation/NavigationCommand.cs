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

namespace Rem.Ria.Infrastructure.Navigation
{
    /// <summary>
    /// NavigationCommand class.
    /// </summary>
    public class NavigationCommand : INavigationCommand
    {
        #region Constants and Fields

        private readonly Func<KeyValuePair<string, string>[], bool> _canNavigateTo;
        private readonly Action<KeyValuePair<string, string>[]> _navigateTo;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationCommand"/> class.
        /// </summary>
        /// <param name="navigateTo">The navigate to.</param>
        /// <param name="canNavigateTo">The can navigate to.</param>
        internal NavigationCommand (
            Action<KeyValuePair<string, string>[]> navigateTo, Func<KeyValuePair<string, string>[], bool> canNavigateTo = null )
        {
            _navigateTo = navigateTo;
            _canNavigateTo = canNavigateTo ?? ( p => true );
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether this instance [can navigate to] the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance [can navigate to] the specified parameters; otherwise, <c>false</c>.</returns>
        public bool CanNavigateTo ( KeyValuePair<string, string>[] parameters )
        {
            return _canNavigateTo ( parameters );
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public void NavigateTo ( KeyValuePair<string, string>[] parameters )
        {
            _navigateTo ( parameters );
        }

        #endregion
    }
}
