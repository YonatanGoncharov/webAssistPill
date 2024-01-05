<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_home.aspx.cs" Inherits="webAssistPill.user_home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Medical Schedule</title>
    <link rel="stylesheet" href="style.css" />
    <script src="script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Today's Medication Schedule</h1>
            <div class="schedule-block-container">
                <!-- Morning Repeater -->
                
                <div class="schedule-block">
                <h3>Morning</h3>
                <asp:Repeater ID="morningRepeater" runat="server">
                <ItemTemplate>
                    <div class="med">
                        <button class="button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
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
                             <button class="button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
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
                             <button class="button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
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
                             <button class="button" type="button" onclick="showMedicationDetails('<%# GetMedName(Eval("medicationIdGS")) %>', '<%# GetMedImgPath(Eval("medicationIdGS")) %>', '<%# GetMedInstructions(Eval("medicationIdGS")) %>', '<%# GetMedDescription(Eval("medicationIdGS")) %>')">
                                 <%# GetMedName(Eval("medicationIdGS")) %>
                             </button> 
                             <div class="med-takingtime"><%# GetMedTakingTime(Eval("takingTimeGS")) %></div>
                         </div>
                     </ItemTemplate>
                </asp:Repeater>
                </div>
            </div>
        </div>
    </form>

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


    <footer>
        <button onclick="location.href ='user_home.aspx'" class="switch-button">Home</button>
        <button onclick="location.href ='attendants.aspx'" class="switch-button">Attendants</button>
    </footer>
    <button class="top-button" onclick="scrollToTop()">Top</button>
    <button class="exit-button" onclick="exitAccount()">Exit</button>
</body>
</html>
