using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_intership.Models
{
    public class SaveSkillsRequestDTO
    {
        public Int64[] skillsIds { get; set; }
        public string userId { get; set; }
    }
}