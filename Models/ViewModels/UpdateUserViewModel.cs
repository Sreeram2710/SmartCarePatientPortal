using System.ComponentModel.DataAnnotations;

namespace SmartCarePatientPortal.Models.ViewModels
{
    public class UpdateUserViewModel
    {
        public string? Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Role { get; set; }
    }

}
