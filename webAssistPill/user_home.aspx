<%@ Page Title="Medical Schedule" Language="C#" MasterPageFile="~/MainUser.Master" AutoEventWireup="true" CodeBehind="user_home.aspx.cs" Inherits="webAssistPill.user_home1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <h1>Today's Medication Schedule</h1>
<div class="schedule-block-container">
    <!-- Morning Repeater -->
    
    <div class="schedule-block">
    <h3>Morning</h3>
    <asp:Repeater ID="morningRepeater" runat="server">
    <ItemTemplate>
        <div class="med ">
            <button class="button med-button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
                <%# GetMedName(Eval("medicationIdGS")) %>
            </button> 
            
            <div class="med-takingtime"><%# GetMedTakingTime(Eval("takingTimeGS")) %></div>
        </div>
    </ItemTemplate>
    </asp:Repeater>
    </div>

    <!-- Noon Repeater -->
    <div class="schedule-block">
    <h3>Noon</h3>
    <asp:Repeater ID="noonRepeater" runat="server">
         <ItemTemplate>
             <div class="med">
                 <button class="button med-button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
                     <%# GetMedName(Eval("medicationIdGS")) %>
                 </button> 
                 <div class="med-takingtime"><%# GetMedTakingTime(Eval("takingTimeGS")) %></div>
             </div>
         </ItemTemplate>
    </asp:Repeater>
    </div>

    <!-- Afternoon Repeater -->
    <div class="schedule-block">
    <h3>Afternoon</h3>
    <asp:Repeater ID="afternoonRepeater" runat="server">
         <ItemTemplate>
             <div class="med">
                 <button class="button med-button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
                     <%# GetMedName(Eval("medicationIdGS")) %>
                 </button> 
                 <div class="med-takingtime"><%# GetMedTakingTime(Eval("takingTimeGS")) %></div>
             </div>
         </ItemTemplate>
    </asp:Repeater>
    </div>

    <!-- Evening Repeater -->
    <div class="schedule-block">
    <h3>Evening</h3>
    <asp:Repeater ID="eveningRepeater" runat="server">
         <ItemTemplate>
             <div class="med">
                 <button class="button med-button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
                     <%# GetMedName(Eval("medicationIdGS")) %>
                 </button> 
                 <div class="med-takingtime"><%# GetMedTakingTime(Eval("takingTimeGS")) %></div>
             </div>
         </ItemTemplate>
    </asp:Repeater>
    </div>
</div>
         <!-- Medication Modal -->
   <div id="medicationModal" class="modal">
       <div class="modal-content">
           <span class="close" onclick="closeMedicationModal()">&times;</span>
           <img id="medicationImg" alt="Medication Image"/>
           <div id="medicationName" style="margin-top: 10px; font-size: 18px;"></div>
           <div id="medicationInstructions" style="margin-top: 10px;"></div>
           <div id="medicationDescription" style="margin-top: 10px;"></div>
       </div>
   </div>
</asp:Content>
