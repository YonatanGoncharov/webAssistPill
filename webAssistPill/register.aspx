<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="webAssistPill.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
     <meta charset="UTF-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
     <title>User Registration</title>
     <link rel="stylesheet" href="style.css"/>
</head>
<body>
    <script src="script.js"></script>
<div class="container register">
    <h2>User Registration</h2>
    <form runat="server">
        <label for="name">First Name:</label>
        <asp:TextBox ID="txtfnameReg" runat="server"></asp:TextBox>
         <asp:CustomValidator ID="cvFname" 
             CssClass="errorMessage" Display="Dynamic" runat="server" 
             ErrorMessage="CustomValidator" 
             ValidateEmptyText="true" 
             ControlToValidate="txtfnameReg" 
             OnServerValidate="cvFname_ServerValidate">
         </asp:CustomValidator>   
        
        <label for="lastName">Last Name:</label>
        <asp:TextBox ID="txtlNameReg" runat="server"></asp:TextBox>
        <asp:CustomValidator ID="cvLname" 
            CssClass="errorMessage" Display="Dynamic" runat="server" 
            ErrorMessage="CustomValidator" 
            ValidateEmptyText="true" 
            ControlToValidate="txtlNameReg" 
            OnServerValidate="cvLname_ServerValidate">
        </asp:CustomValidator> 

        <label for="email">Email:</label>
        <asp:TextBox ID="txtEmailReg" runat="server"></asp:TextBox>
        <asp:CustomValidator ID="cvEmail" 
            CssClass="errorMessage" 
            Display="Dynamic" 
            runat="server" 
            ErrorMessage="Please enter a valid email address!" 
            ValidateEmptyText="true" 
            ControlToValidate="txtEmailReg" 
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

        <label class="checkbox-label">
            <asp:CheckBox ID="isAttendantCb" runat="server" Text="Are you an attendant?" />
        </label>
        <div style="margin: 5px;">
        <asp:Label ID="errorMessage" runat="server" Text=""></asp:Label>
        </div>
        <asp:Button ID="registerButton" runat="server" Text="Register" CssClass="button" OnClick="registerButton_Click"/>
    </form>
</div>
</body>
</html>
