using ArangoDB.Client;

namespace HospitalMSServer.Models.Database
{
    [CollectionProperty(CollectionName = "TimeSlot", Naming = NamingConvention.UnChanged)]
    public class TimeSlot
    {
        [DocumentProperty(Identifier = IdentifierType.Key)]
        public string Key { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string[] Days { get; set; }
        public int RepeatWeeks { get; set; }

    }
}
