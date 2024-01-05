<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="webAssistPill.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>User Login</title>
    <link rel="stylesheet" href="style.css"/>

</head>
<body>
    <script src="script.js"></script>
   <div class="container login">
        <h2>User Login</h2>
        <form runat="server">
            <label for="email">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:CustomValidator ID="cvEmail" 
                CssClass="errorMessage" 
                Display="Dynamic" 
                runat="server" 
                ErrorMessage="Please enter a valid email address!" 
                ValidateEmptyText="true" 
                ControlToValidate="txtEmail" 
                OnServerValidate="cvEmail_ServerValidate">
            </asp:CustomValidator>

            <label for="password">Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CustomValidator ID="cvPassword" 
                CssClass="errorMessage" Display="Dynamic" runat="server" 
                ErrorMessage="CustomValidator" 
                ValidateEmptyText="true" 
                ControlToValidate="txtPassword" 
                OnServerValidate="reqPassword_ServerValidate">
            </asp:CustomValidator>     

            <div style="margin: 5px;">
                <asp:Label ID="errorMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
            </div>
            <asp:Button ID="loginButton" value="Login" runat="server" Text="Login" CssClass="button" OnClick="loginButton_Click"/>
        </form>
        <p>Don't have an account? <a href="register.aspx">Register</a></p>
    </div>
</body>
</html>
