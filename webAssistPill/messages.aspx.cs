using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               Session["user"] = new AttendantBL("gwnzrwbywntn@gmail.com", "123123");
                if (Session["user"] is AttendantBL attendant)
                {
                    MessagesBL messagesBL = new MessagesBL(attendant.attendantIdGS);
                    List<MessageBL> messages = messagesBL.Messages;
                    messagesRepeater.DataSource = messages;
                    messagesRepeater.DataBind();
                    //making the new message seen
                    foreach(MessageBL message in messages)
                    {
                        if (!message.messageSeenGS)
                        {
                            message.MessageHasBeenSeen();
                        }
                    } 
                }
            }
        }
        protected void RemoveMessage_Command(object sender, CommandEventArgs e)
        {
            RemoveMessage(e.CommandArgument);
            // Optionally, you can rebind your data here or take any other necessary actions.
        }

        /// <summary>
        /// removing the message
        /// </summary>
        /// <param name="messageId"></param>
        public void RemoveMessage(object messageId)
        {
            if (Session["user"] is AttendantBL attendant)
            {
                MessagesBL messagesBL = new MessagesBL(attendant.attendantIdGS);
                List<MessageBL> messages = messagesBL.Messages;
                foreach (MessageBL m in messages)
                {
                    if (m.messageIdGS.ToString().Equals(messageId.ToString())) 
                    {
                        m.RemoveMessage();
                    }
                }
            }
        }
        /// <summary>
        /// checking if the message is seen
        /// </summary>
        /// <param name="isSeen"></param>
        /// <returns></returns>
        protected string GetMessageStatus(bool isSeen)
        {
            return isSeen ? "Seen" : "Unseen";
        }
    }
}