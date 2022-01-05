using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Common.Models.PostMetaVMs
{
    public class PostMetaVM : PostMetaCreateVM
    {
        #region Properties

        public long Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Content { get; set; }

        #endregion
    }
}
