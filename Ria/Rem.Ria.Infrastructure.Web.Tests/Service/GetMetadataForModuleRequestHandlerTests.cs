using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Pillar.Common.Tests;
using Rem.Infrastructure.Metadata;
using Rem.Ria.Infrastructure.Web.Service;

namespace Rem.Ria.Infrastructure.Web.Tests.Service
{
    [TestClass]
    public class GetMetadataForModuleRequestHandlerTests : TestFixtureBase
    {
        protected override void OnSetup()
        {
            base.OnSetup();
            SetupAutoMapper ();
        }

        [TestMethod]
        public void GetMetadataForModuleRequest_ExistingMetadataForSpecificModuleName_ReturnedMetadataDtos()
        {
            var metadataRepository = CreateMockedMetadataRepository ();
            var handler = new GetMetadataForModuleRequestHandler ( metadataRepository );
            var request = new GetMetadataForModuleRequest { ModuleNames = { "Rem.Ria.PatientModule" } };

            var response = handler.Handle ( request ) as GetMetadataForModuleResponse;

            var expectedMetadataDtos = GetExpectedMetadataDtoList ();
            AssertMetadataDtosAreEqual(expectedMetadataDtos, response.MetadataDtos);
        }

        private void AssertMetadataDtosAreEqual(IEnumerable<MetadataDto> expectedMetadataDtos, IEnumerable<MetadataDto> testedMetadataDtos  )
        {
            for (int i = 0; i < expectedMetadataDtos.Count(); i++)
            {
                var expectedMetadataDto = expectedMetadataDtos.ElementAt ( i );
                var testedMetadataDto = testedMetadataDtos.ElementAt ( i );

                Assert.AreEqual ( expectedMetadataDto.ResourceName, testedMetadataDto.ResourceName );

                for (int j = 0; j < expectedMetadataDto.MetadataItemDtos.Count; j++)
                {
                    var expectedMetadataItemDto = expectedMetadataDto.MetadataItemDtos[j];
                    var testedMetadataItemDto = testedMetadataDto.MetadataItemDtos[j];

                    AssertMetadataItemDtoAreEqual ( expectedMetadataItemDto, testedMetadataItemDto );
                }

                AssertMetadataDtosAreEqual(expectedMetadataDto.Children, testedMetadataDto.Children);
            }
        }

        private void AssertMetadataItemDtoAreEqual(IMetadataItemDto expectedMetadataItemDto, IMetadataItemDto testedMetadataItemDto)
        {
            Assert.AreEqual(expectedMetadataItemDto.GetType(), testedMetadataItemDto.GetType());

            if (expectedMetadataItemDto is RequiredMetadataItemDto)
            {
                Assert.AreEqual ( (expectedMetadataItemDto as RequiredMetadataItemDto).IsRequired, (testedMetadataItemDto as RequiredMetadataItemDto).IsRequired );
            }
            else if (expectedMetadataItemDto is ReadonlyMetadataItemDto)
            {
                Assert.AreEqual((expectedMetadataItemDto as ReadonlyMetadataItemDto).IsReadonly, (testedMetadataItemDto as ReadonlyMetadataItemDto).IsReadonly);
            }
            else if (expectedMetadataItemDto is HiddenMetadataItemDto)
            {
                Assert.AreEqual((expectedMetadataItemDto as HiddenMetadataItemDto).IsHidden, (testedMetadataItemDto as HiddenMetadataItemDto).IsHidden);
            }
            else if (expectedMetadataItemDto is DisplayNameMetadataItemDto)
            {
                Assert.AreEqual((expectedMetadataItemDto as DisplayNameMetadataItemDto).Name, (testedMetadataItemDto as DisplayNameMetadataItemDto).Name);
            }
            else
            {
                throw new NotSupportedException ( "Not supported yet." );
            }
        }

