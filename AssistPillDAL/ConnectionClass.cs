using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectDAL
{
    public class ConnectionClass
    {
        /// <summary>
        /// updating attendant priorety 
        /// </summary>
        /// <param name="attendantId"></param>
        /// <param name="newPriority"></param>
        /// <param name="userId"></param>
        public static void UpdatePriorety(int attendantId, int newPriority, int userId)
        {
            string sSql = $@"UPDATE ConnectionTBL SET ConnectionTBL.AttendantPriority = {newPriority} WHERE ConnectionTBL.AttendantId = {attendantId} AND ConnectionTBL.UserId = {userId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// getting priorety
        /// </summary>
        /// <param name="attendantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetPriorety(int attendantId, int userId)
        {
            string sSql = $@"Select AttendantPriority from ConnectionTBL WHERE ConnectionTBL.UserId = {userId} AND ConnectionTBL.AttendantId ={attendantId} AND ConnectionTBL.IsRemoved = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// get all attendant patient information
        /// </summary>
        /// <param name="attendantId"></param>
        /// <returns></returns>
        public static DataTable GetAttendantPatients(int attendantId)
        {
            string sSql = $@"SELECT UserTBL.[UserName] , UserTBL.[UserLastName] , UserTBL.[UserEmail] , UserTBL.[UserPassword]
                      FROM UserTBL
                      INNER JOIN ConnectionTBL
                      ON ConnectionTBL.[UserId] = UserTBL.[UserId]
                      WHERE ConnectionTBL.[AttendantId] = {attendantId}
                      AND ConnectionTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// get all patient attendants information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetPatientAttendants(int userId)
        {
            string sSql = $@"SELECT AttendantTBL.[AttendantName] , AttendantTBL.[AttendantLastName] , AttendantTBL.[AttendantEmail] , AttendantTBL.[AttendantPassword]
                      FROM AttendantTBL
                      INNER JOIN ConnectionTBL
                      ON AttendantTBL.[AttendantId] = ConnectionTBL.[AttendantId]
                      WHERE ConnectionTBL.[UserId] = {userId}
                      AND ConnectionTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// inserting a new connection to the db
        /// </summary>
        /// <param name="attendantId"></param>
        /// <param name="userId"></param>
        /// <param name="attendantPriority"></param>
        public static void InsertConnection(int attendantId, int userId , int attendantPriority)
        {
            string sSql = $@"INSERT INTO ConnectionTBL (UserId, AttendantId, AttendantPriority) VALUES ({userId},{attendantId},{attendantPriority})";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// removing connection between the patient and the attendant
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="attendantId"></param>
        public static void RemoveConnection(int userId, int attendantId)
        {
            string sSql = $@"UPDATE ConnectionTBL SET ConnectionTBL.IsRemoved = {true} WHERE ConnectionTBL.AttendantId = {attendantId} AND ConnectionTBL.UserId = {userId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// checking if the connection exists
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="attendantId"></param>
        /// <returns></returns>
        public static bool IsConnectionExist(int userId, int attendantId)
        {
            string sSql = $@"SELECT * FROM ConnectionTBL WHERE ConnectionTBL.[AttendantId] = {attendantId} AND ConnectionTBL.[UserId] = {userId} AND ConnectionTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
    }
}
