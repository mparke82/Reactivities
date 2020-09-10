using System;

namespace Domain
{
    public class Activity
    {
        public Guid Id { get; set; }
        // Guid because it allows us to create the ID from the server
        // side or client side code

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string City { get; set; }

        public string Venue { get; set; }
    }
}