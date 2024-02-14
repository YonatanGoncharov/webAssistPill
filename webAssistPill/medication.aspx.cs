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
                ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showEditedPicture('" + path + "');", true);

                
            }

        }

        protected void RemoveButton_Command(object sender, CommandEventArgs e)
        {
            RemoveButton(e.CommandArgument);
        }

        public void RemoveButton(object medId)
        {
            MedicationBL medication = new MedicationBL(Convert.ToInt32(medId));
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
            // Specify the file path of the image to be moved
            string sourceFilePath = Path.Combine(Server.MapPath("~/images/medication_images/"), medication.MedicationPhotoPath);
            // Specify the destination folder path
            string destinationFolderPath = Server.MapPath("~/images/removed_medication_images/");
            // Specify the new file name with "removed" prefix
            string newFileName = "removed_" + medication.MedicationPhotoPath;
            // Specify the destination file path
            string destinationFilePath = Path.Combine(destinationFolderPath, newFileName);


            try
            {
                // Check if the source file exists before attempting to move it
                if (File.Exists(sourceFilePath))
                {
                    // Check if the destination folder exists, create it if it doesn't
                    if (!Directory.Exists(destinationFolderPath))
                    {
                        Directory.CreateDirectory(destinationFolderPath);
                    }

                    // Check if the destination file already exists
                    while (File.Exists(destinationFilePath))
                    {
                        // Append another "removed" prefix to the file name to make it unique
                        newFileName = "removed_" + newFileName;
                        destinationFilePath = Path.Combine(destinationFolderPath, newFileName);
                    }

                    // Move the file to the destination folder with the new unique file name
                    File.Move(sourceFilePath, destinationFilePath);

                    // Optionally, you can also update your data or database records accordingly
                    // e.g., UpdateImagePathInDatabase("your_image_file_name.jpg", newFileName);
                }
                else
                {
                    // Handle case when the source file does not exist
                    // e.g., display an error message
                    Response.Write("The source file does not exist.");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file access errors
                Response.Write("Error: " + ex.Message);
            }



            medication.MedicationRemove();
            NewMedicationForm.Style["display"] = "none";
            EditMedicationForm.Style["display"] = "none";

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
            int medId = (int)Session["EditMedId"];

            MedicationBL med = new MedicationBL(medId);
            if (!medicationNameEdit.Value.Equals(med.MedicationName))
                med.UpdateName(medicationNameEdit.Value);
            if (!medicationDescriptionEdit.Value.Equals(med.MedicationDescription))
                med.UpdateDescription(medicationDescriptionEdit.Value);
            if (!medicationHowTakeEdit.Value.Equals(med.MedicationInstructions))
                med.UpdateInstructions(medicationHowTakeEdit.Value);
            if (!medicationQuantityEdit.Value.Equals(med.MedicationAmount.ToString()))
                med.UpdateAmount(Convert.ToInt32(medicationQuantityEdit.Value));


            if (Session["SelectedUser"] is UserBL user)
            {
                HttpPostedFile postedFile = medicationPictureEdit.PostedFile;
                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    try
                    {
                        // Check content type
                        string contentType = postedFile.ContentType;
                        if (!contentType.StartsWith("image/") || contentType == "image/webp")
                        {
                            string errorMessage = "Please upload a valid image file.";

                            // Register the JavaScript function call
                            string script = $"showError('{errorMessage}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                            return; // Exit the method
                        }


                        // Specify the file name of the existing picture
                        string existingFileName = $"{med.MedicationPhotoPath}";

                        // Get the file path of the existing picture
                        string existingFilePath = Server.MapPath("~/images/medication_images/") + existingFileName;

                        // Check if the existing picture file exists
                        if (File.Exists(existingFilePath))
                        {
                            // Delete the existing picture file
                            File.Delete(existingFilePath);
                        }

                        // Save the new picture file
                        string originalFileName = Path.GetFileName(postedFile.FileName);
                        string newFileName = $"{user.userIdgs}_{originalFileName}";
                        string folderPath = Server.MapPath("~/images/medication_images/"); // Change the path as per your requirement
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                        postedFile.SaveAs(Path.Combine(folderPath, newFileName));

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

                if (!medicationPictureEdit.Value.Equals(med.MedicationPhotoPath))
                    med.UpdatePhotoPath($"{user.userIdgs}_{medicationPictureEdit.Value}");
            }
      
            EditMedicationForm.Style["display"] = "none";
            PopulateMedications();
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

                if (medicationName.Equals(null) || howToTake.Equals(null) || quantity <= 0)
                {
                    string errorMessage = "Please Fill All fields right!";
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
                        if (!contentType.StartsWith("image/" )|| contentType == "image/webp")
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
            medicationNameEdit.Value = null;
            medicationDescriptionEdit.Value = null;
            medicationHowTakeEdit.Value = null;
            medicationQuantityEdit.Value = null;
            NewMedicationForm.Style["display"] = "none";
        }

        protected void ExitButtonEditForm_Click(object sender, EventArgs e)
        {
            EditMedicationForm.Style["display"] = "none";
        }

        protected void FormExitButton_Click(object sender, EventArgs e)
        {
            NewMedicationForm.Style["display"] = "none";
        }
    } 
}