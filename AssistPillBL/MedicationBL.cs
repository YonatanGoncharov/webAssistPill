using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class MedicationBL
    {
        private int medicationid; //the medication id
        private string medicationName;//the medication name
        private string medicationDescription;//the medication descrption
        private string medicationInstructions;//the medication instructions
        private int medicationAmount;//the medication amount
        private string medicationPhotoPath;//the medication photo path
        private int userId; // the id of the user

        public int Medicationid { get => medicationid; set => medicationid = value; }//medication id get/set
        public string MedicationName { get => medicationName; set => medicationName = value; }//medication name get/set
        public string MedicationDescription { get => medicationDescription; set => medicationDescription = value; }//medication description get/set
        public string MedicationInstructions { get => medicationInstructions; set => medicationInstructions = value; }//medication instructions get/set
        public int MedicationAmount { get => medicationAmount; set => medicationAmount = value; }//medicaion amount get/set
        public string MedicationPhotoPath { get => medicationPhotoPath; set => medicationPhotoPath = value; }//medication photo path get/set
        public int UserId { get => userId; set => userId = value; }//userId get/set

        //builder for new medication
        public MedicationBL(string medicationName, string medicationDescription, string medicationInstructions, int medicationAmount, string medicationPhotoPath , int userId)
        {
            this.medicationName = medicationName;
            this.medicationDescription = medicationDescription;
            this.medicationInstructions = medicationInstructions;
            this.medicationAmount = medicationAmount;
            this.medicationPhotoPath = medicationPhotoPath;
            this.userId = userId;

            MedicationClass.InsertMedication(medicationName, medicationDescription, medicationInstructions, medicationAmount, medicationPhotoPath , userId);
            DataTable dt = MedicationClass.GetMedicationIdByUserIdAndMedName(userId, medicationName);
            DataRow dr = dt.Rows[0];
            this.medicationid = (int)dr[0];
        }
        //builder for existing medication
        public MedicationBL(int medicationid)
        {
            DataTable dt = MedicationClass.GetSpecifiecMedication(medicationid);
            DataRow dr = dt.Rows[0];
            this.medicationid = medicationid;
            this.medicationName = dr[1].ToString();
            this.medicationDescription = dr[2].ToString();
            this.medicationInstructions = dr[3].ToString();
            this.medicationAmount = (int)dr[4];
            this.medicationPhotoPath = dr[5].ToString();
            this.userId = (int)dr[6];
        }

        //getting all the user medications
        public static List<MedicationBL> GetUserMedications(int userId)
        {
            DataTable dt = MedicationClass.GetMedicationByUserId(userId);
            List<MedicationBL> medications = new List<MedicationBL>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                MedicationBL medication = new MedicationBL((int)dr[0]);
                medications.Add(medication);
            }
            return medications;
        }
        //removing specifiec medication
        public void MedicationRemove()
        {
            MedicationClass.RemoveMedication(this.medicationid);
        }
        /// <summary>
        /// Update for name
        /// </summary>
        /// <param name="name"></param>
        public void UpdateName(string name)
        {
            this.medicationName = name;
            MedicationClass.UpdateMedicationName(this.medicationid, name);
        }
        /// <summary>
        /// Update for instructions
        /// </summary>
        /// <param name="instructions"></param>
        public void UpdateInstructions(string instructions)
        {
            this.medicationInstructions = instructions;
            MedicationClass.UpdateMedicationInstructions(this.medicationid, instructions);
        }
        /// <summary>
        /// Update for description
        /// </summary>
        /// <param name="description"></param>
        public void UpdateDescription(string description)
        {
            this.medicationDescription = description;
            MedicationClass.UpdateMedicationDescription(this.medicationid, description);
        }
        /// <summary>
        /// Update for amount
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateAmount(int amount)
        {
            this.medicationAmount = amount;
            MedicationClass.UpdateMedicationAmount(this.medicationid, amount);
        }
        /// <summary>
        /// Update for photo path
        /// </summary>
        /// <param name="photopath"></param>
        public void UpdatePhotoPath(string photopath)
        {
            this.medicationPhotoPath = photopath;
            MedicationClass.UpdateMedicationPhoto(this.medicationid, photopath);
        }

    }
}
