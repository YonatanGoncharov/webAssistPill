<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="webAssistPill.main" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>AssistPill - Home</title>
    <link rel="stylesheet" href="style.css"/> <!-- Link to your external CSS file -->
</head>
<body>
    <form runat="server">
           <div class="quote-container">
           <asp:Label ID="quoteTextLiteral" runat="server" CssClass="quote-text" Text=""></asp:Label>
           <asp:Label ID="quoteAuthorLiteral" CssClass="quote-author" runat="server" Text=""></asp:Label>
      </div>
        <div class="container same-width" >
        <header>
            <h1>Welcome to AssistPill</h1>
            <img src="images/AssistPill.png" alt="AssistPill Logo" width="150"/>
        </header>
        <section>
            <h2>What is AssistPill?</h2>
            <p>
                AssistPill is your trusted medication management and reminder system. 
                It helps you keep track of your medications, medication schedule, and more, 
                making your healthcare journey easier and more efficient.
            </p>
            <!-- Add some photos or illustrations here -->
        </section>
      
        <div class="login-register-buttons-container" runat="server">
            <asp:Button ID="loginButton" CssClass="switch-button" runat="server" Text="Login" OnClick="loginButton_Click"/>
            <asp:Button ID="registerButton" CssClass="switch-button" runat="server" Text="Register" OnClick="registerButton_Click"/>
        </div>
        </div>
        <footer class="same-width">
            <p>&copy; 2023 AssistPill. All rights reserved.</p>
        </footer>
    </form >
</body>
</html>
