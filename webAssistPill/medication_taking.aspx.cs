using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                int scheduleId = int.Parse(queryString["scheduleId"]);
                int userId = int.Parse(queryString["userId"]);
                string confirmationType = queryString["type"];
                string takingDate = queryString["takingDate"];
                if (confirmationType.Equals("medication")) //checking if this is medication taking
                {
                    TakingDetailBL takingDetailBL = new TakingDetailBL(scheduleId, takingDate); //making that the he took the medication in database
                    if (!takingDetailBL.IsTookStatus)
                    {
                        takingDetailBL.NewTaking();
                        UserBL user = new UserBL(userId);
                        List<AttendantBL> attendants = user.GetAttendants();
                        foreach (AttendantBL attendant in attendants)
                        {
                            //sending message to all attendants that he took the medication
                            ScheduleBL schedule = new ScheduleBL(scheduleId);
                            new MessageBL(userId, attendant.attendantIdGS, takingDate, $"{user.userNamegs} Has took the medication {schedule.medicationNameGS} in {takingDate}", false);
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