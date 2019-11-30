using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.SQL
{
    public class DAL
    {
        private SqlConnection GetConnection() 
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var con = new SqlConnection(connectionString);

            con.Open();

            return con;
        }

        private DataSet GetData(string sql)
        {
            var ds = new DataSet();

            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    using (var adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(ds);
                    }
                }
            }

            return ds;
        }

        private void Execute(string sql)
        {
            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllOrders()
        {
            //Don't sure that result should contains Ids
            var sql = "SELECT * FROM [dbo].[Orders]";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
        public DataTable GetAllOrdersWithCustomers()
        {
            //Don't sure that result should contains Ids
            var sql = @"SELECT * FROM [dbo].[Orders] o
JOIN[dbo].[Customers] c ON c.CustomerId = o.OrderCustomerId";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public DataTable GetAllOrdersWithPriceUnder(int price)
        {
            //Don't sure that result should contains Ids
            var sql = $@"SELECT *
FROM[dbo].[Orders] o
JOIN[dbo].[OrdersItems] oi on oi.OrderId = o.OrderId
JOIN[dbo].[Items] i on i.ItemId = oi.ItemId
WHERE i.ItemPrice > {price}";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public void DeleteCustomer(int orderId)
        {
            //suppose order entity can't exists w\o customer
            var sql = $@"DECLARE @CustomerId INT;
SELECT @CustomerId = [OrderCustomerId] FROM [dbo].[Orders] WHERE [OrderId] = {orderId}
DELETE [dbo].[Orders] WHERE [OrderId] = {orderId}
DELETE [dbo].[OrdersItems] WHERE [OrderId] = {orderId}
DELETE [dbo].[Customers] WHERE [CustomerId] = @CustomerId";

            Execute(sql);
        }

        internal DataTable GetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders()
        {
            var sql = @"SELECT i.ItemId, i.ItemName, i.ItemPrice, COUNT(oi.ItemId) AS Count
FROM [dbo].[Items] i 
LEFT JOIN [dbo].[OrdersItems] oi ON i.ItemId = oi.ItemId
GROUP BY i.ItemId, i.ItemName, i.ItemPrice";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
    }
}
