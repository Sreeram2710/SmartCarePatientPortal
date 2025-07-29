using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCarePatientPortal.Models
{
    public class EmergencyContact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string? Relationship { get; set; }

        // Navigation
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
    }
}
