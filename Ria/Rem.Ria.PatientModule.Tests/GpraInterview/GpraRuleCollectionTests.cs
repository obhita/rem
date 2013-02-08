using System;
using System.Collections.Generic;
using System.Net;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.FluentRuleEngine.Constraints;
using Rem.Ria.PatientModule.Web.GpraInterview;

namespace Rem.Ria.PatientModule.Tests.GpraInterview
{
    [TestClass]
    public class GpraRuleCollectionTests
    {
        [TestMethod]
        public void testMethodName ()
        {
            var constraint = new InlineConstraint<GpraNonResponseTypeDto<int?>>(p => Comparer<GpraNonResponseTypeDto<int?>>.Default.Compare(p, 5) == 0);
            var isCompliant = constraint.IsCompliant ( new GpraNonResponseTypeDto<int?> { Value = 5 }, null );
        }

    }
}
