using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.TedsModule
{
    /// <summary>
    /// Defines the base TEDS lookup base class that has code value.
    /// </summary>
    public abstract class TedsLookupBase : LookupBase
    {
        private string _code;

        /// <summary>
        /// Initializes a new instance of the <see cref="TedsLookupBase"/> class.
        /// </summary>
        protected internal TedsLookupBase ()
        {
        }
        
        /// <summary>
        /// Gets the code.
        /// </summary>
        [NotNull]
        public virtual string Code
        {
            get { return _code; }
            set { ApplyPropertyChange(ref _code, () => Code, value); }
        }
    }
}
