using System;
using System.Data;

namespace FinalProjectDAL
{
    public class MessagesClass
    {
        /// <summary>
        /// getting all messages
        /// </summary>
        /// <returns>returns all the messages in the db</returns>
        public static DataTable GetAllMessages()
        {
            string sSql = $@"Select MessageId, SenderId , RecipientId , MessageDate , MessageContent , MessageSeenStatus , IsRemoved from MessagesTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting all the attendant messages
        /// </summary>
        /// <param name="attendantId">the id of the attendant</param>
        /// <returns></returns>
        public static DataTable GetAllAttendantMessages(int attendantId)
        {
            string sSql = $@"Select MessageId, SenderId , RecipientId , MessageDate , MessageContent , MessageSeenStatus , IsRemoved from MessagesTBL WHERE MessagesTBL.RecipientId = {attendantId} AND MessagesTBL.IsRemoved = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specifiec message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecMessage(int messageId)
        {
            string sSql = $@"Select MessageId, SenderId , RecipientId , MessageDate , MessageContent , MessageSeenStatus , IsRemoved from MessagesTBL WHERE MessagesTBL.MessageId = {messageId} AND MessagesTBL.IsRemoved = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }

        /// <summary>
        /// inserting a new update message about the user to the attendant
        /// </summary>
        /// <param name="senderId">the id of the user</param>
        /// <param name="recipientId">the id of the attendant</param>
        /// <param name="messageDate">the date of the message</param>
        /// <param name="messageContent">the content of the message</param>
        /// <param name="messageSeen">the status of the message seen</param>
        public static void InsertMessage(int senderId, int recipientId, string messageDate, string messageContent, bool messageSeen)
        {
            string sSql = $@"INSERT INTO MessagesTBL (SenderId , RecipientId , MessageDate , MessageContent , MessageSeenStatus) VALUES ({senderId} ,{recipientId} , '{Convert.ToDateTime(messageDate)}' , '{messageContent}' , {messageSeen})";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updating the message to seen
        /// </summary>
        /// <param name="messageId">the id of the message</param>
        public static void SeenMessage(int messageId)
        {
            string sSql = $@"UPDATE MessagesTBL SET MessagesTBL.MessageSeenStatus = {true} WHERE MessagesTBL.MessageId = {messageId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// removing the attendant from the system but keeping in the database
        /// </summary>
        /// <param name="messageId">the id of the message</param>
        public static void RemoveMessage(int messageId)
        {
            string sSql = $@"UPDATE MessagesTBL SET MessagesTBL.IsRemoved = {true} WHERE MessagesTBL.MessageId = {messageId}";
            DBHelper.ExecuteNonQuery(sSql);
        }

    }
}
