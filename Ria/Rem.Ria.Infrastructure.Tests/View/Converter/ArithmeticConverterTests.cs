using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.Infrastructure.View.Converter;

namespace Rem.Ria.Infrastructure.Tests.View.Converter
{
    [TestClass]
    public class ArithmeticConverterTests
    {
        private ArithmeticConverter _arithmeticConverter;

        [TestInitialize]
        public void TestInitilize ()
        {
            _arithmeticConverter = new ArithmeticConverter ();
        }

        [TestMethod]
        [ExpectedException ( typeof ( NotSupportedException ) )]
        public void IValueConverterConvertBack_NotSupportedException ()
        {
            _arithmeticConverter.ConvertBack ( null, typeof ( int ), null, null );
        }

        [TestMethod]
        [ExpectedException ( typeof ( NotSupportedException ) )]
        public void IMultiValueConverterConvertBack_NotSupportedException ()
        {
            _arithmeticConverter.ConvertBack ( null, new[] { typeof ( int ) }, null, null );
        }

        [TestMethod]
        public void IValueConverterConvert_SimpleAddition ()
        {
            object ret = _arithmeticConverter.Convert ( 5, typeof ( double ), "{0} + 1", null );
            Assert.AreEqual ( 6.0, ret );
        }

        [TestMethod]
        public void IMultiValueConverterConvert_SimpleAddition ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 5, 1 }, typeof ( double ), "{0} + {1}", null );
            Assert.AreEqual ( 6.0, ret );
        }

        [TestMethod]
        public void IValueConverterConvert_SimpleSubtract ()
        {
            object ret = _arithmeticConverter.Convert ( 5, typeof ( double ), "{0} - 1", null );
            Assert.AreEqual ( 4.0, ret );
        }

        [TestMethod]
        public void IMultiValueConverterConvert_SimpleSubtract ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 5, 1 }, typeof ( double ), "{0} - {1}", null );
            Assert.AreEqual ( 4.0, ret );
        }

        [TestMethod]
        public void IValueConverterConvert_SimpleMultiply ()
        {
            object ret = _arithmeticConverter.Convert ( 5, typeof ( double ), "{0} * 2", null );
            Assert.AreEqual ( 10.0, ret );
        }

        [TestMethod]
        public void IMultiValueConverterConvert_SimpleMultiply ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 5, 2 }, typeof ( double ), "{0} * {1}", null );
            Assert.AreEqual ( 10.0, ret );
        }

        [TestMethod]
        public void IValueConverterConvert_SimpleDivid ()
        {
            object ret = _arithmeticConverter.Convert ( 10, typeof ( double ), "{0} / 2", null );
            Assert.AreEqual ( 5.0, ret );
        }

        [TestMethod]
        public void IMultiValueConverterConvert_SimpleDivid ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 10, 2 }, typeof ( double ), "{0} / {1}", null );
            Assert.AreEqual ( 5.0, ret );
        }

        [TestMethod]
        public void IValueConverterConvert_SimpleModulo ()
        {
            object ret = _arithmeticConverter.Convert ( 10, typeof ( double ), "{0} % 3", null );
            Assert.AreEqual ( 1.0, ret );
        }

        [TestMethod]
        public void IMultiValueConverterConvert_SimpleModulo ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 10, 3 }, typeof ( double ), "{0} % {1}", null );
            Assert.AreEqual ( 1.0, ret );
        }

        [TestMethod]
        public void Convert_ComplexEquation ()
        {
            object ret = _arithmeticConverter.Convert ( new object[] { 5, 12, 3 }, typeof ( double ), "(({0} + 1) * 2) + 3 - {1} / {2}", null );
            Assert.AreEqual ( 11.0, ret );
        }
    }
}