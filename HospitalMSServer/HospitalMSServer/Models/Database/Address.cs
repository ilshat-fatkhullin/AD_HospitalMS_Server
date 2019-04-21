using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "Address", Naming = NamingConvention.UnChanged)]
    public class Address
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Appartment { get; set; }
    }
}
