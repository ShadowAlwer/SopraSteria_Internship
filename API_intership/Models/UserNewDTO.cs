using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_intership.Models
{
    class UserNewDTO
    {
        public string email { get; set; }
        public string name { get; set; }

        
        public UserNewDTO(string email, string name) {
            this.email = email;
            this.name = name;
        }
    }
}
