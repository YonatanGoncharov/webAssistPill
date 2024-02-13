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
                        Quantity: <%# Eval("MedicationAmount") %>
                    </div>
                    <asp:Button ID="RemoveButton" CssClass="remove-button"
                       OnClientClick='<%# "return confirm(\"Are you sure you want to remove this medication? You will remove any connected schedules!\");" %>'  
                        CommandArgument='<%# Eval("Medicationid") %>'
                         OnCommand="RemoveButton_Command" runat="server" Text="Remove" />
                    <asp:Button ID="EditMedicationButton"  CommandArgument='<%# Eval("Medicationid") %>' OnClick="EditMedicationButton_Click" CssClass="form-button" runat="server" Text="Edit" />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Button ID="NewMedicationButton" OnClick="NewMedicationButton_Click" CssClass="form-button" runat="server" Text="Add New Medication" />
    <div class="edit-medication-form" id="EditMedicationForm" runat="server">
        <h2>Edit Medication</h2>
        <div class="form-input">
            <input type="hidden" id="medication-id-edit" value="">
        </div>
        <div class="form-input">
            <label for="medication-name-edit">Medication Name:</label>
            <input type="text" id="medicationNameEdit" runat="server" placeholder="Enter medication name">
        </div>
         <div class="form-input">
             <label for="medication-how-to-take-add">Description:</label>
             <input type="text" id="medicationDescriptionEdit" runat="server" placeholder="Enter description">
         </div>
        <div class="form-input">
            <label for="medication-how-to-take-edit">How to Take:</label>
            <input type="text" id="medicationHowTakeEdit" runat="server" placeholder="Enter how to take">
        </div>
        <div class="form-input">
            <label for="medication-quantity-edit">Quantity:</label>
            <input type="number" id="medicationQuantityEdit" runat="server" placeholder="Enter medication quantity">
        </div>
        <div class="form-input">
            <label for="medication-picture-edit">Picture:</label>
            <input type="file" id="medicationPictureEdit" onchange="previewImage(this)" accept="image/*" runat="server" />
            <img runat="server" id="imagePreviewEdit" src="#" alt="Image Preview" />
        </div>
        <asp:Button ID="SaveEditButton" CssClass="form-button" OnClick="SaveEditButton_Click" runat="server" Text="Save Changes" />
    </div>
    <div class="add-medication-form" id="NewMedicationForm" runat="server">
        <h2>Add New Medication</h2>
        <div class="form-input">
            <label for="medication-name-add">Medication Name:</label>
            <input type="text" id="medicationNameAdd" runat="server" placeholder="Enter medication name">
        </div>
        <div class="form-input">
            <label for="medication-how-to-take-add">Description:</label>
            <input type="text" id="medicationDescriptionAdd" runat="server" placeholder="Enter description">
        </div>
        <div class="form-input">
            <label for="medication-how-to-take-add">How to Take:</label>
            <input type="text" id="medicationHowTakeAdd" runat="server" placeholder="Enter how to take">
        </div>
        <div class="form-input">
            <label for="medication-quantity-add">Quantity:</label>
            <input type="number" id="medicationQuantityAdd" runat="server" placeholder="Enter medication quantity">
        </div>
        <div class="form-input" >
            <label for="medication-picture-edit">Picture:</label>
            <input type="file" id="medicationPictureAdd" onchange="previewImage(this)" accept="image/*" runat="server" />
            <img id="imagePreview" src="#" alt="Image Preview" />
        </div>
        <asp:Button ID="AddNewMedicationButton" CssClass="form-button" OnClick="AddNewMedicationButton_Click" runat="server" Text="Add Medication" />
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
