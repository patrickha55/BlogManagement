namespace BlogManagement.Common.Models.PostMetaVMs
{
    public class PostMetaCreateVM
    {
        public string Key { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// Id of a post which this post belongs to.
        /// </summary>
        public long PostId { get; set; }
    }
}
