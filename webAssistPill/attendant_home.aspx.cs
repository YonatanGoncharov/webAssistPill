using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class attendant_home1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Access the footer in the master page
           
            if (!Page.IsPostBack)
            {
                masterpage master = Master as masterpage;

                if (master != null)
                {
                    master.HomeButton1.Visible = false;  // or false to hide
                    master.HomeButton2.Visible = false;  // or false to hide
                    master.HomeButton3.Visible = false;  // or false to hide
                    master.HomeButton4.Visible = false;  // or false to hide
                }
                //Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123");

                // Check if the user is an attendant
                if (Session["User"] is AttendantBL attendant)
                {
                    // Retrieve the list of patients for the attendant
                    List<UserBL> patients = attendant.patientsGS;

                    // Bind the patients to the dropdown
                    patientDropdown.DataSource = patients;
                    patientDropdown.DataTextField = "userNamegs"; // Replace with the actual property name for patient names
                    patientDropdown.DataValueField = "userIdgs"; // Replace with the actual property name for patient IDs
                    patientDropdown.DataBind();
                }



            }
        }
        protected void viewButton_Click(object sender, EventArgs e)
        {
            // Check if the user is an attendant
            if (Session["User"] is AttendantBL attendant)
            {
                // Get the selected patient ID
                int selectedPatientId = int.Parse(patientDropdown.SelectedValue);

                // Use a method in AttendantBL to get the selected patient's name
                UserBL selectedPatient = attendant.GetSpecifiecPatient(selectedPatientId);

                // Update the label with the selected patient's name
                selectedPatientLabel.Text = $"Viewing: {selectedPatient.userNamegs}";

                Session["SelectedUser"] = selectedPatient;
                
                masterpage master = Master as masterpage;
                if (master != null)
                {
                    master.HomeButton1.Visible = true;  // or false to hide
                    master.HomeButton2.Visible = true;  // or false to hide
                    master.HomeButton3.Visible = true;  // or false to hide
                    master.HomeButton4.Visible = true;  // or false to hide
                }
            }
        }
    }
}