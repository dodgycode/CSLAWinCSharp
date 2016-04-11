using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer
{
    [Serializable]
    public class Orders : BusinessListBase<Orders, Order>
    {
                #region Factory methods
        internal static Orders NewOrderList()
        {
            return DataPortal.CreateChild<Orders>();
        }
        internal static Order GetEditableChildList(object childData)
        {
            return DataPortal.FetchChild<Order>(childData);
        }
        #endregion

        #region Data
        private void Child_Fetch(object childData)
        {
            RaiseListChangedEvents = false;
            foreach (var child in (IList<object>)childData)
                this.Add(GetEditableChildList(child));
            RaiseListChangedEvents = true;
        }
        #endregion
    }
}

