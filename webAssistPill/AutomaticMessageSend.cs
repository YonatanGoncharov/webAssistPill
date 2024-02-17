using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services.Description;

namespace webAssistPill
{
    public class AutomaticMessageSend
    {
        public static void MedicineReminder()
        {
            //checking automaticly with a server the time and sends reminder half an hour before
            //he needs to take it
            UsersBL users = new UsersBL();
            List<UserBL> usersList = users.Users;
            foreach (UserBL user in usersList)
            {
                foreach (ScheduleBL schedule in user.GetSchedule())
                {
                    // Assuming takingTimeGS is a string property
                    string takingTimeString = schedule.takingTimeGS;

                    // Parse the string to a DateTime object
                    if (DateTime.TryParse(takingTimeString, out DateTime takingTime))
                    {
                        // Check if the current time is within 30 minutes of the scheduled time
                        if (IsWithinTimeRange(takingTime, DateTime.Now, TimeSpan.FromMinutes(30)))
                        {
                            // Send a reminder email
                            SendReminderEmail(user.userEmailgs, takingTime);
                        }
                    }
                    else
                    {
                        // Handle parsing error
                        Console.WriteLine($"Error parsing takingTimeGS for user {user.userEmailgs}");
                    }
                }
            }
        }
        private static void AttendantReminder()
        {
            AttendantsBL attendants = new AttendantsBL();
            List<AttendantBL> list = attendants.Attendants;
        }
        private static void SendReminderEmail(string userEmail, DateTime takingTime)
        {
            string recipientEmail = userEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", userEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"It's time to take your medication at {takingTime.ToString("HH:mm")}. Don't forget!";
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
        private static void EmailSend(string senderEmail, string senderPassword, string smtpServer, MailMessage message)
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
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during email sending
                Console.WriteLine("error");
            }
            finally
            {
                // Dispose of the MailMessage and SmtpClient objects
                message.Dispose();
                smtpClient.Dispose();
            }
        }
        static bool IsWithinTimeRange(DateTime targetTime, DateTime currentTime, TimeSpan range)
        {
            return currentTime >= targetTime - range && currentTime <= targetTime;
        }
    }
}