using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppSettings connectionString;

        public OrderRepository(AppSettings connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateOrder(Order order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Orders (Id, UserId, OrderDate, TotalAmount, Status) VALUES (@Id, @UserId, @OrderDate, @TotalAmount, @Status)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", order.Id);
                command.Parameters.AddWithValue("@UserId", order.UserId);
                command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                command.Parameters.AddWithValue("@Status", order.Status);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool UpdateOrderStatus(Guid orderId, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Orders SET Status = @Status WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@Id", orderId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteBook(Guid bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", bookId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public List<Order> GetOrdersByUserId(Guid userId)
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Orders WHERE UserId = @UserId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = (Guid)reader["Id"],
                            UserId = (Guid)reader["UserId"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            TotalAmount = (decimal)reader["TotalAmount"],
                            Status = (string)reader["Status"]
                        });
                    }
                }
            }

            return orders;
        }

    }
}
