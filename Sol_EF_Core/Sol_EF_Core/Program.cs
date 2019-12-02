using Sol_EF_Core.Repository;
using System;
using System.Threading.Tasks;

namespace Sol_EF_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Task.Run(async () => {

                SalesRepository salesRepository = new SalesRepository(new DbModels.DBEntities.AdventureWorks2012Context());

                //var selectQueryDemo = await salesRepository.SelectQueryDemoAsync();

                //var firstOrDefault = await salesRepository.FirstOrDefaultDemoQueryAsync(new Model.SalesOrderHeaderModel()
                //{
                //    SalesOrderID = 1
                //});

                //var singleOrDefault = await salesRepository.SingleOrDefaultDemoQueryAsync(new Model.SalesOrderHeaderModel()
                //{
                //    SalesOrderID = 1
                //});

                //var orderBy = await salesRepository.OrderByDemoQueryAsync();

                //var orderByDec = await salesRepository.OrderByDecendingQueryDemo();

                //var whereClause = await salesRepository.WhereClauseQueryDemo(new Model.SalesOrderHeaderModel()
                //{
                //    SalesOrderID = 1
                //});

                // var groupByQueryDemo = await salesRepository.GroupByQueryDemo();

                //var groupByHavingQueryDemo = await salesRepository.GroupByHavingOrderByQueryDemo();

                //var allQuery = await salesRepository.AllClauseQueryDemo();

                //var topBy = await salesRepository.TopClauseQueryDemoAsync();

                //var pagination = await salesRepository.PaginationQueryDemoAsync();

                //var join = await salesRepository.JoinQueryDemoAsync();

                //var joinMultiple = await salesRepository.JoinMultipleQueryDemoAsync();

                var groupJoin = await salesRepository.GroupJoinQueryDemo();

                //var joinMultipleQuery = await salesRepository.GroupJoinWithHavingQueryDemo();

            }).Wait();

        }
    }
}
