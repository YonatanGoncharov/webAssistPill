<%@ Page Title="Attendants" Language="C#" MasterPageFile="~/MainUser.Master" AutoEventWireup="true" CodeBehind="attendants.aspx.cs" Inherits="webAssistPill.attendants1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
     <h1>Your Attendants</h1>
 <div class="attendants-container" runat="server" id="attendantsContainer">
     <!-- Attendants will be dynamically added here using a repeater -->
    <asp:Repeater ID="repeaterAttendants" runat="server">
         <ItemTemplate>
             <div class="attendant">
                 <div class="attendant-info">
                     <div class="attendant-name"><%# Eval("AttendantNameGS") %></div>
                     <div class="attendant-priority">Priority: <%# GetPriority(Eval("AttendantIdGS")) %></div>
                 </div>
                 <asp:Button ID="removeButton" CssClass="remove-button" OnClientClick='<%# "return confirmRemove(\"" + Eval("AttendantNameGS") + "\");" %>'
                     OnClick="removeButton_Click" runat="server" Text="Remove" CommandArgument='<%# Eval("AttendantIdGS") %>' />
             </div>
         </ItemTemplate>
     </asp:Repeater>
 </div>
 <asp:Button ID="newAttendantButton" runat="server" CssClass="button" Text="Add New Attendant" OnClick="newAttendantButton_Click" />
        
 <!-- Add the new tab for adding attendants -->
 <div id="addAttendantTab" class="add-attendant-tab" runat="server">
     <h2>Add New Attendant</h2>
     <div>
         <label for="txtAttendantEmail">Attendant Email:</label>
         <asp:TextBox ID="txtAttendantEmail" runat="server" CssClass="input-field"></asp:TextBox>
     </div>
     <div>
         <label for="txtAttendantPriority">Priority:</label>
         <asp:TextBox ID="txtAttendantPriority" runat="server" CssClass="input-field"></asp:TextBox>
     </div>
     <div id="errorDiv" runat="server" class="errorMessage" style="display:none;"></div>
     <asp:Button ID="btnSaveAttendant" runat="server" CssClass="button" Text="Save" OnClick="btnSaveAttendant_Click" />
     <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
 </div>
</asp:Content>
