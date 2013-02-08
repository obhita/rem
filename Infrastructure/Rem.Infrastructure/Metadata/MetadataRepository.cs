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
using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Metadata;

namespace Rem.Infrastructure.Metadata
{
    /// <summary>
    /// Provides repository services for the <see cref="T:Pillar.Common.Metadata.IMetadata">IMetadata</see>.
    /// </summary>
    public class MetadataRepository : RavenDbRepositoryBase<IMetadata>, IMetadataRepository
    {
        private readonly IMetadataMerger _merger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRepository"/> class.
        /// </summary>
        /// <param name="documentSessionProvider">The document session provider.</param>
        /// <param name="merger">The merger.</param>
        public MetadataRepository(IDocumentSessionProvider documentSessionProvider, IMetadataMerger merger)
            : base(documentSessionProvider)
        {
            _merger = merger;
        }

        #region Implementation of IMetadataRepository

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>A IMetadata.</returns>
        public IMetadata GetMetadata ( string resourceName )
        {
            var metadataList = 
                Session.Query<MetadataRoot>().Where(p => p.ResourceName == resourceName);
            return metadataList.Any() ? _merger.MergeMetadata(metadataList) : null;
        }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>A IMetadata.</returns>
        public IMetadata GetMetadata ( string resourceName, string layerName )
        {
            IMetadata metadata = null;

            var metadataLayer = Session.Query<MetadataLayer> ().SingleOrDefault(x => x.Name == layerName);

            if (metadataLayer != null)
            {
                var metadataList = Session.Query<MetadataRoot> ()
                    .Where ( p => p.ResourceName == resourceName && p.MetadataLayerId == metadataLayer.Id );

                metadata = metadataList.FirstOrDefault();
            }

            return metadata;
        }

        /// <summary>
        /// Finds the metadata.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns>An IEnumerable&lt;IMetadata&gt;.</returns>
        public IEnumerable<IMetadata> FindMetadata ( string searchString )
        {
            var metadataList = new List<IMetadata>();

            searchString = GetInterpretedSearchString(searchString);

            var metadataQueryResults = Session.Query<MetadataRoot> ()
                .Where ( p => p.ResourceName.StartsWith ( searchString ) )
                .ToList ();

            var metadataGroups = metadataQueryResults.GroupBy(p => p.ResourceName);

            foreach (var group in metadataGroups)
            {
                var metadata = group.Any() ? _merger.MergeMetadata(group.ToList()) : null;
                if (metadata != null)
                {
                    metadataList.Add ( metadata );
                }
            }

            return metadataList;
        }

        /// <summary>
        /// Finds the metadata.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <param name="layerName">Name of the layer.</param>
        /// <returns>An IEnumerable&lt;IMetadata&gt;.</returns>
        public IEnumerable<IMetadata> FindMetadata ( string searchString, string layerName )
        {
            IEnumerable<IMetadata> metadataList = new List<IMetadata>();

            var metadataLayer = Session.Query<MetadataLayer>().SingleOrDefault(x => x.Name == layerName);

            if (metadataLayer != null)
            {
                searchString = GetInterpretedSearchString ( searchString );

                metadataList = Session.Query<MetadataRoot> ()
                    .Where ( p => p.MetadataLayerId == metadataLayer.Id && p.ResourceName.StartsWith ( searchString ) );
            }

            return metadataList;
        }

        #endregion

        private static string GetInterpretedSearchString(string searchString)
        {
            if (searchString.Last() == '*')
            {
                searchString = searchString.TrimEnd("*".ToCharArray());
            }

            return searchString;
        }
    }
}
