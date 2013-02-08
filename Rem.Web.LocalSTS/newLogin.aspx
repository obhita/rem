<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/Site.Master" AutoEventWireup="true" CodeBehind="newLogin.aspx.cs" Inherits="Rem.Web.LocalSTS.newLogin" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="width: 400px; height: 55px; text-align: center">
       <%-- <h1>
            Use &quot;user01&quot; and &quot;password01&quot; to login.</h1>--%>
        <div style="width: 400px; height: 10px; text-align: center">
        </div>
     
    </div>
   
                       <div style="width: 400px;text-align: center">
        <span class="failureNotification">

                                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                            </span>
                            </div>
            <div class="accountInfo">
                <fieldset style="background-color: #D5D5D5">
                    <div style="width: 280px; height: 70px">
                        <h2 style="margin-left: 25px; width: auto">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User ID</asp:Label></h2>
                        <div style="width: 15px; height: 30px; text-align: center">
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="styledtextbox">
                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox></div>
                    </div>
                    <div style="width: 280px; height: 70px">
                        <h2 style="margin-left: 25px; width: auto; margin-top: 0px">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password</asp:Label></h2>
                        <div style="width: 15px; height: 30px; text-align: center">
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="styledtextbox">
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox></div>
                        <div style="margin-left: 15px">
                            <p>
                                Forgot Passord?</p>
                        </div>
                    </div>
                    <div style="width: 280px; height: 40px">
                        <div style="width: 20px; height: 40px">
                        </div>
                        <div style="width: 160px; height: 40px; list-style-position: inherit">
                            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="LoginUserValidationGroup" />
                            
                        </div>
                        <div style="width: 10px; height: 40px; margin-right: 15px">
                            <div class="submitButton">
                                <asp:ImageButton ImageUrl="Styles/images/Authentication-Login-Page_14.png" ID="LoginButton"
                                    runat="server" CommandName="Login" ValidationGroup="LoginUserValidationGroup"
                                    Style="width: 80px; height: 20px" />
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
     
</asp:Content>
