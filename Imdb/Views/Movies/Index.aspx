<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.ViewModels.MovieListViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Movies
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="feedback">&nbsp;</h2>
    <form method="get" action="/Movies/Search">
        <input name="query" type="text" /><input type="submit" value="search" />
    </form>
    <%  ViewData["seenMovies"] = Model.SeenMovies;
        Html.RenderPartial("MovieListing", Model.PaginatedMovies); %>
    
    <% if (Model.PaginatedMovies.HasPrevPage)
       { %>
          <%= Html.RouteLink("Previous page", "AllMovies", new { page = (Model.PaginatedMovies.PageIndex - 1), pageSize = Model.PaginatedMovies.PageSize })%>
    <% } %>

    <% if (Model.PaginatedMovies.HasNextPage)
       { %>
          <%= Html.RouteLink("Next page", "AllMovies", new { page = (Model.PaginatedMovies.PageIndex + 1), pageSize = Model.PaginatedMovies.PageSize })%>
    <% } %>

    <select onchange="location.href = '?pageSize=' + this.value">
        <% foreach (var option in Model.PageSizeOptions)
           { %>
            <option value="<%= option %>" <% if(Model.PaginatedMovies.PageSize == option) { %> selected="selected" <% } %>><%= option %></option>
           <% } %>
    </select>

</asp:Content>
