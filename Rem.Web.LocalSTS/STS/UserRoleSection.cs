using System.Configuration;

namespace Rem.Web.LocalSTS.STS
{
    public class UserRoleSection : ConfigurationSection
    {
        [ConfigurationProperty("Roles", DefaultValue = "", IsRequired = true)]
        public string Roles
        {
            get { return (string)this["Roles"]; }
            set { this["Roles"] = value; }
        }
   
    }
}