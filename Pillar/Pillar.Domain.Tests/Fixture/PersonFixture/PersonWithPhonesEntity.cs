using System;
using System.Collections.Generic;

namespace Pillar.Domain.Tests.Fixture.PersonFixture
{
    public class PersonWithPhonesEntity : PersonEntity, IEventActionDummyInterface2
    {
        private readonly List<PhoneEntity> _phones;

        public PersonWithPhonesEntity ()
        {
            _phones = new List<PhoneEntity> ();
        }

        public IEnumerable<PhoneEntity> Phones
        {
            get { return _phones; }
        }

        public PhoneEntity CreateNewPhone ( string number, string phoneType )
        {
            var phone = new PhoneEntity ( this );
            phone.Number = number;
            phone.Type = phoneType;
            _phones.Add ( phone );

            NotifyItemAdded ( () => Phones, phone );

            OnNewPhoneAdded ( this, new NewPhoneEventArgs ( phone ) );

            return phone;
        }

        public event EventHandler<NewPhoneEventArgs> OnNewPhoneAdded = delegate { };

        #region Nested type: NewPhoneEventArgs

        public class NewPhoneEventArgs : EventArgs
        {
            public NewPhoneEventArgs ( PhoneEntity phone )
            {
                NewPhone = phone;
            }

            public PhoneEntity NewPhone { get; private set; }
        }

        #endregion
    }
}