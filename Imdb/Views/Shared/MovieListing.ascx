<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Imdb.ViewModels.MovieList>" %>

<% if (Model.Movies.Count() == 0) { %> <h4 style="color:Red">No hits</h4> <% } %>
<% else if (Model.Movies.Count() == 1) { %><h4>Found 1 movie</h4> <% } %>
<% else { %><h4>Found <%= Model.Movies.Count() %> movies</h4> <% } %>

<% if (Model.Movies.Count() > 0) { %>
<ul id="movieList">
    <%  foreach (var movie in Model.Movies)
        {
            bool seenIt = false;
            if (Context.User.Identity.IsAuthenticated && Model.SeenMovies != null)
            {
                if (Model.SeenMovies.Contains(movie.ID))
                {
                    seenIt = true;
                }
            }
            
            %>
                <li>
                    <span class="movieRank">
                        <%= movie.Rank %>
                        <img src="/Content/images/<%: Model.GetRankMove(movie.ID, movie.Rank) %>.png" />
                    </span>
                <%
                if (seenIt)
                {%>
                    <a class="seenMovie" href="/Movies/Details/<%= movie.ID %>" id="<%= movie.ID %>"><%= movie.Name%></a>
                    - <a class="unsee">I haven't seen it</a>
                <% }
                else
                { %>
                    <a href="/Movies/Details/<%= movie.ID %>" id="<%= movie.ID %>"><%= movie.Name%></a>
                    - <a class="see">I've seen it</a>
                
                <% } %>

            </li>
        <% } %>

</ul>

<script>
    $(document).ready(function () {
        $(".unsee").live("click", function () {
            handleMovie(this, "/Seen/UnSeenMovie/");
            $(this).attr("class", "see").text("I've seen it").prev("a").removeClass("seenMovie");
        });

        $(".see").live("click", function () {
            handleMovie(this, "/Seen/SeenMovie/");
            $(this).attr("class", "unsee").text("I haven't seen it").prev("a").addClass("seenMovie");
        });
    });

    function handleMovie(elm, url) {
        var link = $(elm).prev("a");
        $.post(url + link.attr("id"), function (data) {
            try {
                console.log(data);
            } catch (e) {
                console.log("error: ", e.message);
            }
        });
    }
</script>

<% } %>