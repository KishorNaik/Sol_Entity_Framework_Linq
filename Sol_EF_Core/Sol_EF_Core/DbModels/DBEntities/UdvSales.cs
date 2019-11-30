using System;
using System.Collections.Generic;

namespace Sol_EF_Core.DbModels.DBEntities
{
    public partial class UdvSales
    {
        public int SalesOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string SalesOrderNumber { get; set; }
        public int? TotalOrderQty { get; set; }
        public decimal? TotalUnitPrice { get; set; }
    }
}
