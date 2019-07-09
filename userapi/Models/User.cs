using System;

namespace UserApi.Models
{
    public class User
    {
        public Int64 id { get; set; }
        public String name { get; set; }
        public String userName { get; set; }
        public DateTime birthDate { get; set; }
        public String passWordHash { get; set; }
        public String passWordSalt { get; set; }
        public DateTime creationDate { get; set; }
        public Boolean isActive { get; set; }
    }
}