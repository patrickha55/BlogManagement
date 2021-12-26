using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Common.Models.TagVMs;

namespace BlogManagement.Common.Models.PostVMs
{
    public class PostDetailVM : PostForIndexVM
    {
        [Display(Name = "Meta Title")]
        public string MetaTitle { get; set; }

        public string Slug { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
#nullable disable
        public IEnumerable<TagVM> Tags { get; set; }
        public IEnumerable<PostMetaVM> PostMetas { get; set; }
    }
}
