using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FinalProjectDAL;


namespace AssistPillBL
{
    public class TakingDetailBL
    {
        private int takingDetailId;//the id of the taking detail
        private int scheduleId;//the id of the schedule
        private string takingDate;//the takeing date of the schedule
        private bool isTookStatus; //the status of the taking detail
        public int TakingDetailId { get => takingDetailId; set => takingDetailId = value; }//get/set for takingdetail
        public int ScheduleId { get => scheduleId; set => scheduleId = value; }//get/set for scheduleid
        public string TakingDate { get => takingDate; set => takingDate = value; }//get/set for takingdate
        public bool IsTookStatus { get => isTookStatus; set => isTookStatus = value; }//get/set for istookstatus

        /// <summary>
        /// new taking detail
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="takingDate"></param>
        public TakingDetailBL(int scheduleId, string takingDate)
        {
            this.scheduleId = scheduleId;
            this.takingDate = takingDate;
            this.isTookStatus = false;
            TakingDetailClass.InsertScheduleTakingDetail(scheduleId, takingDate);
        }
        /// <summary>
        /// existing taking detail
        /// </summary>
        /// <param name="scheduleId"></param>
        public TakingDetailBL(int scheduleId)
        {
            this.scheduleId = scheduleId;
            
            DataTable dt = TakingDetailClass.GetSpecifiecTakingDetail(scheduleId);
            DataRow dr = dt.Rows[0];
            this.takingDate = dr[1].ToString();
            this.isTookStatus = (bool)dr[2];
        }
        /// <summary>
        /// the user took the medication so the status changes
        /// </summary>
        public void NewTaking()
        {
            this.isTookStatus = true;
            TakingDetailClass.ScheduleTakingDetailStatus(this.ScheduleId);   
        }
       
    }
}
