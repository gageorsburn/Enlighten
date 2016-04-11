<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" Runat="Server"></asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header">Register</h1>
        </div>

        <div class="col-md-offset-4 col-md-4">
            <div class="control-group form-group">
                <div class="controls">
                    <label>First Name</label>
                    <asp:TextBox ID="FirstName" CssClass="form-control" runat="server"></asp:TextBox>
                    <p class="help-block"></p>
                </div>
            </div>

            <div class="control-group form-group">
                <div class="controls">
                    <label>Last Name</label>
                    <asp:TextBox ID="LastName" CssClass="form-control" runat="server"></asp:TextBox>
                    <p class="help-block"></p>
                </div>
            </div>

            <div class="control-group form-group">
                <div class="controls">
                    <label>E-mail</label>
                    <asp:TextBox ID="Email" CssClass="form-control" runat="server"></asp:TextBox>
                    <p class="help-block"></p>
                </div>
            </div>

            <div class="control-group form-group">
                <div class="controls">
                    <label>Password</label>
                    <asp:TextBox ID="Password" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    <p class="help-block"></p>
                </div>
            </div>

            <div class="control-group form-group">
                <div class="controls">
                    <asp:Button ID="RegisterButton" CssClass="btn btn-primary " runat="server" Text="Register" OnClick="RegisterButton_Click" />
                </div>
            </div>

            <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label>
        </div>
    </div>
</asp:Content>

