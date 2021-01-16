using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company_UI.Models
{
    public class Employee
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [DisplayName("Date Of Employment")]
        public DateTime? DateOfEmployment { get; set; }
        public string Picture { get; set; }
    }
}
