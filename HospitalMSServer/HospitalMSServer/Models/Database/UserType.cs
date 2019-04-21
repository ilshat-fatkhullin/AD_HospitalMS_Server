using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "UserType", Naming = NamingConvention.UnChanged)]
    public class UserType
    {
        public const string ADMIN = "A";

        public const string PATIENT = "P";
        public const string DOCTOR = "D";
        public const string NURSE = "N";
        public const string RECEPTIONIST = "R";
        public const string PHARMACIST = "H";
        public const string ACCOUNTANT = "A";
        public const string LABORATORIST = "L";

        [DocumentProperty(Identifier = IdentifierType.Key)]
        public char Key { get; set; }
        public string Type { get; set; }
    }
}
