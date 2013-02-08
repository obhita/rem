using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Domain.Core.CommonModule;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Web.DataTransferObject;
using Rem.Ria.Infrastructure.Web.Mapping;
using StructureMap;

namespace Rem.Ria.Infrastructure.Web.Tests.Mapping
{
    [TestClass]
    public class InfrastructureWebEntityToDtoMappingTests : EntityToDtoMappingTestFixtureBase
    {
        [TestMethod]
        public void LookupBase_EntityToDtoMapping_Exists ()
        {
            Assert.IsTrue ( HasEntityToDtoMapping<LookupBase, LookupValueDto> () );
        }

        [TestMethod]
        public void CodedConcept_EntityToDtoMapping_Exists()
        {
            Assert.IsTrue(HasEntityToDtoMapping<CodedConcept, CodedConceptDto>());
        }

        [TestMethod]
        public void AllMapping_EntityToDto_Succeeds ()
        {
            AssertAutoMapperConfigurationIsValid ();
        }

        protected override void OnSetup ()
        {
            var appContainer = new Container();
            appContainer.Configure(x => x.Scan(scanner =>
            {
                scanner.AssembliesFromApplicationBaseDirectory(p => (p.FullName == null) ? false : p.FullName.Contains("Rem."));
                scanner.LookForRegistries();
            }));
            var originalMapperFunction = MapperRegistry.AllMappers;
            MapperRegistry.AllMappers = () =>
            {
                var mappers = (originalMapperFunction.Invoke() as IObjectMapper[]).ToList();
                var objectMappers = appContainer.GetAllInstances<IObjectMapper>();
                mappers.AddRange(objectMappers);
                return mappers.ToArray();
            };
            var autoMapperConfig = new AutoMapperConfig ();
            autoMapperConfig.Execute ();

            base.OnSetup ();
        }
    }
}