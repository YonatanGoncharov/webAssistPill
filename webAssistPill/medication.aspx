<%@ Page Title="" Language="C#" MasterPageFile="~/MainAttendant.Master" AutoEventWireup="true" CodeBehind="medication.aspx.cs" Inherits="webAssistPill.medication" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <script src="script.js"></script>
    <h1>Medication</h1>
        <div class="medication-list">
        <asp:Repeater ID="medicationRepeater" runat="server">
            <ItemTemplate>
                <div class="medication" value='<%# Eval("MedicationId") %>'>
                    <div class="medication-info">
                        Medication Name: <%# Eval("MedicationName") %><br>
                        How to Take: <%# Eval("HowToTake") %><br>
                        Quantity: <%# Eval("Quantity") %>
                    </div>
                    <button class="remove-button" onclick="removeMedication('<%# Eval("MedicationId") %>')">Remove</button>
                    <button class="form-button" onclick="showEditMedicationForm('<%# Eval("MedicationId") %>')">Edit</button>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <button class="form-button" onclick="showAddMedicationForm()">Add Medication</button>
    <div class="edit-medication-form">
        <h2>Edit Medication</h2>
        <div class="form-input">
            <input type="hidden" id="medication-id-edit" value="">
        </div>
        <div class="form-input">
            <label for="medication-name-edit">Medication Name:</label>
            <input type="text" id="medication-name-edit" placeholder="Enter medication name">
        </div>
        <div class="form-input">
            <label for="medication-how-to-take-edit">How to Take:</label>
            <input type="text" id="medication-how-to-take-edit" placeholder="Enter how to take">
        </div>
        <div class="form-input">
            <label for="medication-quantity-edit">Quantity:</label>
            <input type="number" id="medication-quantity-edit" placeholder="Enter medication quantity">
        </div>
        <button class="form-button" onclick="editMedication()">Save Changes</button>
    </div>
    <div class="add-medication-form">
        <h2>Add New Medication</h2>
        <div class="form-input">
            <label for="medication-name-add">Medication Name:</label>
            <input type="text" id="medication-name-add" placeholder="Enter medication name">
        </div>
        <div class="form-input">
            <label for="medication-how-to-take-add">How to Take:</label>
            <input type="text" id="medication-how-to-take-add" placeholder="Enter how to take">
        </div>
        <div class="form-input">
            <label for="medication-quantity-add">Quantity:</label>
            <input type="number" id="medication-quantity-add" placeholder="Enter medication quantity">
        </div>
        <button class="form-button" onclick="addMedicationFormValidation()">Add Medication</button>
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
