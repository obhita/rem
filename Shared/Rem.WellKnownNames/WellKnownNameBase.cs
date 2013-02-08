using System;

namespace Rem.WellKnownNames
{
    public class WellKnownNameBase
    {
        public WellKnownNameBase ( String wellKnownName, string code ) : this ( wellKnownName, code, String.Empty )
        {
        }

        public WellKnownNameBase ( String wellKnownName, string code, string description )
        {
            WellKnownName = wellKnownName;
            Code = code;
            Description = description;
        }

        public String WellKnownName { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
    }
}