<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="password_reset_request.aspx.cs" Inherits="webAssistPill.password_reset_request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Password Reset</title>
    <link rel="stylesheet" href="style.css"/>
</head>
<body>
    <script src="script.js"></script>
    <div class="container password-reset">
        <h1>Password Reset</h1>
        <form class="resetform"  runat="server">
            <label for="email" id="emailLabel">Email: 
                <asp:TextBox ID="emailRequest" runat="server"></asp:TextBox>
            </label>
            <div>
                <asp:CustomValidator ID="cvEmail" 
                CssClass="errorMessage" 
                Display="Dynamic" 
                runat="server" 
                ErrorMessage="Please enter a valid email address!" 
                ValidateEmptyText="true" 
                ControlToValidate="emailRequest" 
                OnServerValidate="cvEmail_ServerValidate">
            </asp:CustomValidator>
            </div>
             <asp:Button ID="emailRequestButton" CssClass="button" OnClick="emailRequestButton_Click" runat="server" Text="Button" />
        </form>
    
        <div style="margin: 5px;">
            <asp:Label ID="errorMessage" runat="server" Text=""></asp:Label>
        </div>
    <p>Remember your password? <a href="login.aspx">Login</a></p>
</div>
</body>
</html>
