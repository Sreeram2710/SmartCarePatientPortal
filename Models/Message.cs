using System;
using System.ComponentModel.DataAnnotations;

namespace SmartCarePatientPortal.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        public string Receiver { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
