using System;
using System.Collections.Generic;
using System.Text;

namespace Sol_EF_Core.Model
{
    public class SalesOrderDetailsModel
    {


        public int SalesOrderDetailsID { get; set; }

        public int OrderQty { get; set; } 

        public decimal UnitPrice { get; set; }

        public int? SalesOrderID { get; set; }

        public int TotalOrderQty { get; set; }

        public decimal TotalUnitPrice { get; set; }

        public int ProductId { get; set; }
    }
}
