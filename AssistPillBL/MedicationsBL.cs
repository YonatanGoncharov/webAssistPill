using FinalProjectDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistPillBL
{
    public class MedicationsBL
    {
        List<MedicationBL> medications;
        /// <summary>
        /// builder for all the medications with the same name
        /// </summary>
        /// <param name="medName"></param>
        public MedicationsBL(string medName)
        {
            this.medications = new List<MedicationBL>();
            DataTable dt = MedicationClass.GetMedicationByName(medName);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                 int medicationid = (int)dr[0]; //the medication id
                this.medications.Add(new MedicationBL(medicationid));
            }
        }

        public List<MedicationBL> medicationsGS { get => medications; set => medications = value; }//get/set for the medications
    }
}
