using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCarePatientPortal.Models.ViewModels
{
    public class AddMedicalRecordViewModel
    {
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime VisitDate { get; set; } = DateTime.Today;

        [Required]
        [StringLength(200)]
        public string Diagnosis { get; set; }

        [StringLength(500)]
        public string? Symptoms { get; set; }

        [StringLength(1000)]
        public string? Treatment { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(200)]
        public string? TestName { get; set; }
    }
}
