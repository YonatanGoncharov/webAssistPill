<%@ Page Title="" Language="C#" MasterPageFile="~/MainAttendant.Master" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="webAssistPill.messages1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
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
    <div class="selected-patient-card">
    <asp:Label ID="selectedPatientLabel" runat="server" CssClass="selected-patient-label"></asp:Label>
    </div>
</asp:Content>
