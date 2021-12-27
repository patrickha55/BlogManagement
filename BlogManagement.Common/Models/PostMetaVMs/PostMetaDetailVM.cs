using BlogManagement.Common.Models.PostVMs;

namespace BlogManagement.Common.Models.PostMetaVMs
{
    public class PostMetaDetailVM : PostMetaVM
    {
        /// <summary>
        /// A post which this post meta belongs to.
        /// </summary>
        public PostVM Post { get; set; }
    }
}
