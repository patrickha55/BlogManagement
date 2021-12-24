using System;

namespace BlogManagement.Data.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
#nullable enable
        public DateTime? UpdatedAt { get; set; }
#nullable disable
    }
}
