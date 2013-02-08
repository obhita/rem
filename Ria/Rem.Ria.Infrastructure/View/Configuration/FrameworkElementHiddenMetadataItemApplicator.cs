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
using System.Windows;
using Pillar.Common.Metadata.Dtos;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Class for applicating framework element hidden metadata item.
    /// </summary>
    public class FrameworkElementHiddenMetadataItemApplicator : MetadataItemApplicatorBase<FrameworkElement, HiddenMetadataItemDto>
    {
        #region Methods

        /// <summary>
        /// Applies the specified framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="metadataItem">The metadata item.</param>
        /// <param name="localStorage">The local storage.</param>
        protected override void Apply (
            FrameworkElement frameworkElement, HiddenMetadataItemDto metadataItem, IDictionary<Type, object> localStorage )
        {
            if ( !localStorage.ContainsKey ( MetadataItemType ) )
            {
                localStorage.Add ( MetadataItemType, frameworkElement.Visibility );
            }

            frameworkElement.Visibility = metadataItem.IsHidden ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Unapplies the specified framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="metadataItem">The metadata item.</param>
        /// <param name="localStorage">The local storage.</param>
        protected override void Unapply (
            FrameworkElement frameworkElement, HiddenMetadataItemDto metadataItem, IDictionary<Type, object> localStorage )
        {
            var storedValue = Visibility.Visible;
            if ( localStorage.ContainsKey ( MetadataItemType ) )
            {
                storedValue = ( Visibility )localStorage[MetadataItemType];
            }

            frameworkElement.Visibility = storedValue;
        }

        #endregion
    }
}
