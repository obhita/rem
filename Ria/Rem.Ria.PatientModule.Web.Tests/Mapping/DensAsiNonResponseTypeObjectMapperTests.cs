using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Tests;
using Rem.Ria.Infrastructure.Web.Mapping;
using Rem.Domain.Clinical.DensAsiModule;
using Rem.Ria.PatientModule.Web.DensAsiInterview;

namespace Rem.Ria.PatientModule.Web.Tests.Mapping
{
    [TestClass]
    public class DensAsiNonResponseTypeObjectMapperTests : TestFixtureBase
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
        public void DensAsiNonResponseTypeObject_Map_Succeed()
        {
            DensAsiNonResponseType<TimeSpan?> source = new DensAsiNonResponseType<TimeSpan?>();
            DensAsiNonResponseTypeDto<TimeSpan?> dest = Mapper.Map<DensAsiNonResponseTypeDto<TimeSpan?>>(source);
            Assert.IsNotNull(dest);
        }
    }
}
