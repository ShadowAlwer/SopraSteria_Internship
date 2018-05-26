using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_intership.Models
{
    public class DetailsFullDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string university { get; set; }
        public int yearOfStudy { get; set; }
        public string fieldOfStudy { get; set; }                
        public UserFullDTO user { get; set; }   
    }
}