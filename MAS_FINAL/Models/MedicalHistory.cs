using System.Collections.Generic;

namespace MAS_FINAL.Models
{
    public class MedicalHistory
    {
        public List<Vaccination> Vaccinations { get; set; }
        public List<Illness> Illnesses { get; set; }
    }
}