using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppSettings _connectionString;

        public BookRepository(AppSettings connectionString)
        {
            this._connectionString = connectionString;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = (Guid)reader["Id"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"],
                            Category = (string)reader["Category"],
                            PublicationDate = (DateTime)reader["PublicationDate"]
                        });
                    }
                }
            }

            return books;
        }

        public bool AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Books (Id, Title, Author, Description, Price, Quantity, Category, PublicationDate) VALUES (@Id, @Title, @Author, @Description, @Price, @Quantity, @Category, @PublicationDate)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", book.Id);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@Quantity", book.Quantity);
                command.Parameters.AddWithValue("@Category", book.Category);
                command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public bool UpdateBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Books SET Title = @Title, Author = @Author, Description = @Description, Price = @Price, Quantity = @Quantity, Category = @Category, PublicationDate = @PublicationDate WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Description", book.Description);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@Quantity", book.Quantity);
                command.Parameters.AddWithValue("@Category", book.Category);
                command.Parameters.AddWithValue("@PublicationDate", book.PublicationDate);
                command.Parameters.AddWithValue("@Id", book.Id);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool DeleteBook(Guid bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Books WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", bookId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public Book GetBookById(Guid bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", bookId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Book
                        {
                            Id = (Guid)reader["Id"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"],
                            Category = (string)reader["Category"],
                            PublicationDate = (DateTime)reader["PublicationDate"]
                        };
                    }
                }
            }

            return new Book(); 
        }

    }
}
