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
using Microsoft.Practices.Unity;
using Rem.Ria.Infrastructure.Context;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Base class for ViewModel
    /// </summary>
    public abstract class ViewModelBase : CustomNotificationObject
    {
        #region Constants and Fields

        private CurrentUserContext _currentUserContext;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [context changed].
        /// </summary>
        public event EventHandler ContextChanged = ( o, e ) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [apply context changes].
        /// </summary>
        /// <value><c>true</c> if [apply context changes]; otherwise, <c>false</c>.</value>
        public bool ApplyContextChanges { get; set; }

        /// <summary>
        /// Gets or sets the current user context.
        /// </summary>
        /// <value>The current user context.</value>
        public CurrentUserContext CurrentUserContext
        {
            get { return _currentUserContext; }
            protected set { ApplyPropertyChange ( ref _currentUserContext, () => CurrentUserContext, value ); }
        }

        /// <summary>
        /// Sets the current user context service.
        /// </summary>
        /// <value>The current user context service.</value>
        [Dependency]
        public ICurrentUserContextService CurrentUserContextService
        {
            set { value.RegisterForContext ( OnContextChanged ); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when [context changed].
        /// </summary>
        /// <param name="currentUserContext">The current user context.</param>
        /// <param name="firstCall">If set to <c>true</c> [first call].</param>
        public void OnContextChanged ( CurrentUserContext currentUserContext, bool firstCall )
        {
            if ( firstCall || ApplyContextChanges )
            {
                CurrentUserContext = currentUserContext;
            }
            ContextChanged ( this, new EventArgs () );
        }

        #endregion
    }
}
