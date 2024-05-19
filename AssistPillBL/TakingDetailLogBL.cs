using FinalProjectDAL;
using System.Data;

namespace AssistPillBL
{
    public class TakingDetailLogBL
    {
        private int takingDetailLogId;//the id of the log
        private int takingDetailId;//the id of the taking detail
        private int numberOfSent;//the number the log was sent


        public int TakingDetailLogId { get => takingDetailLogId; set => takingDetailLogId = value; } //get/set for the takingDetailLogId
        public int TakingDetailId { get => takingDetailId; set => takingDetailId = value; } //get/set for the takingDetailId
        public int NumberOfSent { get => numberOfSent; set => numberOfSent = value; } //get/set for the numerOfSent

        /// <summary>
        /// builder for new log
        /// </summary>
        /// <param name="takingDetailId"></param>
        /// <param name="numerOfSent"></param>
        public TakingDetailLogBL(int takingDetailId, int numberOfSent)
        {
            this.takingDetailId = takingDetailId;
            this.numberOfSent = numberOfSent;
            TakingDetailClass.InsertTakingDetailLog(takingDetailId);
            DataTable dt = TakingDetailClass.GetSpecifiecTakingDetailLog(takingDetailId);
            DataRow dr = dt.Rows[0];
            this.takingDetailLogId = (int)dr[0];
        }
        /// <summary>
        /// builder for exisiting log
        /// </summary>
        /// <param name="takingDetailId"></param>
        public TakingDetailLogBL(int takingDetailId)
        {
            this.takingDetailId = takingDetailId;
            DataTable dt = TakingDetailClass.GetSpecifiecTakingDetailLog(takingDetailId);
            DataRow dr = dt.Rows[0];
            this.takingDetailLogId = (int)dr[0];
            this.numberOfSent = (int)dr[2];
        }

        /// <summary>
        /// adding one the the log of the time it was sent
        /// </summary>
        public void ChangeNumberOfSent()
        {
            TakingDetailClass.UpdateLogTimes(this.takingDetailId, this.numberOfSent + 1);
        }
        /// <summary>
        /// reseting the number of sent in log
        /// </summary>
        public void ResetNumberOfSent()
        {
            TakingDetailClass.UpdateLogTimes(this.takingDetailId, 0);
        }
        /// <summary>
        /// stopping the log and removing him
        /// </summary>
        public void TakingDetailLogStop()
        {
            TakingDetailClass.RemoveTakingDetailLog(this.takingDetailId);
        }
    }
}
