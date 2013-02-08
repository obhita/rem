using System;

namespace Pillar.Domain
{
    /// <summary>
    /// NoneCascadingAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Property, AllowMultiple = false )]
    public sealed class NoneCascadingAttribute : Attribute
    {
    }
}
