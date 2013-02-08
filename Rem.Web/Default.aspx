<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="Pillar.Common.InversionOfControl" %>
<%@ Import Namespace="Rem.Domain.Core.SecurityModule" %>
<%@ Import Namespace="Rem.Infrastructure.Security" %>

<%
    // Stop Caching in IE   
    Response.Cache.SetCacheability ( HttpCacheability.NoCache );
    // Stop Caching in Firefox   
    Response.Cache.SetNoStore ();
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    protected void Page_Load ( object sender, EventArgs e )
    {
        var currentClaimsPrincipalService = IoC.CurrentContainer.Resolve<ICurrentClaimsPrincipalService>();
        var claimsPrincipal = currentClaimsPrincipalService.GetCurrentPrincipal ();
        var accountKey = claimsPrincipal.CurrentAccountKey();

        if (accountKey == 0)
        {
            ShowNoSystemAccountError(claimsPrincipal.Identity);
            return;
        }
        
        var accountRepository = IoC.CurrentContainer.Resolve<ISystemAccountRepository> ();
        var account = accountRepository.GetByKey(accountKey);
     
        if (account == null)
        {
            ShowNoSystemAccountError(claimsPrincipal.Identity);
            return;
        }
            
        var staffs = account.StaffMembers.ToList(); 
            
        if (staffs.Count == 0)
        {
            var errorText = new StringBuilder ();
            errorText.AppendFormat("<div id='error'>You do not have any Staff record associated with your account in REM system.</div>");
            ErrorDiv.InnerHtml = errorText.ToString();
            ErrorDiv.Visible = true;
        }
        else
        {
            if (staffs.Count == 1)
            {
                var signOnService = IoC.CurrentContainer.Resolve<ISignOnService>();
                signOnService.LoginAs(staffs[0]);                
                Response.Redirect("~/Client.aspx");                  
            }
            else if (staffs.Count > 1)
            {
                AccountKeyHidden.Value = account.Key + "";
                StaffSelectPanelDiv.Visible = true;
            }
        }
    }
    
    private void ShowNoSystemAccountError(IIdentity identity)
    {
        var errorText = new StringBuilder();
        errorText.AppendFormat("<div class='noSystemAccountNotification'");
        errorText.AppendFormat("<div id='error'>Authenticated principal ({0}) does not have a system account in REM.", identity.Name);
        errorText.AppendFormat("<a href='{0}'>Logout</a>", ResolveUrl("~/Logout.aspx"));
        errorText.AppendFormat("</div></div>");
        ErrorDiv.InnerHtml = errorText.ToString();
        ErrorDiv.Visible = true;
    }
    
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REM</title>
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: auto;
        }
        body
        {
            padding: 0;
            margin: 0;
        }
        #silverlightControlHost
        {
            height: 100%;
            text-align: center;
        }
        #welcome
        {
            font-size: 30px;
            font-family: Arial;
            color: white;/*#24556B;*/
            text-align: center;
            margin-top: 30px;
            margin-bottom: 30px;
        }
        #error
        {
            font-size: 20px;
            font-family: Arial;
            color: red;
            text-align: center;
            margin-top: 20px;
            margin-bottom: 20px;
            margin-left: auto;
            margin-right: auto;
        }
        
        #username
        {
            font-size: 18px;
            font-family: Arial;
            font-weight: 600;
            text-align: center;
            color: #24556B;
        }
        
        #staffSelectPanel
        {
            width: 100%;
            height: 100%;
            float: left;
        }
        
        div.group
        {
            font-size: 18px;
            font-family: Arial;
            color: white;/*#24556B;*/
            text-align: center;
            border: 2px solid white; 
            width: 400px; 
            height: 120px;
            margin-top: 20px;
            margin-bottom: 20px;
            margin-left: auto;
            margin-right: auto;
        }
        
        div.noSystemAccountNotification
        {
            font-size: 18px;
            font-family: Arial;
            color: white;/*#24556B;*/
            text-align: center;
            border: 2px solid white; 
            width: 400px; 
            height: 120px;
            margin-top: 20px;
            margin-bottom: 20px;
            margin-left: auto;
            margin-right: auto;
            background:white;
        }
        
        a.goButton
        {
        }
    </style>
</head>
<body style="background-color: #08667F">

        <form id="form1" runat="server">

        <div>
        </div>
        <div id="welcome">
            REM System</div>

        <div id="ErrorDiv" runat="server" Visible="false">test</div>
        
        <div id="StaffSelectPanelDiv" runat="server" Visible="false">
        <input id="AccountKeyHidden" type="hidden" runat="server"/>
        <%
            var accountRepository = IoC.CurrentContainer.Resolve<ISystemAccountRepository>();
            var account = accountRepository.GetByKey(long.Parse(AccountKeyHidden.Value));
            var staffs = account.StaffMembers.ToList();
            //staffs.AddRange ( staffs ); // TODO: just for test, remove latter
            foreach ( var staff in staffs )
            {
                var gotoUrl = ResolveUrl("~/LoginHandler.ashx?staffKey=" + staff.Key);
        %>
                <div class="group">
                    <span>Staff: <%= staff.StaffProfile.StaffName.Complete %></span>
                    <span>Agency: <%= staff.Agency.AgencyProfile.AgencyName.DisplayName %></span>
                    <a class="goButton" href="" onclick="this.href='<%= gotoUrl %>'"><img src="Styles/Images/Welcome-Landing-Page_Button.png" alt="Welcome-Landing-Page_Button" style="width: 82px; height: 80px;" /></a>               
                </div>
        <%
            }
        %>
        </div>
        </form>
</body>
</html>
