using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    public class Credential
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }

        public string PasswordHash { get; set; }
    }
}
