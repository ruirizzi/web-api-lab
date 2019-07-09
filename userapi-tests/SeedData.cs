using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Models;

namespace userapi_tests
{
    public static class SeedData
    {
        public static void PopulateTestData(UserContext context)
        {
            context.User.Add(new User()
            {
                id = 1,
                name = "Rui Rizzi",
                userName = "rrizzi",
                birthDate = new DateTime(1989, 3, 31),
                isActive = true,
                passWordHash = "testPwHash",
                passWordSalt = "testPwSalt",
                creationDate = DateTime.Now
            });
            context.User.Add(new User()
            {
                id = 2,
                name = "Nikola Tesla",
                userName = "ntesla",
                birthDate = new DateTime(1856, 7, 10),
                isActive = true,
                passWordHash = "testPwHash",
                passWordSalt = "testPwSalt",
                creationDate = DateTime.Now
            });
            context.User.Add(new User()
            {
                id = 3,
                name = "Alan Turing",
                userName = "aturing",
                birthDate = new DateTime(1912, 6, 23),
                isActive = true,
                passWordHash = "testPwHash",
                passWordSalt = "testPwSalt",
                creationDate = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
