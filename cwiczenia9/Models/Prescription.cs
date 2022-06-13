using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cwiczenia8.Models
{
    public class Prescription
    {
        public Prescription()
        {
            PrescriptionsMedicaments = new HashSet<Prescription_Medicament>();
        }

       [Key]
        public int IdPrescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
       [Required]
        public DateTime DueDate { get; set; }
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }

        [ForeignKey("IdPatient")]
        public virtual Patient PatientNavigation { get; set; }
        [ForeignKey("IdDoctor")]
        public virtual Doctor DoctorNavigation { get; set; }

        public virtual ICollection<Prescription_Medicament> PrescriptionsMedicaments { get; set; }
    }
}
