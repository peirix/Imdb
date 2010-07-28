<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Imdb.Models.Movie>>" %>

<% if (Model.Count() == 0) { %> <h4 style="color:Red">No hits</h4> <% } %>
<% else if (Model.Count() == 1) { %><h4>Found 1 movie</h4> <% } %>
<% else { %><h4>Found <%= Model.Count() %> movies</h4> <% } %>

<% if (Model.Count() > 0) { %>
<ul id="movieList">
    <%  foreach (var movie in Model)
        {
            bool seenIt = false;
            if (ViewData.ContainsKey("seenMovies"))
            {
                List<int> seenMovies = ViewData["seenMovies"] as List<int>;
                if (seenMovies.Contains(movie.ID))
                {
                    seenIt = true;
                }
            }

            if (seenIt)
            {%>
                <li><%= movie.Rank %>. <a class="seenMovie" href="/Movies/Details/<%= movie.ID %>" id="<%= movie.ID %>"><%= movie.Name%></a> - <a class="unsee">I haven't seen it</a>
            <% }
            else
            { %>
                <li><%= movie.Rank %>. <a href="/Movies/Details/<%= movie.ID %>" id="<%= movie.ID %>"><%= movie.Name%></a> - <a class="see">I've seen it</a>
            <% }
        } %>

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