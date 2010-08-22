<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.ViewModels.MoviesIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Movies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="feedback">&nbsp;</h2>
    <form method="get" action="/Movies/Search">
        <input name="query" type="text" /><input type="submit" value="search" />
    </form>
    <small>Last updated: <%: Model.LastUpdated %></small>
    <%  Html.RenderPartial("MovieListing", Model.MovieList); %>
    
    <% if (Model.MovieList.Movies.HasPrevPage)
       { %>
          <%= Html.RouteLink("Previous page", "AllMovies", new { page = (Model.MovieList.Movies.PageIndex - 1), pageSize = Model.MovieList.Movies.PageSize })%>
    <% } %>

    <% if (Model.MovieList.Movies.HasNextPage)
       { %>
          <%= Html.RouteLink("Next page", "AllMovies", new { page = (Model.MovieList.Movies.PageIndex + 1), pageSize = Model.MovieList.Movies.PageSize })%>
    <% } %>

    <select onchange="location.href = '?pageSize=' + this.value">
        <% foreach (var option in Model.PageSizeOptions)
           { %>
            <option value="<%= option %>" <% if(Model.MovieList.Movies.PageSize == option) { %> selected="selected" <% } %>><%= option %></option>
           <% } %>
    </select>

</asp:Content>
