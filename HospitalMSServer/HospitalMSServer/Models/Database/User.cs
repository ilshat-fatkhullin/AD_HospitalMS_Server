using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    public class User
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string VerificationLink { get; set; }
        public bool IsVerified { get; set; }
        public string AddressKey { get; set; }
        public string BirthDate { get; set; }
        public char Gender { get; set; }
        public string PhotoLink { get; set; }
        public char UserType { get; set; }
    }
}
