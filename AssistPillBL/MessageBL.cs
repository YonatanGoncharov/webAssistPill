using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class MessageBL
    {
        //single messages
        private int messageId; //the id of the message
        private int senderId; //the id of the sender
        private int recipientId; //the id of the recipent 
        private string messageDate; //the date of the message
        private string messageContent; //the content of the message
        private bool messageSeen; //checking if the message has been seen

        /// <summary>
        /// new message insert builder
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="senderId"></param>
        /// <param name="recipientId"></param>
        /// <param name="messageDate"></param>
        /// <param name="messageContent"></param>
        /// <param name="messageSeen"></param>
        public MessageBL(int messageId, int senderId, int recipientId, string messageDate, string messageContent, bool messageSeen)
        {
            this.messageId = messageId;
            this.senderId = senderId;
            this.recipientId = recipientId;
            this.messageDate = messageDate;
            this.messageContent = messageContent;
            this.messageSeen = messageSeen;
            MessagesClass.InsertMessage(senderId, recipientId, messageDate, messageContent, messageSeen);
        }
        /// <summary>
        /// builder for existing message
        /// </summary>
        /// <param name="messageId"></param>
        public MessageBL(int messageId)
        {
            DataTable dt = MessagesClass.GetSpecifiecMessage(messageId);

            DataRow dr = dt.Rows[0];
            this.messageId = messageId;
            this.senderId = (int)dr[1];
            this.recipientId = (int)dr[2];
            this.messageDate = dr[3].ToString();
            this.messageContent = dr[4].ToString();
            this.messageSeen = (bool)dr[5];
        }
        public int messageIdGS { get => messageId; set => messageId = value; } //message id get/set
        public int senderIdGS { get => senderId; set => senderId = value; } //sender id get/set
        public int recipientIdGS { get => recipientId; set => recipientId = value; } //recipient id get/set
        public string messageDateGS { get => messageDate; set => messageDate = value; } //message date /get/set
        public string messageContentGS { get => messageContent; set => messageContent = value; } //message content get/set
        public bool messageSeenGS { get => messageSeen; set => messageSeen = value; } //message seen get/set
        
        /// <summary>
        /// making the message to seen
        /// </summary>
        public void MessageHasBeenSeen()
        {
            MessagesClass.SeenMessage(this.messageId);
        }
        /// <summary>
        /// removing the message
        /// </summary>
        public void RemoveMessage()
        {
            MessagesClass.RemoveMessage(this.messageId);
        }
    }
}
