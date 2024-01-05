using System.Data;

namespace FinalProjectDAL
{
    public class AttendantClass
    {
        /// <summary>
        /// getting all the Attendants from the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAttendants()
        {
            string sSql = $@"Select AttendantId, (AttendantName) , (AttendantLastName) , (AttendantEmail) , (AttendantPassword) , (AttendantPriorety) from AttendantTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specific attendant
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecAttendant(string attendantEmail)
        {
            string sSql = $@"Select AttendantId, (AttendantName) , (AttendantLastName) , (AttendantEmail) , (AttendantPassword) , (IsRemoved) from AttendantTBL Where AttendantTBL.[AttendantEmail] = '{attendantEmail}'  AND AttendantTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// checks if the Attendant data exists in the database
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <returns></returns>
        public static bool IsAttendantExist(string attendantEmail)
        {
            string sSql = $@"SELECT AttendantId, (AttendantName) , (AttendantLastName) , (AttendantEmail) , (AttendantPassword) , (IsRemoved) FROM AttendantTBL WHERE AttendantTBL.[AttendantEmail] = '{attendantEmail}' AND AttendantTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// inserting the attendant to the database
        /// checking if the email used in the system already
        /// </summary>
        /// <param name="attendantName"></param>
        /// <param name="attendantLastName"></param>
        /// <param name="attendantEmail"></param>
        /// <param name="attendantPassword"></param>
        public static void InsertAttendant(string attendantName, string attendantLastName, string attendantEmail, string attendantPassword)
        {
            string sSql = $@"INSERT INTO AttendantTBL (AttendantName, AttendantLastName, AttendantEmail, AttendantPassword) VALUES ('{attendantName}','{attendantLastName}','{attendantEmail}','{attendantPassword}')";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating Attendant password
        /// checking if the password dont exists
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="newAttendantPassword"></param>
        public static void UpdatePassword(string attendantEmail, string newAttendantPassword)
        {
            string sSql = $@"UPDATE AttendantTBL SET AttendantTBL.AttendantPassword = '{newAttendantPassword}' WHERE AttendantTBL.AttendantEmail = '{attendantEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating Attendant email
        /// checking if the attendant exists
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="newAttendantEmail"></param>
        public static void UpdateEmail(string attendantEmail, string newAttendantEmail)
        {
            string sSql = $@"UPDATE AttendantTBL SET AttendantTBL.AttendantEmail = '{newAttendantEmail}' WHERE AttendantTBL.AttendantEmail = '{attendantEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
        
        /// <summary>
        /// removing the attendant from the system but keeping in the database
        /// checking if the attendant exits
        /// </summary>
        /// <param name="attendantEmail"></param>
        public static void RemoveAttendant(string attendantEmail)
        {
            string sSql = $@"UPDATE AttendantTBL SET AttendantTBL.IsRemoved = {true} WHERE AttendantTBL.AttendantEmail = '{attendantEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
    }
}
