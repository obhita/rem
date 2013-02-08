using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata.Dtos;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Tests.Service.DataTransferObject
{
    /// <summary>
    /// Contains unit tests that all DTO objects have to pass (to be considered valid - at least at the very basic level).
    /// </summary>
    [TestClass]
    public class DataTransferObjectTest
    {
        [TestMethod]
        public void AddDataErrorInfo_AddsObjectLevelError_Succeeds ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            IList referenceCollection = new ArrayList { dataErrorInfo };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddsPropertyLevelError_Succeeds ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName" );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            IList referenceCollection = new ArrayList { dataErrorInfo };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddsCrossPropertyLevelError_Succeeds ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "CrossPropertyLevelError", ErrorLevel.Error, "FirstName", "LastName" );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            IList referenceCollection = new ArrayList{ dataErrorInfo };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        [ExpectedException ( typeof ( PropertyNotFoundException ) )]
        public void AddDataErrorInfo_NonExistingSingleProperty_ThrowsPropertyNotFoundException ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "NonExistingProperty", ErrorLevel.Error, "Foo" );
            personDto.AddDataErrorInfo ( dataErrorInfo );
        }

        [TestMethod]
        [ExpectedException ( typeof ( PropertyNotFoundException ) )]
        public void DataTransferObject_NonExistingCrossProperty_ThrowsPropertyNotFoundException ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "NonExistingCrossProperty", ErrorLevel.Error, "FirstName", "Foo" );
            personDto.AddDataErrorInfo ( dataErrorInfo );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_ProvidedAnEmptyProperty_RemovesObjectLevelErrors ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "SomeErrorMessage", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.RemoveDataErrorInfo ( "" );

            Assert.IsTrue ( personDto.DataErrorInfoCollection.Count() == 0 );
        }

        [TestMethod]
        [ExpectedException ( typeof ( PropertyNotFoundException ) )]
        public void RemoveDataErrorInfo_ProvidedANonExistentProperty_ThrowAPropertyNotFoundException ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "SomeErrorMessage", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.RemoveDataErrorInfo ( "Foo" );

            Assert.IsTrue ( personDto.DataErrorInfoCollection.Count() == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_OneObjectLevelError_RemovesObjectLevelError ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "SomeErrorMessage", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.RemoveDataErrorInfo ( "" );

            Assert.IsTrue ( personDto.DataErrorInfoCollection.Count() == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_MoreThanOneObjectLevelError_RemovesObjectLevelErrors ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo1 = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error );
            var dataErrorInfo2 = new DataErrorInfo ( "SomeErrorMessage2", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo1 );
            personDto.AddDataErrorInfo ( dataErrorInfo2 );

            personDto.RemoveDataErrorInfo ( "" );

            Assert.IsTrue ( personDto.DataErrorInfoCollection.Count() == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_HasPropertyLevelAndObjectLevelErrors_RemovesObjectLevelErrors ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var propertyDataErrorInfo = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "FirstName" );
            var objectDataErrorInfo = new DataErrorInfo ( "SomeErrorMessage2", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( propertyDataErrorInfo );
            personDto.AddDataErrorInfo ( objectDataErrorInfo );

            personDto.RemoveDataErrorInfo ( "" );
            IList referenceCollection = new ArrayList { propertyDataErrorInfo };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_HasObjectLevelErrorsAndNonObjectLevelErrors_RemovesObjectLevelErrors ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var propertyDataErrorInfo = new DataErrorInfo ( "PropErrorMessage", ErrorLevel.Error, "FirstName" );
            var crossPropertyDataErrorInfo = new DataErrorInfo ( "CrossPropErrorMessage", ErrorLevel.Error, "FirstName",
                                                                 "LastName" );
            var objectDataErrorInfo = new DataErrorInfo ( "ObjectErrorMessage", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( propertyDataErrorInfo );
            personDto.AddDataErrorInfo ( crossPropertyDataErrorInfo );
            personDto.AddDataErrorInfo ( objectDataErrorInfo );

            personDto.RemoveDataErrorInfo ( "" );

            IList referenceCollection = new ArrayList { propertyDataErrorInfo, crossPropertyDataErrorInfo };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_OnePropertyLevelError_RemovesPropertyLevelError ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "FirstName" );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.RemoveDataErrorInfo ( "FirstName" );

            Assert.IsTrue ( personDto.DataErrorInfoCollection.Count() == 0 );
        }

        [TestMethod]
        public void
            RemoveDataErrorInfo_PropertyLevelErrorAndCrossPropertyLevelError_RemovesAllErrorsWithSamePropertyName ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo1 = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "FirstName" );
            var dataErrorInfo2 = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "FirstName", "LastName" );
            var dataErrorInfo3 = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "LastName" );
            personDto.AddDataErrorInfo ( dataErrorInfo1 );
            personDto.AddDataErrorInfo ( dataErrorInfo2 );
            personDto.AddDataErrorInfo ( dataErrorInfo3 );

            personDto.RemoveDataErrorInfo ( "FirstName" );

            IList referenceCollection = new ArrayList { dataErrorInfo3 };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_CrossPropertyLevelError_RemovesWhenPropertyNameIsRemoved ()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo1 = new DataErrorInfo ( "SomeErrorMessage1", ErrorLevel.Error, "FirstName", "LastName" );
            var dataErrorInfo2 = new DataErrorInfo ( "SomeErrorMessage2", ErrorLevel.Error, "FirstName", "LastName" );
            var dataErrorInfo3 = new DataErrorInfo ( "SomeErrorMessage3", ErrorLevel.Error, "LastName" );
            personDto.AddDataErrorInfo ( dataErrorInfo1 );
            personDto.AddDataErrorInfo ( dataErrorInfo2 );
            personDto.AddDataErrorInfo ( dataErrorInfo3 );

            personDto.RemoveDataErrorInfo ( "FirstName" );

            IList referenceCollection = new ArrayList { dataErrorInfo3 };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_CrossPropertyLevelErrorAndObjectLevelError_RemovesCrossPropertyLevelErrorWhenPropertyNameIsRemoved()
        {
            var personDto = new PersonDto{ FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo1 = new DataErrorInfo("SomeErrorMessage1", ErrorLevel.Error, "FirstName", "LastName");
            var dataErrorInfo2 = new DataErrorInfo("SomeErrorMessage2", ErrorLevel.Error);
            personDto.AddDataErrorInfo(dataErrorInfo1);
            personDto.AddDataErrorInfo(dataErrorInfo2);

            personDto.RemoveDataErrorInfo("FirstName");

            IList referenceCollection = new ArrayList{ dataErrorInfo2 };

            CollectionAssert.AreEqual ( referenceCollection, personDto.DataErrorInfoCollection.ToList () );
        }

        [TestMethod]
        public void ClearAllDataErrorInfo_HasObjectLevelErrorsAndPropertyLevelErrorsAndCrossPropertyLevelErrors_ClearsAllErrors()
        {
            var personDto = new PersonDto{ FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo1 = new DataErrorInfo("SomeErrorMessage1", ErrorLevel.Error, "FirstName", "LastName");
            var dataErrorInfo2 = new DataErrorInfo("SomeErrorMessage2", ErrorLevel.Error);
            var dataErrorInfo3 = new DataErrorInfo("SomeErrorMessage3", ErrorLevel.Error, "FirstName" );
            personDto.AddDataErrorInfo(dataErrorInfo1);
            personDto.AddDataErrorInfo(dataErrorInfo2);
            personDto.AddDataErrorInfo(dataErrorInfo3);

            personDto.ClearAllDataErrorInfo ();

            Assert.IsTrue(personDto.DataErrorInfoCollection.Count() == 0);
        }

        [TestMethod]
        public void MetadataDto_SetWithDifferentMetadataDto_FirePropertyChangedEvent()
        {
            bool isPropertyChangedEventFired = false;
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            personDto.PropertyChanged += ( sender, e ) => { isPropertyChangedEventFired = true; };
            
            personDto.MetadataDto = new MetadataDto ( "Default" );

            Assert.IsTrue ( isPropertyChangedEventFired );
        }

        [TestMethod]
        public void MetadataDto_SetWithDifferentMetadataDto_IsDirtyNotSetToTrue()
        {
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };

            personDto.MetadataDto = new MetadataDto("Default");

            Assert.IsFalse ( personDto.IsDirty );
        }

        [TestMethod]
        public void MetadataDto_SetWithSameMetadataDto_NotFirePropertyChangedEvent()
        {
            bool isPropertyChangedEventFired = false;
            var metadataDto = new MetadataDto("Default");
            var personDto = new PersonDto { FirstName = "John", LastName = "Wayne" };
            personDto.MetadataDto = metadataDto;
            personDto.PropertyChanged += (sender, e) => { isPropertyChangedEventFired = true; };

            personDto.MetadataDto = metadataDto;

            Assert.IsFalse(isPropertyChangedEventFired);
        }
   }
}