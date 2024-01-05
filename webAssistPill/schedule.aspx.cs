using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123"); //existing user for testing
                // Check if the user is an attendant
                if (Session["User"] is AttendantBL attendant)
                {
                    // Retrieve the list of patients for the attendant
                   // List<UserBL> patients = attendant.patientsGS;

                    // Bind the patients to the dropdown
                    /*
                    patientDropdown.DataSource = patients;
                    patientDropdown.DataTextField = "userNamegs"; // Replace with the actual property name for patient names
                    patientDropdown.DataValueField = "userIdgs"; // Replace with the actual property name for patient IDs
                    patientDropdown.DataBind();
                    */
                }
            }
        }
        /// <summary>
        /// getting the medicatio name
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        public string GetMedName(object medicationId)
        {
            if (medicationId != null)
            {
                MedicationBL medicationBL = new MedicationBL((int)medicationId);
                return medicationBL.MedicationName;
            }

            return string.Empty;
        }

        public string GetRepeatedDays(object medicationName, object medicationTakingTime) 
        { 

        }
    }
}