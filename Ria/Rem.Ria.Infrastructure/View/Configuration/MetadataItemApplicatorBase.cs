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
    /// Base class for MetadataItemApplicator
    /// </summary>
    /// <typeparam name="TFrameworkElement">The type of the framework element.</typeparam>
    /// <typeparam name="TMetadataItem">The type of the metadata item.</typeparam>
    public abstract class MetadataItemApplicatorBase<TFrameworkElement, TMetadataItem> : IMetadataItemApplicator
        where TFrameworkElement : FrameworkElement
        where TMetadataItem : IMetadataItemDto
    {
        #region Public Properties

        /// <summary>
        /// Gets the type of the framework element.
        /// </summary>
        public Type FrameworkElementType
        {
            get { return typeof( TFrameworkElement ); }
        }

        /// <summary>
        /// Gets the type of the metadata item.
        /// </summary>
        public Type MetadataItemType
        {
            get { return typeof( TMetadataItem ); }
        }

        #endregion

        #region Explicit Interface Methods

        void IMetadataItemApplicator.Apply (
            FrameworkElement frameworkElement,
            IMetadataItemDto metadataItem,
            IDictionary<Type, object> localStorage )
        {
            Apply ( ( TFrameworkElement )frameworkElement, ( TMetadataItem )metadataItem, localStorage );
        }

        void IMetadataItemApplicator.Unapply (
            FrameworkElement frameworkElement,
            IMetadataItemDto metadataItem,
            IDictionary<Type, object> localStorage )
        {
            Unapply ( ( TFrameworkElement )frameworkElement, ( TMetadataItem )metadataItem, localStorage );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the specified framework element.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="metadataItem">The metadata item.</param>
        /// <param name="localStorage">The local storage.</param>
        protected abstract void Apply ( TFrameworkElement frameworkElement, TMetadataItem metadataItem, IDictionary<Type, object> localStorage );

        /// <summary>
        /// Unapplies the specified text block.
        /// </summary>
        /// <param name="textBlock">The text block.</param>
        /// <param name="metadataItem">The metadata item.</param>
        /// <param name="localStorage">The local storage.</param>
        protected abstract void Unapply ( TFrameworkElement textBlock, TMetadataItem metadataItem, IDictionary<Type, object> localStorage );

        #endregion
    }
}
