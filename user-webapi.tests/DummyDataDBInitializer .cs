using System;
using System.Collections.Generic;
using System.Text;
using userwebapi.Models;

namespace userwebapi.tests
{
    public class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        { }

        public void Seed(testDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.User.AddRange(
                new User() { Name = "Ken Thompson", UserName = "kthompson", BirthDate = new DateTime(1943,2,4), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now },
                new User() { Name = "Andrew Tanenbaum", UserName = "atanen", BirthDate = new DateTime(1944,3,16), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now },
                new User() { Name = "Margaret Hamilton", UserName = "mhamil", BirthDate = new DateTime(1936,8,17), IsActive = true, PassWordHash = "psHash", PassWordSalt = "pwSalt", CreationDate = DateTime.Now }
                );

            context.SaveChanges();
        }

        public void Clear(testDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
    }
}
