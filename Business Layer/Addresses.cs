using Csla;
using Csla.Rules;
using Csla.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    [Serializable]
    public class Addresses : BusinessListBase<Addresses, Address>
    {
        #region Factory methods
        internal static Addresses NewAddressList()
        {
            return DataPortal.CreateChild<Addresses>();
        }
        internal static Address GetEditableChildList( object childData)
        {
            return DataPortal.FetchChild<Address>(childData);
        }
        #endregion
               
            }
    }

