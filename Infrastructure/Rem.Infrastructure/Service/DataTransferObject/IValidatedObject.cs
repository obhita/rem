#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using System.Collections.Generic;

namespace Rem.Infrastructure.Service.DataTransferObject
{
    /// <summary>
    /// The <see cref="IValidatedObject"/> interface that provides data error information management.
    /// </summary>
    public interface IValidatedObject
    {
        #region Public Properties

        /// <summary>
        ///   Gets the data error information collection.
        /// </summary>
        IEnumerable<DataErrorInfo> DataErrorInfoCollection { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the data error information.
        /// </summary>
        /// <param name="dataErrorInfo">
        /// The data error information. 
        /// </param>
        void AddDataErrorInfo ( DataErrorInfo dataErrorInfo );

        /// <summary>
        /// Clears all data error information.
        /// </summary>
        void ClearAllDataErrorInfo ();

        /// <summary>
        /// Removes the data error information.
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property which has erroneous data. 
        /// </param>
        void RemoveDataErrorInfo ( string propertyName );

        #endregion
    }
}
