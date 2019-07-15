using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userwebapi.Models;

namespace userwebapi.Repositories
{
    public class UserRepository : IUserRepository
    {
        TestDbContext db;
        public UserRepository(TestDbContext _db)
        {
            db = _db;
        }

        public async Task<Int64> AddUser(User user)
        {
            if (db != null)
            {
                await db.User.AddAsync(user);
                await db.SaveChangesAsync();

                return user.Id;
            }

            return 0;
        }

        public async Task<Int64> DeleteUser(Int64? id)
        {
            Int32 result = 0;

            if (db != null)
            {
                User user = await db.User.FirstOrDefaultAsync(x => x.Id == id);

                if (user != null)
                {
                    db.User.Remove(user);

                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<User> GetUser(Int64? id)
        {
            if (db != null)
            {
                //var users = await db.User.ToListAsync();
                //return users.Where(x => x.Id == id).First();

                return await (from u in db.User
                              where u.Id == id
                              select new User
                              {
                                  Id = u.Id,
                                  Name = u.Name,
                                  UserName = u.UserName,
                                  BirthDate = u.BirthDate,
                                  CreationDate = u.CreationDate,
                                  IsActive = u.IsActive,
                                  PassWordHash = u.PassWordHash,
                                  PassWordSalt = u.PassWordSalt

                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<User>> GetUsers()
        {
            if (db != null)
            {
                return await db.User.ToListAsync();
            }

            return null;
        }

        public async Task UpdateUser(User user)
        {
            if (db != null)
            {
                db.User.Update(user);

                await db.SaveChangesAsync();
            }
        }

    }
}
