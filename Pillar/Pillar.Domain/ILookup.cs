using Pillar.Domain.Primitives;

namespace Pillar.Domain
{
    /// <summary>
    /// ILookup interface.
    /// </summary>
    public interface ILookup
    {
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [default indicator].
        /// </summary>
        bool DefaultIndicator { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the effective date range.
        /// </summary>
        DateRange EffectiveDateRange { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the short name.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// Gets the sort order number.
        /// </summary>
        int? SortOrderNumber { get; }

        /// <summary>
        /// Gets a value indicating whether [system owned indicator].
        /// </summary>
        bool SystemOwnedIndicator { get; }

        /// <summary>
        /// Gets the name of the well known.
        /// </summary>
        string WellKnownName { get; }

        #endregion
    }
}
