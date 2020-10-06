<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">
            Library - LOGIN
        </div>
        <div>
            <br />
            <br />
        </div>
        <div>
            &nbsp;<asp:Label ID="Label1" runat="server" Text="User Name : "></asp:Label>
&nbsp;<asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
        </div>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Password : "></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txt_PWD" runat="server"></asp:TextBox>
        </p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            Admin users : username : admin password : admin</p>
        <p>
            Normal users username : user password : user</p>
        <p>
            Datebase file is in app data folder</p>
    </form>
</body>
</html>
