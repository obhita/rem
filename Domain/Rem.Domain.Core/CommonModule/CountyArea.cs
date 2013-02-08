using Pillar.Domain;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// The CountyArea lookup contains the list of counties.
    /// </summary>
    public class CountyArea : LookupBase
    {
        private readonly StateProvince _stateProvince;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountyArea"/> class.
        /// </summary>
        protected internal CountyArea ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountyArea"/> class.
        /// </summary>
        /// <param name="stateProvince">The state province.</param>
        public CountyArea ( StateProvince stateProvince )
        {
            _stateProvince = stateProvince;
        }

        /// <summary>
        /// Gets the state province.
        /// </summary>
        [NotNull]
        public virtual StateProvince StateProvince
        {
            get { return _stateProvince; }
            private set { }
        }
    }
}