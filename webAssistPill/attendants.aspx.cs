using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class attendants1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the attendants on page load
                //Session["User"] = new UserBL("dolev@gmail.com", "aba123");
                PopulateAttendants();
            }
        }

        private void PopulateAttendants()
        {
            if (Session["User"] is UserBL user)
            {

                List<AttendantBL> attendants = user.GetAttendants();

                // Bind DataTable to repeater
                repeaterAttendants.DataSource = attendants;
                repeaterAttendants.DataBind();
            }
        }

        protected void btnSaveAttendant_Click(object sender, EventArgs e)
        {
            if (Session["User"] is UserBL user)
            {
                string email = txtAttendantEmail.Text;
                int priority;

                if (int.TryParse(txtAttendantPriority.Text, out priority))
                {
                    if (IsPriorityAvailable(priority))
                    {
                        try
                        {
                            user.AddNewAttendant(email, priority);
                            // Refresh the attendants list after adding a new attendant
                            PopulateAttendants();
                            // Hide the add attendant tab after saving
                            addAttendantTab.Style.Add("display", "none");
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Equals("Connection exists"))
                            {
                                // Display an error message that the connection already exists
                                errorDiv.InnerHtml = "Error: This user is already your attendant";
                                errorDiv.Style.Add("display", "block");
                            }
                            else
                            {
                                // Display an error message that the email does not exists
                                errorDiv.InnerHtml = "Error: This user does not exists";
                                errorDiv.Style.Add("display", "block");
                            }
                        }

                    }
                    else
                    {
                        // Display an error message that the priority is not available
                        errorDiv.InnerHtml = "Error: Priority is not available.";
                        errorDiv.Style.Add("display", "block");
                    }
                }
                else
                {
                    // Display an error message that the priority is not a valid number
                    errorDiv.InnerHtml = "Error: Priority must be a valid number.";
                    errorDiv.Style.Add("display", "block");
                }
            }
        }

        private bool IsPriorityAvailable(int priority)
        {
            // Check if the priority is available
            // Example: Call a method from your business logic layer (AssistPillBL) to check availability
            var existingPriorities = GetExistingPriorities(); // Replace this with your actual logic

            return !existingPriorities.Contains(priority);
        }

        private int[] GetExistingPriorities()
        {
            int[] properties = null;
            if (Session["User"] is UserBL user)
            {
                properties = new int[user.attendantsGS.Count];
                for (int i = 0; i < properties.Length; i++)
                {
                    properties[i] = user.attendantsGS[i].GetPriority(user.userIdgs);
                }
            }
            return properties;
        }

        protected void newAttendantButton_Click(object sender, EventArgs e)
        {
            // Show the add attendant tab
            addAttendantTab.Style.Add("display", "block");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Hide the error message when the user cancels
            errorDiv.Style.Add("display", "none");
            // Hide the add attendant tab after canceling
            addAttendantTab.Style.Add("display", "none");
        }

        protected void removeButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Retrieve AttendantIdGS from the CommandArgument
                int attendantId = Convert.ToInt32(btn.CommandArgument);

                if (Session["User"] is UserBL user)
                {
                    AttendantBL attendant = user.GetSpecifiecAttendant(attendantId);

                    if (attendant != null)
                    {
                        string attendantName = attendant.attendantNameGS;

                        // Confirm removal with a JavaScript confirmation dialog
                        if (ConfirmRemove(attendantName))
                        {
                            user.RemoveAttendant(attendantId);
                            PopulateAttendants(); // Refresh the attendants list after removal
                        }
                    }
                }
            }
        }
        private bool ConfirmRemove(string attendantName)
        {
            // Display a JavaScript confirmation dialog
            string confirmScript = $@"<script type='text/javascript'>
                                return confirm('Are you sure you want to remove {attendantName}?');
                            </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "ConfirmRemoveScript", confirmScript);

            // Return the actual confirmation result from the client-side script
            return true;
        }
        protected string GetPriority(object attendantId)
        {
            if (attendantId != null && Session["User"] is UserBL user)
            {
                return user.GetSpecifiecAttendant((int)attendantId).GetPriority(user.userIdgs).ToString();
            }

            return string.Empty;
        }
    }
}