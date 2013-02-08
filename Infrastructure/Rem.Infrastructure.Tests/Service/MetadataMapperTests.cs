using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Rem.Domain.Clinical.PatientModule;
using Rem.Infrastructure.Metadata;
using Rem.Infrastructure.Service;

namespace Rem.Infrastructure.Tests.Service
{
    [TestClass]
    public class MetadataMapperTests
    {
        private class PatientAddressDtoTest
        {
            #region Properties

            public PatientAddressType PatientAddressType { get; set; }

            public bool? ConfidentialIndicator { get; set; }

            public int? YearsOfStayNumber { get; set; }

            #endregion
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MapToDto_NullEntityType_ThrowsArgumentException()
        {
            var metaDataRepository = new Mock<IMetadataRepository>();

            var metadataMapper = new MetadataMapper(metaDataRepository.Object);

            var metaDataDto = new MetadataDto(typeof(PatientAddressDtoTest).FullName);

            metadataMapper.MapToDto(null, typeof(PatientAddressDtoTest), metaDataDto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MapToDto_NullDtoType_ThrowsArgumentException()
        {
            var metaDataRepository = new Mock<IMetadataRepository>();

            var metadataMapper = new MetadataMapper(metaDataRepository.Object);

            var metaDataDto = new MetadataDto(typeof(PatientAddressDtoTest).FullName);

            metadataMapper.MapToDto(typeof(PatientAddress), null, metaDataDto);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MapToDto_NullMetadataDto_ThrowsArgumentException()
        {
            var metaDataRepository = new Mock<IMetadataRepository>();

            var metadataMapper = new MetadataMapper(metaDataRepository.Object);

            metadataMapper.MapToDto(typeof(PatientAddress), typeof(PatientAddressDtoTest), null);
        }

        [TestMethod]
        public void MapToDto_ValidMetadata_Succeeds()
        {
            const string propertyWithMetadata = "PatientAddressType";

            Mapper.CreateMap<PatientAddress, PatientAddressDtoTest> ();
 
            Mapper.CreateMap<IMetadataItem, IMetadataItemDto>()
                .Include<ReadonlyMetadataItem, ReadonlyMetadataItemDto>()
                .Include<RequiredMetadataItem, RequiredMetadataItemDto>()
                .Include<HiddenMetadataItem, HiddenMetadataItemDto>()
                .Include<DisplayNameMetadataItem, DisplayNameMetadataItemDto>();

            var metaDataRepository = new Mock<IMetadataRepository> ();

            var metaDataRoot = new MetadataRoot ( typeof( PatientAddress ).FullName, 1 );

            metaDataRoot.AddChild ( propertyWithMetadata );

            metaDataRoot.Children[0].MetadataItems.Add ( new DisplayNameMetadataItem () );

            metaDataRepository.Setup ( m => m.GetMetadata ( typeof( PatientAddress ).FullName ) ).Returns ( metaDataRoot );

            var metadataMapper = new MetadataMapper ( metaDataRepository.Object );

            var metaDataDto = new MetadataDto ( typeof( PatientAddressDtoTest ).FullName );

            metadataMapper.MapToDto ( typeof(PatientAddress), typeof(PatientAddressDtoTest), metaDataDto );

            Assert.AreEqual ( 1, metaDataDto.Children.Count);
            Assert.AreEqual(1, metaDataDto.Children[0].MetadataItemDtos.Count);
            Assert.AreEqual ( typeof(DisplayNameMetadataItemDto), metaDataDto.Children[0].MetadataItemDtos[0].GetType () );
        }
    }
}
