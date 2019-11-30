using System;
using System.Collections.Generic;
using System.Text;

namespace Sol_EF_Core.Model
{
    public class SalesOrderHeaderModel
    {
        public int? SalesOrderID { get; set; }

        public String SalesOrderNumber { get; set; }

        public String PurchaseOrderNumber { get; set; }


        #region Navigation Property
        public List<SalesOrderDetailsModel> ListSalesOrderDetails { get; set; }
        #endregion 

    }
}
