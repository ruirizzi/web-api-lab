using userwebapi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace userwebapitests
{
    public static class DbContextMocker
    {
        public static TestDbContext GetTestDbContext(String dbName)
        {
            // Create options for DbContext instance
            DbContextOptions<TestDbContext> options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            // Create instance of DbContext
            TestDbContext dbContext = new TestDbContext(options);

            // Add entities in memory
            dbContext.Seed();


            return dbContext;

        }
    }
}
