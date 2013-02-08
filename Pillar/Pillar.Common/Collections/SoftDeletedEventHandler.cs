namespace Pillar.Common.Collections
{
    /// <summary>
    /// The signature for the soft delete event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Pillar.Common.Collections.SoftDeletedEventArgs"/> instance containing the event data.</param>
    public delegate void SoftDeletedEventHandler ( object sender, SoftDeletedEventArgs e );
}
