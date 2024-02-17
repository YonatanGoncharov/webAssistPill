using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class UsersBL
    {
        private List<UserBL> users;//user list

        public List<UserBL> Users { get => users; set => users = value; }//users get/set
        /// <summary>
        /// builder for all the users
        /// </summary>
        public UsersBL()
        {
            DataTable dt = UserClass.GetAllUsers();
            this.users = new List<UserBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                string userEmail = dr[3].ToString();
                string userPassword = dr[4].ToString();
                UserBL user = new UserBL(userEmail, userPassword);
                this.users.Add(user);
            }
        }

       
    }
}
