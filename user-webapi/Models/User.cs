using System;
using System.Collections.Generic;

namespace userwebapi.Models
{
    public partial class User
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public String UserName { get; set; }
        public DateTime? BirthDate { get; set; }
        public String PassWordHash { get; set; }
        public String PassWordSalt { get; set; }
        public DateTime? CreationDate { get; set; }
        public Boolean? IsActive { get; set; }
    }
}
