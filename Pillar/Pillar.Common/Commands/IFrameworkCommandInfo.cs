namespace Pillar.Common.Commands
{
    /// <summary>
    /// Object for holding info about command.
    /// </summary>
    public interface IFrameworkCommandInfo
    {
        /// <summary>
        /// Gets the owner.
        /// </summary>
        object Owner { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }
    }
}