using System;

namespace Pillar.Domain
{
    /// <summary>
    /// CacheAttribute class.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public class CacheAttribute : Attribute
    {
    }
}
