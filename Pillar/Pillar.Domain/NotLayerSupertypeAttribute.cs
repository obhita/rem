using System;

namespace Pillar.Domain
{
    /// <summary>
    /// This attribute is used for ORM mapping.
    /// Abstract class denoted by this attribute is NOT as layer supertype. i.e. will have a mapping table in database.
    /// The default behavior is to consider abstract classes as  layer supertypes (http://martinfowler.com/eaaCatalog/layerSupertype.html) and effectively unmapped.
    /// </summary>
    [AttributeUsage ( AttributeTargets.Class )]
    public sealed class NotLayerSupertypeAttribute : Attribute
    {
    }
}
