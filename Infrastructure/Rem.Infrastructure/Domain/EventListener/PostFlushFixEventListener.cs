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
using NHibernate;
using NHibernate.Event;
using NHibernate.Event.Default;
using NLog;

namespace Rem.Infrastructure.Domain.EventListener
{
    /// <summary>
    /// This class along with <see cref="AutoFlushFixEventListener"/> is used to fix 'Collection was not processed by flush()' exception when using NHibernate event listeners.
    /// This fix has been proposed by allan.ritchie@gmail.com since 2009 until today 1/2012.
    /// See these links in the NHibernate Google discussion group:
    /// http://groups.google.com/group/nhusers/browse_thread/thread/1db7fd843b0b4b56
    /// http://groups.google.com/group/nhusers/browse_thread/thread/ddcffcb5cfe6ee59/1975ada07753356e
    /// http://groups.google.com/group/nhusers/browse_thread/thread/54a3c2e02e41f77b
    /// </summary>
    [Serializable]
    public class PostFlushFixEventListener : DefaultFlushEventListener
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Public Methods

        /// <summary>
        /// Called when [flush].
        /// </summary>
        /// <param name="event">The @event.</param>
        public override void OnFlush ( FlushEvent @event )
        {
            try
            {
                base.OnFlush ( @event );
            }
            catch ( AssertionFailure e )
            {
                // throw away 
                Logger.WarnException("AssertionFailure occurred in " + GetType().Name, e);
            }
        }

        #endregion
    }
}
