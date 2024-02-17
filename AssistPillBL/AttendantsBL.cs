using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class AttendantsBL
    {
        private List<AttendantBL> attendants;//attendant list

        public List<AttendantBL> Attendants { get => attendants; set => attendants = value; }//attendats get/set
        /// <summary>
        /// builder for all the attendants
        /// </summary>
        public AttendantsBL()
        {
            DataTable dt = AttendantClass.GetAllAttendants();
            this.attendants = new List<AttendantBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string userEmail = dr[3].ToString();
                string userPassword = dr[4].ToString();
                AttendantBL attendant = new AttendantBL(userEmail, userPassword);
                this.attendants.Add(attendant);
            }
        }
    }
}
