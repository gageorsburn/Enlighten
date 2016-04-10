<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Course" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header" runat="server">
            <%= course.Title %>
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
        <div class="row">
        <div class="col-md-offset-3 col-md-3 text-center">
                    <div class="thumbnail">
                        <%= GetProfessor().Picture == null ? 
                            new HtmlString(@"<span class='fa-stack fa-lg fa-5x'>
                                  <i class='fa fa-square fa-stack-2x'></i>
                                  <i class='fa fa-user fa-stack-1x fa-inverse'></i>
                                </span>") : new HtmlString("<img style='width:130px;height:140px;padding-top:10px;border-radius: 25px;' src='"+ GetPictureUrl(GetProfessor()) +"' />")%>
                        <div class="caption">
                            <h4><%= string.Format("{0} {1}", GetProfessor().FirstName, GetProfessor().LastName) %> <br />
                                <small><%= GetProfessor().Email %></small>
                            </h4>
                            <p>Professor</p>
                        </div>
                    </div>
                </div>

        <div class="col-md-3 text-center">
                    <div class="thumbnail">
                        <%= GetAssistant().Picture == null ? 
                            new HtmlString(@"<span class='fa-stack fa-lg fa-5x'>
                                  <i class='fa fa-square fa-stack-2x'></i>
                                  <i class='fa fa-user fa-stack-1x fa-inverse'></i>
                                </span>") : new HtmlString("<img style='width:130px;height:140px;padding-top:10px;border-radius: 25px;' src='"+ GetPictureUrl(GetAssistant()) +"' />")%>
                        <div class="caption">
                            <h4> <%= string.Format("{0} {1}", GetAssistant().FirstName, GetAssistant().LastName) %><br>
                                <small><%= GetAssistant().Email %></small>
                            </h4>
                            <p>Assistant</p>
                        </div>
                    </div>
                </div>
            </div>
        <hr />
        <div class="row">
        <asp:Repeater ID="ClassListRepeater" ItemType="Enlighten.Models.Member" SelectMethod="ClassListRepeater_GetData" runat="server">
            <ItemTemplate>
                <div class="col-md-3 text-center">
                    <div class="thumbnail">
                        <%# Item.Picture == null ? 
                            new HtmlString(@"<span class='fa-stack fa-lg fa-5x'>
                                  <i class='fa fa-square fa-stack-2x'></i>
                                  <i class='fa fa-user fa-stack-1x fa-inverse'></i>
                                </span>") : new HtmlString("<img style='width:130px;height:140px;padding-top:10px;border-radius: 25px;' src='"+ GetPictureUrl(Item) +"' />")%>


                        <div class="caption">
                            <h4><%# string.Format("{0} {1}", Item.FirstName, Item.LastName) %><br>
                                <small><%# Item.Email %></small>
                            </h4>
                            <p>Student</p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
            </div>
    </asp:Panel>
</asp:Content>

