<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attendant_home.aspx.cs" Inherits="webAssistPill.attendant_home" %>


<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Home</title>
    <link rel="stylesheet" href="style.css"/>
</head>
<body>
    <form runat="server">
         <script src="script.js"></script>
        <div class="container">
            <h1>Patients List</h1>
            <div class="dropdownContainer">
                <label for="patient-dropdown">Select a Patient:</label>
                <asp:DropDownList ID="patientDropdown" runat="server" CssClass="patient-dropdown" DataValueField="PatientId" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="viewButton" CssClass="patient-button view-button" OnClick="viewButton_Click" runat="server" Text="View" />
            </div>
            <div class="selected-patient-card">
                <asp:Label ID="selectedPatientLabel" runat="server" CssClass="selected-patient-label"></asp:Label>
            </div>
        </div>
         <footer>
             <button onclick="location.href ='attendant_home.aspx'" class="switch-button">Home</button>
             <button onclick="location.href ='schedule.aspx'" class="switch-button">Schedule</button>
             <button onclick="location.href ='messages.aspx'" class="switch-button">Messages</button>
             <button onclick="location.href ='medication.aspx'" class="switch-button">Medication</button>
             <button class="exit-button" onclick="exitAccount()">Exit</button>
         </footer>
    </form>
</body>
</html>

