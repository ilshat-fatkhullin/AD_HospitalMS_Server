using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    public class UserType
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public char Key { get; set; }
        public string Type { get; set; }
    }
}
