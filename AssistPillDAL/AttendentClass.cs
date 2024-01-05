using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectDAL
{
    public class AttendentClass
    {
        /// <summary>
        /// getting all the Attendents from the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAttendents()
        {
            string sSql = $@"Select AttendentId, (AttendentName) , (AttendentLastName) , (AttendentEmail) , (AttendentPassword) , (AttendentPriorety) , (UserId) , (IsRemoved) from AttendentTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specific attendent
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecAttendent(string attendentEmail)
        {
            string sSql = $@"Select AttendentId, (AttendentName) , (AttendentLastName) , (AttendentEmail) , (AttendentPassword) , (AttendentPriorety) , (UserId) , (IsRemoved) from AttendentTBL Where AttendentTBL.[AttendentEmail] = '{attendentEmail}  AND AttendentTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// checkeing if the password exists
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <param name="attendentPassword"></param>
        /// <returns></returns>
        private static bool IsPasswordExists(string attendentEmail, string attendentPassword)
        {
            if (!IsAttendentExist(attendentEmail))
            {
                return false;
            }
            string sSql = $@"SELECT AttendentId, (AttendentName) , (AttendentLastName) , (AttendentEmail) , (AttendentPassword) , (AttendentPriorety) , (UserId) , (IsRemoved) FROM AttendentTBL WHERE AttendentTBL.[AttendentPassword] = '{attendentPassword}'";
            DataTable dt = DBHelper.GetDataTable(sSql);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// checks if the Attendent data exists in the database
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <returns></returns>
        public static bool IsAttendentExist(string attendentEmail)
        {
            string sSql = $@"SELECT AttendentId, (AttendentName) , (AttendentLastName) , (AttendentEmail) , (AttendentPassword) , (AttendentPriorety) , (UserId) , (IsRemoved) FROM AttendentTBL WHERE AttendentTBL.[AttendentEmail] = '{attendentEmail}' AND AttendentTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            if (dt.Rows.Count == 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// inserting the attendent to the database
        /// </summary>
        /// <param name="attendentName"></param>
        /// <param name="attendentLastName"></param>
        /// <param name="attendentEmail"></param>
        /// <param name="attendentPassword"></param>
        /// <param name="attendentPriorety"></param>
        /// <param name="userId"></param>
        public static void InsertAttendent(string attendentName, string attendentLastName, string attendentEmail, string attendentPassword , int attendentPriorety , int userId)
        {
            if (!IsAttendentExist(attendentEmail) && !(UserClass.IsUserExist(attendentEmail)))
            {
                string sSql = $@"INSERT INTO AttendentTBL (AttendentName, AttendentLastName, AttendentEmail, AttendentPassword, AttendentPriorety ,UserId) VALUES ('{attendentName}','{attendentLastName}','{attendentEmail}','{attendentPassword}' , {attendentPriorety} , {userId})";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
        /// <summary>
        /// updating Attendent password
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <param name="newAttendentPassword"></param>
        public static void UpdatePassword(string attendentEmail, string newAttendentPassword)
        {
            if (!IsPasswordExists(attendentEmail, newAttendentPassword))
            {
                string sSql = $@"UPDATE AttendentTBL SET AttendentTBL.AttendentPassword = '{newAttendentPassword}' WHERE AttendentTBL.AttendentEmail = '{attendentEmail}'";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
        /// <summary>
        /// updating Attendent email
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <param name="newAttendentEmail"></param>
        public static void UpdateEmail(string attendentEmail, string newAttendentEmail)
        {
            if (IsAttendentExist(attendentEmail))
            {
                string sSql = $@"UPDATE AttendentTBL SET AttendentTBL.AttendentEmail = '{newAttendentEmail}' WHERE AttendentTBL.AttendentEmail = '{attendentEmail}'";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
        /// <summary>
        /// updating attendent priorety
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <param name="newPriorety"></param>
        public static void UpdatePriorety(string attendentEmail, int newPriorety)
        {
            if (IsAttendentExist(attendentEmail))
            {
                string sSql = $@"UPDATE AttendentTBL SET AttendentTBL.AttendentPriorety = {newPriorety} WHERE AttendentTBL.AttendentEmail = '{attendentEmail}'";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
        /// <summary>
        /// updating attendent user id
        /// </summary>
        /// <param name="attendentEmail"></param>
        /// <param name="userId"></param>
        public static void UpdateUserId(string attendentEmail, int userId)
        {
            if (IsAttendentExist(attendentEmail))
            {
                string sSql = $@"UPDATE AttendentTBL SET AttendentTBL.UserId = {userId} WHERE AttendentTBL.AttendentEmail = '{attendentEmail}'";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
        /// <summary>
        /// removing the attendent from the system but keeping in the database
        /// </summary>
        /// <param name="attendentEmail"></param>
        public static void RemoveAttendent(string attendentEmail)
        {
            if (IsAttendentExist(attendentEmail))
            {
                string sSql = $@"UPDATE AttendentTBL SET AttendentTBL.IsRemoved = {true} WHERE AttendentTBL.AttendentEmail = '{attendentEmail}'";
                DBHelper.ExecuteNonQuery(sSql);
            }
        }
    }
}
