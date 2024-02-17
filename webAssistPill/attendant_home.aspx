<%@ Page Title="Home" Language="C#" MasterPageFile="~/MainAttendant.Master" AutoEventWireup="true" CodeBehind="attendant_home.aspx.cs" Inherits="webAssistPill.attendant_home1"%>
<%@ MasterType VirtualPath="~/MainAttendant.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <h1>Patients List</h1>
    <div class="dropdownContainer">
    <label for="patient-dropdown">Select a Patient:</label>
    <asp:DropDownList ID="patientDropdown" runat="server" CssClass="patient-dropdown" DataValueField="PatientId" AutoPostBack="true"></asp:DropDownList>
    <asp:Button ID="viewButton" CssClass="patient-button view-button" OnClick="viewButton_Click" runat="server" Text="View" />
    <div class="selected-patient-card">
        <asp:Label ID="selectedPatientLabel" runat="server" CssClass="selected-patient-label"></asp:Label>
    </div>
</div>
</asp:Content>
