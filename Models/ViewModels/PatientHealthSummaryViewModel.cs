using SmartCarePatientPortal.Models;

public class PatientHealthSummaryViewModel
{
    public IEnumerable<Appointment> UpcomingAppointments { get; set; }
    public IEnumerable<Appointment> PastAppointments { get; set; }
    public IEnumerable<MedicalRecord> HealthStats { get; set; }
    public List<string> Alerts { get; set; }
}
