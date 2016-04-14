using Csla;
using Csla.Data;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

namespace Business_Layer
{
    [Serializable]
    public class PatientList : BusinessListBase<PatientList, PatientEdit>
    {
                   #region Factory methods
        internal static PatientList NewPatientList()
        {
            return new PatientList();
        }
        #endregion

        public List<PatientEdit> GetPatientList()
        {
         List<PatientEdit>   GetPatientList = new List<PatientEdit>();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString))
            {
                conn.Open();

                // create a command to read the data from the table
                using (var command = new SqlCommand())
                {
                    command.CommandTimeout = 30;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectPatientList";
                    command.Connection = conn;

                    // execute the data reader
                    using (var reader = new SafeDataReader(command.ExecuteReader()))
                    {
                        // PatientEdit
                        while (reader.Read()) // Get PatientEdit table info
                        {
                            GetPatientList.Add(new PatientEdit
                            {
                              FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            Id = reader.GetGuid(reader.GetOrdinal("PxID")),  
                            });
                            }
                         }
                }
                conn.Close();
            }
            
            return GetPatientList;
        }

        //protected void DataPortal_Fetch()
        //{
        //          using (var conn = new SqlConnection(ConfigurationManager.AppSettings["LocalDBConnection"]))
        //    {
        //        conn.Open();

        //        // create a command to read the data from the table
        //        using (var command = new SqlCommand())
        //        {
        //            command.CommandTimeout = 30;
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = "SelectPatientList";
        //            command.Connection = conn;
                    
        //            // execute the data reader
        //            using (var reader = new SafeDataReader(command.ExecuteReader()))
        //            {
        //                // PatientEdit
        //                while (reader.Read()) // Get PatientEdit table info
        //                {
        //                    FirstName = reader.GetString("FirstName");
        //                    LastName = reader.GetString(reader.GetOrdinal("LastName"));
        //                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
        //                    Id = reader.GetGuid(reader.GetOrdinal("PxID"));
        //                }
        //                reader.NextResult();
        //                               }
        //        }
        //                 }
        //}


    }
}

