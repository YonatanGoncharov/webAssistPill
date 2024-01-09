using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class attendant_home1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123");

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
            }
        }
    }
}