<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Imdb.Models.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= ViewData["username"] %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= ViewData["username"] %></h2>

    <h4>Badges</h4>
    <% List<Imdb.Models.Badge> badges = ViewData["badges"] as List<Imdb.Models.Badge>;
       foreach (var badge in badges)
       { %>
            <%: badge.Name %> (<%: badge.Type.ToLower() %>)<br />
        <% } %>


    <% Html.RenderPartial("MovieListing"); %>
</asp:Content>
