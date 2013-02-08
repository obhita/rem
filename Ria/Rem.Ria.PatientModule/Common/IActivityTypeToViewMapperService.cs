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

namespace Rem.Ria.PatientModule.Common
{
    /// <summary>
    /// IActivityTypeToViewMapperService interface.
    /// </summary>
    public interface IActivityTypeToViewMapperService
    {
        #region Public Methods

        /// <summary>
        /// Gets the type of the view name from activity.
        /// </summary>
        /// <param name="activityTypeWellKnownName">Name of the activity type well known.</param>
        /// <returns>A <see cref="System.String"/></returns>
        string GetViewNameFromActivityType ( string activityTypeWellKnownName );

        /// <summary>
        /// Registers the type of the view for activity.
        /// </summary>
        /// <param name="activityFactoryType">Type of the activity factory.</param>
        /// <param name="activityTypeWellKnowName">Name of the activity type well know.</param>
        void RegisterViewForActivityType ( Type activityFactoryType, string activityTypeWellKnowName );

        #endregion
    }
}
