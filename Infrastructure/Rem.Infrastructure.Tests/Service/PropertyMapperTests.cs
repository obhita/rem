using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Infrastructure.Tests.Service.Fixtures;

namespace Rem.Infrastructure.Tests.Service
{
    [TestClass]
    public class PropertyMapperTests
    {
        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void PropertyMapper_GivenNullEntity_ThrowsArgumentException ()
        {
            var validatedObject = new Mock<IValidatedObject> ();
            
            new PropertyMapper<PropertyMapperTestEntity> ( null, validatedObject.Object );
        }

        // validate that MapProperty throws an exception when an int is given for a string
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MapProperty_GivenANonPropertyExpression_ThrowsArgumentException()
        {
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity ();

            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);

            propertyMapper.MapProperty ( e => e.SomeMethod(), "SomeString" );
        }

        [TestMethod]
        public void MapProperty_GivenAllProperParameters_Succeeds ()
        {
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();

            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);

            var returnValue = propertyMapper.MapProperty(e => e.StringProperty, "SomeString");

            Assert.AreSame( propertyMapper, returnValue );
        }

        [TestMethod]
        public void Map_GivenNoPropertyValueListItems_Succeeds()
        {
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();
            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);

            bool returnValue = propertyMapper.Map();

            Assert.IsTrue(returnValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Map_GivenAReadOnlyPropertyInfo_ThrowsArgumentException()
        {
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();
            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);
            propertyMapper.MapProperty(p => p.ReadOnlyIntProperty, 1);

            propertyMapper.Map ();
        }

        [TestMethod]
        public void Map_GivenAWritablePropertyInfo_Succeeds()
        {
            var stringValue = "SomeString";
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();
            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);
            propertyMapper.MapProperty(p => p.StringProperty, stringValue);

            bool returnValue = propertyMapper.Map();

            Assert.IsTrue(returnValue);
            Assert.AreEqual(entity.StringProperty, stringValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Map_GivenAPropertyInfoWithPrivateSet_Succeeds()
        {
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();
            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);
            propertyMapper.MapProperty(p => p.ObjectPropertyWithPrivateSetter, null);

            bool returnValue = propertyMapper.Map();
        }

        [TestMethod]
        public void Map_GivenMultipleItemsInPropertyValueList_Succeeds()
        {
            var stringValue = "SomeString";
            var boolValue = true;
            var objectValue = new object ();
            var validatedObject = new Mock<IValidatedObject>();
            var entity = new PropertyMapperTestEntity();
            var propertyMapper = new PropertyMapper<PropertyMapperTestEntity>(entity, validatedObject.Object);
            propertyMapper.MapProperty(p => p.StringProperty, stringValue);
            propertyMapper.MapProperty(p => p.BooleanProperty, boolValue);

            bool returnValue = propertyMapper.Map();

            Assert.IsTrue(returnValue);
            Assert.AreEqual(entity.StringProperty, stringValue);
            Assert.AreEqual(entity.BooleanProperty, boolValue);
        }

        [TestMethod]
        public void Map_GivenNullValueForNotNullableProperty_ShowsAnError()
        {
            var entity = new PropertyMapperTestEntity ();
            var validatedObject = new ValidatedObjectFixture ();

            new PropertyMapper<PropertyMapperTestEntity> ( entity, validatedObject )
                .MapProperty ( p => p.NotNullableProperty, null )
                .Map ();
            
            Assert.AreEqual( validatedObject.DataErrorInfoCollection.FirstOrDefault().Message, entity.NullArgumentMessage );
        }
    }
}