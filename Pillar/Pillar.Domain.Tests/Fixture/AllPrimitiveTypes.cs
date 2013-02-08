namespace Pillar.Domain.Tests.Fixture
{
    public class AllPrimitiveTypes : SimpleAggregateRoot
    {
        // Integer Types
        private bool _boolProperty;
        private byte _byteProperty;
        private char _charProperty;
        private decimal _decimalProperty;
        private double _doubleProperty;
        private float _floatProperty;
        private int _intProperty;
        private long _longProperty;
        private bool? _nullableBoolProperty;
        private byte? _nullableByteProperty;
        private char? _nullableCharProperty;
        private decimal? _nullableDecimalProperty;
        private double? _nullableDoubleProperty;
        private float? _nullableFloatProperty;
        private int? _nullableIntProperty;
        private long? _nullableLongProperty;
        private sbyte? _nullableSByteProperty;
        private short? _nullableShortProperty;
        private uint? _nullableUIntProperty;
        private ulong? _nullableULongProperty;
        private ushort? _nullableUShortProperty;
        private sbyte _sByteProperty;
        private short _shortProperty;
        private string _stringProperty;
        private uint _uIntProperty;
        private ulong _uLongProperty;
        private ushort _uShortProperty;

        public sbyte SByteProperty
        {
            get { return _sByteProperty; }
            set { ApplyPropertyChange ( ref _sByteProperty, () => SByteProperty, value ); }
        }

        public byte ByteProperty
        {
            get { return _byteProperty; }
            set { ApplyPropertyChange ( ref _byteProperty, () => ByteProperty, value ); }
        }

        public short ShortProperty
        {
            get { return _shortProperty; }
            set { ApplyPropertyChange ( ref _shortProperty, () => ShortProperty, value ); }
        }

        public ushort UShortProperty
        {
            get { return _uShortProperty; }
            set { ApplyPropertyChange ( ref _uShortProperty, () => UShortProperty, value ); }
        }

        public int IntProperty
        {
            get { return _intProperty; }
            set { ApplyPropertyChange ( ref _intProperty, () => IntProperty, value ); }
        }

        public uint UIntProperty
        {
            get { return _uIntProperty; }
            set { ApplyPropertyChange ( ref _uIntProperty, () => UIntProperty, value ); }
        }

        public long LongProperty
        {
            get { return _longProperty; }
            set { ApplyPropertyChange ( ref _longProperty, () => LongProperty, value ); }
        }

        public ulong ULongProperty
        {
            get { return _uLongProperty; }
            set { ApplyPropertyChange ( ref _uLongProperty, () => ULongProperty, value ); }
        }

        // Floating Point Types

        public float FloatProperty
        {
            get { return _floatProperty; }
            set { ApplyPropertyChange ( ref _floatProperty, () => FloatProperty, value ); }
        }

        public double DoubleProperty
        {
            get { return _doubleProperty; }
            set { ApplyPropertyChange ( ref _doubleProperty, () => DoubleProperty, value ); }
        }

        // Decimal Type

        public decimal DecimalProperty
        {
            get { return _decimalProperty; }
            set { ApplyPropertyChange ( ref _decimalProperty, () => DecimalProperty, value ); }
        }

        // Other Types

        public bool BoolProperty
        {
            get { return _boolProperty; }
            set { ApplyPropertyChange ( ref _boolProperty, () => BoolProperty, value ); }
        }

        public char CharProperty
        {
            get { return _charProperty; }
            set { ApplyPropertyChange ( ref _charProperty, () => CharProperty, value ); }
        }

        // Integer Types

        public sbyte? NullableSByteProperty
        {
            get { return _nullableSByteProperty; }
            set { ApplyPropertyChange ( ref _nullableSByteProperty, () => NullableSByteProperty, value ); }
        }

        public byte? NullableByteProperty
        {
            get { return _nullableByteProperty; }
            set { ApplyPropertyChange ( ref _nullableByteProperty, () => NullableByteProperty, value ); }
        }

        public short? NullableShortProperty
        {
            get { return _nullableShortProperty; }
            set { ApplyPropertyChange ( ref _nullableShortProperty, () => NullableShortProperty, value ); }
        }

        public ushort? NullableUShortProperty
        {
            get { return _nullableUShortProperty; }
            set { ApplyPropertyChange ( ref _nullableUShortProperty, () => NullableUShortProperty, value ); }
        }

        public int? NullableIntProperty
        {
            get { return _nullableIntProperty; }
            set { ApplyPropertyChange ( ref _nullableIntProperty, () => NullableIntProperty, value ); }
        }

        public uint? NullableUIntProperty
        {
            get { return _nullableUIntProperty; }
            set { ApplyPropertyChange ( ref _nullableUIntProperty, () => NullableUIntProperty, value ); }
        }

        public long? NullableLongProperty
        {
            get { return _nullableLongProperty; }
            set { ApplyPropertyChange ( ref _nullableLongProperty, () => NullableLongProperty, value ); }
        }

        public ulong? NullableULongProperty
        {
            get { return _nullableULongProperty; }
            set { ApplyPropertyChange ( ref _nullableULongProperty, () => NullableULongProperty, value ); }
        }

        // Floating Point Types

        public float? NullableFloatProperty
        {
            get { return _nullableFloatProperty; }
            set { ApplyPropertyChange ( ref _nullableFloatProperty, () => NullableFloatProperty, value ); }
        }

        public double? NullableDoubleProperty
        {
            get { return _nullableDoubleProperty; }
            set { ApplyPropertyChange ( ref _nullableDoubleProperty, () => NullableDoubleProperty, value ); }
        }

        // Decimal Type

        public decimal? NullableDecimalProperty
        {
            get { return _nullableDecimalProperty; }
            set { ApplyPropertyChange ( ref _nullableDecimalProperty, () => NullableDecimalProperty, value ); }
        }

        // Other Types

        public bool? NullableBoolProperty
        {
            get { return _nullableBoolProperty; }
            set { ApplyPropertyChange ( ref _nullableBoolProperty, () => NullableBoolProperty, value ); }
        }

        public char? NullableCharProperty
        {
            get { return _nullableCharProperty; }
            set { ApplyPropertyChange ( ref _nullableCharProperty, () => NullableCharProperty, value ); }
        }

        // String

        public string StringProperty
        {
            get { return _stringProperty; }
            set { ApplyPropertyChange ( ref _stringProperty, () => StringProperty, value ); }
        }
    }
}