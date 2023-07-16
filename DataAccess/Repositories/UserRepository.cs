using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _connectionString;

        public UserRepository(AppSettings connectionString)
        {
            this._connectionString = connectionString;
        }

        // Create a new user in the database
        public bool CreateUser(User user)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    string query = "INSERT INTO Users (Id, UserName, Password, Email, FirstName, LastName, Address, PhoneNumber, Salt, IsEmailConfirmed, IsPhoneNumberActive) " +
                                   "VALUES (@Id, @UserName, @Password, @Email, @FirstName, @LastName, @Address, @PhoneNumber, @Salt, @IsEmailConfirmed, @IsPhoneNumberActive)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@Salt", user.Salt);
                        command.Parameters.AddWithValue("@IsEmailConfirmed", user.IsEmailConfirmed);
                        command.Parameters.AddWithValue("@IsPhoneNumberActive", user.IsPhoneNumberActive);

                        int rowAffected = command.ExecuteNonQuery();
                        return rowAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in while creating User:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        // Retrieve a user by their Id
        public User GetUserById(Guid id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapUserFromReader(reader);
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in while getting User by Id:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        public User GetUserByEmail(string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapUserFromReader(reader);
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while getting User by Email:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }


        // Update an existing user in the database
        public bool UpdateUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    string query = "UPDATE Users SET UserName = @UserName, Password = @Password, " +
                                   "Email = @Email, FirstName = @FirstName, LastName = @LastName, " +
                                   "Address = @Address, PhoneNumber = @PhoneNumber WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@Id", user.Id);

                        int rowAffected = command.ExecuteNonQuery();
                        return rowAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in while updating User:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        // Delete a user from the database
        public bool DeleteUser(Guid id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    string query = "DELETE FROM Users WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        connection.Open();
                        int rowAffected = command.ExecuteNonQuery();
                        return rowAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in while deleting User:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("GetAllUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(MapUserFromReader(reader));
                            }
                        }
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred in while gettings Users:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        private User MapUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = (Guid)reader["Id"],
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString(),
                Email = reader["Email"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Address = reader["Address"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString()
            };
        }
    }
}
