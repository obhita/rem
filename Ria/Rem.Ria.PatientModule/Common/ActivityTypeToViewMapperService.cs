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

namespace Rem.Ria.PatientModule.Common
{
    /// <summary>
    /// ActivityTypeToViewMapperService class.
    /// </summary>
    public class ActivityTypeToViewMapperService : IActivityTypeToViewMapperService
    {
        #region Constants and Fields

        private readonly Dictionary<string, Type> _wellKnownNameViewDictionary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityTypeToViewMapperService"/> class.
        /// </summary>
        public ActivityTypeToViewMapperService ()
        {
            _wellKnownNameViewDictionary = new Dictionary<string, Type> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the type of the view name from activity.
        /// </summary>
        /// <param name="activityTypeWellKnownName">Name of the activity type well known.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public string GetViewNameFromActivityType ( string activityTypeWellKnownName )
        {
            string viewName = null;

            if ( _wellKnownNameViewDictionary.ContainsKey ( activityTypeWellKnownName ) )
            {
                viewName = _wellKnownNameViewDictionary[activityTypeWellKnownName].Name;
            }

            return viewName;
        }

        /// <summary>
        /// Registers the type of the view for activity.
        /// </summary>
        /// <param name="activityViewType">Type of the activity view.</param>
        /// <param name="activityTypeWellKnowName">Name of the activity type well know.</param>
        public void RegisterViewForActivityType ( Type activityViewType, string activityTypeWellKnowName )
        {
            _wellKnownNameViewDictionary.Add ( activityTypeWellKnowName, activityViewType );
        }

        #endregion
    }
}
