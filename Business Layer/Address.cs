using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    [Serializable]
    public class Address : BusinessBase<Address>
    {
        #region Properties

        public static readonly PropertyInfo<string> phoneNumber = RegisterProperty(typeof(Address), new PropertyInfo<string>("Telephone number"));
        public string PhoneNumber
        {
            get
            {
                return GetProperty(phoneNumber);
            }
            set
            {
                SetProperty(phoneNumber, value);
            }
        }

        public static readonly PropertyInfo<string> addressLine1 = RegisterProperty(typeof(Address), new PropertyInfo<string>("Address Line 1"));
        public string AddressLine1
        {
            get
            {
                return GetProperty(addressLine1);
            }
            set
            {
                SetProperty(addressLine1, value);
            }
        }

        public static readonly PropertyInfo<string> postCode = RegisterProperty(typeof(Address), new PropertyInfo<string>("Postcode"));
        public string PostCode
        {
            get
            {
                return GetProperty(postCode);
            }
            set
            {
                SetProperty(postCode, value);
            }
        }

        public static readonly PropertyInfo<string> emailAddress = RegisterProperty(typeof(Address), new PropertyInfo<string>("Email Address"));
        public string EmailAddress
        {
            get
            {
                return GetProperty(emailAddress);
            }
            set
            {
                SetProperty(emailAddress, value);
            }
        }
        #endregion

        #region Validation Rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(phoneNumber, "Phone number is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(phoneNumber, 15, "Phone number cannot be longer than 15 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(addressLine1, "Address is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(addressLine1, 100, "Address cannot be longer than 100 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(postCode, "Postcode is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(postCode, 10, "Postcode cannot be longer than 10 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(emailAddress, "Email address is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(emailAddress, 100, "Email address cannot be longer than 100 characters"));
        }
        #endregion

        #region Factory methods
        internal static Address NewEditableChild()
        {
            return DataPortal.CreateChild<Address>();
        }
              #endregion
            }
}
