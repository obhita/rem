using System;

namespace Pillar.Domain
{
    /// <summary>
    /// This attribute denotes that a class is a component in ORM mapping.
    /// A component is a contained object that is persisted as a value type, not an entity. 
    /// The term "component" refers to the object-oriented notion of composition (not to architecture-level components). 
    /// </summary>
    [AttributeUsage ( AttributeTargets.Class | AttributeTargets.Struct )]
    public sealed class ComponentAttribute : Attribute
    {
    }
}
