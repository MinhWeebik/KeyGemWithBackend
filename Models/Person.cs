using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KeyGem.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
    }
}