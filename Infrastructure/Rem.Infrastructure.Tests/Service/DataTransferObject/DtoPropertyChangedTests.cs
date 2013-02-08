using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Tests.Service.DataTransferObject
{
    [TestClass]
    public class DtoPropertyChangedTests
    {
        [TestMethod]
        public void PropertyChange_DtoPropertyChanged_SetsNewValueAndRaisesPropertyChanged ()
        {
            var dto = new TestDto ();
            var propertyDto1 = new PropertyDto { Key = 1, Name = "Test1" };
            var propertyDto2 = new PropertyDto { Key = 2, Name = "Test2" };
            bool propertyChanged = false;

            dto.PropertyChanged += ( s, e ) => { propertyChanged = true; };
            dto.PropertyDto = propertyDto1;
            Assert.AreEqual ( dto.PropertyDto, propertyDto1 );
            Assert.IsTrue ( propertyChanged );

            propertyChanged = false;
            dto.PropertyDto = propertyDto2;
            Assert.AreEqual ( dto.PropertyDto, propertyDto2 );
            Assert.IsTrue ( propertyChanged );
        }

        [TestMethod]
        public void PropertyChange_DtoPropertyNotChanged_NoChangeInValueAndPropertyChangedIsNotRaised ()
        {
            var dto = new TestDto ();
            var propertyDto = new PropertyDto { Key = 1, Name = "Test" };
            bool propertyChanged = false;

            dto.PropertyChanged += ( s, e ) => { propertyChanged = true; };
            dto.PropertyDto = propertyDto;
            Assert.AreEqual ( dto.PropertyDto, propertyDto );
            Assert.IsTrue ( propertyChanged );

            propertyChanged = false;
            dto.PropertyDto = propertyDto;
            Assert.AreEqual ( dto.PropertyDto, propertyDto );
            Assert.IsFalse ( propertyChanged );
        }

        [TestMethod]
        public void PropertyCollectionChange_DtoPropertyCollectionChanged_AddsToCollectionAndRaisesPropertyChanged ()
        {
            var dto = new TestDto () { PropertyCollectionDtos = new ObservableCollection<PropertyCollectionDto> () };
            IList<PropertyCollectionDto> collectionDtos = new List<PropertyCollectionDto> ()
                                                              {
                                                                  new PropertyCollectionDto { Key = 1, Name = "First" },
                                                                  new PropertyCollectionDto { Key = 2, Name = "Second" }
                                                              };
            bool propertyChanged = false;

            dto.PropertyChanged += ( s, e ) => { propertyChanged = true; };
            dto.PropertyCollectionDtos = new ObservableCollection<PropertyCollectionDto> ( collectionDtos );
            Assert.AreEqual ( dto.PropertyCollectionDtos.Count, collectionDtos.Count );
            Assert.IsTrue ( propertyChanged );
        }

        private class TestDto : KeyedDataTransferObject
        {
            private PropertyDto _propertyDto;
            private ObservableCollection<PropertyCollectionDto> _propertyCollectionDto;

            public TestDto()
            {
                _propertyCollectionDto = new ObservableCollection<PropertyCollectionDto>();
            }

            public PropertyDto PropertyDto
            {
                get { return _propertyDto; }
                set { ApplyPropertyChange(ref _propertyDto, () => PropertyDto, value); }
            }

            public ObservableCollection<PropertyCollectionDto> PropertyCollectionDtos
            {
                get { return _propertyCollectionDto; }
                set { ApplyCollectionChange(ref _propertyCollectionDto, () => PropertyCollectionDtos, value); }
            }
        }

        private class PropertyCollectionDto : KeyedDataTransferObject
        {
            public string Name { get; set; }
        }

        private class PropertyDto : KeyedDataTransferObject
        {
            public string Name { get; set; }
        }
    }

    
}