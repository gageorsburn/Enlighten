<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DebugHelper.aspx.cs" Inherits="DebugHelper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3>Courses</h3><br />
    <asp:Repeater ID="CourseRepeater" ItemType="Enlighten.Models.Course" SelectMethod="CourseRepeater_GetData" runat="server">
        <ItemTemplate>
            <%# Item.Id %> - <%# Item.Title %>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>
    <h3>Members</h3> <br />
    <asp:Repeater ID="MemberRepeater" ItemType="Enlighten.Models.Member" SelectMethod="MemberRepeater_GetData" runat="server">
        <ItemTemplate>
            <%# Item.Id %> - <%# Item.FullName %>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
    </asp:Repeater>
</asp:Content>

