using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace AssistPillBL
{
    public class UserBL
    {
        private int userId; //the id of the user (the key)
        private string userName; //the name of the user
        private string userLastName; //the last name of the user
        private string userEmail; //the email of the user
        private string userPassword; //the password of the user
        private List<ScheduleBL> weeklySchedule;//the week schedule of the user
        private List<AttendantBL> attendants;//the attendants of the user

        public int userIdgs { get => userId; } //get/set to user id
        public string userNamegs { get => userName; set => userName = value; }//get/set to user name
        public string userLastNamegs { get => userLastName; set => userLastName = value; }//get/set to user last name
        public string userEmailgs { get => userEmail; set => userEmail = value; }//get/set to user email
        public string userPasswordgs { get => userPassword; set => userPassword = value; }//get/set to user password
        public List<ScheduleBL> weeklyScheduleGS { get => weeklySchedule; set => weeklySchedule = value; }//get/set to user weekly schedule
        public List<AttendantBL> attendantsGS { get => attendants; set => attendants = value; }//get/set to the user attendants list


        /// <summary>
        /// builder for login
        /// </summary>
        /// <param name="userEmail"></param>
        public UserBL(string userEmail, string userPassword)
        {
            if (!UserClass.IsUserExist(userEmail))
                throw new Exception("The user doesn't exist");
            DataTable dt = UserClass.GetSpecifiecUser(userEmail);
            DataRow dr = dt.Rows[0];
            this.userPassword = dr[4].ToString();
            if (!this.userPassword.Equals(userPassword))
                throw new Exception("Wrong password");
            this.userId = (int)dr[0];
            this.userName = dr[1].ToString();
            this.userLastName = dr[2].ToString();
            this.userEmail = dr[3].ToString();
            this.weeklyScheduleGS = GetSchedule();
        }
        /// <summary>
        /// builder for registration  
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userLastName"></param>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        public UserBL(string userName, string userLastName, string userEmail, string userPassword)
        {
            if (AttendantClass.IsAttendantExist(userEmail) || UserClass.IsUserExist(userEmail))
                throw new Exception("Used Email");
            this.userName = userName;
            this.userLastName = userLastName;
            this.userEmail = userEmail;
            this.userPassword = userPassword;
            UserClass.InsertUser(userName, userLastName, userEmail, userPassword);
        }
        /// <summary>
        /// updating the password
        /// </summary>
        /// <param name="newPassword"></param>
        public void UpdatePassword(string newPassword)
        {
            if (newPassword.Equals(this.userPassword))
                throw new Exception("Same password");
            UserClass.UpdatePassword(this.userEmail, newPassword);
            this.userPassword = newPassword;
        }
        /// <summary>
        /// updating the email
        /// </summary>
        /// <param name="newEmail"></param>
        public void UpdateEmail(string newEmail)
        {
            if (newEmail.Equals(this.userEmail))
                throw new Exception("Same Email");
            UserClass.UpdateEmail(this.userEmail, newEmail);
            this.userEmail = newEmail;
        }
        /// <summary>
        /// removing the user
        /// </summary>
        public void RemoveUser()
        {
            UserClass.RemoveUser(this.userEmail);
        }

        /// <summary>
        /// setting up the schedule
        /// </summary>
        public List<ScheduleBL> GetSchedule()
        {
            DataTable dt = UserClass.GetWeeklySchedule(this.userId);
            this.weeklySchedule = new List<ScheduleBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ScheduleBL schedule = new ScheduleBL((int)dr[0], (int)dr[1], (int)dr[2], dr[3].ToString(), (int)dr[4]);
                this.weeklySchedule.Add(schedule);
            }
            return this.weeklySchedule;
        }
        /// <summary>
        /// getting the attendants of the patient
        /// </summary>
        /// <returns></returns>
        public List<AttendantBL> GetAttendants()
        {
            DataTable dt = ConnectionClass.GetPatientAttendants(this.userId);
            this.attendants = new List<AttendantBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                AttendantBL attendant = new AttendantBL(dr[2].ToString(), dr[3].ToString());
                this.attendants.Add(attendant);
            }
            return this.attendants;
        }
        /// <summary>
        /// getting specifiec attendant
        /// </summary>
        /// <param name="attendantId"></param>
        /// <returns></returns>
        public AttendantBL GetSpecifiecAttendant(int attendantId)
        {
            List<AttendantBL> attendants = attendantsGS;
            for (int i = 0; i < attendantsGS.Count; i++)
            {
                if (attendants[i].attendantIdGS == attendantId)
                {
                    return attendants[i];
                }
            }
            return null;
        }
        /// <summary>
        /// adding a new attendant connection
        /// </summary>
        /// <param name="email"></param>
        /// <param name="priority"></param>
        public void AddNewAttendant(string email , int priority)
        {
            DataTable dt = AttendantClass.GetSpecifiecAttendant(email);
            DataRow dr = dt.Rows[0];
            if (ConnectionClass.IsConnectionExist(this.userId, (int)dr[0]))
                throw new Exception("Connection exists");
            AttendantBL attendant = new AttendantBL(dr[3].ToString(), dr[4].ToString());
            attendantsGS.Add(attendant);
            ConnectionClass.InsertConnection((int)dr[0], this.userId, priority);
        }
        //removing attendant
        public void RemoveAttendant(int attendantId)
        {
            List<AttendantBL> attendants = attendantsGS;
            for (int i = 0; i < attendantsGS.Count; i++)
            {
                if (attendants[i] != null)
                {
                    if (attendants[i].attendantIdGS == attendantId)
                    {
                        ConnectionClass.RemoveConnection(this.userId, attendantId);
                        this.attendantsGS.RemoveAt(i);
                        attendantsGS.Sort();
                        break;
                    }
                }
            }
        }
    }
}
