using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    public class Test
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string LaboratoristKey { get; set; }
        public string Title { get; set; }
        public string PatientKey { get; set; }
        public string Date { get; set; }
        public string ReportURL { get; set; }
    }
}
