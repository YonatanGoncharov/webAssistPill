using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FinalProjectDAL;


namespace FinalProjectDAL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTable user;
            //user = UserClass.GetAllUsers();
            //user = UserClass.GetSpecifiecUser("dolev@gmail.com");
            //UserClass.InsertUser("dat", "gasa", "gasv1@gemail.gas", "ba132");
            //UserClass.UpdatePassword("dolcev@gmail.cok", "aba123");
            //UserClass.UpdateEmail("dolcev@gmail.cok", "dolev@gmail.com");
            //user = UserClass.GetWeeklySchedule(1);
            //UserClass.RemoveUser("dolev@gmail.com");
            DataTable attendant;
            //attendant = AttendantClass.GetAllAttendants();
            //AttendantClass.InsertAttendant("david", "bold", "davidbold@gmail.com", "ez1xz");
            //AttendantClass.UpdatePassword("davidbold@gmail.com", "eax!");
            //AttendantClass.UpdateEmail("davidbold@gmail.com", "davidbold@yahoo.com");
            //AttendantClass.UpdatePriorety("davidbold@gmail.com", 4);
            //AttendantClass.UpdateUserId("davidbold@gmail.com", 3);
            //attendant = AttendantClass.GetAttendantPassword("davidbold@gmail.com");
            //attendant = AttendantClass.GetSpecifiecAttendant("davidbold@gmail.com");
            //attendant = AttendantClass.GetWeeklySchedule(1);
            //AttendantClass.RemoveAttendant("davidbold@gmail.com");
            DataTable medication;
            //medication = MedicationClass.GetAllMedications();
            //medication = MedicationClass.GetSpecifiecMedication(1);
            //MedicationClass.InsertMedication("goldmer", "good for helping drink","take after launch", 5, "medication4.png");
            //MedicationClass.UpdateMedicationDescription(3, "good for something");
            //MedicationClass.UpdateMedicationInstructions(1, "eat before launch");
            //MedicationClass.UpdateMedicationName(1, "advil");
            //MedicationClass.UpdateMedicationPhoto(1, "good for eating");
            //MedicationClass.UpdateMedicationAmount(1, 1);
            //MedicationClass.RemoveMedication(1);
            DataTable schedule;
            //schedule = ScheduleClass.GetAllSchedules();
            //schedule = ScheduleClass.GetSpecifiecSchedule(1);
            //ScheduleClass.InsertSchedule(4, 5, "6am", 3);
            //ScheduleClass.UpdateDayOfWeek(11, 3);
            //ScheduleClass.UpdateMedicationId(1, 7);
            //ScheduleClass.UpdateUserId(1, 8);
            //ScheduleClass.UpdateTakingTime(11, "2am");
            //ScheduleClass.InsertScheduleTakingDetail(11, "4am");
            //ScheduleClass.ScheduleTakingDetailStatus(11);
            //schedule = ScheduleClass.GetMedicationTakingDetail(11);
            //ScheduleClass.RemoveSchedule(11);
            //ScheduleClass.RemoveScheduleTakingDetail(11);
            DataTable messages;
            //messages = MessagesClass.GetAllMessages();
            //MessagesClass.InsertMessage(1, 1, "#2019/05/05#", "hello how are you");
            //messages = MessagesClass.GetAllAttendantMessages(1);
            //MessagesClass.RemoveMessage(1);
            //MessagesClass.SeenMessage(1);
            //Console.WriteLine(passwordMatch);
            //PrintTable(attendant);
            Console.ReadKey();
        }
        public static void PrintTable(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                    Console.Write(dt.Rows[i][j] + "\t");
                Console.WriteLine();
            }
        }
    }
}
