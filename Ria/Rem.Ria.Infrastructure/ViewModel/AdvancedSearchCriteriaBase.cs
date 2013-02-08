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

using System.Reflection;

namespace Rem.Ria.Infrastructure.ViewModel
{
    /// <summary>
    /// Base class for AdvancedSearchCriteria
    /// </summary>
    public class AdvancedSearchCriteriaBase : CustomNotificationObject
    {
        #region Public Methods

        /// <summary>
        /// Cleans up advanced search fields.
        /// </summary>
        public void CleanUpAdvancedSearchFields ()
        {
            var publicProperties = GetType ().GetProperties ();
            foreach ( var publicProperty in publicProperties )
            {
                if ( publicProperty.CanWrite )
                {
                    publicProperty.SetValue ( this, null, null );
                }
            }
        }

        /// <summary>
        /// Determines whether [has enough public properties with value] [the specified num of properties with value required].
        /// </summary>
        /// <param name="numOfPropertiesWithValueRequired">The num of properties with value required.</param>
        /// <returns><c>true</c> if [has enough public properties with value] [the specified num of properties with value required]; otherwise, <c>false</c>.</returns>
        public virtual bool HasEnoughPublicPropertiesWithValue ( int numOfPropertiesWithValueRequired )
        {
            return true;
        }

        #endregion
    }
}
