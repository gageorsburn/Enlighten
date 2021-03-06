﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Course" %>

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
            <ol class="breadcrumb text-center">
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
        <div class="row">
                    <div class="col-md-8">
                         <% if (IsMemberProfessor()) { %>

                        <div id="newarticlemodal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Create New Article</h4>
                                </div>
                                <div class="modal-body">
                                    <!--<div class="col-md-offset-4 col-md-4">-->

                                    <div class="control-group form-group">
                                        <div class="controls">
                                            <label>Article Title</label>
                                            <asp:TextBox ID="NewArticleTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                            <p class="help-block"></p>
                                        </div>
                                    </div>

                                    <div class="control-group form-group">
                                        <div class="controls">
                                            <label>Article Content</label>
                                            <asp:TextBox ID="NewArticleContent" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            <p class="help-block"></p>
                                        </div>
                                    </div>

                                    <!--</div>-->
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="NewArticleButton" CssClass="btn btn-primary" OnClick="NewArticleButton_Click" runat="server" Text="Create New Article" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                        <a class="btn list-group-item" data-toggle="modal" data-target="#newarticlemodal">Create New Article</a>
                        <hr />
                        <% } %>
        <asp:Repeater ID="CourseArticleRepeater" ItemType="Enlighten.Models.CourseArticle" SelectMethod="CourseArticleRepeater_GetData" runat="server">
            <HeaderTemplate>

            </HeaderTemplate>
            <ItemTemplate>
                <h2>
                    <%# Item.Title %>

                    <% if (IsMemberProfessor())
                            { %>
                            <span class="fa">
                                <asp:LinkButton ID="DeleteArticleLink" OnCommand="DeleteArticleLink_Command" CommandArgument="<%# Item.Id %>" CssClass="fa-times" runat="server"></asp:LinkButton>
                            </span>
                            <% } %>
                </h2>
                <p class="lead">
                    by <a href="#"><%# Item.Author.FullName %></a>
                </p>
                <p><i class="fa fa-clock-o"></i> Posted on <%# Item.PostedOn %></p>
                <hr />
                <p><%# Item.Content %></p>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
            <FooterTemplate>
                <asp:Label runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Text="The professor has not posted any news!"></asp:Label>
                
            </FooterTemplate>
            </asp:Repeater>
            </div>
                <div class="col-md-4">
                    <div class="well">
                        <asp:Repeater ID="CourseUrlRepeater" ItemType="Enlighten.Models.CourseUrl" SelectMethod="CourseUrlRepeater_GetData" runat="server">
                            <HeaderTemplate>
                                <h3 class="text-center">Important Urls</h3>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink NavigateUrl="<%# Item.Url %>" runat="server"><%# Item.Url %></asp:HyperLink>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                </div>
        
    </asp:Panel>

    <asp:Panel ID="LessonPanel" runat="server">
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">
                    <% if (IsMemberProfessor())
                        {%>
                    <div id="newlessonmodal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Create New Lesson</h4>
                                </div>
                                <div class="modal-body">
                                    <!--<div class="col-md-offset-4 col-md-4">-->

                                    <div class="control-group form-group">
                                        <div class="controls">
                                            <label>Lesson Title</label>
                                            <asp:TextBox ID="NewLessonTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                            <p class="help-block"></p>
                                        </div>
                                    </div>

                                    <div class="control-group form-group">
                                        <div class="controls">
                                            <label>Lesson Content</label>
                                            <asp:TextBox ID="NewLessonContent" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                            <p class="help-block"></p>
                                        </div>
                                    </div>

                                    <!--</div>-->
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="NewLessonButton" CssClass="btn btn-primary" OnClick="NewLessonButton_Click" runat="server" Text="Create New Lesson" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <a class="btn list-group-item" data-toggle="modal" data-target="#newlessonmodal">Create New Lesson</a>

                    <% } %>
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

                            <% if (IsMemberProfessor())
                            { %>
                            <span class="fa">
                                <asp:LinkButton ID="DeleteLessonLink" OnClick="DeleteLessonLink_Click" CssClass="fa-times" runat="server"></asp:LinkButton>
                            </span>
                            <% } %>
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
        <div class="row">
            <div class="col-md-3">
                <div class="list-group">

                    <% if (IsMemberProfessor())
                       {%>
                   <div id="newassignmentmodal" class="modal fade" role="dialog">
                       <div class="modal-dialog">
                           <div class="modal-content">
                               <div class="modal-header">
                                   <h4 class="modal-title">Create New Assignment</h4>
                               </div>
                               <div class="modal-body">
                                   <!--<div class="col-md-offset-4 col-md-4">-->

                                   <div class="control-group form-group">
                                       <div class="controls">
                                           <label>Assignment Title</label>
                                           <asp:TextBox ID="NewAssignmentTitle" CssClass="form-control" runat="server"></asp:TextBox>
                                           <p class="help-block"></p>
                                       </div>
                                   </div>

                                   <div class="control-group form-group">
                                       <div class="controls">
                                           <label>Assignment Possible Points</label>
                                           <asp:TextBox ID="NewAssignmentPossiblePoints" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                           <p class="help-block"></p>
                                       </div>
                                   </div>

                                   <div class="control-group form-group">
                                       <div class="controls">
                                           <label>Assignment Content</label>
                                           <asp:TextBox ID="NewAssignmentContent" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                           <p class="help-block"></p>
                                       </div>
                                   </div>

                                   <!--</div>-->
                               </div>
                               <div class="modal-footer">
                                   <asp:Button ID="NewAssignmentButton" CssClass="btn btn-primary" OnClick="NewAssignmentButton_Click" runat="server" Text="Create New Lesson" />
                                   <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                               </div>
                           </div>
                       </div>
                   </div>

                   <a class="btn list-group-item" data-toggle="modal" data-target="#newassignmentmodal">Create New Assignment</a>

                   <% } %>

                    <asp:Repeater ID="AssignmentRepeater" ItemType="Enlighten.Models.Assignment" SelectMethod="AssignmentRepeater_GetData" OnItemCommand="AssignmentRepeater_ItemCommand" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton CommandArgument="<%# Item.Id %>" CssClass="list-group-item" runat="server"><%# Item.Title %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="col-md-9 ">
               <% if (AssignmentTitleLabel.Text != string.Empty)
                   { %>
               <div class="panel panel-default">
                   <div class="panel-heading text-center">
                       <h2>
                           <asp:Label ID="AssignmentTitleLabel" runat="server" Text=""></asp:Label>

                           <% if (IsMemberProfessor())
                           { %>
                           <span class="fa">
                               <asp:LinkButton ID="DeleteAssignmentLink" OnClick="DeleteAssignmentLink_Click" CssClass="fa-times" runat="server"></asp:LinkButton>
                           </span>
                           <% } %>
                       </h2>

                   </div>
                   <div class="panel-body">
                       <p>
                           <asp:Label ID="AssignmentContentLabel" runat="server" Text=""></asp:Label>
                       </p>
                   </div>
                   <div class="panel-footer">
                       <div class="row">
                           <div class="col-md-8">
                               <h4>Submissions</h4>
                           </div>
                       </div>
                      
                       <% if (!IsMemberProfessor())
                           { %>
                       <asp:Repeater ID="SubmissionRepeater" ItemType="Enlighten.Models.Submission" SelectMethod="SubmissionRepeater_GetData" OnItemCommand="SubmissionRepeater_ItemCommand" runat="server">
                           <ItemTemplate>
                               <asp:LinkButton CommandName="Download" CommandArgument="<%# Item.Id %>" runat="server"><%# Item.Title %></asp:LinkButton>
                           </ItemTemplate>
                           <FooterTemplate>
                               <asp:Label runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Text="No submissions."></asp:Label>
                           </FooterTemplate>
                       </asp:Repeater>
                       <hr />
                       
                       <div class="row">
                           <div class="col-md-8">
                               <div class="control-group form-group">
                                   <div class="controls">
                                       <asp:FileUpload CssClass="form-control" ID="SubmissionUpload" runat="server" />
                                   </div>
                               </div>
                               <div class="control-group form-group">
                                   <div class="controls">
                                       <asp:LinkButton CssClass="btn btn-primary" ID="SubmissionButton" OnClick="SubmissionButton_Click" runat="server">Submit Assignment</asp:LinkButton>
                                   </div>
                               </div>
                           </div>
                       </div>
                       <% }
    else { %>
                       
                       <div class="table-responsive">
                       <asp:GridView ID="GradeSubmissionView" ItemType="Enlighten.Models.Submission" AutoGenerateColumns="false" AutoGenerateEditButton="true" SelectMethod="GradeSubmissionView_GetData" UpdateMethod="GradeSubmissionView_UpdateItem" OnRowEditing="GradeSubmissionView_RowEditing" OnRowCancelingEdit="GradeSubmissionView_RowCancelingEdit"  OnRowUpdating="GradeSubmissionView_RowUpdating" DataKeyNames="Id" CssClass="table" runat="server">
                           <Columns>
                               <asp:TemplateField HeaderText="Title">
                                   <ItemTemplate>
                                       <asp:LinkButton ID="DownloadStudentSubmissionLink" CommandArgument="<%# Item.Id %>" OnCommand="DownloadStudentSubmissionLink_Command" runat="server">
                                           <%# Item.Title %>
                                       </asp:LinkButton>
                                       
                                   </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Score">
                                   <ItemTemplate>
                                       <%# Item.Score %>
                                   </ItemTemplate>
                                   <EditItemTemplate>
                                       <asp:TextBox ID="EditScoreText" TextMode="Number" Text="<%# BindItem.Score %>" runat="server"></asp:TextBox>
                                   </EditItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Student">
                                   <ItemTemplate>
                                       <%# Item.Member.FullName %>
                                   </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>
                           </div>
                       <asp:ValidationSummary ID="ValidationSummary1" ShowModelStateErrors="true" runat="server" />
                       <% } %>
                   </div>
               </div>
               <%} %>
           </div>

        </div>

    </asp:Panel>

    <asp:Panel ID="GradePanel" runat="server">
        <div class="col-md-12 table-responsive">
            <asp:GridView ID="GradeView" CssClass="table" SelectMethod="GradeView_GetData" AutoGenerateColumns="false" ItemType="Enlighten.Models.Submission" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="Assignment Title">
                        <ItemTemplate><%# Item.Assignment.Title %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Submission Title">
                        <ItemTemplate><%# Item.Title %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Score" ItemStyle-CssClass="active">
                        <ItemTemplate><%# Item.Score %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Points Possible">
                        <ItemTemplate><%# Item.Assignment.PossiblePoints %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Percentage">
                        <ItemTemplate><%# string.Format("{0:P2}", Item.Score/Item.Assignment.PossiblePoints) %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No grades!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
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

