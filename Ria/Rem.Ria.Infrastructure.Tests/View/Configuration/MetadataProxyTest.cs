using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;
using Pillar.Common.Metadata.Dtos;
using Rem.Infrastructure.Metadata;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.Service;
using Rem.Ria.Infrastructure.View.Configuration;

namespace Rem.Ria.Infrastructure.Tests.View.Configuration
{
    [TestClass]
    public class MetadataProxyTest
    {
        private IMetadataProvider _metadataProvider;
        private IMetadataService _metadataService;

        [TestInitialize]
        public void OnSetup()
        {
            var customizedMetadataDto = CreateCustomizedMetadataDto ();
            _metadataProvider = new PatientProfileDto { MetadataDto = customizedMetadataDto };

            var defaultMetadataDto = CreateDefaultMetadataDto ();
            var metadataServiceMock = new Mock<IMetadataService> ();
            metadataServiceMock.Setup ( x => x.GetMetadata ( It.IsAny<Type> () ) ).Returns ( defaultMetadataDto );
            _metadataService = metadataServiceMock.Object;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullMetadataService_ThrowArgumentNullException()
        {
            var metadataProxy = new MetadataProxy ( null, new Mock<IMetadataProvider> ().Object );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WithNullMetadataProvider_ThrowArgumentNullException()
        {
            var metadataProxy = new MetadataProxy(new Mock<IMetadataService>().Object, null);
        }

        [TestMethod]
        public void Constructor_ForBaseMetadata_Succeed()
        {
            var metadataProxy = new MetadataProxy ( _metadataService, _metadataProvider );
        }

        [TestMethod]
        public void Constructor_ForChildMetadata_Succeed()
        {
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider, "LastName");
        }

        [TestMethod]
        public void ReportInitialMetadata_MetadataChangedEventIsDefault_Succeed()
        {
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider);

            metadataProxy.ReportInitialMetadata();
        }

        [TestMethod]
        public void ReportInitialMetadata_WithMetadataChangedEvent_FireMetadataChangedEvent()
        {
            bool isMetadataChanged = false;
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider, "LastName");
            metadataProxy.MetadataChanged += (sender, args) => { isMetadataChanged = true; };

            metadataProxy.ReportInitialMetadata();

            Assert.IsTrue(isMetadataChanged);
        }

        [TestMethod]
        public void ReportInitialMetadata_InvokeTwice_TheSecondTimeWillAlsoFireMetadataChangedEvent()
        {
            bool isMetadataChanged = false;
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider, "LastName");
            metadataProxy.MetadataChanged += (sender, args) => { isMetadataChanged = true; };
            
            metadataProxy.ReportInitialMetadata();
            isMetadataChanged = false;

            metadataProxy.ReportInitialMetadata ();

            Assert.IsTrue(isMetadataChanged);
        }

        [TestMethod]
        public void ReportInitialMetadata_ChangeMetadataDtoFromIMetadataProvider_MetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefault()
        {
            var metadataChangeList = new List<MetadataChangedEventArgs> ();
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider, "LastName");
            _metadataProvider.MetadataDto = CreateNewCustomizedMetadataDto();
            metadataProxy.MetadataChanged += (sender, args) => metadataChangeList.Add(args);

            metadataProxy.ReportInitialMetadata ();

            AssertMetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefault (metadataChangeList);
        }

        [TestMethod]
        public void ReportInitialMetadata_AddAndRemoveMetadataItemDtos_MetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefaultAndCustomized()
        {
            var metadataChangeList = new List<MetadataChangedEventArgs>();
            var metadataProxy = new MetadataProxy(_metadataService, _metadataProvider, "LastName");
            var metadata = _metadataProvider.MetadataDto.GetChildMetadata ( "LastName" );
            metadata.RemoveMetadataItem<HiddenMetadataItemDto> ();
            metadata.AddMetadataItem ( new RequiredMetadataItemDto { IsRequired = true } );
            metadata.AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = false } );
            metadataProxy.MetadataChanged += (sender, args) => metadataChangeList.Add(args);

            metadataProxy.ReportInitialMetadata();

            AssertMetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefaultAndCustomized(metadataChangeList);
        }

        private void AssertMetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefault(IEnumerable<MetadataChangedEventArgs> metadataChangedEventArgs)
        {
            var addedItems = metadataChangedEventArgs
                .Where ( x => x.MetadataAction == MetadataAction.Added )
                .Select(x => x.MetadataItemDto);

            Assert.AreEqual("Default LastName", addedItems.OfType<DisplayNameMetadataItemDto>().Single().Name);
            Assert.IsTrue(addedItems.OfType<RequiredMetadataItemDto>().Single().IsRequired);
            Assert.IsFalse(addedItems.OfType<HiddenMetadataItemDto>().Single().IsHidden);
        }

        private void AssertMetadataItemDtoChangedListAreNewCustomizedAndMergedWithDefaultAndCustomized(IEnumerable<MetadataChangedEventArgs> metadataChangedEventArgs)
        {
            var addedItems = metadataChangedEventArgs
                .Where ( x => x.MetadataAction == MetadataAction.Added )
                .Select ( x => x.MetadataItemDto );

            Assert.AreEqual ( "Default LastName", addedItems.OfType<DisplayNameMetadataItemDto> ().Single ().Name );
            Assert.IsTrue ( addedItems.OfType<RequiredMetadataItemDto> ().Single ().IsRequired );
            Assert.IsFalse ( addedItems.OfType<HiddenMetadataItemDto> ().Single ().IsHidden );
            Assert.IsTrue ( addedItems.OfType<ReadonlyMetadataItemDto> ().Single ().IsReadonly );
        }

        private MetadataDto CreateDefaultMetadataDto()
        {
            var metadataDto = new MetadataDto("Rem.Ria.PatientModule.Web.Common.PatientProfileDto");

            var firstNameNode = metadataDto.AddChildMetadata("FirstName");
            firstNameNode.AddMetadataItem ( new DisplayNameMetadataItemDto { Name = "The Patient's First Name" } );
            firstNameNode.AddMetadataItem ( new ReadonlyMetadataItemDto { IsReadonly = true } );

            var middleNameNode = metadataDto.AddChildMetadata("MiddleName");
            middleNameNode.AddMetadataItem ( new RequiredMetadataItemDto { IsRequired = true } );

            var lastNameNode = metadataDto.AddChildMetadata("LastName");
            lastNameNode.AddMetadataItem ( new DisplayNameMetadataItemDto { Name = "Default LastName" } );
            lastNameNode.AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = true } );

            return metadataDto;
        }

        private MetadataDto CreateCustomizedMetadataDto()
        {
            var metadataDto = new MetadataDto("Rem.Ria.PatientModule.Web.Common.PatientProfileDto");

            var lastNameNode = metadataDto.AddChildMetadata("LastName");
            lastNameNode.AddMetadataItem ( new ReadonlyMetadataItemDto { IsReadonly = true } );
            lastNameNode.AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = false } );

            return metadataDto;
        }

        private MetadataDto CreateNewCustomizedMetadataDto()
        {
            var metadataDto = new MetadataDto("Rem.Ria.PatientModule.Web.Common.PatientProfileDto");

            var lastNameNode = metadataDto.AddChildMetadata("LastName");
            lastNameNode.AddMetadataItem(new RequiredMetadataItemDto { IsRequired = true });
            lastNameNode.AddMetadataItem ( new HiddenMetadataItemDto { IsHidden = false } );

            return metadataDto;
        }

        internal class PatientProfileDto : AbstractDataTransferObject
        {
        }
    }
}
