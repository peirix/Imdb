<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Imdb.Models.Movie>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ShowList</h2>

    <table>
        <tr>
            <th>
                Name
            </th>
            <th>
                ReleaseYear
            </th>
            <th>
                Rank
            </th>
            <th>
                Rating
            </th>
            <th>
                Link
            </th>
            <th>
                Votes
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.ReleaseYear %>
            </td>
            <td>
                <%: item.Rank %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.Rating) %>
            </td>
            <td>
                <%: item.Link %>
            </td>
            <td>
                <%: item.Votes %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

