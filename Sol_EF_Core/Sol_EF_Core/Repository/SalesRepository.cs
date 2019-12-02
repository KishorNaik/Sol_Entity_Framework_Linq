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
                        ?.FirstOrDefault((lesalesOrderHeaderModel) => lesalesOrderHeaderModel.SalesOrderID == salesOrderHeaderModel.SalesOrderID);
                        

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
                    ?.SingleOrDefault((leSalesOrderHeaderModel) => leSalesOrderHeaderModel.SalesOrderID == salesOrderHeaderModel.SalesOrderID);

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
                    ?.Where((leSalesOrderHeader) => leSalesOrderHeader?.SalesOrderId == salesOrderHeaderModel.SalesOrderID)
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
                ?.Where((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.TotalOrderQty >= 10)
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
                ?.Where((leSalesOrderDetails)=>leSalesOrderDetails.OrderQty>=10)
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
                ?.Where((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.TotalOrderQty >= 10)
                ?.OrderBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.SalesOrderID)
                ?.ThenBy((leSalesOrderDetailsModel) => leSalesOrderDetailsModel.ProductId)
                ?.ToList();

                return data;

            });
        }

        // Top By
        public async Task<List<SalesOrderHeaderModel>> TopClauseQueryDemoAsync()
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.Take(5)
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.ToList();

                return data;
            });

        }

        // Pagination
        public async Task<List<SalesOrderHeaderModel>> PaginationQueryDemoAsync()
        {
            return await Task.Run(() => {

                var data =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.AsEnumerable()
                    ?.Skip(1)
                    ?.Take(10)
                    ?.OrderBy((leSalesOrderHeader) => leSalesOrderHeader.SalesOrderId)
                    ?.Select(this.FuncSalesOrdersColumnsMapping())
                    ?.ToList();

                return data;
            
            });
        }

        // Join (One to One)
        public async Task<List<SalesOrderHeaderModel>> JoinQueryDemoAsync()
        {
            return await Task.Run(() => {

                // Get Sales Order Header Data
                var salesOrderHeaderData =
                    adventureWorks2012Context
                    ?.SalesOrderHeader
                    ?.ToList();

                // Get Sales Order Details Data
                var salesOrderDetailsData =
                    adventureWorks2012Context
                    ?.SalesOrderDetail
                    ?.ToList();

                var data =
                    salesOrderHeaderData  // Parent Table Data
                    ?.Join(
                                salesOrderDetailsData, // Child Table Data
                                (leSOH) => leSOH.SalesOrderId, // Parent Primary Key
                                (leSOD) => leSOD.SalesOrderId, // Child Forign Key
                                (leSOH, leSOD) => new SalesOrderHeaderModel()
                                {
                                    SalesOrderID = leSOH.SalesOrderId,
                                    PurchaseOrderNumber = leSOH.PurchaseOrderNumber,
                                    SalesOrderNumber = leSOH.SalesOrderNumber,
                                    SalesOrderDetails = new SalesOrderDetailsModel()
                                    {
                                        SalesOrderID = leSOD.SalesOrderId,
                                        OrderQty = leSOD.OrderQty,
                                        UnitPrice = leSOD.UnitPrice
                                    }
                                }
                            )
                            .OrderBy((leSOH) => leSOH.SalesOrderID)
                            .ThenBy((leSOH) => leSOH.PurchaseOrderNumber)
                            ?.ToList();

                return data;

              
                  

            });
        }

        // Join With Multiple Tables (One to One)
        public async Task<List<SalesOrderHeaderModel>> JoinMultipleQueryDemoAsync()
        {
            return await Task.Run(() => {

                // get Sales Header Data
                var salesOrderHeaderObj =
                     adventureWorks2012Context
                     ?.SalesOrderHeader
                     ?.ToList();

                // get Sales Details Data
                var salesOrderDetailsData =
                    adventureWorks2012Context
                    ?.SalesOrderDetail
                    ?.ToList();

                // get product Model
                var productData =
                    adventureWorks2012Context
                    ?.Product
                    ?.ToList();

                var data =
                        salesOrderHeaderObj
                        ?.Join(
                            salesOrderDetailsData,
                            (leSOH) => leSOH.SalesOrderId,
                            (leSOD) => leSOD.SalesOrderId,
                            (leSOH, leSOD) => new SalesOrderHeaderModel()
                            {
                                SalesOrderID = leSOH.SalesOrderId,
                                PurchaseOrderNumber = leSOH.PurchaseOrderNumber,
                                SalesOrderNumber = leSOH.SalesOrderNumber,
                                SalesOrderDetails = new SalesOrderDetailsModel()
                                {
                                    SalesOrderID = leSOD.SalesOrderId,
                                    ProductId = leSOD.ProductId,
                                    OrderQty = leSOD.OrderQty,
                                    UnitPrice = leSOD.UnitPrice
                                }
                            }
                            )
                        ?.Join(
                                productData,
                                (leSOHM) => leSOHM.SalesOrderDetails.ProductId,
                                (leProduct) => leProduct.ProductId,
                                (leSOHM, leProduct) => new SalesOrderHeaderModel()
                                {
                                    SalesOrderID = leSOHM.SalesOrderID,
                                    PurchaseOrderNumber = leSOHM.PurchaseOrderNumber,
                                    SalesOrderNumber = leSOHM.SalesOrderNumber,
                                    SalesOrderDetails = leSOHM.SalesOrderDetails,
                                    Products = new Model.ProductModel()
                                    {
                                        ProductId = leProduct.ProductId,
                                        Name = leProduct.Name
                                    }
                                }
                            )
                        ?.ToList();


                return data;
            });
        }

        // Group Join
        public async Task<IEnumerable<SalesOrderHeaderModel>> GroupJoinQueryDemo()
        {
            try
            {
                return await Task.Run(() =>
                {
                    // Get Sales Order Header Data
                    var salesOrderHeaderData =
                        adventureWorks2012Context
                        ?.SalesOrderHeader
                        ?.ToList();

                    // Get Sales Order Details Data
                    var salesOrderDetailsData =
                        adventureWorks2012Context
                        ?.SalesOrderDetail
                        ?.ToList();

                    // Perform Group Join
                    var data =
                            salesOrderHeaderData  // Parent Table Data
                            ?.GroupJoin(
                                    salesOrderDetailsData, // Child Table Data
                                    (leSOH) => leSOH.SalesOrderId, // Parent Primary Key
                                    (leSOD) => leSOD.SalesOrderId, // Child Forign Key
                                    (leSOH, leGroupSOD) => new SalesOrderHeaderModel()
                                    {
                                        SalesOrderID = leSOH.SalesOrderId,
                                        PurchaseOrderNumber = leSOH.PurchaseOrderNumber,
                                        SalesOrderNumber = leSOH.SalesOrderNumber,
                                        SalesOrderDetails = new SalesOrderDetailsModel()
                                        {
                                            TotalOrderQty = leGroupSOD.Sum((leSODGroup) => leSODGroup.OrderQty),
                                            TotalUnitPrice = leGroupSOD.Sum((leSODGroup) => leSODGroup.UnitPrice)
                                        }
                                    }
                                )
                                ?.Where((leGroupData) => leGroupData.SalesOrderDetails.TotalOrderQty >= 10)
                                ?.OrderBy((leGroupData) => leGroupData.SalesOrderID)
                                ?.ThenBy((leGroupData) => leGroupData.SalesOrderNumber)
                                ?.ToList();

                    return data;
                });
            }
            finally
            { }
        }

        // Group Join with Having
        public async Task<IEnumerable<SalesOrderHeaderModel>> GroupJoinWithHavingQueryDemo()
        {
            try
            {
                return await Task.Run(() =>
                {
                    // Get Sales Order Header Data
                    var salesOrderHeaderData =
                        adventureWorks2012Context
                        ?.SalesOrderHeader
                        ?.ToList();

                    // Get Sales Order Details Data
                    var salesOrderDetailsData =
                        adventureWorks2012Context
                        ?.SalesOrderDetail
                        ?.ToList();

                    // Perform Group Join
                    var data =
                            salesOrderHeaderData  // Parent Table Data
                            ?.GroupJoin(
                                    salesOrderDetailsData, // Child Table Data
                                    (leSOH) => leSOH.SalesOrderId, // Parent Primary Key
                                    (leSOD) => leSOD.SalesOrderId, // Child Forign Key
                                    (leSOH, leGroupSOD) => new SalesOrderHeaderModel()
                                    {
                                        SalesOrderID = leSOH.SalesOrderId,
                                        PurchaseOrderNumber = leSOH.PurchaseOrderNumber,
                                        SalesOrderNumber = leSOH.SalesOrderNumber,
                                        SalesOrderDetails = new SalesOrderDetailsModel()
                                        {
                                            TotalOrderQty = leGroupSOD.Sum((leSODGroup) => leSODGroup.OrderQty),
                                            TotalUnitPrice = leGroupSOD.Sum((leSODGroup) => leSODGroup.UnitPrice)
                                        }
                                    }
                                )
                                ?.Where((leGroupData) => leGroupData.SalesOrderDetails.TotalOrderQty >= 10)
                                ?.OrderBy((leGroupData) => leGroupData.SalesOrderID)
                                ?.ThenBy((leGroupData) => leGroupData.SalesOrderNumber)
                                ?.ToList();

                    return data;
                });
            }
            finally
            { }
        }
    }
}
