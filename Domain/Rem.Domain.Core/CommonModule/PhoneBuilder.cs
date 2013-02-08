namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// PhoneBuilder provides a fluent interface for creating a Phone.
    /// </summary>
    public class PhoneBuilder
    {
        private string _phoneExtensionNumber;
        private string _phoneNumber;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PhoneBuilder"/> to <see cref="Phone"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Phone(PhoneBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A PhoneBuilder.</returns>
        public PhoneBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        /// <summary>
        /// Assigns the phone extension number.
        /// </summary>
        /// <param name="phoneExtensionNumber">The phone extension number.</param>
        /// <returns>A PhoneBuilder.</returns>
        public PhoneBuilder WithPhoneExtensionNumber(string phoneExtensionNumber)
        {
            _phoneExtensionNumber = phoneExtensionNumber;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A Phone.</returns>
        public Phone Build()
        {
            return new Phone(_phoneNumber, _phoneExtensionNumber);
        }
    }
}