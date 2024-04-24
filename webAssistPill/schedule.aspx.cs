using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace webAssistPill
{
    public partial class schedule1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Session["SelectedUser"] = new UserBL("dolev@gmail.com", "aba123");
                //Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123"); //existing user for testing
                PopulateMedications();//populating the medication repiter
                PopulateMedicationsDropDown();//populating the medication dropdown
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
                List<ScheduleBL> schedules = user.GetSchedule();
                List<ScheduleBL> schedulesRepeated = GettingReapeatedSchedule(schedules);

                // Bind DataTable to repeater
                medicationsRepeater.DataSource = schedulesRepeated;
                medicationsRepeater.DataBind();
            }
        }
        /// <summary>
        /// gives me the hour of the date
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public string TimetoString(object day)
        {
            if (DateTime.TryParse(day.ToString(), out DateTime parsedDateTime))
            {
                // Extract the time part
                string timeString = parsedDateTime.ToString("HH:mm");
                return timeString;
            }
            return null;
        }

        //making a drop down with the medications
        private void PopulateMedicationsDropDown()
        {
            if (Session["SelectedUser"] is UserBL user)
            {
                List<MedicationBL> medications = MedicationBL.GetUserMedications(user.userIdgs);

                medicationDropdown.DataSource = medications;
                medicationDropdown.DataTextField = "MedicationName";
                medicationDropdown.DataValueField = "Medicationid";
                medicationDropdown.DataBind();
            }

        }
        /// <summary>
        /// getting the days that the taking time is same
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationTakingTime"></param>
        /// <returns></returns>
        public string GetRepeatedDays(object medicationId, object medicationTakingTime)
        {
            if (medicationId != null)
            {
                if (Session["SelectedUser"] is UserBL selectedPatient)
                {
                    List<ScheduleBL> schedules = selectedPatient.GetSchedule();
                    string days = "";
                    foreach (ScheduleBL sch in schedules)
                    {
                        if (sch.medicationIdGS.Equals((int)medicationId) && sch.takingTimeGS.Equals(medicationTakingTime))
                        {
                            if (!days.Equals(""))
                            {
                                days += ", ";
                            }

                            DateTime dayOfWeek = new DateTime(2022, 1, 1).AddDays(sch.dayOfTheWeekGS);

                            // Get the day name as a string in English

                            string dayString = dayOfWeek.ToString("dddd", CultureInfo.InvariantCulture);

                            days += $"{dayString}";
                        }

                    }
                    return days;
                }

            }
            return string.Empty;
        }
        /// <summary>
        /// removing the selected schedule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        protected void RemoveButton_Command(object sender, CommandEventArgs e)
        {
            RemoveButton(e.CommandArgument);
        }

        public void RemoveButton(object medInfo)
        {
            // Retrieve medicationId and takingTime from the CommandArgument
            string[] medIdAndTakingtime = Convert.ToString(medInfo).Split('*');
            int medId = int.Parse(medIdAndTakingtime[0]);
            string medTakingtime = medIdAndTakingtime[1];


            // Further logic for removing medication from the backend and refreshing UI
            if (Session["SelectedUser"] is UserBL user)
            {
                List<ScheduleBL> schedules = user.GetSchedule();

                foreach (ScheduleBL sch in schedules)
                {
                    if (sch.medicationIdGS.Equals(medId) && sch.takingTimeGS.Equals(medTakingtime))
                    {
                        sch.ScheduleRemove();
                    }
                }
                PopulateMedications();
                PopulateMedicationsDropDown();
            }

        }

        /// <summary>
        /// getting the schedule list of all of repeated schedules
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private List<ScheduleBL> GettingReapeatedSchedule(List<ScheduleBL> schedules)
        {
            List<(int, string, ScheduleBL)> medicationIdAndTime = new List<(int, string, ScheduleBL)>();

            foreach (ScheduleBL sch in schedules)
            {
                var medicationPair = (sch.medicationIdGS, sch.takingTimeGS, sch);

                if (!medicationIdAndTime.Any(t => t.Item1 == medicationPair.Item1 && t.Item2 == medicationPair.Item2))
                {
                    medicationIdAndTime.Add(medicationPair);
                }
            }

            List<ScheduleBL> schedulesRepeated = medicationIdAndTime.Select(t => t.Item3).ToList();

            return schedulesRepeated;
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                if (newMedicationForm.Style["display"] == "block")
                {
                    newMedicationForm.Style["display"] = "none"; // Hide the div
                }
                editMedicationForm.Style["display"] = "block"; // Show the div

                // Retrieve medicationId and takingTime from the CommandArgument
                string[] medIdAndTakingtime = Convert.ToString(button.CommandArgument).Split('*');
                int medId = int.Parse(medIdAndTakingtime[0]);
                string medTakingtime = medIdAndTakingtime[1];
                Session["EditScheduleMedId"] = medId;
                Session["EditScheduleMedTakingTime"] = medTakingtime;
                MedicationBL med = new MedicationBL(medId);
                MedicationEditLabel.Text = med.MedicationName;
                string repDays = GetRepeatedDays(medId, medTakingtime);
                foreach (Control control in formInputDaysEdit.Controls)
                {
                    if (control is HtmlInputCheckBox checkbox)
                    {
                        checkbox.Checked = false;
                    }
                }

                if (repDays.Contains("Monday"))
                    mondayEdit.Checked = true;
                if (repDays.Contains("Tuesday"))
                    tuesdayEdit.Checked = true;
                if (repDays.Contains("Wednesday"))
                    wednesdayEdit.Checked = true;
                if (repDays.Contains("Thursday"))
                    thursdayEdit.Checked = true;
                if (repDays.Contains("Friday"))
                    fridayEdit.Checked = true;
                if (repDays.Contains("Saturday"))
                    saturdayEdit.Checked = true;
                if (repDays.Contains("Sunday"))
                    sundayEdit.Checked = true;
                medicationTimeEdit.Value = TimetoString(medTakingtime);

            }

        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (editMedicationForm.Style["display"] == "block")
            {
                editMedicationForm.Style["display"] = "none"; // Hide the div
            }
            newMedicationForm.Style["display"] = "block"; // Show the div
        }

        protected void ExitButtonEditForm_Click(object sender, EventArgs e)
        {
            editMedicationForm.Style["display"] = "none";
        }

        //saving the changes in the edit form
        protected void SaveChangeEditForm_Click(object sender, EventArgs e)
        {
            object medId = Session["EditScheduleMedId"];
            object medTakingTime = Session["EditScheduleMedTakingTime"];
            string repDays = GetRepeatedDays(medId, medTakingTime);

            if (Session["SelectedUser"] is UserBL user)
            {
                if (sundayEdit.Checked)
                {
                    if (!repDays.Contains("Sunday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 1, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 1);
                        new ScheduleBL((int)medId, 1, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Sunday"))
                        FindAndRemoveByDay(medId, medTakingTime, 1);
                }
                if (mondayEdit.Checked)
                {
                    if (!repDays.Contains("Monday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 2, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 2);
                        new ScheduleBL((int)medId, 2, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Monday"))
                        FindAndRemoveByDay(medId, medTakingTime, 2);
                }
                if (tuesdayEdit.Checked)
                {
                    if (!repDays.Contains("Tuesday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 3, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 3);
                        new ScheduleBL((int)medId, 3, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Tuesday"))
                        FindAndRemoveByDay(medId, medTakingTime, 3);
                }
                if (wednesdayEdit.Checked)
                {
                    if (!repDays.Contains("Wednesday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 4, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 4);
                        new ScheduleBL((int)medId, 4, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Wednesday"))
                        FindAndRemoveByDay(medId, medTakingTime, 4);
                }
                if (thursdayEdit.Checked)
                {
                    if (!repDays.Contains("Thursday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 5, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 5);
                        new ScheduleBL((int)medId, 5, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Thursday"))
                        FindAndRemoveByDay(medId, medTakingTime, 5);
                }
                if (fridayEdit.Checked)
                {
                    if (!repDays.Contains("Friday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 6, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 6);
                        new ScheduleBL((int)medId, 6, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Friday"))
                        FindAndRemoveByDay(medId, medTakingTime, 6);
                }
                if (saturdayEdit.Checked)
                {
                    if (!repDays.Contains("Saturday"))//adding new day incase he doesnt have it
                        new ScheduleBL((int)medId, 7, medicationTimeEdit.Value, user.userIdgs);
                    else if (!medicationTimeEdit.Value.Equals(TimetoString(medTakingTime)))//removing the day and adding again becauase he changed the time
                    {
                        FindAndRemoveByDay(medId, medTakingTime, 7);
                        new ScheduleBL((int)medId, 7, medicationTimeEdit.Value, user.userIdgs);
                    }
                }
                else
                {
                    if (repDays.Contains("Saturday"))
                        FindAndRemoveByDay(medId, medTakingTime, 7);
                }
                PopulateMedications();
                editMedicationForm.Style["display"] = "none";
            }


        }
        /// <summary>
        /// finding specific schedule in specific day
        /// </summary>
        /// <param name="medId"></param>
        /// <param name="medTakingTime"></param>
        private void FindAndRemoveByDay(object medId, object medTakingTime, int day)
        {
            if (Session["SelectedUser"] is UserBL user)
            {
                foreach (ScheduleBL schedule in user.GetSchedule())
                {
                    if (schedule.takingTimeGS.Equals(medTakingTime) && schedule.dayOfTheWeekGS == day && schedule.medicationIdGS.Equals(medId))
                    {
                        schedule.ScheduleRemove();
                        break;
                    }
                }
            }
        }

        protected void FormAddMedButton_Click(object sender, EventArgs e)
        {
            // Check if a user is selected in the session
            if (Session["SelectedUser"] is UserBL user)
            {
                // Parse selected medication ID and time to take from form inputs
                int medicationId = int.Parse(medicationDropdown.SelectedValue);
                string timeToTake = medicationTime.Value;
                List<int> daysToRepeat = new List<int>();

                // Collect selected days to repeat medication intake
                if (sunday.Checked)
                    daysToRepeat.Add(1);
                if (monday.Checked)
                    daysToRepeat.Add(2);
                if (tuesday.Checked)
                    daysToRepeat.Add(3);
                if (wednesday.Checked)
                    daysToRepeat.Add(4);
                if (thursday.Checked)
                    daysToRepeat.Add(5);
                if (friday.Checked)
                    daysToRepeat.Add(6);
                if (saturday.Checked)
                    daysToRepeat.Add(7);

                bool flag = false;
                // Retrieve all repeated schedules for the user
                List<ScheduleBL> schedules = GettingReapeatedSchedule(user.GetSchedule());
                foreach (ScheduleBL schedule in schedules)
                {
                    // Check if there's an existing schedule with the same time and medication ID
                    string time = TimetoString(schedule.takingTimeGS);
                    if (time.Equals(timeToTake) && schedule.medicationIdGS.Equals(medicationId))
                    {
                        flag = true;
                        break;
                    }
                }

                // Validate if all required fields are filled
                if (medicationDropdown.SelectedValue.Equals(null) || medicationTime.Value.Equals(null) || daysToRepeat.Count == 0)
                {
                    string errorMessage = "Please Fill All!";

                    // Register the JavaScript function call to show error message
                    string script = $"showError('{errorMessage}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                }
                // Check if there's already an existing similar schedule
                else if (flag)
                {
                    string errorMessage = "There is already existing similar schedule!";

                    // Register the JavaScript function call to show error message
                    string script = $"showError('{errorMessage}');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowErrorScript", script, true);
                }
                else
                {
                    // Create new schedules for selected days to repeat medication intake
                    foreach (int day in daysToRepeat)
                    {
                        new ScheduleBL(medicationId, day, timeToTake, user.userIdgs);
                    }
                    // Populate medication dropdown with updated data
                    PopulateMedications();
                }
                // Hide the new medication form after submission
                newMedicationForm.Style["display"] = "none";
                // Uncheck all checkboxes for days of intake
                foreach (Control control in formInputDays.Controls)
                {
                    if (control is HtmlInputCheckBox checkbox)
                    {
                        checkbox.Checked = false;
                    }
                }
                // Clear the medication time input field
                medicationTime.Value = null;
            }
        }

        //exiting the form
        protected void FormExitButton_Click(object sender, EventArgs e)
        {
            newMedicationForm.Style["display"] = "none";
        }
    }
}