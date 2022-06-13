using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cwiczenia8.Models
{
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirsName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public virtual ICollection<Prescription> Prescription { get; set; }
    }
}
