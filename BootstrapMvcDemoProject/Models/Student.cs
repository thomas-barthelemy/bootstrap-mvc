using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapMvcDemoProject.Models
{
    public class Student
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
    }
}