namespace Pillar.Common.Bootstrapper
{
    /// <summary>
    /// Interface for bootstrapper task.
    /// </summary>
    public interface IBootstrapperTask
    {
        #region Public Methods

        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute ();

        #endregion
    }
}
