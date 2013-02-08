using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Primitives;

namespace Pillar.Domain.Tests.Primitives
{
    [TestClass]
    public class DateRangeTests
    {
        [TestMethod]
        public void Constructor_NullEffectiveDate_Succeeds()
        {
            DateTime? effectiveDate = null;
            DateTime expirationDate = new DateTime ( 2000, 1, 1 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void Constructor_LaterThanExpiration_ThrowsException ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 2 );
            DateTime expirationDate = new DateTime ( 2000, 1, 1 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );
        }

        [TestMethod]
        public void Constructor_ValidDateRange_Succeeds ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime expirationDate = new DateTime ( 2000, 2, 1 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );
        }

        [TestMethod]
        public void Constructor_ValidDateRangeExpirationNull_Succeeds()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime? expirationDate = null;

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );
        }

        [TestMethod]
        public void Includes_DateIsBetween_ReturnsTrue()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime expirationDate = new DateTime ( 2000, 2, 1 );
            DateTime otherDate = new DateTime ( 2000, 1, 15 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );

            Assert.IsTrue ( dateRange.Includes ( otherDate ) );
        }

        [TestMethod]
        public void Includes_DateIsNotBetween_ReturnsTrue ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime expirationDate = new DateTime ( 2000, 2, 1 );
            DateTime otherDate = new DateTime ( 2000, 2, 15 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );

            Assert.IsFalse ( dateRange.Includes ( otherDate ) );
        }

        [TestMethod]
        public void Includes_IsOnExpiration_ReturnsTrue ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime expirationDate = new DateTime ( 2000, 2, 1 );
            DateTime otherDate = new DateTime ( 2000, 2, 1 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );

            Assert.IsTrue ( dateRange.Includes ( otherDate ) );
        }

        [TestMethod]
        public void Includes_IsOnEffectiveDate_ReturnsTrue ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime expirationDate = new DateTime ( 2000, 2, 1 );
            DateTime otherDate = new DateTime ( 2000, 1, 11 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );

            Assert.IsTrue ( dateRange.Includes ( otherDate ) );
        }

        [TestMethod]
        public void Includes_ExpirationIsNull_ReturnsTrue ()
        {
            DateTime effectiveDate = new DateTime ( 2000, 1, 1 );
            DateTime? expirationDate = null;
            DateTime otherDate = new DateTime ( 2000, 1, 15 );

            DateRange dateRange = new DateRange ( effectiveDate, expirationDate );

            Assert.IsTrue ( dateRange.Includes ( otherDate ) );
        }

        [TestMethod]
        public void Equals_GivenVariousScenarios_ReturnsAsExpected()
        {
            DateRange first;
            DateRange second;

            first = new DateRange(null, null);
            second = new DateRange(null, null);
            Assert.IsTrue(first.Equals(second));

            first = new DateRange(null, null);
            second = new DateRange(DateTime.Now, null);
            Assert.IsFalse(first.Equals(second));

            DateTime dateTime = DateTime.Now;
            first = new DateRange(dateTime, null);
            second = new DateRange(dateTime, null);
            Assert.IsTrue(first.Equals(second));
        }
    }
}