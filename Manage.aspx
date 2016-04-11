<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Manage" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" Runat="Server"></asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Manage Your Profile</h1>
        </div>

        <%= member.Email %>
    </div>
</asp:Content>

