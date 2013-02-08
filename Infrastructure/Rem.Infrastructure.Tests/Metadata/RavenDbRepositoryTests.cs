using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Infrastructure.Metadata;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class RavenDbRepositoryTests : RavenDbTestBase
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentNullException ) )]
        public void Constructor_GivenNullDocumentSessionProvider_ThrowsArgumentNullException ()
        {
            IDocumentSessionProvider documentSessionProvider = null;
            var fakeRespository = new FakeRepository(documentSessionProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_DocumentSessionProviderProvideNullSession_ThrowsArgumentNullException()
        {
            var documentSessionProvider = new Mock<IDocumentSessionProvider>();
            documentSessionProvider
                .Setup ( x => x.GetDocumentSession () )
                .Returns ( () => null );

            var fakeRespository = new FakeRepository(documentSessionProvider.Object);
        }

        [TestMethod]
        public void GetById_WithValidId_Succeed()
        {
            var fakeEntity = CreateFakeEntity ();
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var fakeRepository = new FakeRepository(documentSessionProvider);

            var fakeEntity2 = fakeRepository.GetById(fakeEntity.Id);

            Assert.IsNotNull(fakeEntity2);
        }

        [TestMethod]
        public void MakePersistent_AddNewEntity_Succeed()
        {
            var fakeEntity = new FakeEntity { Name = "Fake" };
            var documentSessionProvider = new DocumentSessionProvider(Store);
            var fakeRespository = new FakeRepository ( documentSessionProvider );
            
            fakeRespository.MakePersistent ( fakeEntity );
            fakeRespository.SaveChanges();

            using (var session = Store.OpenSession())
            {
                var fakeEntity2 = session.Load<FakeEntity> ( fakeEntity.Id );
                Assert.IsNotNull ( fakeEntity2 );
            }
        }

        [TestMethod]
        public void MakePersistent_UpdateEntity_Succeed()
        {
            var fakeEntity = CreateFakeEntity ();
            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var fakeRepository = new FakeRepository ( documentSessionProvider );

            fakeEntity.Name = "Changed";
            fakeRepository.MakePersistent(fakeEntity);
            fakeRepository.SaveChanges ();

            using ( var session = Store.OpenSession () )
            {
                var fakeEntity2 = session.Load<FakeEntity> ( fakeEntity.Id );

                Assert.AreEqual ( "Changed", fakeEntity2.Name );
            }
        }

        [TestMethod]
        public void MakeTransient_WithValidEntity_Succeed()
        {
            var fakeEntity = CreateFakeEntity ();


            var documentSessionProvider = new DocumentSessionProvider ( Store );
            var fakeRepository = new FakeRepository ( documentSessionProvider );
            var deletingEntity = fakeRepository.GetById ( fakeEntity.Id );

            fakeRepository.MakeTransient(deletingEntity);
            fakeRepository.SaveChanges();

            using ( var session = Store.OpenSession () )
            {
                var entities = session.Query<FakeEntity> ();

                Assert.AreEqual ( 0, entities.Count () );
            }
        }

        private FakeEntity CreateFakeEntity()
        {
            var fakeEntity = new FakeEntity { Name = "Fake" };
            using (var session = Store.OpenSession())
            {
                session.Store(fakeEntity);
                session.SaveChanges();

                return fakeEntity;
            }
        }
    }

    internal class FakeRepository : RavenDbRepositoryBase<FakeEntity>
    {
        public FakeRepository(IDocumentSessionProvider documentSessionProvider)
            : base(documentSessionProvider)
        {
        }
    }

    internal class FakeEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