        private static void SetupAutoMapper()
        {
            Mapper.CreateMap<IMetadataItem, IMetadataItemDto> ()
                .Include<ReadonlyMetadataItem, ReadonlyMetadataItemDto> ()
                .Include<RequiredMetadataItem, RequiredMetadataItemDto> ()
                .Include<HiddenMetadataItem, HiddenMetadataItemDto> ()
                .Include<DisplayNameMetadataItem, DisplayNameMetadataItemDto> ();
            Mapper.CreateMap<ReadonlyMetadataItem, ReadonlyMetadataItemDto> ();
            Mapper.CreateMap<RequiredMetadataItem, RequiredMetadataItemDto> ();
            Mapper.CreateMap<HiddenMetadataItem, HiddenMetadataItemDto>();
            Mapper.CreateMap<DisplayNameMetadataItem, DisplayNameMetadataItemDto> ();
        }

        private static IMetadataRepository CreateMockedMetadataRepository()
        {
            IEnumerable<IMetadata> testedMetadataList = GetTestedMetadataList();

            var metadataRepository = new Mock<IMetadataRepository>();
            metadataRepository.Setup(x => x.FindMetadata("Rem.Ria.PatientModule"))
                .Returns(() => testedMetadataList);

            return metadataRepository.Object;
        }

        private static  IEnumerable<IMetadata> GetTestedMetadataList()
        {
            var metadataNodes = new List<MetadataNode>
                                       {
                                           new MetadataNode ( "Rem.Ria.PatientModule.PatientProfileDto")
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                                           new HiddenMetadataItem { IsHidden = false },
                                                                           new RequiredMetadataItem { IsRequired = true },
                                                                           new DisplayNameMetadataItem { Name = "Patient Profile" }
                                                                       }
                                               },
                                           new MetadataNode ( "Rem.Ria.PatientModule.PatientContactDto")
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true }
                                                                       }
                                               }
                                       };

            var child11 = metadataNodes[0].AddChild("FirstName");
            child11.MetadataItems = new List<IMetadataItem>
                                       {
                                           new ReadonlyMetadataItem { IsReadonly = true },
                                           new HiddenMetadataItem { IsHidden = true }
                                       };
            var child12 = metadataNodes[ 0 ].AddChild ( "DateOfBirth" );
            child12.MetadataItems = new List<IMetadataItem>
                                        {
                                            new RequiredMetadataItem { IsRequired = true }, 
                                            new DisplayNameMetadataItem { Name = "Date Of Birth" }
                                        };

            return metadataNodes;
        }

        private static IEnumerable<MetadataDto> GetExpectedMetadataDtoList()
        {
            var metadataDtos = new List<MetadataDto>
                                   {
                                       new MetadataDto ( "Rem.Ria.PatientModule.PatientProfileDto" ),
                                       new MetadataDto ( "Rem.Ria.PatientModule.PatientContactDto" )
                                   };

            metadataDtos[0].AddMetadataItem ( new ReadonlyMetadataItemDto { IsReadonly = false } );
            metadataDtos[0].AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = false } );
            metadataDtos[0].AddMetadataItem ( new RequiredMetadataItemDto { IsRequired = true } );
            metadataDtos[ 0 ].AddMetadataItem ( new DisplayNameMetadataItemDto { Name = "Patient Profile" } );

            metadataDtos[ 1 ].AddMetadataItem ( new ReadonlyMetadataItemDto { IsReadonly = true } );

            var child11 = metadataDtos[0].AddChildMetadata ("FirstName");
            child11.AddMetadataItem ( new ReadonlyMetadataItemDto { IsReadonly = true } );
            child11.AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = true } );

            var child12 = metadataDtos[0].AddChildMetadata("DateOfBirth");
            child12.AddMetadataItem ( new RequiredMetadataItemDto { IsRequired = true } );
            child12.AddMetadataItem ( new DisplayNameMetadataItemDto { Name = "Date Of Birth" } );

            return metadataDtos;
        }
    }
}
