using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class MessagesBL
    {
        int attendantId; //the id of the attendant
        List<MessageBL> messages; //the list of the messages

        /// <summary>
        /// builder for all of the messages of the attendent
        /// </summary>
        /// <param name="attendantId"></param>
        public MessagesBL(int attendantId)
        {
            this.messages = new List<MessageBL>();
            this.attendantId = attendantId;
            DataTable dt = MessagesClass.GetAllAttendantMessages(attendantId);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int messageId = (int)dr[0];
                this.messages.Add(new MessageBL(messageId));
            }
        }

        public int AttendantId { get => attendantId; set => attendantId = value; } //the id of the attendant get/set
        public List<MessageBL> Messages { get => messages; set => messages = value; } //the messages get/set

    }
}
