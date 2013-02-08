<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="Rem.Web.LogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        h1
        {
            font-size:20px;
            font-family:Arial;
            color:#24556B;
            text-align:center;
        }
         a
        {
            font-size:12px;
            font-family:Arial;
            font-weight:600;
            text-align:center;
            color:#24556B;
          
            
        
        }
        A:link 
        {
            text-decoration: none
            } 
            
        A:visited 
        {
            text-decoration: none
            } 
            
        A:active 
        {
            text-decoration: none
            } 
            
        A:hover 
        {
            text-decoration: underline           
        }
               
    </style>
</head>
<body style="background-color:#08667F">
<div style="background-image:url('Styles/Images/SessionTimeOut-Page-Background.png'); background-repeat:no-repeat; width:1024px; height:910px; margin-left:auto; margin-right:auto;" >        

      <div style="width:1024px; height:385px"></div>
        <div style="width:580px; height:80px; float:left"></div>
        <div style="width:200px; height:80px; float:left; text-align:center">
        <h1>Your session has timed out.</h1>
        </div>
        <div style="width:244px; height:80px; float:left"></div>
        <div style="width:1024px; height:445px; float:left"></div>

    </div>
</body>
</html>
