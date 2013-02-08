using System.Configuration;

namespace Rem.Web.LocalSTS.STS
{
    /// <summary>
    /// Handler for custom configuration section named LoginUser.
    /// </summary>
    public class UserLoginSection : ConfigurationSection
    {
        [ConfigurationProperty ( "userName", DefaultValue = "", IsRequired = true )]
        public string UserLogin
        {
            get { return ( string ) this [ "userName" ]; }
            set { this [ "userName" ] = value; }
        }

        [ConfigurationProperty ( "userPassword", DefaultValue = "", IsRequired = true )]
        public string UserPassword
        {
            get { return ( string ) this [ "userPassword" ]; }
            set { this [ "userPassword" ] = value; }
        }
    }
}