using Csla;
using Csla.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Business_Layer
{
    [Serializable]
    public class AddressEdit : BusinessBase<AddressEdit>
    {
        #region Properties

        public static readonly PropertyInfo<Guid> IDProperty = RegisterProperty<Guid>(c => c.Id);
        public Guid Id
        {
            get { return GetProperty(IDProperty); }
             set { SetProperty(IDProperty, value); }
        }

        public static readonly PropertyInfo<Guid> PxIDProperty = RegisterProperty<Guid>(c => c.PxID);
        public Guid PxID
        {
            get
            {
                return GetProperty(PxIDProperty);
            }
            set
            {
                LoadProperty(PxIDProperty, value);
            }
        }

        public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(c => c.PhoneNumber);
        public string PhoneNumber
        {
            get
            {
                return GetProperty(PhoneNumberProperty);
            }
            set
            {
                SetProperty(PhoneNumberProperty, value);
            }
        }

        public static readonly PropertyInfo<string> AddressLine1Property = RegisterProperty<string>(c => c.AddressLine1);
        public string AddressLine1
        {
            get
            {
                return GetProperty(AddressLine1Property);
            }
            set
            {
                SetProperty(AddressLine1Property, value);
            }
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(c => c.PostCode);
        public string PostCode
        {
            get
            {
                return GetProperty(PostCodeProperty);
            }
            set
            {
                SetProperty(PostCodeProperty, value);
            }
        }

        public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);
        public string EmailAddress
        {
            get
            {
                return GetProperty(EmailAddressProperty);
            }
            set
            {
                SetProperty(EmailAddressProperty, value);
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty;
            }
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid;
            }
        }

        #endregion

        #region Constructor
        
        /// <summary>
        ///     public constructor
        /// </summary>
        public AddressEdit()
        {
            MarkAsChild();
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        #endregion

        #region Shared Methods

        public static AddressEdit GetAddressEdit(SafeDataReader reader)
        {
            var address = new AddressEdit();

            address.PxID = reader.GetGuid(reader.GetOrdinal("PxID"));
            address.Id = reader.GetGuid(reader.GetOrdinal("AddressID"));
            address.AddressLine1 = reader.GetString(reader.GetOrdinal("AddressLine1"));

            address.PostCode = reader.GetString(reader.GetOrdinal("Postcode"));
            address.PhoneNumber = reader.GetString(reader.GetOrdinal("Phone"));
            address.EmailAddress = reader.GetString(reader.GetOrdinal("Email"));

                      return address;     
            }
            
       
        public static AddressEdit NewAddressEdit()
        {
            return DataPortal.Create<AddressEdit>(new Criteria());
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

              public void Update(SqlTransaction tran)
        {
                                                    // create a command to read the data from the table
                        using (var command = new SqlCommand())
                        {
                            command.CommandTimeout = 30;
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = IsNew ? "InsertAddress" : "UpdateAddress";
                            command.Connection = tran.Connection;
                            command.Transaction = tran;
                            command.Parameters.AddRange(IsNew ? GetInsertParams() : GetUpdateParams());
                            command.ExecuteNonQuery();
                        }
                                             MarkOld();
                                  }

        #endregion
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
                    ParameterName = "@AddressID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = Id
                },
                 new SqlParameter
                {
                    ParameterName = "@PxID",
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    Value = PxID
                },
                  new SqlParameter
                {
                    ParameterName = "@AddressLine1",
                    SqlDbType = SqlDbType.VarChar,
                    Value = AddressLine1
                },
                   new SqlParameter
                {
                    ParameterName = "@Postcode",
                    SqlDbType = SqlDbType.VarChar,
                    Value = PostCode
                },
                    new SqlParameter
                {
                    ParameterName = "@Email",
                    SqlDbType = SqlDbType.VarChar,
                    Value = EmailAddress
                },
                     new SqlParameter
                {
                    ParameterName = "@Phone",
                    SqlDbType = SqlDbType.VarChar,
                    Value = PhoneNumber
                },
            };
            return Params;
        }

        #endregion
    }
}
