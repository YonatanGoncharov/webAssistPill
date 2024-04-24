using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using FinalProjectDAL;

namespace AssistPillBL
{
    public class ScheduleBL
    {
        private int scheduleId;
        private int medicationId;
        private int dayOfTheWeek;
        private string takingTime;
        private int userId;
        private string medicationName;

        public int scheduleIdGS { get => scheduleId; set => scheduleId = value; }
        public int medicationIdGS { get => medicationId; set => medicationId = value; }
        public int dayOfTheWeekGS { get => dayOfTheWeek; set => dayOfTheWeek = value; }
        public string takingTimeGS { get => takingTime; set => takingTime = value; }
        public int userIdGS { get => userId; set => userId = value; }
        public string medicationNameGS { get => medicationName; set => medicationName = value; }

        /// <summary>
        /// putting existing schedule information to varibles
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="medicationId"></param>
        /// <param name="dayOfTheWeek"></param>
        /// <param name="takingTime"></param>
        /// <param name="userId"></param>
        public ScheduleBL(int scheduleId , int medicationId, int dayOfTheWeek, string takingTime, int userId)
        {
            this.scheduleId = scheduleId;
            this.medicationId = medicationId;
            this.dayOfTheWeek = dayOfTheWeek;
            this.takingTime = takingTime;
            this.userId = userId;

            DataTable dt = MedicationClass.GetSpecifiecMedication(this.medicationId);
            DataRow dr = dt.Rows[0];
            this.medicationName = dr[1].ToString();
        }
        /// <summary>
        /// existing schedule with only id
        /// </summary>
        /// <param name="scheduleId"></param>
        public ScheduleBL(int scheduleId)
        {
            this.scheduleId = scheduleId;
          
            DataTable dt = ScheduleClass.GetSpecifiecSchedule(scheduleId);
            DataRow dr = dt.Rows[0];
            this.medicationId = (int)dr[1];
            this.dayOfTheWeek = (int)dr[2];
            this.takingTime = dr[3].ToString();
            this.userId = (int)dr[4];
            DataTable dtv = MedicationClass.GetSpecifiecMedication(this.medicationId);
            DataRow drv = dtv.Rows[0];
            this.medicationName = drv[1].ToString();
        }
        /// <summary>
        /// inserting new schedule
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="dayOfTheWeek"></param>
        /// <param name="takingTime"></param>
        /// <param name="userId"></param>
        public ScheduleBL(int medicationId, int dayOfTheWeek, string takingTime, int userId)
        {
            this.medicationId = medicationId;
            this.dayOfTheWeek = dayOfTheWeek;
            this.takingTime = takingTime;
            this.userId = userId;
            ScheduleClass.InsertSchedule(medicationId, dayOfTheWeek, takingTime, userId);
            DataTable dt = MedicationClass.GetSpecifiecMedication(this.medicationId);
            DataRow dr = dt.Rows[0];
            this.medicationName = dr[1].ToString();
        }
        /// <summary>
        /// removing the schedule
        /// </summary>
        public void ScheduleRemove()
        {
            ScheduleClass.RemoveSchedule(this.scheduleId);
        }
    }
}
