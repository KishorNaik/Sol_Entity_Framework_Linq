using Sol_EF_Core.DbModels.DBEntities;
using Sol_EF_Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Sol_EF_Core.Repository
{
    public class SalesRepository
    {
        private readonly AdventureWorks2012Context adventureWorks2012Context = null;

        public SalesRepository(AdventureWorks2012Context adventureWorks2012Context)
        {
            this.adventureWorks2012Context = adventureWorks2012Context;
        }

        #region Private Methods
        private Func<SalesOrderHeader,SalesOrderHeaderModel> FuncSalesOrdersColumnsMapping()
        {
            Func<SalesOrderHeader, SalesOrderHeaderModel> funcData = (salesOrderHeaderObj) => new SalesOrderHeaderModel()
            {
                PurchaseOrderNumber = salesOrderHeaderObj?.PurchaseOrderNumber,
                SalesOrderID = salesOrderHeaderObj?.SalesOrderId,
                SalesOrderNumber = salesOrderHeaderObj?.SalesOrderNumber
            };

            return funcData;
              
        }
        #endregion 

        // Select Query
        public async Task<IEnumerable<SalesOrderHeaderModel>> SelectQueryDemoAsync()
        {
            try
            {
                return await Task.Run(() => {

                    var data =
                        adventureWorks2012Context
                        ?.SalesOrderHeader
                        ?.AsEnumerable()
                        ?.Select(this.FuncSalesOrdersColumnsMapping())
                        ?.ToList();

                    return data;

                });
                
            }
            catch
            {
                throw;
            }
        }

        //  First Or Defult
        public async Task<SalesOrderHeaderModel> FirstOrDefaultDemoQueryAsync(SalesOrderHeaderModel salesOrderHeaderModel)
        {
            return await Task.Run(() => {

                var data =
                        adventureWorks2012Context
                        ?.SalesOrderHeader
                        ?.AsEnumerable()
                        ?.Select(this.FuncSalesOrdersColumnsMapping())
                        ?.FirstOrDefault((lesalesOrderHeaderModel) => lesalesOrderHeaderModel.SalesOrderNumber == salesOrderHeaderModel.SalesOrderNumber);
                        

                return data;
            
            });
        }

        // Single Or Default
        public async Task<SalesOrderHeaderModel> SingleOrDefaultDemoQueryAsync(SalesOrderHeaderModel salesOrderHeaderModel)
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.SingleOrDefault((leSalesOrderHeaderModel) => leSalesOrderHeaderModel.SalesOrderNumber == salesOrderHeaderModel.SalesOrderNumber);

                return data;
            
            });
        }

        // Order By
        public async Task<List<SalesOrderHeaderModel>>  OrderByDemoQueryAsync()
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.OrderBy((leSalesOrderHeader) => leSalesOrderHeader.PurchaseOrderNumber)
                    ?.ThenBy((leSaleOrderHeader)=>leSaleOrderHeader.OrderDate)
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.ToList();

                return data;

            });
        }

        // Order decendeing 
        public async Task<List<SalesOrderHeaderModel>> OrderByDecendingQueryDemo()
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.OrderByDescending((leSalesOrderHeader) => leSalesOrderHeader.PurchaseOrderNumber)
                    ?.ThenByDescending((leSalesOrderHeader) => leSalesOrderHeader.OrderDate)
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.ToList();

                return data;
            });
        }

        // Where Clause
        public async Task<List<SalesOrderHeaderModel>> WhereClauseQueryDemo(SalesOrderHeaderModel salesOrderHeaderModel)
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.Where((leSalesOrderHeader) => leSalesOrderHeader?.PurchaseOrderNumber == salesOrderHeaderModel.PurchaseOrderNumber)
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.ToList();

                return data;
            
            });
        }

        // Group By
        public async Task<List<SalesOrderDetailsModel>> GroupByQueryDemo()
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderDetail
                    ?.AsEnumerable()
                    ?.GroupBy((leSalesOrderDetails) => new
                    {
                        leSalesOrderDetails.SalesOrderId,
                        leSalesOrderDetails.ProductId
                    })
                    ?.Select((leSalesOrderDetailsGroup) => new SalesOrderDetailsModel()
                    {
                        SalesOrderID = leSalesOrderDetailsGroup.Key.SalesOrderId,
                        ProductId = leSalesOrderDetailsGroup.Key.ProductId,
                        TotalOrderQty = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.OrderQty),
                        TotalUnitPrice = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.UnitPrice)

                    })
                    ?.ToList();

                return data;
            
            });
        }

        // Group By & Having Clause & Order By

        public async Task<List<SalesOrderDetailsModel>> GroupByHavingOrderByQueryDemo()
        {
            return await Task.Run(() => {

                var data =
                adventureWorks2012Context
                ?.SalesOrderDetail
                ?.AsEnumerable()
                ?.GroupBy((leSalesOrderDetails) => new
                {
                    leSalesOrderDetails.SalesOrderId,
                    leSalesOrderDetails.ProductId
                })
                ?.Select((leSalesOrderDetailsGroup) => new SalesOrderDetailsModel()
                {

                    SalesOrderID = leSalesOrderDetailsGroup.Key.SalesOrderId,
                    ProductId = leSalesOrderDetailsGroup.Key.ProductId,
                    TotalOrderQty = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.OrderQty),
                    TotalUnitPrice = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.UnitPrice)

                })
                ?.Where((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.TotalOrderQty >= 1000)
                ?.OrderBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.SalesOrderID)
                ?.ThenBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.ProductId)
                ?.ToList();

                return data;

            });
        }

        // All Clause
        public async Task<List<SalesOrderDetailsModel>>  AllClauseQueryDemo()
        {
            return await Task.Run(() => {

                var data =
                adventureWorks2012Context
                ?.SalesOrderDetail
                ?.AsEnumerable()
                ?.Where((leSalesOrderDetails)=>leSalesOrderDetails.OrderQty>=100)
                ?.GroupBy((leSalesOrderDetails) => new
                {
                    leSalesOrderDetails.SalesOrderId,
                    leSalesOrderDetails.ProductId
                })
                ?.Select((leSalesOrderDetailsGroup) => new SalesOrderDetailsModel()
                {

                    SalesOrderID = leSalesOrderDetailsGroup.Key.SalesOrderId,
                    ProductId = leSalesOrderDetailsGroup.Key.ProductId,
                    TotalOrderQty = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.OrderQty),
                    TotalUnitPrice = leSalesOrderDetailsGroup.Sum((leSalesOrderDetails) => leSalesOrderDetails.UnitPrice)

                })
                ?.Where((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.TotalOrderQty >= 1000)
                ?.OrderBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.SalesOrderID)
                ?.ThenBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.ProductId)
                ?.ToList();

                return data;

            });
        }


    }
}
