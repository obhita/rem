using System.Linq;
using Agatha.Common.WCF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata.Dtos;
using Rem.Infrastructure.Metadata;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class MetadataKnownTypeProviderTests
    {
        [TestMethod]
        public void RegisterTypes_InSpecificAssembly_Succeed()
        {
            var assembly = typeof ( IMetadataItemDto ).Assembly;
            var metadataKnowTypeProvider = new MetadataKnownTypeProvider ( assembly );
            
            metadataKnowTypeProvider.RegisterTypes ();

            var knownTypes = KnownTypeProvider.GetKnownTypes ( null ).ToList ();
            var expectedKnownTypes = assembly.GetExportedTypes()
                .Where(x => typeof(IMetadataItemDto).IsAssignableFrom(x))
                .ToList();
            CollectionAssert.AreEqual ( expectedKnownTypes, knownTypes );
        }
    }
}
