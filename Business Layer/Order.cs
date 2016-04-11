using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace Business_Layer
{
    [Serializable]
  public class Order : BusinessBase<Order>
    {
        public static readonly PropertyInfo<int> orderNumber = RegisterProperty(typeof(Order), new PropertyInfo<int>("Order Number"));
        public int OrderNumber
        {
            get
            {
                return GetProperty(orderNumber);
            }
            set
            {
                SetProperty(orderNumber, value);
            }
        }
        public static readonly PropertyInfo<decimal> totalPrice = RegisterProperty(typeof(Order), new PropertyInfo<decimal>("Total Price"));
        public decimal TotalPrice
        {
            get
            {
                return GetProperty(totalPrice);
                            }
            set
            {
                SetProperty(totalPrice, value);
            }
        }

     }
}
