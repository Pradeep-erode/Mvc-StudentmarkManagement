using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sample_mvc5.Models
{
    public class Studentclass
    {
        public int StudentId { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public System.DateTime Studentsdob { get; set; }
        public int Age { get; set; }
        public string Favoritesubject { get; set; }
        public string InterestedCourse { get; set; }
        public int Mathsmark { get; set; }
        public int ChemistryMark { get; set; }
        public int CompMark { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
    }
   
}