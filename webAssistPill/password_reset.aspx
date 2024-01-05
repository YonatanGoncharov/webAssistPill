<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="password_reset.aspx.cs" Inherits="webAssistPill.password_reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Password Change</title>
    <link rel="stylesheet" href="style.css"/>
</head>
<body>
<script src="script.js"></script>
    <div class="container password-change">
        <h2>Password Change</h2>
        <form runat="server">
            <label for="new-password">New Password: 
                <asp:TextBox ID="txtNewPass" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="cvPassword" 
                    CssClass="errorMessage" Display="Dynamic" runat="server" 
                    ErrorMessage="CustomValidator" 
                    ValidateEmptyText="true" 
                    ControlToValidate="txtNewPass" 
                    OnServerValidate="cvPassword_ServerValidate">
                </asp:CustomValidator> 
            </label>
            <label for="confirm-password">Confirm New Password: 
                 <asp:TextBox ID="txtConPass" runat="server"></asp:TextBox>
                <asp:CustomValidator ID="cvConfirmPassword" 
                    CssClass="errorMessage" Display="Dynamic" runat="server" 
                    ErrorMessage="CustomValidator" 
                    ValidateEmptyText="true" 
                    ControlToValidate="txtConPass" 
                    OnServerValidate="cvConfirmPassword_ServerValidate">
                </asp:CustomValidator>
            </label>
            <asp:Button ID="changePasButton" runat="server" Text="Change Password" CssClass="button" OnClick="changePasButton_Click"/>
        </form>
         <div style="margin-top: 5px;">
             <asp:Label ID="errorMessage" runat="server" Text=""></asp:Label>
         </div>
        <p>Want to go back? <a href="main.aspx">Home</a></p>
    </div>
</body>
</html>
