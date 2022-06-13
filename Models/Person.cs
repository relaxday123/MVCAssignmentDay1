using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace b1.Models
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string BirthPlace { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
        
        public bool IsGraduated { get; set; }

        public string FullName
        {
            get
            {
                return string.Format(FirstName + " " + LastName);
            }
        }
    }
}