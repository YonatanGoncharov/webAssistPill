using System;
using System.Collections.Generic;
using System.Linq;
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

        public int scheduleIdGS { get => scheduleId; set => scheduleId = value; }
        public int medicationIdGS { get => medicationId; set => medicationId = value; }
        public int dayOfTheWeekGS { get => dayOfTheWeek; set => dayOfTheWeek = value; }
        public string takingTimeGS { get => takingTime; set => takingTime = value; }
        public int userIdGS { get => userId; set => userId = value; }
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
        }

    }
}
