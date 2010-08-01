<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.ViewModels.UserIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["username"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.Username %></h2>

    <h4>Badges</h4>
    <% foreach (var badge in Model.Badges)
       { %>
        <%: badge.Name %> (<%: badge.Type.ToLower() %>)<br />
    <% } %>


    <% Html.RenderPartial("MovieListing", Model.Movies); %>
</asp:Content>
