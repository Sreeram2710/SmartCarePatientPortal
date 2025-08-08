using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarePatientPortal.Models
{
    public enum LabTestStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public class LabTest
    {
        [Key]
        public int LabTestId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public string TestName { get; set; } = string.Empty;

        public DateTime OrderedDate { get; set; } = DateTime.Now;

        [Required]
        public LabTestStatus Status { get; set; } = LabTestStatus.Pending;

        public string ResultFilePath { get; set; } = string.Empty;

        public DateTime? AcceptedByPatient { get; set; }

        public DateTime? CompletedByAdmin { get; set; }

        public string Comments { get; set; } = string.Empty;

        // Navigation
        [ForeignKey("PatientId")]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Patient? Patient { get; set; } 

        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }
    }
}
