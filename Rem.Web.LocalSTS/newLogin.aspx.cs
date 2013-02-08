using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Configuration;
using Rem.Web.LocalSTS.STS;

namespace Rem.Web.LocalSTS
{
    public partial class newLogin : System.Web.UI.Page
    {
        class UserLogin
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private UserLogin[] _authenticatedUsers;
        private UserLogin _configUser;

        private bool IsAuthentic(UserLogin userLogin)
        {
            if (userLogin == null)
                return false;

            bool valid = _authenticatedUsers.Any(u => (u.Login == userLogin.Login && u.Password == userLogin.Password));
            return valid;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (_authenticatedUsers == null)
            {
                _authenticatedUsers = new UserLogin[]
                                      {
                                          new UserLogin { Login = "cindy.thomas", Password = "P@$$w0rd" },
                                          new UserLogin { Login = "leo.smith", Password = "P@$$w0rd" },
                                          new UserLogin { Login = "lily.foley", Password = "P@$$w0rd" },
                                          new UserLogin { Login = "chris.white", Password = "P@$$w0rd" },
                                          new UserLogin { Login = "dennis.ladder", Password = "P@$$w0rd" },
                                          new UserLogin { Login = "BillingClaimsProcessor", Password = "P@$$w0rd" },
                                      };
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string user = UserName.Text;
            string password = Password.Text;


            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                UserLoginSection _userLoginSection = (UserLoginSection)ConfigurationManager.GetSection("userLoginGroup/userLogin");
                if (_userLoginSection != null)
                {
                    _configUser = new UserLogin() { Login = _userLoginSection.UserLogin, Password = _userLoginSection.UserPassword };
                }
            }
            else
            {
                _configUser = new UserLogin() { Login = user, Password = password };
            }

            // Note: Add code to validate user name, password. This code is for illustrative purpose only.
            // Do not use it in production environment.)
            if (IsAuthentic(_configUser))
            {
                if (Request.QueryString["ReturnUrl"] != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(_configUser.Login, false);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(_configUser.Login, false);
                    Response.Redirect("default.aspx");
                }
            }
        }
    }
}