using System.Data;

namespace FinalProjectDAL
{
    public class MedicationClass
    {
        /// <summary>
        /// getting all the Medications from the data base
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMedications()
        {
            string sSql = $@"Select MedicationId, (MedicationName) , (MedicationDescription) , (MedicationInstructions) , (MedicationAmount) , (MedicationPhoto) , (IsRemoved) from MedicationTBL";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// getting specifiec medication from the database
        /// </summary>
        /// <param name="medicationId"></param>
        /// <returns></returns>
        public static DataTable GetSpecifiecMedication(int medicationId)
        {
            string sSql = $@"SELECT MedicationId, (MedicationName) , (MedicationDescription) , (MedicationInstructions) , (MedicationAmount), (MedicationPhoto) from MedicationTBL WHERE MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);
            return dt;
        }
        /// <summary>
        /// checking if the medication name exists already
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationName"></param>
        /// <returns></returns>
        public static bool IsMedicationNameExists(int medicationId, string medicationName)
        {
            string sSql = $@"SELECT * from MedicationTBL WHERE MedicationTBL.[MedicationName] = '{medicationName}' AND MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the medication description exists already
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationDescription"></param>
        /// <returns></returns>
        public static bool IsMedicationDescriptionExists(int medicationId, string medicationDescription)
        {
            string sSql = $@"SELECT * from MedicationTBL WHERE MedicationTBL.[MedicationDescription] = '{medicationDescription}' AND MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the medication instruction exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationInstruction"></param>
        /// <returns></returns>
        public static bool IsMedicationInstructionExists(int medicationId, string medicationInstruction)
        {
            string sSql = $@"SELECT * from MedicationTBL WHERE MedicationTBL.[MedicationInstruction] = '{medicationInstruction}' AND MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] =  {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the medication amount exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationAmount"></param>
        /// <returns></returns>
        public static bool IsMedicationAmountExists(int medicationId, int medicationAmount)
        {
            string sSql = $@"SELECT * from MedicationTBL WHERE MedicationTBL.[MedicationAmount] = {medicationAmount} AND MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the medication photo exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="medicationPhoto"></param>
        /// <returns></returns>
        public static bool IsMedicationPhotoExists(int medicationId, string medicationPhoto)
        {
            string sSql = $@"SELECT * from MedicationTBL WHERE MedicationTBL.[MedicationPhoto] = '{medicationPhoto}' AND MedicationTBL.[MedicationId] = {medicationId} AND MedicationTBL.[IsRemoved] =  {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// checking if the medication params already exists together
        /// </summary>
        /// <param name="medicationName"></param>
        /// <param name="medicationDescription"></param>
        /// <param name="medicationInstructions"></param>
        /// <param name="medicationAmount"></param>
        /// <param name="medicationPhoto"></param>
        /// <returns></returns>
        public static bool IsMedicationExists(string medicationName, string medicationDescription, string medicationInstructions, int medicationAmount, string medicationPhoto)
        {
            string sSql = $@"Select * from MedicationTBL WHERE MedicationTBL.MedicationName = '{medicationName}' AND MedicationTBL.MedicationDescription = '{medicationDescription}' AND MedicationTBL.MedicationInstructions = '{medicationInstructions}' AND MedicationTBL.MedicationAmount = {medicationAmount} AND MedicationTBL.MedicationPhoto = '{medicationPhoto}' AND MedicationTBL.IsRemoved = {false}";
            DataTable dt = DBHelper.GetDataTable(sSql);

            return dt.Rows.Count > 0;
        }
        /// <summary>
        /// inserting new medication to the database
        /// checking if the medication params already exists together
        /// </summary>
        /// <param name="medicationName"></param>
        /// <param name="medicationDescription"></param>
        /// <param name="medicationInstructions"></param>
        /// <param name="medicationAmount"></param>
        /// <param name="medicationPhoto"></param>
        public static void InsertMedication(string medicationName, string medicationDescription, string medicationInstructions, int medicationAmount, string medicationPhoto)
        {
            string gSql = $@"INSERT INTO MedicationTBL (medicationName, medicationDescription, medicationInstructions, medicationAmount, medicationPhoto) VALUES ('{medicationName}','{medicationDescription}','{medicationInstructions}',{medicationAmount} , '{medicationPhoto}')";
            DBHelper.ExecuteNonQuery(gSql);
        }
        /// <summary>
        /// updating medication name
        /// checking if medication name dont exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="newMedicationName"></param>
        public static void UpdateMedicationName(int medicationId, string newMedicationName)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.MedicationName = '{newMedicationName}' WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// update medication description
        /// checking if the medication description dont exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="newMedicationDescription"></param>
        public static void UpdateMedicationDescription(int medicationId, string newMedicationDescription)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.MedicationDescription = '{newMedicationDescription}' WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// update medication instructions
        /// checking if the instruction dont exits
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="newMedicationInstructions"></param>
        public static void UpdateMedicationInstructions(int medicationId, string newMedicationInstructions)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.MedicationInstructions = '{newMedicationInstructions}' WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updates medication amount
        /// checking if the amount dont exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="newMedicationAmount"></param>
        public static void UpdateMedicationAmount(int medicationId, int newMedicationAmount)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.MedicationAmount = {newMedicationAmount} WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// updates medication photo
        /// checking if the medication photo not exists
        /// </summary>
        /// <param name="medicationId"></param>
        /// <param name="newMedicationPhoto"></param>
        public static void UpdateMedicationPhoto(int medicationId, string newMedicationPhoto)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.MedicationPhoto = '{newMedicationPhoto}' WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }
        /// <summary>
        /// removing the medication from the system but keeping in the database 
        /// </summary>
        /// <param name="medicationId"></param>
        public static void RemoveMedication(int medicationId)
        {
            string sSql = $@"UPDATE MedicationTBL SET MedicationTBL.IsRemoved = {true} WHERE MedicationTBL.MedicationId = {medicationId}";
            DBHelper.ExecuteNonQuery(sSql);
        }

    }
}
