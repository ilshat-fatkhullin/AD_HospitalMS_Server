﻿using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "Medicine", Naming = NamingConvention.UnChanged)]
    public class Medicine
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public string ExpirationDate { get; set; }
    }
}
