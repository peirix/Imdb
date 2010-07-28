<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.Models.Movie>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.Name %></h2>
    <a href="http://www.imdb.com/title/<%= Model.Link %>">Read more at IMDb</a>
    <h4>Ranking history</h4>
    <ul>
        <li><strong><%= Model.Rank %></strong></li>
        <% List<int> rankLog = ViewData["rankLog"] as List<int>;
           foreach (var rank in rankLog)
           { %>
                <li><%= rank %></li>
           <% } %>
    </ul>
    <div id="usersSeenMovie">
        <h4>Users who have seen this movie</h4>
        <ul>
            <% foreach (var user in ViewData["users"] as List<string>)
               {
                   if (user == User.Identity.Name)
                   { %>
                    <li><a href="/User">You</a></li>
                    <% }
                   else
                   { %>
                    <li><a href="/User/<%= user %>"><%= user%></a> <small>(<a href="/User/Compare/<%= user %>">compare to</a>)</small></li>
               <% }
            } %>
        </ul>
    </div>
</asp:Content>
