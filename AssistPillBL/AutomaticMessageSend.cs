using AssistPillBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace webAssistPill
{
    //Class for server side methods
    public static class AutomaticMessageSend
    {
        /// <summary>
        /// reminder to user to take hes medicine
        /// </summary>
        public static void MedicineReminder()
        {
            //checking automaticly with a server the time and sends reminder in the 30 minutes every 30 minutes
            //he needs to take it
            UsersBL users = new UsersBL();
            List<UserBL> usersList = users.Users;
            foreach (UserBL user in usersList)
            {
                foreach (ScheduleBL schedule in user.GetSchedule())
                {
                    // Assuming takingTimeGS is a string property
                    string takingTimeString = schedule.takingTimeGS;

                    int day = schedule.dayOfTheWeekGS;

                    // Get the day of the week
                    DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;
                    int dayOfWeekNumber = (int)dayOfWeek;

                    if (day == dayOfWeekNumber + 1)
                    {
                        // Parse the string to a DateTime object
                        if (DateTime.TryParse(takingTimeString, out DateTime takingTime))
                        {
                            // Check if the current time is within 30 minutes of the scheduled time
                            if (IsWithinTimeRange(takingTime, DateTime.Now, TimeSpan.FromMinutes(30)))
                            {
                                // Send a reminder email

                                new TakingDetailBL(schedule.scheduleIdGS, takingTimeString);
                                SendReminderEmail(user.userEmailgs, takingTime, schedule.scheduleIdGS, user.userIdgs);
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
                if (!goneMedications.Equals("")) //checking if the user has medication that are gone
                {
                    new MedicationStorageBL(user.userIdgs, DateTime.Now.ToString("yyyy-MM-dd") , false); //making new event for medication storage that is gone
                    List<AttendantBL> attendants = user.GetAttendants();
                    foreach (AttendantBL attendant in attendants) //sending medication stock reminder for all of the attendatns of the user
                    {
                        SendMedicationStockReminder(attendant.attendantEmailGS, goneMedications, user.userIdgs, user.userNamegs, user.userLastNamegs);
                    }
                }
            }
        }
        /// <summary>
        /// checking if all the users took all of their medicine until now for today
        /// </summary>
        public static void CheckUserTakingMedicine()
        {
            UsersBL users = new UsersBL();
            List<UserBL> usersList = users.Users;
            foreach (UserBL user in usersList)
            {
                foreach (ScheduleBL schedule in user.GetSchedule())
                {
                    try
                    {
                        TakingDetailBL td = new TakingDetailBL(schedule.scheduleIdGS);

                        // Parse takingDate string to DateTime
                        DateTime takingDateTime = DateTime.ParseExact(td.TakingDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                        // Extract date part only
                        DateTime takingDate = takingDateTime.Date;


                        // Check if the current date is the same as takingDate
                        if (takingDate == DateTime.Today)
                        {
                            // Get the current time
                            DateTime currentTime = DateTime.Now;

                            // Check if the current time is later than takingDateTime
                            if (currentTime > takingDateTime && !td.IsTookStatus)
                            {
                                List<AttendantBL> attendantBLs = user.GetAttendants();
                                // Create a list to store tuples containing attendantIdGS and priority
                                List<(int attendantId, int priority)> attendantInfo = new List<(int attendantId, int priority)>();

                                // Iterate through each AttendantBL object
                                foreach (AttendantBL attendant in attendantBLs)
                                {
                                    // Get the priority for the current user
                                    int priority = attendant.GetPriority(user.userIdgs);

                                    // Add the attendantIdGS and priority to the list
                                    attendantInfo.Add((attendant.attendantIdGS, priority));
                                }

                                // Order the list based on priority
                                attendantInfo.Sort((x, y) => x.priority.CompareTo(y.priority));

                                // Extract only the attendantIdGS into a list
                                List<int> sortedAttendantIds = attendantInfo.Select(attendant => attendant.attendantId).ToList();
                                TakingDetailLogBL takingDetailLogBL;
                                try
                                {
                                    takingDetailLogBL = new TakingDetailLogBL(td.TakingDetailId);
                                }
                                catch (Exception ex)
                                {
                                    //the TakingDetailLogBL still does not exists so we make one
                                    takingDetailLogBL = new TakingDetailLogBL(td.TakingDetailId, 0);
                                }

                                //getting the first selected attendants in the user priority
                                //checking if the count of the sent is already bigger then the amount of the attendats
                                int chosenAttendant;
                                if (takingDetailLogBL.NumberOfSent + 1 > sortedAttendantIds.Count)
                                {
                                    takingDetailLogBL.ResetNumberOfSent(); //making the number of sent zero again to send to the first priority attendant
                                    chosenAttendant = sortedAttendantIds[0];
                                }
                                else
                                {
                                    chosenAttendant = sortedAttendantIds[takingDetailLogBL.NumberOfSent];
                                    takingDetailLogBL.ChangeNumberOfSent();
                                }

                                // Find the AttendantBL object with the chosenAttendant ID

                                AttendantBL chosenAttendantBL = attendantBLs.Find(attendant => attendant.attendantIdGS == chosenAttendant);


                                SendAttendantMedicationReminderEmail(chosenAttendantBL.attendantEmailGS, takingDateTime, takingDetailLogBL.TakingDetailId, user.userNamegs);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // The taking detail does not exist or still does not exist
                    }
                }
            }
        }
        /// <summary>
        /// sending that the stock quest have been taken by an attendant
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="claimerEmail"></param>
        /// <param name="date"></param>
        public static void SendMessageIsClaimed(string attendantEmail, string claimerEmail, string date , string userName)
        {
            string recipientEmail = attendantEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", attendantEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"The medication stock duty for {date} of the patient {userName} has been taken by {claimerEmail}, there is no need to buy yourself.";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        /// <summary>
        /// sending medication stock reminder to attendant
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="goneMedications"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="userLastName"></param>
        private static void SendMedicationStockReminder(string attendantEmail, string goneMedications, int userId, string userName, string userLastName)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            string resetToken = Guid.NewGuid().ToString();
            string recipientEmail = attendantEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", attendantEmail);
            message.Subject = "Medication Stock Reminder";
            message.Body = $"The following medication for your patient {userName} {userLastName} are gone, please resupply them. \nMedications: {goneMedications}\n" +
                $"If you saw this email please confirm by clicking on this link: http://localhost:51422/attendant_confirmation_page.aspx?attendant={recipientEmail}&type=stock&user={userId}&token={resetToken}&date={currentDate}";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        /// <summary>
        /// sending email to user to take hes medicine
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="takingTime"></param>
        /// <param name="scheduleId"></param>
        /// <param name="userId"></param>
        private static void SendReminderEmail(string userEmail, DateTime takingTime, int scheduleId, int userId)
        {
            string resetToken = Guid.NewGuid().ToString();
            string recipientEmail = userEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", userEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"It's time to take your medication at {takingTime.ToString("HH:mm")}. Don't forget! \n" +
                $"If you saw this email please confirm by clicking on this link: http://localhost:51422/medication_taking.aspx?userId={userId}&type=medication&takingDate={takingTime.ToString("HH:mm")}&schedule={scheduleId}&token={resetToken}";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        /// <summary>
        /// sending email to attendant to remind him that his patient did not took his medicine
        /// </summary>
        /// <param name="attendantEmail"></param>
        /// <param name="takingTime"></param>
        /// <param name="takingdetaillogId"></param>
        /// <param name="name"></param>
        private static void SendAttendantMedicationReminderEmail(string attendantEmail, DateTime takingTime, int takingdetailId, string name)
        {
            string resetToken = Guid.NewGuid().ToString();//making a token for uniuqe site
            string recipientEmail = attendantEmail;
            string senderEmail = "assistpillwebservice@gmail.com";
            string senderPassword = "zecq zbvq jocp hgwi";
            MailMessage message = new MailMessage("your_email@example.com", attendantEmail);
            message.Subject = "Medication Reminder";
            message.Body = $"Your patient {name} has missed his medicine at {takingTime.ToString("HH:mm")}. Please remind him now! if you didn't see this after a time this will pass to the next attendant of the patient. \n" +
                $"If you saw this email please confirm by clicking on this link: http://localhost:51422/attendant_confirmation_page.aspx?&type=medication&takingdetailId={takingdetailId}&token={resetToken}";
            EmailSend(senderEmail, recipientEmail, senderPassword, message);
        }
        /// <summary>
        /// sending email with smtp
        /// </summary>
        /// <param name="senderEmail"></param>
        /// <param name="recipientEmail"></param>
        /// <param name="senderPassword"></param>
        /// <param name="message"></param>
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
        /// <summary>
        /// checking if time now is withing the time range that you give it
        /// </summary>
        /// <param name="targetTime"></param>
        /// <param name="currentTime"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        private static bool IsWithinTimeRange(DateTime targetTime, DateTime currentTime, TimeSpan range)
        {
            DateTime normalizedCurrentTime = NormalizeTime(currentTime);
            DateTime normalizedTargetTime = NormalizeTime(targetTime);

            DateTime lowerBound = normalizedTargetTime.Subtract(range);

            bool flag = normalizedCurrentTime >= lowerBound && normalizedCurrentTime <= normalizedTargetTime;
            return flag;

        }
        // This helper method sets the date part of a DateTime object to 0001-01-01, keeping only the time component.
        private static DateTime NormalizeTime(DateTime dateTime)
        {
            return new DateTime(1, 1, 1, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }
    }
}