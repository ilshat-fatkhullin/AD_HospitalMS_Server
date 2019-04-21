using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "Test", Naming = NamingConvention.UnChanged)]
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
