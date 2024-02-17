using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using AssistPillBL;

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
            string recipientEmail = emailRequest.Text;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";

            // Check if the user or attendant with the given email exists
            if (!UserBL.IsUser(recipientEmail) && !AttendantBL.IsAttendant(recipientEmail))
            {
                cvEmail.ErrorMessage = "Please enter a valid email address!";
                return;
            }

            // Generate a unique token for the password reset link
            string resetToken = Guid.NewGuid().ToString();

            // Create a MailMessage object
            MailMessage message = new MailMessage(senderEmail, recipientEmail);

            // Set the subject and body of the email with the unique token
            message.Subject = "Password Reset";
            message.Body = $@"Click the following link to reset your password: http://localhost:51422/password_reset.aspx?user={recipientEmail}&token={resetToken}";

            // Determine the SMTP server based on the email provider
            if (recipientEmail.Contains("gmail"))
            {
                EmailSend(senderEmail, senderPassword, "smtp.gmail.com", message);
            }
            else if (recipientEmail.Contains("yahoo"))
            {
                EmailSend(senderEmail, senderPassword, "smtp.mail.yahoo.com", message);
            }
            else if (recipientEmail.Contains("office365"))
            {
                EmailSend(senderEmail, senderPassword, "smtp.office365.com", message);
            }

        }
        public void EmailSend(string senderEmail, string senderPassword, string smtpServer, MailMessage message)
        {
            // Create a SmtpClient to send the email
            SmtpClient smtpClient = new SmtpClient(smtpServer);

            // If authentication is required, provide credentials
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

            // Specify the SMTP server port (usually 587 for secure email)
            smtpClient.Port = 587;

            // Enable SSL if your email provider requires it
            smtpClient.EnableSsl = true;

            try
            {
                // Send the email
                smtpClient.Send(message);

                // Optionally, display a success message or redirect to a success page
                Response.Write("Email sent successfully!");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during email sending
                Response.Write("Error sending email: " + ex.Message);
            }
            finally
            {
                // Dispose of the MailMessage and SmtpClient objects
                message.Dispose();
                smtpClient.Dispose();
            }
        }
        
    }
}