<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Course" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header" runat="server">
        <asp:Label ID="CourseTitleLabel" runat="server" Text="Label"></asp:Label>
                    <small>
            <asp:Label ID="ActivePanelLabel" runat="server" Text="Label"></asp:Label></small>
                </h1>
                <ol class="breadcrumb">
                    <li class="active"><asp:LinkButton ID="HomeHyperLink" runat="server" OnClick="HomeHyperLink_Click">Course Home</asp:LinkButton></li>
                    <li><asp:LinkButton ID="LessonHyperLink" runat="server" OnClick="LessonHyperLink_Click">Lessons</asp:LinkButton></li>
                    <li><asp:LinkButton ID="AssignmentHyperLink" runat="server" OnClick="AssignmentHyperLink_Click">Assignments</asp:LinkButton></li>
                    <li><asp:LinkButton ID="GradeHyperLink" runat="server" OnClick="GradeHyperLink_Click">Grades</asp:LinkButton></li>
                    <li><asp:LinkButton ID="ClassListHyperLink" runat="server" OnClick="ClassListHyperLink_Click">Class List</asp:LinkButton></li>
                </ol>
            </div>
        </div>

    <asp:Panel ID="HomePanel" runat="server">
        Home
    </asp:Panel>

    <asp:Panel ID="LessonPanel" runat="server">
        Lesson
    </asp:Panel>

    <asp:Panel ID="AssignmentPanel" runat="server">
        Assignment
    </asp:Panel>

    <asp:Panel ID="GradePanel" runat="server">
        Grade
    </asp:Panel>

    <asp:Panel ID="ClassListPanel" runat="server">
        ClassList
    </asp:Panel>
</asp:Content>

