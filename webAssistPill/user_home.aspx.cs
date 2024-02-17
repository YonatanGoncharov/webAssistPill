using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class user_home1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["User"] = new UserBL("dolev@gmail.com", "aba123");
                if (Session["User"] is UserBL user)
                {
                    List<ScheduleBL> schedule = user.GetSchedule();
                    List<ScheduleBL> morningSchedule = new List<ScheduleBL>();
                    List<ScheduleBL> afterNoonSchedule = new List<ScheduleBL>();
                    List<ScheduleBL> eveningSchedule = new List<ScheduleBL>();
                    List<ScheduleBL> noonSchedule = new List<ScheduleBL>();
                    foreach (ScheduleBL sch in schedule)
                    {
                        string takingTime = sch.takingTimeGS;
                        string hourString = takingTime.Substring(11, 2);
                        int hour = int.Parse(hourString);

                        int day = sch.dayOfTheWeekGS;

                        // Get the day of the week
                        DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
                        int dayOfWeekNumber = (int)dayOfWeek;

                        if (day == dayOfWeekNumber + 1)
                        {
                             
                            //putting the right medication in the right med time
                            if (hour >= 6 && hour < 12)
                            {
                                morningSchedule.Add(sch);
                            }
                            else if (hour >= 12 && hour < 17)
                            {
                                noonSchedule.Add(sch);
                            }
                            else if (hour >= 17 && hour < 20)
                            {
                                afterNoonSchedule.Add(sch);
                            }
                            else
                            {
                                eveningSchedule.Add(sch);
                            }
                        }
                    }
                    morningRepeater.DataSource = morningSchedule;
                    morningRepeater.DataBind();
                    noonRepeater.DataSource = noonSchedule;
                    noonRepeater.DataBind();
                    afternoonRepeater.DataSource = afterNoonSchedule;
                    afternoonRepeater.DataBind();
                    eveningRepeater.DataSource = eveningSchedule;
                    eveningRepeater.DataBind();
                }

            }
        }
        /// <summary>
        /// getting the taking time hour from the format
        /// </summary>
        /// <param name="takingTime"></param>
        /// <returns></returns>
        protected string GetMedTakingTime(object takingTime)
        {
            return takingTime.ToString().Split(' ')[1];
        }
        /// <summary>
        /// getting the img path from the data base
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        protected string GetMedImgPath(object medicationId)
        {
            if (medicationId != null && Session["User"] is UserBL user)
            {
                MedicationBL medicationBL = new MedicationBL((int)medicationId);
                return medicationBL.MedicationPhotoPath;
            }

            return string.Empty;
        }
        /// <summary>
        /// getting the img name from the data base
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        protected string GetMedName(object medicationId)
        {
            if (medicationId != null && Session["User"] is UserBL user)
            {
                MedicationBL medicationBL = new MedicationBL((int)medicationId);
                return medicationBL.MedicationName;
            }

            return string.Empty;
        }
        /// <summary>
        /// getting the med instructiions from the database
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        protected string GetMedInstructions(object medicationId)
        {
            if (medicationId != null && Session["User"] is UserBL user)
            {
                MedicationBL medicationBL = new MedicationBL((int)medicationId);
                return medicationBL.MedicationInstructions;
            }

            return string.Empty;
        }
        /// <summary>
        /// getting the med description from the database
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        protected string GetMedDescription(object medicationId)
        {
            if (medicationId != null && Session["User"] is UserBL user)
            {
                MedicationBL medicationBL = new MedicationBL((int)medicationId);
                return medicationBL.MedicationDescription;
            }

            return string.Empty;
        }
    }
}