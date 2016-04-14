using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DAL
{
    public class PatientDAL 
    {
        #region Fetching
        public List<DataClass.PatientClass> GetPatientList()
        {
            List<DataClass.PatientClass> result = new List<DataClass.PatientClass>();

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "SelectPatientList", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataClass.PatientClass pX = new DataClass.PatientClass();
                                pX.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                pX.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                pX.dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                                pX.pxID = reader.GetGuid(reader.GetOrdinal("PxID"));
                                result.Add(pX);
                            }
                        }
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return result;
          }

        public DataClass.PatientClass GetSinglePatient(Guid pxID)
        {
            DataClass.PatientClass result = new DataClass.PatientClass();

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "SelectPatientList", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", pxID);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Get patient table info
                            {
                                result.firstName = reader.GetString("FirstName");
                                result.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                result.dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                                result.pxID = reader.GetGuid(reader.GetOrdinal("PxID"));
                            }
                                                    reader.NextResult();

                            while (reader.Read())  // Get addresses for the patient
                            {
                                result.addressList.Add(new DataClass.AddressClass
                                {
                                    pxID  = reader.GetGuid(reader.GetOrdinal("PxID")),
                                    AddressID = reader.GetGuid(reader.GetOrdinal("AddressID")),
                                    addressLine1 =  reader.GetString(reader.GetOrdinal("AddressLine1")),
                                    postcode = reader.GetString(reader.GetOrdinal("Postcode")),
                                  phone = reader.GetString(reader.GetOrdinal("Phone")),
                                  email = reader.GetString(reader.GetOrdinal("Email"))
                                });
                            }
                        }
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return result;
        }

        public List<DataClass.AddressClass> GetAddresses()
        {
            List<DataClass.AddressClass> result = new List<DataClass.AddressClass>();

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "SelectAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", dfgshgf,)
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DataClass.PatientClass pX = new DataClass.PatientClass();
                                pX.firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                pX.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                                pX.dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                                pX.pxID = reader.GetGuid(reader.GetOrdinal("PxID"));
                                result.Add(pX);
                            }
                        }
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return result;
        }
        #endregion

        #region Inserting
        public bool InsertAddress(DataClass.AddressClass singleAddress)
        {
           bool success = false;
         
            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", singleAddress.pxID);
                    cmd.Parameters.AddWithValue("@AddressID", singleAddress.AddressID);
                    cmd.Parameters.AddWithValue("@AddressLine1", singleAddress.addressLine1);
                    cmd.Parameters.AddWithValue("@Postcode", singleAddress.postcode);
                    cmd.Parameters.AddWithValue("@Email", singleAddress.email);
                    cmd.Parameters.AddWithValue("@Phone", singleAddress.phone);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }

        public bool InsertPatientWithAddress(DataClass.PatientClass patient)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertPatientWithAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", patient.pxID);
                    cmd.Parameters.AddWithValue("@AddressID", patient.addressList.First().AddressID);
                    cmd.Parameters.AddWithValue("@AddressLine1", patient.addressList.First().addressLine1);
                    cmd.Parameters.AddWithValue("@Postcode", patient.addressList.First().postcode);
                    cmd.Parameters.AddWithValue("@Email", patient.addressList.First().email);
                    cmd.Parameters.AddWithValue("@Phone", patient.addressList.First().phone);
                    cmd.Parameters.AddWithValue("@FirstName", patient.firstName);
                    cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }

        public bool InsertPatient(DataClass.PatientClass patient)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertPatient", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", patient.pxID);
                                       cmd.Parameters.AddWithValue("@FirstName", patient.firstName);
                    cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }
        #endregion

        #region Updating
        public bool UpdateAddress(DataClass.AddressClass singleAddress)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", singleAddress.pxID);
                    cmd.Parameters.AddWithValue("@AddressID", singleAddress.AddressID);
                    cmd.Parameters.AddWithValue("@AddressLine1", singleAddress.addressLine1);
                    cmd.Parameters.AddWithValue("@Postcode", singleAddress.postcode);
                    cmd.Parameters.AddWithValue("@Email", singleAddress.email);
                    cmd.Parameters.AddWithValue("@Phone", singleAddress.phone);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }

        public bool UpdatePatientWithAddress(DataClass.PatientClass patient)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertPatientWithAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", patient.pxID);
                    cmd.Parameters.AddWithValue("@AddressID", patient.addressList.First().AddressID);
                    cmd.Parameters.AddWithValue("@AddressLine1", patient.addressList.First().addressLine1);
                    cmd.Parameters.AddWithValue("@Postcode", patient.addressList.First().postcode);
                    cmd.Parameters.AddWithValue("@Email", patient.addressList.First().email);
                    cmd.Parameters.AddWithValue("@Phone", patient.addressList.First().phone);
                    cmd.Parameters.AddWithValue("@FirstName", patient.firstName);
                    cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }

        public bool UpdatePatient(DataClass.PatientClass patient)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "InsertPatient", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", patient.pxID);
                    cmd.Parameters.AddWithValue("@FirstName", patient.firstName);
                    cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                    cmd.Parameters.AddWithValue("@DateOfBirth", patient.dateOfBirth);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }
        #endregion

        #region Deleting
        public bool DeletePatientsAddresses(DataClass.AddressClass singleAddress)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "DeletePatientAddress", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", singleAddress.pxID);
                                       try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }

        public bool DeletePatient(DataClass.PatientClass patient)
        {
            bool success = false;

            using (SqlConnection conn = new SqlConnection(CommonFunctions.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand { CommandText = "DeletePatient", CommandType = System.Data.CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@PxID", patient.pxID);
                                       try
                    {
                        if (conn.State != System.Data.ConnectionState.Open) { conn.Open(); };
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated > 0) { success = true; };
                    }
                    finally
                    {
                        if (conn.State != System.Data.ConnectionState.Closed) { conn.Close(); };
                    }
                }
            }

            return success;
        }
                       #endregion
            }

}
