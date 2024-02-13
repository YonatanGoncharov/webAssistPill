<%@ Page Title="Schedule" Language="C#" MasterPageFile="~/MainAttendant.Master" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="webAssistPill.schedule1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
       <script src="script.js"></script>

       <h1>Medication Schedule</h1>
       <div class="medication-list">
           <asp:Repeater ID="medicationsRepeater" runat="server">
               <ItemTemplate>
                   <div class="medication" value='<%#Eval("medicationIdGS") + "" + Eval("takingTimeGS")%>'>
                       <div class="medication-info"> 
                           Medication Name: <%#Eval("medicationNameGS")%><br/>
                           Time to Take: <%# TimetoString( Eval("takingTimeGS")) %><br/>
                           Repeat Days: <%# GetRepeatedDays( Eval("medicationIdGS") , Eval("takingTimeGS"))%>
                       </div>
                     <asp:Button ID="RemoveButton" CssClass="remove-button" runat="server" Text="Remove" 
                    OnClientClick='<%# "return confirm(\"Are you sure you want to remove this schedule?\");" %>'
                    OnCommand="RemoveButton_Command"
                    CommandArgument='<%# Eval("medicationIdGS") + "*" + Eval("takingTimeGS") %>' />
                       <asp:Button ID="EditButton" CommandArgument='<%# Eval("medicationIdGS") + "*" + Eval("takingTimeGS") %>' OnClick="EditButton_Click" CssClass="edit-button" runat="server" Text="Edit" />
                   </div>
               </ItemTemplate>
           </asp:Repeater>
       </div>
        <asp:Button ID="AddButton" runat="server" Text="Add New Schedule"  CssClass="add-medication-button" OnClick="AddButton_Click"/>
     
   <div class="new-medication-form" runat="server" id="newMedicationForm">
       <h2>Add New Medication</h2>
       <div class="form-input">
           <label for="medication-dropdown">Select Medication:</label>
           <asp:DropDownList ID="medicationDropdown" runat="server" CssClass="medication-dropdown" DataValueField="MedicationId" DataTextField="MedicationName" AutoPostBack="true"></asp:DropDownList>
       </div>
       <div class="form-input">
           <label for="medication-time">Time to Take:</label>
           <input type="time" id="medicationTime" runat="server">
       </div>
       <div class="form-input">
           <label>Repeat on Days:</label><br>
           <div id="formInputDays" runat="server"> 
                <input type="checkbox" runat="server" id="monday"> Monday
                <input type="checkbox" runat="server" id="tuesday"> Tuesday
                <input type="checkbox" runat="server" id="wednesday"> Wednesday
                <input type="checkbox" runat="server" id="thursday"> Thursday
                <input type="checkbox" runat="server" id="friday"> Friday
                <input type="checkbox" runat="server" id="saturday"> Saturday
                <input type="checkbox" runat="server" id="sunday"> Sunday
           </div>
       </div>
       <asp:Button ID="FormAddMedButton" CssClass="form-button" OnClick="FormAddMedButton_Click" runat="server" Text="Add Schedule" />
       <asp:Button ID="FormExitButton" OnClick="FormExitButton_Click" CssClass="form-exit-button" runat="server" Text="Exit" />
   </div>
   <div class="edit-medication-form" runat="server" id="editMedicationForm"> 
       <h2>Edit Medication</h2>
       <div class="form-input">
           <asp:Label ID="MedicationEditLabel" runat="server" Text=""></asp:Label>
       </div>
       <div class="form-input">
           <label for="medication-time-edit">Time to Take:</label>
           <input type="time" id="medicationTimeEdit" runat="server">
       </div>
       <div class="form-input">
           <label>Repeat on Days:</label>
           <div id="formInputDaysEdit" runat="server"> 
               <input type="checkbox" runat="server" id="mondayEdit"> Monday
               <input type="checkbox" runat="server" id="tuesdayEdit"> Tuesday
               <input type="checkbox" runat="server" id="wednesdayEdit"> Wednesday
               <input type="checkbox" runat="server" id="thursdayEdit"> Thursday
               <input type="checkbox" runat="server" id="fridayEdit"> Friday
               <input type="checkbox" runat="server" id="saturdayEdit"> Saturday
               <input type="checkbox" runat="server" id="sundayEdit"> Sunday
           </div>
       </div>
       <input type="hidden" id="medication-id-edit" value="">
       <asp:Button ID="SaveChangeEditForm" CssClass="form-button" OnClick="SaveChangeEditForm_Click" runat="server" Text="Save Changes" />
       <asp:Button ID="ExitButtonEditForm" CssClass="form-exit-button" OnClick="ExitButtonEditForm_Click" runat="server" Text="Exit" />
   </div>

   <!-- Popup div for displaying error messages -->
   <div class="popup" id="error-popup">
       <div class="popup-content">
           <span class="close-popup" onclick="closeErrorPopup()">&times;</span>
           <p id="error-message"></p>
       </div>
   </div>
    <div class="selected-patient-card">
    <asp:Label ID="selectedPatientLabel" runat="server" CssClass="selected-patient-label"></asp:Label>
    </div>
</asp:Content>
