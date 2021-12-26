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
        public string Content { get; set; }
#nullable enable
        [Display(Name = "Date Published")]
        public DateTime? PublishedAt { get; set; }
        public double? Rating { get; set; }
#nullable disable
        public List<TagVM> Tags { get; set; } = new();
        public List<PostMetaDetailVM> PostMetas { get; set; } = new();
    }
}
