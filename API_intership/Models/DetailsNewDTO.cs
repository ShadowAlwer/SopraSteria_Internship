using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_intership.Models
{
    public class DetailsNewDTO
    {
        public string fieldOfStudy { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string university { get; set; }
        public int yearOfStudy { get; set; }
    }
}