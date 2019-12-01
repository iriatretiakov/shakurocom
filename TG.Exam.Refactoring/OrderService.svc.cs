using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using log4net;
using log4net.Config;

namespace TG.Exam.Refactoring
{
    public class OrderService : IOrderService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderService));

        readonly string connectionString = ConfigurationManager.ConnectionStrings["OrdersDBConnectionString"].ConnectionString;

        public static IDictionary<string, Order> cache = new Dictionary<string, Order>(); //cache should be static, I suppose

        public OrderService()
        {
            BasicConfigurator.Configure();
        }

        public Order LoadOrder(string orderId)
        {
            try
            {
                Debug.Assert(null != orderId && orderId != "");
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                //return null immediately, if orderId null or empty
                if (string.IsNullOrEmpty(orderId))
                {
                    stopWatch.Stop();
                    logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                    return null;
                }
                lock (cache)
                {
                    if (cache.ContainsKey(orderId))
                    {
                        stopWatch.Stop();
                        logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                        return cache[orderId];
                    }
                }

                //Be aware of Sql injection (should use EF?)
                string queryTemplate =
                  "SELECT OrderId, OrderCustomerId, OrderDate" +
                  "  FROM dbo.Orders where OrderId='{0}'";
                string query = string.Format(queryTemplate, orderId);
                
                //SqlConnection should be called in using statmenets as disposable
                using (SqlConnection connection =
                      new SqlConnection(this.connectionString))
                {
                    SqlCommand command =
                      new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderId = (int)reader[0],
                            OrderCustomerId = (int)reader[1],
                            OrderDate = (DateTime)reader[2]
                        };

                        lock (cache)
                        {
                            if (!cache.ContainsKey(orderId))
                                cache[orderId] = order;
                        }
                        stopWatch.Stop();
                        logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                        return order;
                    }
                }
                stopWatch.Stop();
                logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                return null;
            }
            catch (SqlException ex)
            {
                logger.Error(ex.Message);
                throw new ApplicationException("Error");
            }
        }
    }
}
