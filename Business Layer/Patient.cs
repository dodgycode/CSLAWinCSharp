using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;
using Csla.Rules;
using System.Reflection;
using Csla.Core;

namespace Business_Layer
{
    #region Patient
        [Serializable]
    public class Patient : BusinessBase<Patient>
    {
        public static readonly PropertyInfo<string> firstName = RegisterProperty(typeof(Patient),new PropertyInfo<string>("First Name"));
               public string FirstName
        {
            get
            {
            return GetProperty(firstName);
            }
            set 
            {
                SetProperty(firstName, value);
                           }
         }

        public static readonly PropertyInfo<string> lastName = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Last Name"));
        public string LastName
        {
            get { return GetProperty(lastName); }
            set {
                SetProperty(lastName, value);
                    }
                    }
        public static readonly PropertyInfo<DateTime>  dateOfBirth = RegisterProperty(typeof(Patient), new PropertyInfo<DateTime>("Date Of Birth"));
        public DateTime DateOfBirth
        {
            get { return GetProperty(dateOfBirth); }
            set
            {
                SetProperty(dateOfBirth, value);
            }
        }

        public static readonly PropertyInfo<Addresses> addressList = RegisterProperty(typeof(Patient), new PropertyInfo<Addresses>("Addresses"));
        public Addresses AddressList
        {
            get { return GetProperty(addressList); }
            set
            {
                SetProperty(addressList, value);
            }
        }

        //public static readonly PropertyInfo<Orders> orderList = RegisterProperty(typeof(Patient), new PropertyInfo<Orders>("Orders"));
        //public Orders OrderList
        //{
        //    get { return GetProperty(orderList); }
        //    set
        //    {
        //        SetProperty(orderList, value);
        //    }
        //}

        public override bool IsDirty
        {
            get
            {
                return base.IsDirty || AddressList.IsDirty; //|| OrderList.IsDirty;
            }
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && AddressList.IsValid;//&& OrderList.IsValid;
            }
        }
             

        #region Rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(firstName,"First name is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(firstName, 50, "Name cannot be longer than 50 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(lastName, "Last name is required"));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.MaxLength(lastName, 50, "Name cannot be longer than 50 characters"));

            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(dateOfBirth, "Date of birth is required"));
                    }
        #endregion


    }

}
