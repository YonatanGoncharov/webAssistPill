using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssistPill
{
    public partial class password_reset_request : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = args.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                cvEmail.ErrorMessage = "Please enter an email address!";
                args.IsValid = false;
            }
            else
            {
                // Add your custom email validation logic here
                // For example, you can use .NET's built-in MailAddress class
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(email);
                    args.IsValid = true;
                }
                catch (FormatException)
                {
                    cvEmail.ErrorMessage = "Please enter a valid email address!";
                    args.IsValid = false;
                }
            }
        }

        protected void emailRequestButton_Click(object sender, EventArgs e)
        {
            //sends email to the client then adressing him to the password_reset.aspx
            string email = emailRequest.Text;
            Response.Redirect("password_reset.aspx");
        }
    }
}