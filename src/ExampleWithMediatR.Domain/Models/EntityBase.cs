using System;

namespace ExampleWithMediatR.Domain.Models
{
    public class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
