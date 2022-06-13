using System;
using System.Collections;
using System.Collections.Generic;

namespace cwiczenia8.Models.DTO.Request
{
    public class PrescritptionDTO
    {

        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmail { get; set; }
        public string  PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientBirthDate { get; set; }

        public IEnumerable<MedicamentDTO> Medicaments { get; set; }



    }
}
