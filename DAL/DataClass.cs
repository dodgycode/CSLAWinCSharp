using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataClass
    {
        public class PatientClass
        {
            public string firstName, LastName;
            public DateTime dateOfBirth;
            public Guid pxID;
            public List<AddressClass> addressList = new List<AddressClass>();
        }

        public class AddressClass
        {
            public Guid pxID, AddressID;
            public string addressLine1, postcode, email, phone;
        }
    }
}
