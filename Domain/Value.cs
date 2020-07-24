using System;

namespace Domain
{
    public class Value
    {
        // Entity Framework is convention based
        // When we give a property inside one of our entities the name of ID
        // it is automatically used as the primary key
        // database will automatically generate a number
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
