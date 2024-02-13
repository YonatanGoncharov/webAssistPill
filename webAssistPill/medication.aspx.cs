using AssistPillBL;
using System;
using System.Collections.Generic;
using System.IO;
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
                PopulateMedications();
                if (Session["SelectedUser"] is UserBL selectedPatient)
                {
                    // Update the label with the selected patient's name
                    selectedPatientLabel.Text = $"Viewing: {selectedPatient.userNamegs}";
                }
            }
        }
        private void PopulateMedications()
        {
            if (Session["SelectedUser"] is UserBL user)
            {
                // Bind DataTable to repeater
                List<MedicationBL> medications = MedicationBL.GetUserMedications(user.userIdgs);
                medicationRepeater.DataSource = medications;
                medicationRepeater.DataBind();
            }
        }


        protected void EditMedicationButton_Click(object sender, EventArgs e)
        {
            if (NewMedicationForm.Style["display"] == "block")
            {
                NewMedicationForm.Style["display"] = "none"; // Hide the div
            }
            EditMedicationForm.Style["display"] = "block"; // Show the div

            if (sender is Button btn)
            {
                int medId = int.Parse(btn.CommandArgument);
                Session["EditMedId"] = medId;
                MedicationBL med = new MedicationBL(medId);
                medicationNameEdit.Value = med.MedicationName;
                medicationDescriptionEdit.Value = med.MedicationDescription;
                medicationHowTakeEdit.Value = med.MedicationInstructions;
                medicationQuantityEdit.Value = med.MedicationAmount.ToString();

                string path = med.MedicationPhotoPath;
                // Construct the file path based on the ID
                string imagePath = string.Format("~/images/medication_images/{0}", path);

                // Set the value of the file input element with the image path
                medicationPictureEdit.Value = Server.MapPath(imagePath);

                // Display the image preview
                imagePreviewEdit.Src = ResolveUrl(imagePath);
            }
        }

        protected void RemoveButton_Command(object sender, CommandEventArgs e)
        {
            RemoveButton(e.CommandArgument);
        }

        public void RemoveButton(object medId)
        {
            MedicationBL medication = new MedicationBL((int)medId);
            if (Session["SelectedUser"] is UserBL user)
            {
                foreach (ScheduleBL schedule in user.GetSchedule())
                {
                    if (schedule.medicationIdGS.Equals(medication.Medicationid))
                    {
                        schedule.ScheduleRemove();
                    }
                }
            }
            medication.MedicationRemove();
            PopulateMedications();
        }

        protected void NewMedicationButton_Click(object sender, EventArgs e)
        {
            if (EditMedicationForm.Style["display"] == "block")
            {
                EditMedicationForm.Style["display"] = "none"; // Hide the div
            }
            NewMedicationForm.Style["display"] = "block"; // Show the div
        }

        protected void SaveEditButton_Click(object sender, EventArgs e)
        {

        }

        protected void NewMedicationButton_Click1(object sender, EventArgs e)
        {

        }

        protected void AddNewMedicationButton_Click(object sender, EventArgs e)
        {
            if (Session["SelectedUser"] is UserBL user)
            {
                string medicationName = medicationNameAdd.Value;
                string howToTake = medicationHowTakeAdd.Value;
                int quantity = int.Parse(medicationQuantityAdd.Value);
                string description = medicationDescriptionAdd.Value;
                List<MedicationBL> medications = MedicationBL.GetUserMedications(user.userIdgs);
                foreach (MedicationBL medication in medications)
                {
                    if (medication.MedicationName.Equals(medicationName))
                    {
                        string errorMessage = "There is already medication with the same name!";
                        string script = $"showError('{errorMessage}');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                        return;
                    }
                }


                if (medicationName.Equals(null) || howToTake.Equals(null) || quantity == 0)
                {
                    string errorMessage = "Please Fill All fields!";
                    string script = $"showError('{errorMessage}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                    return;
                }
                
                HttpPostedFile postedFile = medicationPictureAdd.PostedFile;
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    try
                    {
                        // Check content type
                        string contentType = postedFile.ContentType;
                        if (!contentType.StartsWith("image/"))
                        {
                            string errorMessage = "Please upload a valid image file.";

                            // Register the JavaScript function call
                            string script = $"showError('{errorMessage}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                            return; // Exit the method
                        }

                        string originalFileName = Path.GetFileName(postedFile.FileName);
                        string newFileName = $"{user.userIdgs}_{originalFileName}";

                        string folderPath = Server.MapPath("~/images/medication_images/"); // Change the path as per your requirement
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        postedFile.SaveAs(Path.Combine(folderPath, newFileName));

                        new MedicationBL(medicationName, howToTake, description, quantity, newFileName, user.userIdgs);
                        PopulateMedications();
                        // Optionally, you can save the filename or path in the database here
                        // e.g., SaveFilePathToDatabase(Path.Combine(folderPath, filename));
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        // e.g., display error message
                        string errorMessage = "Error: " + ex.Message;

                        // Register the JavaScript function call
                        string script = $"showError('{errorMessage}');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                    }
                }
                else
                {
                    // Display error message if no file is selected
                    string errorMessage = "Please select a file";

                    // Register the JavaScript function call
                    string script = $"showError('{errorMessage}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                }
            }
        }
    } 
}