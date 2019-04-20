using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    public class Appointment
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string PatientKey { get; set; }
        public string DoctorKey { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ReportURL { get; set; }
    }
}
