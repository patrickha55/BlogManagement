using System;

namespace BlogManagement.Common.Models.PostCommentVMs
{
    public class PostCommentForIndexVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public byte Published { get; set; }
        public DateTime CreatedAt { get; set; }
#nullable enable
        public DateTime? PublishedAt { get; set; }
#nullable disable
    }
}
