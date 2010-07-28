<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.Helpers.PaginatedList<Imdb.Models.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Movies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="feedback">&nbsp;</h2>
    <form method="get" action="/Movies/Search">
        <input name="query" type="text" /><input type="submit" />
    </form>
    <% Html.RenderPartial("MovieListing"); %>

    <% if (Model.HasPrevPage)
       { %>
          <%= Html.RouteLink("Previous page", "AllMovies", new { page = (Model.PageIndex-1) }) %>
    <% } %>

    <% if (Model.HasNextPage)
       { %>
          <%= Html.RouteLink("Next page", "AllMovies", new { page = (Model.PageIndex+1) }) %>
    <% } %>

    <select onchange="location.href = '?pageSize=' + this.value">
        <% foreach (var option in ViewData["pageSizeOptions"] as List<int>)
           { %>
            <option value="<%= option %>" <% if(Model.PageSize == option) { %> selected="selected" <% } %>><%= option %></option>
           <% } %>
    </select>

</asp:Content>
