<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bookdetails.aspx.cs" Inherits="WebApplication1.Bookdetails" %>
<%@ Register Src="~/capestartMT.ascx" TagPrefix="uc1" TagName="ContactUC" %>  
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="jumbotron">
        <uc1:ContactUC runat="server" id="ContactUC" Header="Books User Control" />
    </div>
    <asp:Label ID="username" runat="server" Text="Book Name"></asp:Label> <br />
    <asp:TextBox ID="txtusername" runat="server"></asp:TextBox> <br />
        <br />
    <asp:Label ID="date" runat="server" Text="No of pages"></asp:Label> <br />
    <asp:TextBox ID="Txtdate" runat="server"></asp:TextBox> <br />
        <br />
    <asp:Label ID="Label1" runat="server" Text="Author"></asp:Label> <br />
    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
     <br />
     <br />
    <asp:Label ID="Label2" runat="server" Text="Publisher"></asp:Label> <br />
    <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
     <br />
     <br />
    <asp:Label ID="Label3" runat="server" Text="Upload Cover Photo"></asp:Label> 
     <asp:FileUpload ID="FileUpload1" runat="server" />
     <br />
     <br />
<asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
     <br />
     <br />
<br />
    <div class="row">
        <div class="col-md-4">
            <asp:Button ID="btnadd" runat="server" Text="Save" OnClick="btnadd_Click" /> 
            <asp:Button ID="btnview" runat="server" Text="View" OnClick="btnview_Click" />
            <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
        </div>
        <div class="col-md-4">
            
        </div>
        <div class="col-md-4">
            
            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated" OnSelectedIndexChanging="GridView1_SelectedIndexChanging">
            </asp:GridView>
            
        </div>
    </div>
</asp:Content>
