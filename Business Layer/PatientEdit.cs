using Csla;
using Csla.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Business_Layer
{
    [Serializable]
    public class PatientEdit : BusinessBase<PatientEdit>
    {
        #region Properties

        public static readonly PropertyInfo<Guid> IDProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IDProperty); }
             set { SetProperty(IDProperty, value); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public string FirstName
        {
            get
            {
                return GetProperty(FirstNameProperty);
            }
            set
            {
                SetProperty(FirstNameProperty, value);
            }
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set
            {
                SetProperty(LastNameProperty, value);
            }
        }

        public static readonly PropertyInfo<DateTime> DateOfBirthProperty = RegisterProperty<DateTime>(c => c.DateOfBirth);
        public DateTime DateOfBirth
        {
            get { return GetProperty(DateOfBirthProperty); }
            set
            {
                SetProperty(DateOfBirthProperty, value);
            }
        }

        public static readonly PropertyInfo<Addresses> AddressListProperty = RegisterProperty<Addresses>(c => c.AddressList);
        public Addresses AddressList
        {
            get { return GetProperty(AddressListProperty); }
            set
            {
                SetProperty(AddressListProperty, value);
            }
        }

        public override bool IsDirty
        {
            get
            {
                return base.IsDirty || AddressList.IsDirty;
            }
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && AddressList.IsValid;
            }
        }

        #endregion

               #region Business Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FirstNameProperty, "First name is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(FirstNameProperty, 50, "Name cannot be longer than 50 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(LastNameProperty, "Last name is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(LastNameProperty, 50, "Name cannot be longer than 50 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(DateOfBirthProperty, "Date of birth is required"));
        }

        #endregion

        #region Shared Methods

        public static PatientEdit GetPatientEdit(Guid id)
        {
            return DataPortal.Fetch<PatientEdit>(new Criteria { Id = id });
        }

        public static PatientEdit NewPatientEdit()
        {
            return DataPortal.Create<PatientEdit>(new Criteria());
        }

        #endregion

        #region Criteria

        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public Guid Id { get; set; }
        }

        #endregion

        #region Data Access

        protected override void DataPortal_Create()
        {
            Id = Guid.NewGuid();

            MarkNew();
        }

        protected void DataPortal_Fetch(Criteria criteria)
        {
            var crit = (Criteria)criteria;

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString))
            {
                conn.Open();

                // create a command to read the data from the table
                using (var command = new SqlCommand())
                {
                    command.CommandTimeout = 30;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectPatientByPxID";
                    command.Connection = conn;

                    var paramlist = new SqlParameter[1];
                    paramlist[0] = new SqlParameter("@PxID", SqlDbType.UniqueIdentifier) { Value = crit.Id };

                    command.Parameters.AddRange(paramlist);

                    // execute the data reader
                    using (var reader = new SafeDataReader(command.ExecuteReader()))
                    {
                        // PatientEdit
                        if (reader.Read()) // Get PatientEdit table info
                        {
                            FirstName = reader.GetString("FirstName");
                            LastName = reader.GetString(reader.GetOrdinal("LastName"));
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                            Id = reader.GetGuid(reader.GetOrdinal("PxID"));
                        }
                        reader.NextResult();
                        AddressList = Addresses.GetAddresses(reader);                                
                    }
                }
                conn.Close();
                MarkOld();
            }
        }

        protected override void DataPortal_Update()
        {
            Update();
        }

        protected override void DataPortal_Insert()
        {
            Update();
        }

        private void Update()
        {
            // create the connection
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalDBConnection"].ConnectionString))
            {
                // open it
                conn.Open();

                // create the transaction
                using (SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    try
                    {
                        // create a command to read the data from the table
                        using (var command = new SqlCommand())
                        {
                            command.CommandTimeout = 30;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = IsNew ? "InsertPatient" : "UpdatePatient";
                            command.Connection = conn;
                            command.Transaction = tran;
                            command.Parameters.AddRange(IsNew ? GetInsertParams() : GetUpdateParams());
                            // command.ExecuteNonQuery();
                            foreach (AddressEdit adrs in AddressList)
                            {
                                if (adrs.IsDirty)
                                { adrs.Update(tran); };
                            }
                                                       
                            command.ExecuteNonQuery();

                            tran.Commit();
                        }
                           
                        MarkOld();
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();

                        throw;
                    }
                    finally
                    {if(conn.State != ConnectionState.Closed) { conn.Close(); } }
                    
                }
            }
        }

        #region GetParams

        /// <summary>
        /// Insert specific parameters
        /// </summary>
        /// <returns></returns>
        private SqlParameter[] GetInsertParams()
        {
            var result = GetParams();
            return result.ToArray();
        }

        /// <summary>
        /// Update specific parameters
        /// </summary>
        /// <returns></returns>
        private SqlParameter[] GetUpdateParams()
        {
            var result = GetParams();
            return result.ToArray();
        }

        private List<SqlParameter> GetParams()
        {
            var Params = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@PxID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = Id
                },
                new SqlParameter
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = FirstName
                },
                new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.VarChar,
                    Value = LastName
                },
                 new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    SqlDbType = SqlDbType.Date,
                    Value = DateOfBirth
                }, };

                         return Params;
        }

        #endregion

        #endregion
    }
}
