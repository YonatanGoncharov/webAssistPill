using System.Data;

namespace FinalProjectDAL
{
    public class UserClass
    {
        /// <summary>
        /// getting all the users from the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUsers()
        {
            string sSql = $@"Select UserId, (UserName) , (UserLastName) , (UserEmail) , (UserPassword) , (IsRemoved) from UserTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specifiec user by his email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecUser(string userEmail)
        {
            string sSql = $@"Select UserId, (UserName) , (UserLastName) , (UserEmail) , (UserPassword) from UserTBL Where UserTBL.[UserEmail] = '{userEmail}' AND UserTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// checks if the user data exists in the database
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public static bool IsUserExist(string userEmail)
        {
            string sSql = $@"SELECT UserId, UserName, UserLastName, UserEmail, UserPassword , IsRemoved FROM UserTBL WHERE UserEmail = '{userEmail}' AND IsRemoved = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt.Rows.Count > 0; 
        }
        /// <summary>
        /// inserting the user to the database
        /// checking if the user dont exists and if attendant dont exists with this email
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userLastName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        public static void InsertUser(string userName, string userLastName, string userEmail, string userPassword)
        {
            string sSql = $@"INSERT INTO UserTBL (UserName, UserLastName, UserEmail, UserPassword) VALUES ('{userName}','{userLastName}','{userEmail}','{userPassword}')";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating user password
        /// checking if the password dont exists
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="newUserPassword"></param>
        public static void UpdatePassword(string userEmail, string newUserPassword)
        {
            string sSql = $@"UPDATE UserTBL SET UserTBL.UserPassword = '{newUserPassword}' WHERE UserTBL.UserEmail = '{userEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating user email
        /// checking if the user exists
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="newUserEmail"></param>
        public static void UpdateEmail(string userEmail, string newUserEmail)
        {
            string sSql = $@"UPDATE UserTBL SET UserTBL.UserEmail = '{newUserEmail}' WHERE UserTBL.UserEmail = '{userEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// getting user weekly schedule
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataTable GetWeeklySchedule(int userId)
        {
            string sSql = $@"SELECT ScheduleTBL.[ScheduleId] ,ScheduleTBL.[MedicationId] , ScheduleTBL.[DayOfWeek] , ScheduleTBL.[TakingTime] , ScheduleTBL.[UserId]
                      FROM ScheduleTBL
                      INNER JOIN UserTBL
                      ON UserTBL.[UserId] = ScheduleTBL.[UserId]
                      WHERE ScheduleTBL.[UserId] = {userId}
                      AND ScheduleTBL.[IsRemoved] = {false};";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// removing the user from the system but keeping in the database
        /// checking if the user exists
        /// </summary>
        /// <param name="userEmail"></param>
        public static void RemoveUser(string userEmail)
        {
            string sSql = $@"UPDATE UserTBL SET UserTBL.IsRemoved = {true} WHERE UserTBL.UserEmail = '{userEmail}'";
            DBHelper.ExecuteNonQuery(sSql);
        }
    }
}
