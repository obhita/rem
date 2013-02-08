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

using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="IKeyedDtoFactory&lt;TDto&gt;"/> is used to create a dto with a key, that uniquely idetifies it.
    /// </summary>
    /// <typeparam name="TDto">
    /// The type of the dto. 
    /// </typeparam>
    public interface IKeyedDtoFactory<out TDto>
        where TDto : KeyedDataTransferObject
    {
        #region Public Methods

        /// <summary>
        /// Creates a dto with a key that uniquely identifes it.
        /// </summary>
        /// <param name="key">
        /// The key of the dto. 
        /// </param>
        /// <returns>
        /// Returns the dto. 
        /// </returns>
        TDto CreateKeyedDto ( long key );

        #endregion
    }

    /// <summary>
    /// The <see cref="IDtoFactory&lt;TDto&gt;"/> is used to create a dto.
    /// </summary>
    /// <typeparam name="TDto">
    /// The type of the dto. 
    /// </typeparam>
    public interface IDtoFactory<out TDto>
        where TDto : class, new ()
    {
        #region Public Methods

        /// <summary>
        /// Creates the dto.
        /// </summary>
        /// <returns>
        /// Returns the dto. 
        /// </returns>
        TDto CreateDto ();

        #endregion
    }
}
