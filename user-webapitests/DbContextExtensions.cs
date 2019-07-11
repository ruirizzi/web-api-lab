using System;
using System.Collections.Generic;
using System.Text;
using userwebapi.Models;

namespace userwebapitests
{
    public static class DbContextExtensions
    {
        public static void Seed(this testDbContext dbContext)
        {
            dbContext.User.AddRange(
                new User() { Name = "Ken Thompson", UserName = "kthompson", BirthDate = new DateTime(1943, 2, 4), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now },
                new User() { Name = "Andrew Tanenbaum", UserName = "atanen", BirthDate = new DateTime(1944, 3, 16), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now },
                new User() { Name = "Margaret Hamilton", UserName = "mhamil", BirthDate = new DateTime(1936, 8, 17), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now }
                );

            dbContext.SaveChanges();
        }
    }
}
