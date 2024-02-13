using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class medication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["SelectedUser"] = new UserBL("dolev@gmail.com", "aba123");
                Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123"); //existing user for testing
              //  PopulateMedications();
               // PopulateMedicationsDropDown();
                if (Session["SelectedUser"] is UserBL selectedPatient)
                {
                    // Update the label with the selected patient's name
                    selectedPatientLabel.Text = $"Viewing: {selectedPatient.userNamegs}";
                }
            }
        }
    }
}