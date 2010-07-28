<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Imdb.Models.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Search</h2>
    <form method="get" action="/Movies/Search">
        <input type="text" name="query" value="<%: ViewData["query"] %>" /> <input type="submit" value="Search" />
    </form>
    <% if (Model.Count() < 1) { %> <h4 style="color:Red;">No hits</h4> <% } %>
    
    <% else { Html.RenderPartial("MovieListing"); } %>
</asp:Content>
