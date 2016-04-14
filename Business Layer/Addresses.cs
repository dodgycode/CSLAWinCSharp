using Csla;
using System;
using Csla.Data;

namespace Business_Layer
{
    [Serializable]
    public class Addresses : BusinessListBase<Addresses, AddressEdit>
    {
     
        #region Factory methods
        internal static Addresses NewAddressList()
        {
            return new Addresses();
        }
        #endregion

        #region Shared methods
        public static Addresses GetAddresses(SafeDataReader reader)
        {
          Addresses  GetAddresses = new Addresses();
         while (reader.Read())
            {
                GetAddresses.Add(AddressEdit.GetAddressEdit(reader));
                             }

            return GetAddresses;
                   }
        #endregion


    }
}

