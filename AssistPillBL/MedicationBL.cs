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

        public int Medicationid { get => medicationid; set => medicationid = value; }//medication id get/set
        public string MedicationName { get => medicationName; set => medicationName = value; }//medication name get/set
        public string MedicationDescription { get => medicationDescription; set => medicationDescription = value; }//medication description get/set
        public string MedicationInstructions { get => medicationInstructions; set => medicationInstructions = value; }//medication instructions get/set
        public int MedicationAmount { get => medicationAmount; set => medicationAmount = value; }//medicaion amount get/set
        public string MedicationPhotoPath { get => medicationPhotoPath; set => medicationPhotoPath = value; }//medication photo path get/set

        //builder for new medication
        public MedicationBL(int medicationid, string medicationName, string medicationDescription, string medicationInstructions, int medicationAmount, string medicationPhotoPath)
        {
            this.medicationid = medicationid;
            this.medicationName = medicationName;
            this.medicationDescription = medicationDescription;
            this.medicationInstructions = medicationInstructions;
            this.medicationAmount = medicationAmount;
            this.medicationPhotoPath = medicationPhotoPath;

            MedicationClass.InsertMedication(medicationName, medicationDescription, medicationInstructions, medicationAmount, medicationPhotoPath);
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
        }


    }
}
