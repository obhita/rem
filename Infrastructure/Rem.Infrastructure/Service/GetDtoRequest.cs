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

using Agatha.Common;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    // TODO: Rename this to GetDtoByKeyRequest once the old GetDtoByKeyRequest is gone

    /// <summary>
    /// The <see cref="GetDtoRequest&lt;TDto&gt;"/> is a generic web request containing the dto key.
    /// </summary>
    /// <typeparam name="TDto">
    /// The type of the dto. 
    /// </typeparam>
    public class GetDtoRequest<TDto> : Request
        where TDto : KeyedDataTransferObject
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="GetDtoRequest&lt;TDto&gt;" /> class. This is required by WCF.
        /// </summary>
        public GetDtoRequest ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetDtoRequest&lt;TDto&gt;"/> class. This is required by REM to use Activator to create closed generic type instance.
        /// </summary>
        /// <param name="key">
        /// The dto key. 
        /// </param>
        public GetDtoRequest ( long key )
        {
            Key = key;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the key.
        /// </summary>
        /// <value> The dto key. </value>
        public long Key { get; set; }

        #endregion
    }
}
