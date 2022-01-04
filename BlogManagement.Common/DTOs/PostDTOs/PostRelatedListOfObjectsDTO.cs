using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using System.Collections.Generic;

namespace BlogManagement.Common.DTOs.PostDTOs
{
    public class PostRelatedListOfObjectsDTO
    {
        public IEnumerable<CategoryVM> CategoryDTOs { get; set; }
        public IEnumerable<TagVM> TagDTOs { get; set; }
        public IEnumerable<PostVM> PostDTOs { get; set; }
    }
}
