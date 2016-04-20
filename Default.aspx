<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeaderContent" runat="Server">
    <header id="myCarousel" class="carousel slide">
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="2"></li>
        </ol>

        <div class="carousel-inner">
            <div class="item active">
                <img src="https://mexicoinstitute.files.wordpress.com/2013/01/education2.jpg" />
                <div class="carousel-caption">
                    <h2>Caption 1</h2>
                </div>
            </div>
            <div class="item">
                <img src="http://imrodmartin.com/images/wp-content/2014/01/How-Technology-Is-Improving-Education-.jpg" />
                <div class="carousel-caption">
                    <h2>Caption 2</h2>
                </div>
            </div>
            <div class="item">
                <img src="http://d.fastcompany.net/multisite_files/coexist/imagecache/1280/poster/2012/12/1681097-poster-4-1280-higher-education-2020.jpg" />
                <div class="carousel-caption">
                    <h2>Caption 3</h2>
                </div>
            </div>
        </div>

        <a class="left carousel-control" href="#myCarousel" data-slide="prev">
            <span class="icon-prev"></span>
        </a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">
            <span class="icon-next"></span>
        </a>
    </header>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="Server">
    
    <asp:Panel ID="AnonymousPanel" runat="server">
        <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Login</h1>
                </div>

                <div class="col-md-offset-4 col-md-4">
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
                            <label>Remember me? </label>
                            <asp:CheckBox ID="RememberMe" runat="server" />
                            <p class="help-block"></p>
                        </div>
                    </div>

                    <div class="control-group form-group">
                        <div class="controls">
                    <asp:Button ID="LoginButton" CssClass="btn btn-primary " runat="server" Text="Submit" OnClick="LoginButton_Click" />
                            </div>
                        </div>
                    <asp:Label ID="SuccessLabel" runat="server" Text=""></asp:Label>
                </div>
            </div>
    </asp:Panel>

    <asp:Panel ID="LoggedInPanel" runat="server">
        <div class="row">
                <div class="col-lg-12">
                    <h1 class="page-header">Home</h1>
                </div>
           
        </div>

        <asp:Repeater ID="CourseRepeater" runat="server" ItemType="Enlighten.Models.Course" SelectMethod="CourseRepeater_GetData" EnableTheming="true">
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-7">
                        <a href="portfolio-item.html">
                            <img class="img-responsive img-hover" src="http://placehold.it/700x300" alt="">
                        </a>
                    &nbsp;</div>
                    <div class="col-md-5">
                        <h3><%# Item.Title %></h3>
                        <h4><%# Item.Location.BuildingName %> - <%# Item.Time %></h4>
                        <h5>Professor: <%# GetProfessorById(Item.ProfessorId).FullName %></h5>
                        <h6>E-mail: <%# GetProfessorById(Item.ProfessorId).Email %></h6>
                        <p><%# Item.Description %></p>
                        <a class="btn btn-primary" href="/Course?Id=<%# Item.Id %>">View Course</i></a>
                    </div>
                </div>

            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
            <FooterTemplate>
                <asp:Label ID="lblEmptyData" runat="server" Visible='<%# ((Repeater)Container.NamingContainer).Items.Count == 0 %>' Text="Not enrolled in any classes!"></asp:Label>
            </FooterTemplate>
        </asp:Repeater>
        
    </asp:Panel>

</asp:Content>

