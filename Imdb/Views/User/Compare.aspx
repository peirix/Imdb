<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Imdb.Models.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Compare
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Compare</h2>
    <%
        List<Imdb.Models.Movie> mySeen = ViewData["mySeen"] as List<Imdb.Models.Movie>;
        List<Imdb.Models.Movie> otherSeen = ViewData["otherSeen"] as List<Imdb.Models.Movie>;
        List<Imdb.Models.Movie> bothSeen = ViewData["bothSeen"] as List<Imdb.Models.Movie>;
    %>

    <h4 class="column3">Only you've seen</h4>
    <h4 class="column3">Only they've seen</h4>
    <h4 class="column3">Both seen</h4>
    <ul class="column3">
        <% foreach (var movie in mySeen)
           { %>
           <li><%= movie.Rank %>. <a href="/Movies/Details/<%= movie.ID %>"><%= movie.Name %></a></li>
        <% } %>
    </ul>
    <ul class="column3">
        <% foreach (var movie in otherSeen)
           { %>
           <li><%= movie.Rank %>. <a href="/Movies/Details/<%= movie.ID %>"><%= movie.Name %></a></li>
        <% } %>
    </ul>
    <ul class="column3">
        <% foreach (var movie in bothSeen)
           { %>
           <li><%= movie.Rank %>. <a href="/Movies/Details/<%= movie.ID %>"><%= movie.Name %></a></li>
        <% } %>
    </ul>

    <div style="clear: left;"></div>

</asp:Content>
