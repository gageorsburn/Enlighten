<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Course" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-lg-12">
            <h1 class="page-header" runat="server">
                <%= course.Title %>
                <small>
                    <asp:Label ID="ActivePanelLabel" runat="server" Text="Label"></asp:Label></small>
            </h1>
            <ol class="breadcrumb">
                <li class="active">
                    <asp:LinkButton ID="HomeHyperLink" runat="server" OnClick="HomeHyperLink_Click">Course Home</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LessonHyperLink" runat="server" OnClick="LessonHyperLink_Click">Lessons</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="AssignmentHyperLink" runat="server" OnClick="AssignmentHyperLink_Click">Assignments</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="GradeHyperLink" runat="server" OnClick="GradeHyperLink_Click">Grades</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="ClassListHyperLink" runat="server" OnClick="ClassListHyperLink_Click">Class List</asp:LinkButton></li>
            </ol>
        </div>
    </div>

    <asp:Panel ID="HomePanel" runat="server">
        Home
    </asp:Panel>

    <asp:Panel ID="LessonPanel" runat="server">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <asp:Repeater ID="LessonRepeater" ItemType="Enlighten.Models.Lesson" SelectMethod="LessonRepeater_GetData" OnItemCommand="LessonRepeater_ItemCommand" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument="<%# Item.Id %>" CssClass="list-group-item" runat="server"><%# Item.Title %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="col-md-9 ">
                <% if (LessonTitleLabel.Text != string.Empty)
                    { %>
                <div class="panel panel-default">
                    <div class="panel-heading text-center">
                        <h2>
                            <asp:Label ID="LessonTitleLabel" runat="server" Text=""></asp:Label>
                        </h2>
                    </div>
                    <div class="panel-body">
                        <p>
                            <asp:Label ID="LessonContentLabel" runat="server" Text=""></asp:Label>
                        </p>
                    </div>
                    <div class="panel-footer">
                        <div class="row">
                            <div class="col-md-8">
                        <h4>Attachments</h4>
                                </div>

                        <asp:Repeater ID="LessonAttachmentRepeater" ItemType="Enlighten.Models.LessonAttachment" SelectMethod="LessonAttachmentRepeater_GetData" OnItemCommand="LessonAttachmentRepeater_ItemCommand" runat="server">
                            <ItemTemplate>
                                <div class="col-md-8">
                                <i class="fa fa-file"></i>&nbsp;
                                <asp:LinkButton CommandName="Download" CommandArgument="<%# Item.Id %>" runat="server"><%# Item.Title %></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <hr />
                            </SeparatorTemplate>
                        </asp:Repeater>
                            </div>
                        
                                    <% if (IsMemberProfessor())
                                        {%>
                            <hr />
                        <div class="row">
                        <div class="col-md-8">
                            <div class="control-group form-group">
                                <div class="controls">
                                    <asp:FileUpload CssClass="form-control" ID="LessonAttachmentUpload" runat="server" />
                                    </div>
                            </div>
                            <div class="control-group form-group">
                                <div class="controls">
                            <asp:LinkButton CssClass="btn btn-primary" ID="LessonAttachmentButton" OnClick="LessonAttachmentButton_Click" runat="server">Upload Attachment</asp:LinkButton>
                                    </div>
                                </div>
                        </div>
                        </div>
                                    <%} %>
                    </div>
                </div>
                <%} %>
            </div>
   </div>
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
                        <h4><%= string.Format("{0} {1}", GetProfessor().FirstName, GetProfessor().LastName) %>
                            <br />
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
                        <h4><%= string.Format("{0} {1}", GetAssistant().FirstName, GetAssistant().LastName) %><br>
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

