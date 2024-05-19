using AssistPillBL;
using System;
using System.Collections.Generic;

namespace webAssistPill
{
    public partial class MedicationTaking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getting and and veribles from the url
            string thisUrl = Request.Url.AbsoluteUri;
            System.Collections.Specialized.NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(new Uri(thisUrl).Query);
            try
            {
                string currentDate = DateTime.Now.ToString("HH:mm");
                int scheduleId = int.Parse(queryString["schedule"]);
                int userId = int.Parse(queryString["userId"]);
                string confirmationType = queryString["type"];
                if (confirmationType.Equals("medication")) //checking if this is medication taking
                {
                    TakingDetailBL takingDetailBL = new TakingDetailBL(scheduleId); //making that the he took the medication in database
                    if (!takingDetailBL.IsTookStatus)
                    {
                        takingDetailBL.NewTaking();
                        UserBL user = new UserBL(userId);
                        List<AttendantBL> attendants = user.GetAttendants();
                        foreach (AttendantBL attendant in attendants)
                        {
                            //sending message to all attendants that he took the medication
                            ScheduleBL schedule = new ScheduleBL(scheduleId);
                            new MessageBL(userId, attendant.attendantIdGS, currentDate, $"{user.userNamegs} Has took the medication {schedule.medicationNameGS} in {currentDate}", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //incase testing
            }


        }
    }
}