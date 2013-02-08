<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Import Namespace="Pillar.Common.InversionOfControl" %>
<%@ Import Namespace="Pillar.Security.AccessControl" %>
<%@ Import Namespace="Rem.Infrastructure.Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head id="Head1" runat="server">
        <title>REM</title>
        <style type="text/css">
            html, body {
                height: 100%;
                overflow: auto;
            }

            body {
                padding: 0;
                margin: 0;
		
		
            }

            #silverlightControlHost {
                height: 100%;
                text-align: center;
            }
        </style>
        <script type="text/javascript" src="Silverlight.js"> </script>
    </head>
    <body style="background-color: #08667F">
        <form id="form1" runat="server" style="height: 100%">
            <%             
                var currentClaimsPrincipalService = IoC.CurrentContainer.Resolve<ICurrentClaimsPrincipalService>();
                var claimsPrincipal = currentClaimsPrincipalService.GetCurrentPrincipal();

                var statffKey = claimsPrincipal.CurrentStaffKey ();

                if (statffKey == 0)
                {
                    Response.Redirect("~/Default.aspx");
                }              
         
                var accessControlManager = IoC.CurrentContainer.Resolve<IAccessControlManager> ();

                if (accessControlManager.CanAccess(new ResourceRequest { "ClientApplication" }))
                {
            %>
                    <div id="silverlightControlHost" style="min-width: 1024px">
                        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
                            <param name="source" value="ClientBin/Rem.Ria.Shell.xap"/>
                            <param name="onError" value="onSilverlightError" />
                            <param name="background" value="#08667F" />
                            <param name="minRuntimeVersion" value="4.0.60129.0" />
                            <param name="autoUpgrade" value="true" />
                            <param name="splashscreensource" value="LoadSplash.xaml"/>    
                            <param name="onSourceDownloadProgressChanged" value="onSourceDownloadProgressChanged"/>
                            <param name="onSourceDownloadComplete" value="onSourceDownloadComplete"/>
                            <param name="initParams" value='ServiceUrl=<%= String.Format("https://{0}{1}", Request.Url.Authority, ResolveUrl("~/ReportService.svc")) %>;DebugMode=Full' />

                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50303.0" style="text-decoration: none">
                                <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight" style="border-style: none"/>
                            </a>
                        </object>
                        <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px; border: 0px"></iframe>
                    </div>
            <%
                }
                else
                {
            %>
                    <div id='error'>You do not have permission to launch the REM Client.</div>
            <%
                }
            %>
        </form>
    </body>
</html>
