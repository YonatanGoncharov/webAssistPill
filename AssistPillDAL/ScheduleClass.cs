using System;
using System.Data;

namespace FinalProjectDAL
{
    public class ScheduleClass
    {
        /// <summary>
        /// getting all schedules
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllSchedules()
        {
            string sSql = $@"Select ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) , (IsRemoved) from ScheduleTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specifiec Schedule from the database
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecSchedule(int scheduleId)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId)  from ScheduleTBL WHERE ScheduleTBL.[ScheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// checking if the Schedule exists
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public static bool IsScheduleExists(int scheduleId)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId)  from ScheduleTBL WHERE ScheduleTBL.[ScheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the schedule exist when paramas are similar
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="takingTime"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsScheduleExists(int medicationId, int dayOfWeek, string takingTime, int userId)
        {
            string sSql = $@"Select ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) , (IsRemoved) from ScheduleTBL WHERE ScheduleTBL.MedicationId = {medicationId} AND ScheduleTBL.DayOfWeek = {dayOfWeek} AND ScheduleTBL.TakingTime = '{takingTime}' AND ScheduleTBL.UserId = {userId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// checking if the medication Id exists with the schedule already
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        public static bool IsMedicationIdExists(int scheduleId, int medicationId)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) from ScheduleTBL WHERE ScheduleTBL.[MedicationId] = {medicationId} AND ScheduleTBL.[ScheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// checking if the date exists with the schedule already
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static bool IsDayOfWeekExists(int scheduleId, int dayOfWeek)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) from ScheduleTBL WHERE ScheduleTBL.[DayOfWeek] = {dayOfWeek} AND ScheduleTBL.[ScheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the taking time exits with the schedule already
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="takingTime"></param>
        /// <returns></returns>
        public static bool IsTakingTimeExists(int scheduleId, string takingTime)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) from ScheduleTBL WHERE ScheduleTBL.[TakingTime] = #{takingTime}# AND ScheduleTBL.[scheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the user id exists with the schedule already
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool IsUserIdExists(int scheduleId, int userId)
        {
            string sSql = $@"SELECT ScheduleId, (MedicationId) , (DayOfWeek) , (TakingTime) , (UserId) from ScheduleTBL WHERE ScheduleTBL.[UserId] = {userId} AND ScheduleTBL.[scheduleId] = {scheduleId} AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// inserting schedule
        /// checking if the schedule exist when paramas already exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="takingTime"></param>
        /// <param name="userId"></param>
        public static void InsertSchedule(int medicationId, int dayOfWeek, string takingTime, int userId)
        {
            string sSql = $@"INSERT INTO ScheduleTBL (medicationId , dayOfWeek , takingTime , userId) VALUES ({medicationId} ,{dayOfWeek},'{Convert.ToDateTime(takingTime)}',{userId})";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating medication id
        /// check if medicaton not exists and if schedule exists
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="newMedicationId"></param>
        public static void UpdateMedicationId(int scheduleId, int newMedicationId)
        {
            string sSql = $@"UPDATE ScheduleTBL SET ScheduleTBL.MedicationId = {newMedicationId} WHERE ScheduleTBL.ScheduleId = {scheduleId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating day of the week
        /// checking if the day not exists and if schedule exists
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="newDayOfWeek"></param>
        public static void UpdateDayOfWeek(int scheduleId, int newDayOfWeek)
        {
            string sSql = $@"UPDATE ScheduleTBL SET ScheduleTBL.DayOfWeek = {newDayOfWeek} WHERE ScheduleTBL.ScheduleId = {scheduleId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating taking time
        /// checking if the schedule exists then checking if the time dont exists
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="newTakingTime"></param>
        public static void UpdateTakingTime(int scheduleId, string newTakingTime)
        {
            string sSql = $@"UPDATE ScheduleTBL SET ScheduleTBL.TakingTime = '{Convert.ToDateTime(newTakingTime)}' WHERE ScheduleTBL.ScheduleId = {scheduleId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating user id
        /// checking if the user id dont exists
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="newUserId"></param>
        public static void UpdateUserId(int scheduleId, int newUserId)
        {
            string sSql = $@"UPDATE ScheduleTBL SET ScheduleTBL.UserId = {newUserId} WHERE ScheduleTBL.ScheduleId = {scheduleId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// removing the schedule from the system but keeping in the database
        /// checking if the schedule exists
        /// </summary>
        /// <param name="scheduleId"></param>
        public static void RemoveSchedule(int scheduleId)
        {
            string sSql = $@"UPDATE ScheduleTBL SET ScheduleTBL.IsRemoved = {true} WHERE ScheduleTBL.ScheduleId = {scheduleId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
    
    }
}
