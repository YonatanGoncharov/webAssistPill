using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class AttendantConfirmationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getting and and veribles from the url
            string thisUrl = Request.Url.AbsoluteUri;
            System.Collections.Specialized.NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(new Uri(thisUrl).Query);

            try
            {
                string confirmationType = queryString["type"];
                //checking if the type of the confirmation is related to the medication stock
                if (confirmationType.Equals("stock"))
                {
                    int userId = int.Parse(queryString["user"]);
                    string attendantEmail = queryString["attendant"];
                    string date = queryString["date"];
                    MedicationStorageBL msr = new MedicationStorageBL(userId, date);
                    if (!msr.IsSawStatus) //if the user is the first to see the email the status will show as not seen
                    {
                        msr.SeenUpdate();
                        UserBL user = new UserBL(userId);
                        List<AttendantBL> attendants = user.GetAttendants();
                        foreach (AttendantBL attendant in attendants)
                        {
                            //sending to all of the other attendants that this task has been claimed by him
                            string email = attendant.attendantEmailGS;
                            AutomaticMessageSend.SendMessageIsClaimed(email, attendantEmail, date);
                        }
                    }
                }
                else if (confirmationType.Equals("medication"))
                {
                    //stoping from sending any more emails to the other priority attedants because he took the task
                    int takingdetaillogId = int.Parse(queryString["takingdetaillogId"]);
                    TakingDetailLogBL takingDetailLogBL = new TakingDetailLogBL(takingdetaillogId);
                    takingDetailLogBL.TakingDetailLogStop();
                }

            }
            catch (Exception ex)
            {
                //incase of testing
            }


        }
    }
}