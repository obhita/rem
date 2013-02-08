using System;

namespace Pillar.Domain
{
    /// <summary>
    /// NotNullAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property )]
    public sealed class NotNullAttribute : Attribute
    {
    }
}
