//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Configuration;
using Rem.Web.LocalSTS.STS;

namespace Rem.Web.LocalSTS
{
    public partial class Login : System.Web.UI.Page
    {
        class UserLogin
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private UserLogin [] _authenticatedUsers;
        private UserLogin _configUser;

        private bool IsAuthentic ( UserLogin userLogin )
        {
            if ( userLogin == null )
                return false;

            bool valid = _authenticatedUsers.Any ( u => ( u.Login == userLogin.Login && u.Password == userLogin.Password ) );
            return valid;
        }

        protected void Page_Init ( object sender, EventArgs e )
        {
            if ( _authenticatedUsers == null )
            {
                _authenticatedUsers = new UserLogin []
                                      {
                                          new UserLogin { Login = "user01", Password = "password01" },
                                          new UserLogin { Login = "user02", Password = "password02" },
                                          new UserLogin { Login = "user03", Password = "password03" },
                                      };
            }
        }

        protected void Page_Load ( object sender, EventArgs e )
        {
            if ( string.IsNullOrEmpty ( txtUserName.Text ) || string.IsNullOrEmpty ( txtPassword.Text ) )
            {
                UserLoginSection _userLoginSection = ( UserLoginSection ) ConfigurationManager.GetSection ( "userLoginGroup/userLogin" );
                if ( _userLoginSection != null )
                {
                    _configUser = new UserLogin () { Login = _userLoginSection.UserLogin, Password = _userLoginSection.UserPassword };
                }
            }
            else
            {
                _configUser = new UserLogin () { Login = txtUserName.Text, Password = txtPassword.Text };
            }

            // Note: Add code to validate user name, password. This code is for illustrative purpose only.
            // Do not use it in production environment.)
            if ( IsAuthentic ( _configUser ) )
            {
                if ( Request.QueryString [ "ReturnUrl" ] != null )
                {
                    FormsAuthentication.RedirectFromLoginPage ( _configUser.Login, false );
                }
                else
                {
                    FormsAuthentication.SetAuthCookie ( _configUser.Login, false );
                    Response.Redirect ( "default.aspx" );
                }
            }
        }
    }
}