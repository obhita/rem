using System;
using System.Windows;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rem.Ria.Infrastructure.View.Converter;

namespace Rem.Ria.Infrastructure.Tests.View.Converter
{
    [TestClass]
    public class RangeToVisibilityConverterTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            _converter = new RangeToVisibilityConverter();
        }

        #region Normal Use Cases

        [TestMethod]
        public void Convert_ParameterLessThanOrEqualsAValue()
        {
            var result = _converter.Convert(5, typeof(Visibility), "min, 5", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert(20, typeof(Visibility), "min, 5", null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ParameterGreaterThanOrEqualsAValue()
        {
            var result = _converter.Convert(5, typeof(Visibility), "5, max", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert(0, typeof(Visibility), "5, max", null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ParameterInValueRanges()
        {
            var result = _converter.Convert(5, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert(10, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert(0, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Collapsed, result);

            result = _converter.Convert(20, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ParameterIsNullString()
        {
            var result = _converter.Convert(null, typeof(Visibility), "null", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert(5, typeof(Visibility), "null", null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ParameterIsNull()
        {
            var result = _converter.Convert(null, typeof(Visibility), null, null);
            Assert.AreEqual(Visibility.Collapsed, result);

            result = _converter.Convert(5, typeof(Visibility), null, null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        #endregion

        #region Supported & NotSupported Value Types

        [TestMethod]
        public void Convert_ValueIsNull()
        {
            var result = _converter.Convert(null, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Collapsed, result);
        }

        [TestMethod]
        public void Convert_ValueIsInterger()
        {
            var result = _converter.Convert((short)5, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert((int)5, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert((long)5, typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ValueIsFload()
        {
            var result = _converter.Convert((float)5.1f, typeof(Visibility), "5.1, 10.1", null);
            Assert.AreEqual(Visibility.Visible, result);

            result = _converter.Convert((double)5.1, typeof(Visibility), "5.1, 10.1", null);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ValueIsDecimal()
        {
            var result = _converter.Convert((decimal)5.1m, typeof(Visibility), "5.1, 10.1", null);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        public void Convert_ValueIsNumberString()
        {
            var result = _converter.Convert("5", typeof(Visibility), "5, 10", null);
            Assert.AreEqual(Visibility.Visible, result);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Convert_ValueIsNonNumber()
        {
            _converter.Convert("Value", typeof(Visibility), "5, 10", null);
        }

        #endregion

        #region Exception Cases

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_SingleParameter_ArgumentException()
        {
            _converter.Convert(5, typeof(Visibility), "param", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Lower bound value should be 'min' or a number.")]
        public void Convert_LowerBoundInvalidParameter_ArgumentException()
        {
            _converter.Convert(5, typeof(Visibility), "param1, 10", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Upper bound value should be 'max' or a number.")]
        public void Convert_UpperBoundInvalidParameter_ArgumentException()
        {
            _converter.Convert(5, typeof(Visibility), "5, param2", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_ParameterNotUseComma_ArgumentException()
        {
            _converter.Convert(5, typeof(Visibility), "5-9", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_TooManyParameters_ArgumentException()
        {
            _converter.Convert(5, typeof(Visibility), "5, 9, 10", null);
        }

        #endregion

        private IValueConverter _converter;
    }
}
