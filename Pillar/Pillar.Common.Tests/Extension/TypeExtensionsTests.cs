using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Extension;

namespace Pillar.Common.Tests.Extension
{
    internal enum SomeEnum
    {
        SomeValue1,
        SomeValue2
    }

    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void TestIsNullable_GivenSbyte_IsNotNullable ()
        {
            var type = typeof( sbyte );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenByte_IsNotNullable ()
        {
            var type = typeof( byte );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenShort_IsNotNullable ()
        {
            var type = typeof( short );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenUShort_IsNotNullable ()
        {
            var type = typeof( ushort );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenInt_IsNotNullable ()
        {
            var type = typeof( int );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenUnt_IsNotNullable ()
        {
            var type = typeof( uint );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenLong_IsNotNullable ()
        {
            var type = typeof( long );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenUong_IsNotNullable ()
        {
            var type = typeof( ulong );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenFloat_IsNotNullable ()
        {
            var type = typeof( float );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenDouble_IsNotNullable ()
        {
            var type = typeof( double );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenDecimal_IsNotNullable ()
        {
            var type = typeof( decimal );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenBool_IsNotNullable ()
        {
            var type = typeof( bool );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenChar_IsNotNullable ()
        {
            var type = typeof( char );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenEnum_IsNotNullable ()
        {
            var type = typeof( SomeEnum );
            Assert.IsFalse ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableSbyte_IsNullable ()
        {
            var type = typeof( sbyte? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableByte_IsNullable ()
        {
            var type = typeof( byte? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableShort_IsNullable ()
        {
            var type = typeof( short? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableUShort_IsNullable ()
        {
            var type = typeof( ushort? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableInt_IsNullable ()
        {
            var type = typeof( int? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableUnt_IsNullable ()
        {
            var type = typeof( uint? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableLong_IsNullable ()
        {
            var type = typeof( long? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableUong_IsNullable ()
        {
            var type = typeof( ulong? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableFloat_IsNullable ()
        {
            var type = typeof( float? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableDouble_IsNullable ()
        {
            var type = typeof( double? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableDecimal_IsNullable ()
        {
            var type = typeof( decimal? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableBool_IsNullable ()
        {
            var type = typeof( bool? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableChar_IsNullable ()
        {
            var type = typeof( char? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenNullableEnum_IsNullable ()
        {
            var type = typeof( SomeEnum? );
            Assert.IsTrue ( type.IsNullable () );
        }

        [TestMethod]
        public void TestIsNullable_GivenString_IsNullable ()
        {
            var type = typeof( string );
            Assert.IsTrue ( type.IsNullable () );
        }
    }
}
