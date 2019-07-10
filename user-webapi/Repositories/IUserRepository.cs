using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userwebapi.ViewModel;
using userwebapi.Models;

namespace userwebapi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(Int64? id);
        Task<Int64> AddUser(User user);
        Task<Int64> DeleteUser(Int64? id);
        Task UpdateUser(User user);
    }
}
