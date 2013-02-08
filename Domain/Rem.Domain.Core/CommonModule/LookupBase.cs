using System;
using Pillar.Domain;
using Pillar.Domain.Primitives;

namespace Rem.Domain.Core.CommonModule
{
    /// <summary>
    /// Provides a base implementation for lookups.
    /// </summary>
    public abstract class LookupBase : AuditableAggregateRootBase, ILookup
    {
        private string _wellKnownName;
        private bool _defaultIndicator;
        private string _description;
        private DateRange _effectiveDateRange;
        private string _name;
        private string _shortName;
        private int? _sortOrderNumber;
        private bool _systemOwnedIndicator;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupBase"/> class.
        /// </summary>
        protected internal LookupBase ()
        {
            _effectiveDateRange = new DateRange ( new DateTime (), new DateTime () );
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name of the well known.
        /// </summary>
        /// <value>
        /// The name of the well known.
        /// </value>
        public virtual string WellKnownName
        {
            get { return _wellKnownName; }
            set { ApplyPropertyChange ( ref _wellKnownName, () => WellKnownName, value ); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [NotNull]
        [NaturalIndex]
        public virtual string Name
        {
            get { return _name; }
            set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets or sets the short name.
        /// </summary>
        /// <value>
        /// The short name.
        /// </value>
        public virtual string ShortName
        {
            get { return _shortName; }
            set { ApplyPropertyChange(ref _shortName, () => ShortName, value); }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description
        {
            get { return _description; }
            set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        /// Gets or sets the sort order number.
        /// </summary>
        /// <value>
        /// The sort order number.
        /// </value>
        public virtual int? SortOrderNumber
        {
            get { return _sortOrderNumber; }
            set { ApplyPropertyChange ( ref _sortOrderNumber, () => SortOrderNumber, value ); }
        }

        /// <summary>
        /// Gets or sets the effective date range.
        /// </summary>
        /// <value>
        /// The effective date range.
        /// </value>
        public virtual DateRange EffectiveDateRange
        {
            get { return _effectiveDateRange; }
            set { ApplyPropertyChange ( ref _effectiveDateRange, () => EffectiveDateRange, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [system owned indicator].
        /// </summary>
        /// <value>
        /// <c>true</c> if [system owned indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool SystemOwnedIndicator
        {
            get { return _systemOwnedIndicator; }
            set { ApplyPropertyChange ( ref _systemOwnedIndicator, () => SystemOwnedIndicator, value ); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [default indicator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [default indicator]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool DefaultIndicator
        {
            get { return _defaultIndicator; }
            set { ApplyPropertyChange ( ref _defaultIndicator, () => DefaultIndicator, value ); }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}