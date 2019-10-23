using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Data.Interface
{
    public interface IUser : IDisposable
    {
        List<User> GetUsers();
        User GetUserById(int id);
        User CreateUser(User item);
        void UpdateUser(User item);
        void DeleteUser(User item);
    }
}
