<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="webAssistPill.messages" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Messages</title>
    <link rel="stylesheet" href="style.css">
</head>
<body>
    <form id="form1" runat="server">
        <script src="script.js"></script>
        <div class="container">
            <h1>Messages</h1>
            <div class="message-list">
                <asp:Repeater ID="messagesRepeater" runat="server">
                    <ItemTemplate>
                        <div class="message">
                            <div class="message-status"><%# GetMessageStatus((bool)Eval("messageSeenGS")) %></div>
                            <div class="message-content"><%# Eval("messageContentGS") %></div>
                            <asp:Button runat="server" CssClass="remove-button" Text="Remove" 
                                OnClientClick='<%# "return confirm(\"Are you sure you want to remove this message?\");" %>' 
                                OnCommand="RemoveMessage_Command" CommandArgument='<%# Eval("messageIdGS") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </div>
    </form>
    <footer>
        <button onclick="location.href ='attendant_home.aspx'" class="switch-button">Home</button>
        <button onclick="location.href ='schedule.aspx'" class="switch-button">Schedule</button>
        <button onclick="location.href ='messages.aspx'" class="switch-button">Messages</button>
        <button onclick="location.href ='medication.aspx'" class="switch-button">Medication</button>
        <button class="exit-button" onclick="exitAccount()">Exit</button>
    </footer>
</body>
</html>
