using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.FluentRuleEngine;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Clinical.VisitModule;

namespace Rem.Domain.Clinical.Tests.TedsModule
{
    [TestClass]
    public class TedsAdmissionInterviewTests
    {
        //[ExpectedException(typeof(ArgumentException))]
        //[TestMethod]
        //public void ReviseSystemDataSet_GivenNullSystemDataSet_ThrowsException()
        //{
        //    // Setup
        //    TedsAdmissionKeyFields tedsAdmissionKeyFields = null;
        //    var sut = new TedsAdmissionInterview ( new Mock<Visit> ().Object, new Mock<ActivityType> ().Object );

        //    // Exercise
        //    sut.ReviseTedsAdmissionKeyFields ( tedsAdmissionKeyFields );

        //    // Verify
        //}
    }
}
