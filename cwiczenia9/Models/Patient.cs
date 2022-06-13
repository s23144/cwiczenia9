using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cwiczenia8.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirsName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
