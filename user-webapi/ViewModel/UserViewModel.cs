using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace userwebapi.ViewModel
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PassWordHash { get; set; }
        public string PassWordSalt { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
