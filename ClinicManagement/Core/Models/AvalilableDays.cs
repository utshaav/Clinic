namespace ClinicManagement.Core.Models
{
    public class AvailableDays
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Days { get; set; }
    }
}