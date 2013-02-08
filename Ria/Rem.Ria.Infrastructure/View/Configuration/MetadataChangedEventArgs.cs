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
using Pillar.Common.Metadata.Dtos;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Metadata Action
    /// </summary>
    public enum MetadataAction
    {
        /// <summary>
        /// Added action.
        /// </summary>
        Added,

        /// <summary>
        /// Removed action.
        /// </summary>
        Removed
    }

    /// <summary>
    /// MetadataChangedEventArgs class.
    /// </summary>
    public class MetadataChangedEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataChangedEventArgs"/> class.
        /// </summary>
        /// <param name="metadataAction">The metadata action.</param>
        /// <param name="metadataItemDto">The metadata item dto.</param>
        public MetadataChangedEventArgs (
            MetadataAction metadataAction,
            IMetadataItemDto metadataItemDto )
        {
            MetadataAction = metadataAction;
            MetadataItemDto = metadataItemDto;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the metadata action.
        /// </summary>
        public MetadataAction MetadataAction { get; private set; }

        /// <summary>
        /// Gets the metadata item dto.
        /// </summary>
        public IMetadataItemDto MetadataItemDto { get; private set; }

        #endregion
    }
}
