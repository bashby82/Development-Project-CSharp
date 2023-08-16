﻿using Interview.Service.Models;
using Sparcpoint.SqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Interview.Service.Inventory
{
    public class InventoryRepo : IInventoryRepo
    {
        private readonly ISqlExecutor _sqlExecutor;

        public InventoryRepo(ISqlExecutor sqlExecutor)
        {
            _sqlExecutor = sqlExecutor;
        }

        public List<InventoryTransaction> AddInventory(List<InventoryTransaction> products)
        {
            var result = new List<InventoryTransaction>();
            string sql = "";

            foreach (var product in products)
            {
                // EVAL: Valdiate that product being added exists in Products table (this is better served in a business service layer to avoid dependency between Inventory and Product repo classes)
                // EVAL: -- what happens when product does not exist? Does it get added, or invalid product exception is thrown for that item?
                sql += "INSERT INTO InventoryTransactions (ProductInstanceId, Quantity, StartedTimestamp, CompletedTimestamp, TypeCategory) VALUES (@productId, @qty, @startedTimestamp, @completedTimestamp, @TypeCategory);";
                // EVAL: Create command
                // EVAL: result.Add(await _sqlExecutor.ExecuteAsync(AddCommand))
            }
            // EVAL: result.Add(await ExecuteAsync<List<InventoryTransaction>>(InventoryAddCommand)

            return result;
        }

        public List<Product> GetProductInventory(ProductFilterParams parms)
        {
            List<Product> result = new List<Product>();
            string sql = "SELECT P.InstanceId, P.Name, P.Description, COUNT(T.InstanceId) AS RecordCount, SUM(T.Quantity) AS InventoryQty FROM InventoryTransactions T INNER JOIN Products P ON P.InstanceId = T.ProductInstanceId WHERE 1=1";

            if (parms.Id != 0)
                sql += " AND P.InstanceId = @id";
            if (parms.Name != "")
                sql += " AND P.Name LIKE %@name%";
            // EVAL: Build where clause
            // EVAL: result = await ExecuteAsync(InventoryCountCommand)
            // EVAL: perform mapping
            return result;
        }

        //public Task InventoryCountCommand(IDbConnection conn, IDbTransaction trans)
        //{
        //    // Not familiar with how to interact with SQLServerExecutor
        //}

        //public Task InventoryDeleteCommand(IDbConnection conn, IDbTransaction trans)
        //{
        //    // Not familiar with how to interact with SQLServerExecutor
        //}

        //public Task InventoryAddCommand(IDbConnection conn, IDbTransaction trans)
        //{
        //   // Not familiar with how to interact with SQLServerExecutor
        //}
    }
}
