using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectDAL
{
    public class MedicationStorageClass
    {
        /// <summary>
        /// getting all of the msr
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMedicationStorageReminders()
        {
            string sSql = $@"Select StorageReminderId, (UserId) , (SendingDate) , (IsSawStatus) , (IsRemoved) from MedicationStorageReminderTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specifiec msr by date and user id
        /// </summary>
        /// <param name="receiverEmail"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecMSR(int userId, string date)
        {
            string sSql = $@"Select StorageReminderId, (UserId) , (SendingDate) , (IsSawStatus) , (IsRemoved) from MedicationStorageReminderTBL WHERE MedicationStorageReminderTBL.UserId = {userId} AND MedicationStorageReminderTBL.SendingDate = #{date}# AND MedicationStorageReminderTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// updating msr to seen
        /// </summary>
        /// <param name="storageReminderId"></param>
        public static void UpdateMSRtoSeen(int storageReminderId)
        {
            string sSql = $@"UPDATE MedicationStorageReminderTBL SET MedicationStorageReminderTBL.IsSawStatus = {true} WHERE MedicationStorageReminderTBL.MedicationId = {storageReminderId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// inserting new msr
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendingDate"></param>
        public static void InsertMSR(int userId, string sendingDate, bool isSaw)
        {
            string gSql = $@"INSERT INTO MedicationTBL (UserId, SendingDate, IsSawStatus) VALUES ({userId},'{Convert.ToDateTime(sendingDate)}',{isSaw})";
            DBHelper.ExecuteNonQuery(gSql);
        }

        /// <summary>
        /// removing msr
        /// </summary>
        /// <param name="storageReminderId"></param>
        public static void RemoveMSR(int storageReminderId)
        {
            string sSql = $@"UPDATE MedicationStorageReminderTBL SET MedicationStorageReminderTBL.IsRemoved = {true} WHERE MedicationStorageReminderTBL.MedicationId = {storageReminderId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
    }
}
