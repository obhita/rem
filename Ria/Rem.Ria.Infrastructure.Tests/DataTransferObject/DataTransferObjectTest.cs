using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.Infrastructure.DataTransferObject;
using Rem.Ria.Infrastructure.Tests.Fixture;

namespace Rem.Ria.Infrastructure.Tests.DataTransferObject
{
    [TestClass]
    public class DataTransferObjectTest
    {
        /// <summary>
        /// Makes sure that every DTO public property "setter" properly implements 
        /// INotifyPropertyChnaged interface by calling OnPropertyChange() 
        /// while providing parameter equal to property's name.
        /// </summary>
        [TestMethod]
        public void DataTransferObject_PublicPropertySetterCallsOnPropertyChanged_Succeeds ()
        {
            List<string> ignoredDtoProperties = new List<string> () { "Notifications" };

            IDtoFactory dtoFactory = new DtoFactory ();

            foreach ( AssemblyPart assemblyPart in Deployment.Current.Parts )
            {
                StreamResourceInfo sri =
                    Application.GetResourceStream (
                        new Uri ( assemblyPart.Source, UriKind.Relative ) );

                Assembly dtoAssembly = new AssemblyPart ().Load ( sri.Stream );

                var allDtos = dtoAssembly.GetTypes ()
                    .Where ( t => typeof ( EditableDataTransferObject ).IsAssignableFrom ( t ) && !t.IsAbstract )
                    .Select
                    ( t => new
                               {
                                   Dto = dtoFactory.CreateDto ( t ),
                                   PropSet = t.GetProperties ()
                               .Select
                               ( p => new
                                          {
                                              PropertyName = p.Name,
                                              PropertySetter = p.GetSetMethod ()
                                          }
                               )
                               .Where ( p => p.PropertySetter != null
                                             && !ignoredDtoProperties.Contains ( p.PropertyName ) )
                               }
                    );

                foreach ( var d in allDtos )
                {
                    INotifyPropertyChanged inpc = d.Dto;

                    foreach ( var p in d.PropSet )
                    {
                        // Setup event handler for the property being examined
                        string changedPropertyName = String.Empty;
                        // use named variable (as opposed to anonymous delegate) so we can unsubscribe
                        PropertyChangedEventHandler inpcOnPropertyChanged =
                            ( obj, e ) => changedPropertyName = e.PropertyName;
                        inpc.PropertyChanged += inpcOnPropertyChanged;

                        // Simulate property change by invoking setter directly
                        p.PropertySetter.Invoke ( d.Dto, new object[] { null } );

                        Assert.AreEqual (
                            p.PropertyName,
                            changedPropertyName,
                            String.Format (
                                "Offending DTO type::property {0}::{1}",
                                d.Dto.GetType (),
                                p.PropertyName ) );

                        // Reset event handler
                        inpc.PropertyChanged -= inpcOnPropertyChanged;
                    }
                }
            }
        }

        // NOTE: The server side AbstractDataTransferObject has already been tested in REM.Infrastructure.Tests.
        //       These tests are meant to validate the client side partial class add-ins.

        // IValidatedObject Tests
        // public void AddDataErrorInfo ( DataErrorInfo dataErrorInfo )
        // -- AddObjectLevelError 
        //    -- ErrorsChangedEventShouldFire with a null/empty property name
        //    -- GetErrors should return the added DataErrorInfo class
        // -- AddPropertyLevelError
        //    -- ErrorsChangedEventShouldFire with the given property name
        //    -- GetErrors should return the added DataErrorInfo class
        // -- AddCrossPropertyLevelError
        //    -- ErrorsChangedEventShouldFire for 'every' given property name (should fire multiple times)
        //    -- GetErrors should return the added DataErrorInfo class
        [TestMethod]
        public void AddDataErrorInfo_AddingAnObjectLevelError_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error );
            string propertyName = "Dummy";
            personDto.ErrorsChanged += ( s, e ) => { propertyName = e.PropertyName; };

