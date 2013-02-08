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

using Pillar.Domain.Primitives;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// ISystemAccountFactory interface defines basic repository services for the <see cref="T:Rem.Domain.Core.SecurityModule.SystemAccount">SystemAccount</see>.
    /// </summary>
    public interface ISystemAccountFactory
    {
        /// <summary>
        /// Creates the system account.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="identityProviderName">Name of the identity provider.</param>
        /// <param name="identityProviderUri">The identity provider URI.</param>
        /// <returns>A SystemAccount.</returns>
        SystemAccount CreateSystemAccount (
            string identifier, string displayName, EmailAddress emailAddress, string identityProviderName, string identityProviderUri );

        /// <summary>
        /// Destroys the system account.
        /// </summary>
        /// <param name="systemAccount">The system account.</param>
        void DestroySystemAccount(SystemAccount systemAccount);
    }
}
