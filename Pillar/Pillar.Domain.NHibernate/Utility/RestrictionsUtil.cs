using NHibernate.Criterion;

namespace Pillar.Domain.NHibernate.Utility
{
    /// <summary>
    /// RestrictionsUtil class.
    /// </summary>
    public static class RestrictionsUtil
    {
        #region Public Methods

        /// <summary>
        /// Returns Always true criterion.
        /// </summary>
        /// <returns>A ICriterion</returns>
        public static ICriterion AlwaysTrueCriterion ()
        {
            var alwaysTrueCriterion = Restrictions.Conjunction ();
            return alwaysTrueCriterion;
        }

        #endregion
    }
}