            personDto.AddDataErrorInfo ( dataErrorInfo );

            Assert.IsTrue ( string.IsNullOrEmpty ( propertyName ) );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingAnObjectLevelError_GetErrorsShouldReturnGivenError ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error );
            IEnumerable<DataErrorInfo> errors = new List<DataErrorInfo> ();

            personDto.ErrorsChanged +=
                ( s, e ) => { errors = personDto.GetErrors ( e.PropertyName ) as IEnumerable<DataErrorInfo>; };

            personDto.AddDataErrorInfo ( dataErrorInfo );

            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );
            List<DataErrorInfo> referenceList = new List<DataErrorInfo> { dataErrorInfo };

            CollectionAssert.AreEqual ( returnedList, referenceList );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingAPropertyLevelError_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error, "FirstName" );
            string propertyName = "Dummy";
            personDto.ErrorsChanged += ( s, e ) => { propertyName = e.PropertyName; };

            personDto.AddDataErrorInfo ( dataErrorInfo );

            Assert.AreEqual ( propertyName, "FirstName" );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingAPropertyLevelError_GetErrorsShouldReturnGivenError ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName" );
            IEnumerable<DataErrorInfo> errors = new List<DataErrorInfo> ();

            personDto.ErrorsChanged +=
                ( s, e ) => { errors = personDto.GetErrors ( e.PropertyName ) as IEnumerable<DataErrorInfo>; };

            personDto.AddDataErrorInfo ( dataErrorInfo );

            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );
            List<DataErrorInfo> referenceList = new List<DataErrorInfo> { dataErrorInfo };

            CollectionAssert.AreEqual ( returnedList, referenceList );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingACrossPropertyLevelError_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error, "FirstName", "LastName" );
            List<string> propertyNames = new List<string> { "FirstName", "LastName" };

            personDto.ErrorsChanged += ( s, e ) => RemoveFromList ( propertyNames, e.PropertyName );

            personDto.AddDataErrorInfo ( dataErrorInfo );

            Assert.IsTrue ( propertyNames.Count == 0 );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingACrossPropertyLevelError_GetErrorsShouldReturnGivenErrorForFirstNameProperty
            ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName", "LastName" );

            personDto.AddDataErrorInfo ( dataErrorInfo );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "FirstName" ) as IEnumerable<DataErrorInfo>;

            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );
            List<DataErrorInfo> referenceList = new List<DataErrorInfo> { dataErrorInfo };

            CollectionAssert.AreEqual ( returnedList, referenceList );
        }

        [TestMethod]
        public void AddDataErrorInfo_AddingACrossPropertyLevelError_GetErrorsShouldReturnGivenErrorForLastNameProperty ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName", "LastName" );

            personDto.AddDataErrorInfo ( dataErrorInfo );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "LastName" ) as IEnumerable<DataErrorInfo>;

            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );
            List<DataErrorInfo> referenceList = new List<DataErrorInfo> { dataErrorInfo };

            CollectionAssert.AreEqual ( returnedList, referenceList );
        }

        // public void RemoveDataErrorInfo ( string propertyName )
        // -- RemoveObjectLevelError 
        //    -- ErrorsChangedEventShouldFire with a null/empty property name
        //    -- GetErrors should no longer return the removed error
        // -- RemovePropertyLevelError
        //    -- ErrorsChangedEventShouldFire with the given property name
        //    -- GetErrors should no longer return the removed error
        // -- RemoveCrossPropertyLevelError
        //    -- ErrorsChangedEventShouldFire for 'every' given property name (should fire multiple times)
        //    -- GetErrors should no longer return the removed error

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingAnObjectLevelError_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error );
            string propertyName = "Dummy";

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.ErrorsChanged += ( s, e ) => { propertyName = e.PropertyName; };
            personDto.RemoveDataErrorInfo ( String.Empty );

            Assert.IsTrue ( string.IsNullOrEmpty ( propertyName ) );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingAnObjectLevelError_GetErrorsShouldReturnGivenError ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error );
            IEnumerable<DataErrorInfo> errors = new List<DataErrorInfo> ();

            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.ErrorsChanged +=
                ( s, e ) => { errors = personDto.GetErrors ( e.PropertyName ) as IEnumerable<DataErrorInfo>; };

            personDto.RemoveDataErrorInfo ( String.Empty );

            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( returnedList.Count == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingAPropertyLevelError_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error, "FirstName" );
            string propertyName = "Dummy";

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.ErrorsChanged += ( s, e ) => { propertyName = e.PropertyName; };
            personDto.RemoveDataErrorInfo ( "FirstName" );

            Assert.AreEqual ( propertyName, "FirstName" );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingAPropertyLevelError_GetErrorsShouldReturnGivenError ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName" );

            personDto.AddDataErrorInfo ( dataErrorInfo );

            personDto.RemoveDataErrorInfo ( "FirstName" );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "FirstName" ) as IEnumerable<DataErrorInfo>;
            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( returnedList.Count == 0 );
        }

        [TestMethod]
        public void
            RemoveDataErrorInfo_RemovingACrossPropertyLevelError_ErrorsChangedEventShouldFireWhenFirstNameRemoved ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error, "FirstName", "LastName" );
            List<string> propertyNames = new List<string> { "FirstName", "LastName" };

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.ErrorsChanged += ( s, e ) => RemoveFromList ( propertyNames, e.PropertyName );
            personDto.RemoveDataErrorInfo ( "FirstName" );

            Assert.IsTrue ( propertyNames.Count == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingACrossPropertyLevelError_ErrorsChangedEventShouldFireWhenLastNameRemoved
            ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "EntityLevelError", ErrorLevel.Error, "FirstName", "LastName" );
            List<string> propertyNames = new List<string> { "FirstName", "LastName" };

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.ErrorsChanged += ( s, e ) => RemoveFromList ( propertyNames, e.PropertyName );
            personDto.RemoveDataErrorInfo ( "LastName" );

            Assert.IsTrue ( propertyNames.Count == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingACrossPropertyLevelErrorFirstName_GetErrorsShouldReturnEmptySet ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName", "LastName" );

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.RemoveDataErrorInfo ( "FirstName" );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "FirstName" ) as IEnumerable<DataErrorInfo>;
            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( returnedList.Count () == 0 );
        }

        [TestMethod]
        public void RemoveDataErrorInfo_RemovingACrossPropertyLevelErrorLastName_GetErrorsShouldReturEmptySet ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var dataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName", "LastName" );

            personDto.AddDataErrorInfo ( dataErrorInfo );
            personDto.RemoveDataErrorInfo ( "FirstName" );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "LastName" ) as IEnumerable<DataErrorInfo>;
            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( returnedList.Count () == 0 );
        }

        // public void ClearAllDataErrorInfo ()
        // -- ErrorsChangedEventShouldFire a total of once for 'every' property that has a registered error
        //    and only once for object level errors
        //    -- this means that if there are two object level errors the event should only fire once for 
        //       the object level, same goes for property level
        // -- GetErrors should return the empty set

        [TestMethod]
        public void ClearAllDataErrorInfo_ErrorsChangedEventShouldFire ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var objectDataErrorInfo1 = new DataErrorInfo ( "ObjectLevelError1", ErrorLevel.Error );
            var objectDataErrorInfo2 = new DataErrorInfo ( "ObjectLevelError2", ErrorLevel.Error );
            var propertyDataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName" );
            var crossDataErrorInfo = new DataErrorInfo ( "CrossPropertyLevelError", ErrorLevel.Error, "FirstName",
                                                         "LastName" );

            HashSet<string> referenceNames = new HashSet<string> { "FirstName", "", "LastName" };
            HashSet<string> propertyNames = new HashSet<string> ();

            personDto.AddDataErrorInfo ( objectDataErrorInfo1 );
            personDto.AddDataErrorInfo ( objectDataErrorInfo2 );
            personDto.AddDataErrorInfo ( propertyDataErrorInfo );
            personDto.AddDataErrorInfo ( crossDataErrorInfo );
            personDto.ErrorsChanged += ( s, e ) => propertyNames.Add ( e.PropertyName );

            personDto.ClearAllDataErrorInfo ();

            AssertSetsEqual ( referenceNames, propertyNames );
        }

        [TestMethod]
        public void ClearAllDataErrorInfo_GetErrorsShouldReturnEmptySet ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            var objectDataErrorInfo1 = new DataErrorInfo ( "ObjectLevelError1", ErrorLevel.Error );
            var objectDataErrorInfo2 = new DataErrorInfo ( "ObjectLevelError2", ErrorLevel.Error );
            var propertyDataErrorInfo = new DataErrorInfo ( "PropertyLevelError", ErrorLevel.Error, "FirstName" );
            var crossDataErrorInfo = new DataErrorInfo ( "CrossPropertyLevelError", ErrorLevel.Error, "FirstName",
                                                         "LastName" );

            personDto.AddDataErrorInfo ( objectDataErrorInfo1 );
            personDto.AddDataErrorInfo ( objectDataErrorInfo2 );
            personDto.AddDataErrorInfo ( propertyDataErrorInfo );
            personDto.AddDataErrorInfo ( crossDataErrorInfo );

            personDto.ClearAllDataErrorInfo ();

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( null ) as IEnumerable<DataErrorInfo>;
            List<DataErrorInfo> returnedList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( returnedList.Count () == 0 );
        }

        [TestMethod]
        public void RaiseErrorsChanged_GivenAValidPropertyName_ErrorsChangedEventReportsPropertyChange ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            string name = string.Empty;
            personDto.ErrorsChanged += ( s, e ) => name = e.PropertyName;
            personDto.RaiseErrorsChanged ( "FirstName" );

            Assert.AreSame ( name, "FirstName" );
        }

        [TestMethod]
        [ExpectedException ( typeof ( PropertyNotFoundException ) )]
        public void RaiseErrorsChanged_GivenAnInvalidPropertyName_RaisesPropertyNotFoundException ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            personDto.RaiseErrorsChanged ( "Dummy" );
        }

        // public IEnumerable GetErrors ( string propertyName )
        // -- calling geterrors with a property name when there is only an object level error
        [TestMethod]
        public void GetErrors_WithAPropertyNameButOnlyAnObjectLevelErrorExists_ReturnAnEmptyList ()
        {
            var personDto = new TestPersonDto { FirstName = "John", LastName = "Wayne" };
            DataErrorInfo dataErrorInfo = new DataErrorInfo ( "Error", ErrorLevel.Error );
            personDto.AddDataErrorInfo ( dataErrorInfo );

            IEnumerable<DataErrorInfo> errors = personDto.GetErrors ( "FirstName" ) as IEnumerable<DataErrorInfo>;
            IList<DataErrorInfo> errorList = new List<DataErrorInfo> ( errors );

            Assert.IsTrue ( errorList.Count() == 0 );
        }

        #region Helper Methods

        private static void AssertSetsEqual ( HashSet<string> a, HashSet<string> b )
        {
            if ( ( a == null ) && ( b == null ) )
            {
                return;
            }
            else if ( ( a == null ) || ( b == null ) )
            {
                throw new ArgumentException ( "Sets are not equal" );
            }
            else
            {
                if ( a.Count != b.Count )
                {
                    throw new ArgumentException ( "Sets are not equal" );
                }

                foreach ( string s in a )
                {
                    if ( !b.Contains ( s ) )
                    {
                        throw new ArgumentException ( "Sets are not equal" );
                    }
                }
            }
        }

        private static void RemoveFromList ( IList<string> list, string value )
        {
            if ( list.Contains ( value ) )
            {
                list.Remove ( value );
            }
            else
            {
                throw new ArgumentException ( "not in list" );
            }
        }

        #endregion
    }
}