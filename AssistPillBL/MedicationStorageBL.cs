using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class MedicationStorageBL
    {
        private int storageReminderId; //id of storage reminder
        private int userId;//id of user
        private string sendingDate;//the sending date of the email
        private bool isSawStatus;//is saw status

        public int StorageReminderId { get => storageReminderId; set => storageReminderId = value; }//get/set for storagereminderid
        public int UserId { get => userId; set => userId = value; } //get/set for userid
        public string SendingDate { get => sendingDate; set => sendingDate = value; } //get/set for sending date
        public bool IsSawStatus { get => isSawStatus; set => isSawStatus = value; } //get/set for issawsatatus

        //builder for new msr
        public MedicationStorageBL(int userId, string sendingDate, bool isTook)
        {
            this.userId = userId;
            this.sendingDate = sendingDate;
            this.isSawStatus = false;
            MedicationStorageClass.InsertMSR(userId, sendingDate, isTook);
            DataTable dt = MedicationStorageClass.GetSpecifiecMSR(userId , sendingDate);
            DataRow dr = dt.Rows[0];
            this.storageReminderId = (int)dr[0];       
        }
        //builder for existing msr
        public MedicationStorageBL(int userId, string sendingDate)
        {
            DataTable dt = MedicationStorageClass.GetSpecifiecMSR(userId, sendingDate);
            DataRow dr = dt.Rows[0];
            this.storageReminderId = (int)dr[0];
            this.userId = userId;
            this.sendingDate = sendingDate;
            this.isSawStatus = (bool)dr[3];
        }
        //updating the issawstatus to seen
        public void SeenUpdate()
        {
            MedicationStorageClass.UpdateMSRtoSeen(this.storageReminderId);
            this.isSawStatus = true;
        }
    }
}
