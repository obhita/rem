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
using System.Linq;
using System.Windows;
using Pillar.Common.Metadata.Dtos;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// MetadataItemApplicatorService class.
    /// </summary>
    public class MetadataItemApplicatorService : IMetadataItemApplicatorService
    {
        #region Constants and Fields

        private readonly IList<IMetadataItemApplicator> _metadataItemApplicators = new List<IMetadataItemApplicator> ();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataItemApplicatorService"/> class.
        /// </summary>
        public MetadataItemApplicatorService ()
        {
            Initialize ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the base types.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>A collection of <see cref="Type"/>.</returns>
        public static IEnumerable<Type> GetBaseTypes ( Type target )
        {
            while ( target != null && target != typeof( UIElement ) )
            {
                yield return target;
                target = target.BaseType;
            }
        }

        /// <summary>
        /// Gets the metadata item applicator.
        /// </summary>
        /// <param name="frameworkElement">The framework element.</param>
        /// <param name="metadataItem">The metadata item.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.View.Configuration.IMetadataItemApplicator"/></returns>
        public IMetadataItemApplicator GetMetadataItemApplicator ( FrameworkElement frameworkElement, IMetadataItemDto metadataItem )
        {
            IMetadataItemApplicator metadataItemApplicator = null;
            var elementTypes = GetBaseTypes ( frameworkElement.GetType () );
            foreach ( var elementType in elementTypes )
            {
                var candidateElementType = elementType;
                var metadataItemApplicators =
                    from applicator in _metadataItemApplicators
                    where applicator.FrameworkElementType == candidateElementType && applicator.MetadataItemType == metadataItem.GetType ()
                    select applicator;

                metadataItemApplicator = metadataItemApplicators.SingleOrDefault ();
                if ( metadataItemApplicator != null )
                {
                    break;
                }
            }

            return metadataItemApplicator;
        }

        #endregion

        #region Methods

        private void Initialize ()
        {
            var applicatorTypes = from type in typeof( IMetadataItemApplicatorService ).Assembly.GetTypes ()
                                                where typeof( IMetadataItemApplicator ).IsAssignableFrom ( type ) && type.IsClass && !type.IsAbstract
                                                select type;
            foreach ( var applicatorType in applicatorTypes )
            {
                var applicator = ( IMetadataItemApplicator )Activator.CreateInstance ( applicatorType );
                _metadataItemApplicators.Add ( applicator );
            }
        }

        #endregion
    }
}
