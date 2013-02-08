namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// The StateProvince lookup contains a list of states and provinces.
    /// </summary>
    public class StateProvince : LookupBase
    {
        private readonly Country _country;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateProvince"/> class.
        /// </summary>
        protected internal StateProvince ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateProvince"/> class.
        /// </summary>
        /// <param name="country">The country.</param>
        public StateProvince ( Country country )
        {
            _country = country;
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        public virtual Country Country
        {
            get { return _country; }
            private set { }
        }
    }
}