using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user);
        User GetUserById(Guid userId);
        bool UpdateUser(User user);
        bool DeleteUser(Guid id);
        List<User> GetAllUsers();
        User GetUserByEmail(string email);
    }
}
