<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Imdb.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.
    </p>

    <style>
        input[type=range]::before { content:attr(min); }
        input[type=range]::after { content:attr(max); }
    </style>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.UserName) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.UserName) %>
                    <%: Html.ValidationMessageFor(m => m.UserName) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Email) %>
                </div>
                <div class="editor-field">
                    <%: Html.TextBoxFor(m => m.Email) %>
                    <%: Html.ValidationMessageFor(m => m.Email) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.Password) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.Password) %>
                    <%: Html.ValidationMessageFor(m => m.Password) %>
                </div>
                
                <div class="editor-label">
                    <%: Html.LabelFor(m => m.ConfirmPassword) %>
                </div>
                <div class="editor-field">
                    <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                </div>
                
                <p>
                    <input type="submit" value="Register" />
                </p>
            </fieldset>
        </div>
    <% } %>

    <%-- WebForm 2.0 testing
    <% using (Html.BeginForm()) { %>
        <fieldset>
            <legend>Personal details</legend>

            <label for="FirstName">Fornavn:</label><input type="text" id="FirstName" placeholder="Fornavn">
            <label for="LastName">Etternavn:</label><input type="text" id="LastName" placeholder="Etternavn">
            <br>
            <label for="BirthDate">Født:</label><input type="date" id="BirthDate">
            <br>
            <label for="Phone">Mobil:</label><input type="tel" required="required" id="Phone">
            <br>
            <label for="ColorTheme">Fargetema:</label><input type="color" id="ColorTheme">
            <br>
            <label for="MoviesPerWeek">Hvor mange filmer ser du i uken?</label>
            <input type="range" min="1" max="10" step="1" value="1" id="MoviesPerWeek">
            <br>
            <br>
            <input type="submit" value="Gønn på">
        </fieldset>
    <% } %>
    --%>
</asp:Content>
