<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="lendbook.aspx.cs" Inherits="WebApplication1.lendbook" %>
<%@ Register Src="~/capestartMT.ascx" TagPrefix="uc1" TagName="ContactUC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <uc1:contactuc runat="server" id="ContactUC" Header="Users Tab" />
    </div>
    <asp:Label ID="username" runat="server" Text="Search Book"></asp:Label> <br />
    <asp:DropDownList ID="DropDownList2" runat="server">
        <asp:ListItem Value="1">Book</asp:ListItem>
        <asp:ListItem Value="2">Author</asp:ListItem>
        <asp:ListItem Value="3">Publisher</asp:ListItem>
    </asp:DropDownList>
     &nbsp;&nbsp;
    <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
    <br />
     <br />
     <br />
       <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" /> 
 
<br />
    <div class="row">
        <div class="col-md-4">
     
        </div>
        <div class="col-md-4">
            
        </div>
        <div class="col-md-4">
            
            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            </asp:GridView>
            
        </div>
    </div>
</asp:Content>
