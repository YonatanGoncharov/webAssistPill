<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scheduled.aspx.cs" Inherits="webAssistPill.schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Medication Schedule</title>
    <link rel="stylesheet" href="style.css" />
</head>                                          
<body>
    <form id="form1" runat="server">
        <script src="script.js"></script>
        <div class="container">
            <h1>Medication Schedule</h1>
            <div class="medication-list">
                <asp:Repeater ID="medicationsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="medication" value='<%# Eval("medicationIdGS") %>'>
                            <div class="medication-info"> 
                                Medication Name: <%# GetMedName(Eval("medicationIdGS"))  %><br />
                                Time to Take: <%# Eval("takingTimeGS") %><br />
                                Repeat Days: <%# GetRepeatedDays( GetMedName(Eval("medicationIdGS")) , Eval("takingTimeGS"))%>
                            </div>
                            <button class="remove-button" onclick='removeMedication(<%# Eval("MedicationId") %>)'>Remove</button>
                            <button class="edit-button" onclick='showEditMedicationScheduleForm(<%# Eval("MedicationId") %>)'>Edit</button>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <button class="add-medication-button" onclick="showNewMedicationForm()">Add New Medication</button>
        </div>
        <div class="new-medication-form">
            <h2>Add New Medication</h2>
            <div class="form-input">
                <label for="medication-dropdown">Select Medication:</label>
                <select id="medication-dropdown">
                <asp:DropDownList ID="medicationDropdown" runat="server" CssClass="medication-dropdown" DataValueField="MedicationId" DataTextField="MedicationName" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="viewButton" CssClass="medication-button view-button" OnClick="viewButton_Click" runat="server" Text="View" />
                </select>
            </div>
            <div class="form-input">
                <label for="medication-time">Time to Take:</label>
                <input type="time" id="medication-time">
            </div>
            <div class="form-input">
                <label>Repeat on Days:</label><br>
                <input type="checkbox" id="monday"> Monday
                <input type="checkbox" id="tuesday"> Tuesday
                <input type="checkbox" id="wednesday"> Wednesday
                <input type="checkbox" id="thursday"> Thursday
                <input type="checkbox" id="friday"> Friday
                <input type="checkbox" id="saturday"> Saturday
                <input type="checkbox" id="sunday"> Sunday
            </div>
            <button class="form-button" onclick="addMedicationSchedule()">Add Medication</button>
            <button class="exit-button-form" onclick="exitForm('.new-medication-form')">Exit</button>
        </div>
        <div class="edit-medication-form">
            <h2>Edit Medication</h2>
            <div class="form-input">
                <label for="medication-dropdown-edit">Select Medication:</label>
                <select id="medication-dropdown-edit">
                    <option value="">Select Medication</option>
                    <option value="Medication Name 1">Medication Name 1</option>
                    <option value="Medication Name 2">Medication Name 2</option>
                    <option value="Medication Name 3">Medication Name 3</option>
                    <!-- JavaScript will populate options here -->
                </select>
            </div>
            <div class="form-input">
                <label for="medication-time-edit">Time to Take:</label>
                <input type="time" id="medication-time-edit">
            </div>
            <div class="form-input">
                <label>Repeat on Days:</label><br>
                <input type="checkbox" id="monday"> Monday
                <input type="checkbox" id="tuesday"> Tuesday
                <input type="checkbox" id="wednesday"> Wednesday
                <input type="checkbox" id="thursday"> Thursday
                <input type="checkbox" id="friday"> Friday
                <input type="checkbox" id="saturday"> Saturday
                <input type="checkbox" id="sunday"> Sunday
            </div>
            <input type="hidden" id="medication-id-edit" value="">
            <button class="form-button" onclick="editMedicationSchedule()">Save Changes</button>
            <button class="exit-button-form" onclick="exitForm('.edit-medication-form')">Exit</button>
        </div>

        <!-- Popup div for displaying error messages -->
        <div class="popup" id="error-popup">
            <div class="popup-content">
                <span class="close-popup" onclick="closeErrorPopup()">&times;</span>
                <p id="error-message"></p>
            </div>
        </div>

        <footer>
            <button onclick="location.href ='attendant_home.html'" class="switch-button">Home</button>
            <button onclick="location.href ='schedule.html'" class="switch-button">Schedule</button>
            <button onclick="location.href ='messages.html'" class="switch-button">Messages</button>
            <button onclick="location.href ='medication.html'" class="switch-button">Medication</button>
            <button class="exit-button" onclick="exitAccount()">Exit</button>
        </footer>
    </form>
</body>
</html>
