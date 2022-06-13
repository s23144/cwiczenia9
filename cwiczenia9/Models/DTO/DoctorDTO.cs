using System.ComponentModel.DataAnnotations;

namespace cwiczenia8.Models.DTO
{
    public class DoctorDTO
    {
        
        [Required(ErrorMessage ="Imie wymagane")]
        [MaxLength(100, ErrorMessage = "Max długość 100")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko wymagane")]
        [MaxLength(100,ErrorMessage ="Max długość 100")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Email wymagany")]
        [EmailAddress]
        public string  Email { get; set; }

    }
}
