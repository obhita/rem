using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain.Primitives;

namespace Pillar.Domain.Tests.Primitives
{
    [TestClass]
    public class TimeRangeTests
    {
        [TestMethod]
        public void Equals_GivenVariousScenarios_ReturnsAsExpected()
        {
            TimeRange first;
            TimeRange second;

            first = new TimeRange(null, null);
            second = new TimeRange(null, null);
            Assert.IsTrue(first.Equals(second));

            first = new TimeRange(null, null);
            second = new TimeRange(new TimeSpan(), null);
            Assert.IsFalse(first.Equals(second));

            var timeSpan = new TimeSpan();
            first = new TimeRange(timeSpan, null);
            second = new TimeRange(timeSpan, null);
            Assert.IsTrue(first.Equals(second));
        }
    }
}