using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProjectDAL;
using System.Data;
namespace AssistPillBL
{
    public class AttendantBL
    {
        private int attendantId; //the id of the attendant (the key)
        private string attendantName; //the name of the attendant
        private string attendantLastName; //the last name of the attendant
        private string attendantEmail; //the email of the attendant
        private string attendantPassword; //the password of the attendant
        private List<UserBL> patients; //the patients of the attendant
        public int attendantIdGS { get => attendantId;} //get/set to attendant id
        public string attendantNameGS { get => attendantName; set => attendantName = value; } //get/set to attendant name
        public string attendantLastNameGS { get => attendantLastName; set => attendantLastName = value; }  //get/set to attendant last name
        public string attendantEmailGS { get => attendantEmail; set => attendantEmail = value; } //get/set to attendant email
        public string attendantPasswordGS { get => attendantPassword; set => attendantPassword = value; } //get/set to attendant password
        public List<UserBL> patientsGS { get => patients; set => patients = value; } //get/set to the patients

        /// <summary>
        /// builder for register
        /// </summary>
        /// <param name="attendantName"></param>
        /// <param name="attendantLastName"></param>
        /// <param name="attendantEmail"></param>
        /// <param name="attendantPassword"></param>
        public AttendantBL(string attendantName, string attendantLastName, string attendantEmail, string attendantPassword)
        {
            if (AttendantClass.IsAttendantExist(attendantEmail) || UserClass.IsUserExist(attendantEmail))
                throw new Exception("Used Email");
            
            this.attendantName = attendantName;
            this.attendantLastName = attendantLastName;
            this.attendantEmail = attendantEmail;
            this.attendantPassword = attendantPassword;
            AttendantClass.InsertAttendant(attendantName, attendantLastName, attendantEmail, attendantPassword);
            DataTable dt = AttendantClass.GetSpecifiecAttendant(attendantEmail);
            DataRow dr = dt.Rows[0];
            this.attendantId = (int)dr[0]; 
        }
        /// <summary>
        /// builder for login
        /// </summary>
        /// <param name="attendantEmail"></param>
        public AttendantBL(string attendantEmail , string attendantPassword)
        {
            if (!AttendantClass.IsAttendantExist(attendantEmail))
                throw new Exception("User dont Exists");
            DataTable dt = AttendantClass.GetSpecifiecAttendant(attendantEmail);
            DataRow dr = dt.Rows[0];
            string dbPass = dr[4].ToString();
            if (!dbPass.Equals(attendantPassword))
                throw new Exception("Wrong password");
            this.attendantId = (int)dr[0];
            this.attendantName = dr[1].ToString();
            this.attendantLastName = dr[2].ToString();
            this.attendantEmail = dr[3].ToString();
            this.attendantPassword = attendantPassword;
            this.patientsGS = this.GetPatients();
        }
        /// <summary>
        /// updating password
        /// </summary>
        /// <param name="newPassword"></param>
        public void UpdatePassword(string newPassword)
        {
            if (newPassword.Equals(this.attendantPassword))
                throw new Exception("Same password");
            AttendantClass.UpdatePassword(this.attendantEmail, newPassword);
            this.attendantPassword = newPassword;
        }
        /// <summary>
        /// updating the email
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="newEmail"></param>
        public void UpdateEmail(string attendantEmail, string newEmail)
        {
            if (newEmail.Equals(this.attendantEmail))
                throw new Exception("Same email");
            AttendantClass.UpdateEmail(attendantEmail, newEmail);
            this.attendantEmail = newEmail;
        }
        /// <summary>
        /// removing the attendant
        /// </summary>
        /// <param name="attendantEmail"></param>
        public void RemoveUser(string attendantEmail)
        {
            UserClass.RemoveUser(attendantEmail);
        }
        /// <summary>
        /// getting the patients of the attendant
        /// </summary>
        /// <returns></returns>
        public List<UserBL> GetPatients()
        {
            DataTable dt = ConnectionClass.GetAttendantPatients(this.attendantId);
            this.patients = new List<UserBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                UserBL patient = new UserBL(dr[2].ToString(), dr[3].ToString());
                this.patients.Add(patient);
            }
            return this.patients;
        }
        /// <summary>
        /// getting specifiec patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public UserBL GetSpecifiecPatient(int patientId)
        {
            List<UserBL> users = patientsGS;
            for (int i = 0; i < patientsGS.Count; i++)
            {
                if (users[i].userIdgs == patientId)
                {
                    return users[i];
                }
            }
            return null;
        }
        /// <summary>
        /// adding a new patient connection
        /// </summary>
        /// <param name="email"></param>
        /// <param name="priority"></param>
        public void AddNewPatient(string email, int priority)
        {
            
            DataTable dt = UserClass.GetSpecifiecUser(email);
            DataRow dr = dt.Rows[0];
            if (ConnectionClass.IsConnectionExist((int)dr[0], this.attendantId))
                throw new Exception("Connection exists");
            UserBL user = new UserBL(dr[2].ToString(), dr[3].ToString());
            patientsGS.Add(user);
            ConnectionClass.InsertConnection(this.attendantId, (int)dr[0], priority);
        }
        /// <summary>
        /// returning the priority of the attendant
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetPriority(int userId)
        {
            DataTable dt = ConnectionClass.GetPriorety(this.attendantId, userId);
            DataRow dr = dt.Rows[0];

            return (int)dr[0];
        }
    }
}
