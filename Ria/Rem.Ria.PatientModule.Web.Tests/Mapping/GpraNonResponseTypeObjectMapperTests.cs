using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Rem.Domain.Clinical.GpraModule;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Ria.PatientModule.Web.GpraInterview;

namespace Rem.Ria.PatientModule.Web.Tests.Mapping
{
    [TestClass]
    public class GpraNonResponseTypeObjectMapperTests : TestFixtureBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            
            Mapper.Reset(); // to make TFS test happy

            var originalMapperFunction = MapperRegistry.AllMappers;
            MapperRegistry.AllMappers = () =>
            {
                var mappers = (originalMapperFunction.Invoke() as IObjectMapper[]).ToList();
                mappers.Add(new DefaulObjectMapper());
                return mappers.ToArray();
            };
        }

        [TestMethod]
        public void GpraNonResponseTypeObject_Map_Succeed()
        {
            GpraNonResponseType<bool?> source = new GpraNonResponseType<bool?>();
            GpraNonResponseTypeDto<bool?> dest = Mapper.Map<GpraNonResponseTypeDto<bool?>>(source);
            Assert.IsNotNull(dest);
        }
    }
}
