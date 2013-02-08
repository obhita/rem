// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensionsTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Extension;

namespace Pillar.Common.Tests.Extension
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void DistictBy_GivenListOfTuples_ReturnsDistictList ()
        {
            var listOfTuples = new[]
                {
                    new Tuple<int, string> ( 1, "item1" ), new Tuple<int, string> ( 2, "item2" ), new Tuple<int, string> ( 3, "item3" ),
                    new Tuple<int, string> ( 1, "item4" ), new Tuple<int, string> ( 2, "item5" ), new Tuple<int, string> ( 3, "item6" ),
                };

            var distictList = listOfTuples.DistinctBy ( t => t.Item1 ).Select ( t => t.Item2 ).ToArray ();

            var referenceArray = new[] { "item1", "item2", "item3" };
            CollectionAssert.AreEqual ( distictList, referenceArray );
        }

        [TestMethod]
        public void ForEach_GivenListOfNumbers_SumsThemUp ()
        {
            var listOfNumbers = new[] { 1, 1, 1 };
            var sum = 0;
            listOfNumbers.ForEach ( e => sum = sum + e );

            Assert.AreEqual ( 3, sum );
        }
    }
}
