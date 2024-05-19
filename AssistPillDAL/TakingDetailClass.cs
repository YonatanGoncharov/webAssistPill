using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectDAL
{
    public class TakingDetailClass
    {
        /// <summary>
        /// getting specifiec taking detail by schedule id
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="takingDate"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecTakingDetail(int scheduleId)
        {
            string sSql = $@"SELECT TakingDetailId, (ScheduleId) , (TakingDate) , (IsTookStatus)  from TakingDetailTBL WHERE TakingDetailTBL.[ScheduleId] = {scheduleId} AND TakingDetailTBL.[IsTookStatus] = {false} AND TakingDetailTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// inserting the schedule taking detail to the database
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="takingDate"></param>
        public static void InsertScheduleTakingDetail(int scheduleId, string takingDate)
        {
            string sSql = $@"INSERT INTO TakingDetailTBL (ScheduleId, TakingDate) VALUES ({scheduleId}, '{Convert.ToDateTime(takingDate)}')";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// changing the schedule taking detail status to took
        /// </summary>
        /// <param name="scheduleId"></param>
        public static void ScheduleTakingDetailStatus(int scheduleId)
        {
            string sSql = $@"UPDATE TakingDetailTBL SET IsTookStatus = {true} WHERE ScheduleId = {scheduleId} AND IsRemoved = {false};";
            DBHelper.ExecuteNonQuery(sSql);
        }

        /// <summary>
        /// removing the schedule taking detail
        /// </summary>
        /// <param name="scheduleId"></param>
        public static void RemoveScheduleTakingDetail(int scheduleId)
        {
            string sSql = $@"UPDATE TakingDetailTBL SET IsRemoved = {true} WHERE ScheduleId = {scheduleId} AND IsRemoved = {false};";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// getting specifiec taking detail log
        /// </summary>
        /// <param name="takingDetailId"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecTakingDetailLog(int takingDetailId)
        {
            string sSql = $@"SELECT TakingDetailLogId, TakingDetailId, (NumberOfSent) from TakingDetailLogTBL WHERE TakingDetailLogTBL.[TakingDetailId] = {takingDetailId} AND TakingDetailLogTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// updating the number of sent in the log
        /// </summary>
        /// <param name="takingDetailId"></param>
        /// <param name="numberOfSent"></param>
        public static void UpdateLogTimes(int takingDetailId , int numberOfSent)
        {
            string sSql = $@"UPDATE TakingDetailLogTBL SET NumberOfSent = {numberOfSent} WHERE TakingDetailLogTBL.TakingDetailId = {takingDetailId} AND TakingDetailLogTBL.IsRemoved = {false};";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// inserting new taking detail log
        /// </summary>
        /// <param name="takingDetailId"></param>
        public static void InsertTakingDetailLog(int takingDetailId)
        {
            string sSql = $@"INSERT INTO TakingDetailLogTBL (TakingDetailId, NumberOfSent) VALUES ({takingDetailId}, {0})";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// removing the taking detail log
        /// </summary>
        /// <param name="takingDetailId"></param>
        public static void RemoveTakingDetailLog(int takingDetailId)
        {
            string sSql = $@"UPDATE TakingDetailLogTBL SET TakingDetailLogTBL.IsRemoved = {true} WHERE TakingDetailLogTBL.TakingDetailId = {takingDetailId} AND TakingDetailLogTBL.IsRemoved = {false};";
            DBHelper.ExecuteNonQuery(sSql);
        }
    }
}
