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
            string thisUrl = Request.Url.AbsoluteUri;
            System.Collections.Specialized.NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(new Uri(thisUrl).Query);
            int userId = int.Parse(queryString["user"]);
            string attendantEmail = queryString["attendant"];
            string confirmationType = queryString["type"];
            string date = queryString["date"];
            if (confirmationType.Equals("stock"))
            {
                MedicationStorageBL msr = new MedicationStorageBL(userId, date);
                if (!msr.IsSawStatus)
                {
                    msr.SeenUpdate();
                    UserBL user = new UserBL(userId);
                    List<AttendantBL> attendants = user.GetAttendants();
                    foreach (AttendantBL attendant in attendants)
                    {
                        string email = attendant.attendantEmailGS;

                    }
                }

            }

        }
    }
}