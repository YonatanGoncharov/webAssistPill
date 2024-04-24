<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="medication_taking.aspx.cs" Inherits="webAssistPill.MedicationTaking" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8"/>
   <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
   <title>Medication Confirmation Page</title>
   <link rel="stylesheet" href="style.css"/>
</head>
<body>
     <div class="container">
     <form runat="server">
         <div class="centered-label">
             <asp:Label ID="ConfirmationLabel" CssClass="confirmLabel" runat="server" Text="Thank you, have a nice day"></asp:Label>
         </div>
     </form>
 </div>
</body>
</html>
