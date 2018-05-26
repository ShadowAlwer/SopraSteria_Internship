using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_intership.Models
{
    public class UserCreatedDTO
    {
        public string email { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
    }
}