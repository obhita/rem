using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Extension;
using Pillar.Common.Tests.Fixture;

namespace Pillar.Common.Tests.Extension
{
    [TestClass]
    public class DeepCopyExtensionTests
    {
        [TestMethod]
        public void TestCopy_Primitives_Succeeds ()
        {
            const string Original = "Test Copy";
            var copy = Original.DeepCopy ();

            Assert.AreEqual ( Original, copy );
        }

        [TestMethod]
        public void TestCopy_TestPhoneDto_Succeeds ()
        {
            var phoneDto = new TestPhoneDto { PhoneNumber = "123-456-7890" };

            var copyPhoneDto = phoneDto.DeepCopy ();

            Assert.AreEqual ( phoneDto.PhoneNumber, copyPhoneDto.PhoneNumber );

            phoneDto.PhoneNumber = "new";

            Assert.AreEqual ( "123-456-7890", copyPhoneDto.PhoneNumber );
        }

        [TestMethod]
        public void TestCopy_TestPersonDto_Succeeds ()
        {
            var personPersonDto = new TestPersonDto { BirthDate = DateTime.Today.AddDays ( -1 ), FirstName = "Donald", LastName = "Duck" };

            var phoneDto = new TestPhoneDto { PhoneNumber = "123-456-7890" };

            personPersonDto.Phones.Add ( phoneDto );

            var copyPersonDto = personPersonDto.DeepCopy ();

            Assert.AreEqual ( personPersonDto.FirstName, copyPersonDto.FirstName );
            Assert.AreEqual ( personPersonDto.LastName, copyPersonDto.LastName );
            Assert.AreEqual ( personPersonDto.BirthDate, copyPersonDto.BirthDate );
            Assert.AreEqual ( personPersonDto.Phones.Count, copyPersonDto.Phones.Count );
            Assert.AreNotEqual ( personPersonDto.Phones[0].GetHashCode (), copyPersonDto.Phones[0].GetHashCode () );

            //change the phone number object
            var newPhoneDto = new TestPhoneDto { PhoneNumber = "987-654-3210" };

            personPersonDto.Phones[0] = newPhoneDto;

            Assert.AreEqual ( "123-456-7890", copyPersonDto.Phones[0].PhoneNumber );
        }

        [TestMethod]
        public void TestCopy_Collections ()
        {
            IList<TestPhoneDto> phones = new List<TestPhoneDto> ();
            phones.Add ( new TestPhoneDto { PhoneNumber = "1" } );
            phones.Add ( new TestPhoneDto { PhoneNumber = "2" } );
            phones.Add ( new TestPhoneDto { PhoneNumber = "3" } );
            phones.Add ( new TestPhoneDto { PhoneNumber = "4" } );
            phones.Add ( new TestPhoneDto { PhoneNumber = "5" } );

            var copy = phones.DeepCopy ();

            Assert.AreEqual ( phones.Count, copy.Count );
            Assert.AreEqual ( 1, copy.Where ( p => p.PhoneNumber == "1" ).Count () );
            Assert.AreEqual ( 1, copy.Where ( p => p.PhoneNumber == "2" ).Count () );
            Assert.AreEqual ( 1, copy.Where ( p => p.PhoneNumber == "3" ).Count () );
            Assert.AreEqual ( 1, copy.Where ( p => p.PhoneNumber == "4" ).Count () );
            Assert.AreEqual ( 1, copy.Where ( p => p.PhoneNumber == "5" ).Count () );
        }
    }
}
