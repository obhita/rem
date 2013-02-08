using System;
using Pillar.Domain;

namespace Rem.Infrastructure.Tests.Service.Fixtures
{
    public class PropertyMapperTestEntity : Entity
    {
        public readonly string NullArgumentMessage = "Argument cannot be null";

        public string StringProperty { get; set; }

        public bool BooleanProperty { get; set; }

        public int ReadOnlyIntProperty
        {
            get { return default( int ); }
        }

        public object ObjectPropertyWithPrivateSetter { get; private set; }

        public string SomeMethod ()
        {
            throw new NotImplementedException ();
        }

        public string NotNullableProperty
        {
            get { return default( string ); }
            set
            {
                if ( value == null )
                {
                    throw new ArgumentException ( NullArgumentMessage );
                }
            }
        }
    }
}
