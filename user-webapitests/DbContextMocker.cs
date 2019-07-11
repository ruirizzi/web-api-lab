using userwebapi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace userwebapitests
{
    public static class DbContextMocker
    {
        public static testDbContext GetTestDbContext(String dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<testDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new testDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;

        }
    }
}
