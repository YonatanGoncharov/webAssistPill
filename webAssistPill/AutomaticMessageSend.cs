using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace webAssistPill
{
    public class AutomaticMessageSend
    {
        public static void MedicineReminder()
        {
            //checking automaticly with a server the time and sends reminder in the 30 minutes
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
        /// <summary>
        /// checking once a day if all of the medications are in stock
        /// </summary>
        public static void MedInStockReminder()
        {
            UsersBL users = new UsersBL();
            List<UserBL> usersList = users.Users;

            foreach (UserBL user in usersList)
            {
                string goneMedications = "";
                List<MedicationBL> medications = MedicationBL.GetUserMedications(user.userIdgs);
                foreach (MedicationBL medication in medications)
                {
                    if (medication.MedicationAmount == 0)
                    {
                        if (goneMedications != "")
                        {
                            goneMedications += ", "; // Add comma separator if not the first medication
                        }
                        goneMedications += medication.MedicationName;
                    }
                }
                if (!goneMedications.Equals(""))
                {
                    new MedicationStorageBL(user.userIdgs, DateTime.Now.ToString("yyyy-MM-dd"));
                    List<AttendantBL> attendants = user.GetAttendants();
                    foreach (AttendantBL attendant in attendants)
                    {
                        SendMedicationStockReminder(attendant.attendantEmailGS, goneMedications, user.userIdgs, user.userNamegs, user.userLastNamegs);
                    }
                }
            }
        }
        public static void SendMessageIsClaimed(string attendantEmail , string claimerEmail, string date)
        {
            string recipientEmail = attendantEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", attendantEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"The medication stock duty for {date} has been taken by {claimerEmail}, there is no need to buy yourself.";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        private static void SendMedicationStockReminder(string attendantEmail, string goneMedications, int userId , string userName , string userLastName)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            string resetToken = Guid.NewGuid().ToString();
            string recipientEmail = attendantEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", attendantEmail);
            message.Subject = "Medication Stock Reminder";
            message.Body = $"The following medication for your patient {userName} {userLastName} are gone, please resupply them. \nMedications: {goneMedications} \n" +
                $"If you saw this email please confirm by clicking on this link: http://localhost:51422/attendant_confirmation_page.aspx?attendant={recipientEmail}&type=stock&user={userId}&token={resetToken}&date={currentDate}";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        private static void AttendantReminder()
        {
            UsersBL users = new UsersBL();
            List<UserBL> usersList = users.Users;

        }
        private static void SendReminderEmail(string userEmail, DateTime takingTime)
        {
            string recipientEmail = userEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", userEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"It's time to take your medication at {takingTime.ToString("HH:mm")}. Don't forget!";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        private static void EmailSend(string senderEmail, string recipientEmail, string senderPassword, MailMessage message)
        {
            string smtpServer = "";
            if (recipientEmail.Contains("gmail"))
            {
                smtpServer = "smtp.gmail.com";
            }
            else if (recipientEmail.Contains("yahoo"))
            {
                smtpServer = "smtp.mail.yahoo.com";
            }
            else if (recipientEmail.Contains("office365"))
            {
                smtpServer = "smtp.office365.com";
            }
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