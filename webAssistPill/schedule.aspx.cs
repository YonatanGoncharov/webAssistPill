using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace webAssistPill
{
    public partial class schedule1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["SelectedUser"] = new UserBL("dolev@gmail.com", "aba123");
                Session["User"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123"); //existing user for testing
                PopulateMedications();
                PopulateMedicationsDropDown();
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
                string timeString = parsedDateTime.ToString("HH:mm:ss");
                return timeString;
            }
            return null;
        }


        private void PopulateMedicationsDropDown()
        {
            if (Session["SelectedUser"] is UserBL user)
            {
                List<ScheduleBL> schedules = user.GetSchedule();
                List<MedicationBL> medications = new List<MedicationBL>();
                List<int> medicationsIds = new List<int>();
                foreach (ScheduleBL sch in schedules)
                {
                    int schId = sch.medicationIdGS;
                    if (!medicationsIds.Contains(schId))
                    {
                        medicationsIds.Add(schId);
                        medications.Add(new MedicationBL(schId));
                    }
                }

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

                            DateTime dayOfWeek = new DateTime(2022, 1, 1).AddDays(sch.dayOfTheWeekGS - 1);

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

                if (!medicationIdAndTime.Contains(medicationPair))
                {
                    medicationIdAndTime.Add(medicationPair);
                }
            }
            List<ScheduleBL> schedulesRepeated = new List<ScheduleBL>();
            foreach ((int medicationId, string time, ScheduleBL ch) in medicationIdAndTime)
            {
                schedulesRepeated.Add(ch);
            }

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
                MedicationBL med = new MedicationBL(medId);
                MedicationEditLabel.Text = med.MedicationName;
                string repDays = GetRepeatedDays(medId, medTakingtime);
                foreach (Control control in formInputDays.Controls)
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
            if (sender is Button button)
            {
                if (editMedicationForm.Style["display"] == "block")
                {
                    editMedicationForm.Style["display"] = "none"; // Hide the div
                }
                newMedicationForm.Style["display"] = "block"; // Show the div



            }

        }

        protected void ExitButtonEditForm_Click(object sender, EventArgs e)
        {
            editMedicationForm.Style["display"] = "none";
        }

        protected void SaveChangeEditForm_Click(object sender, EventArgs e)
        {

        }
    }
}