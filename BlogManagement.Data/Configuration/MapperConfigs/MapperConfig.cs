using AutoMapper;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Data.Entities;

namespace BlogManagement.Data.Configuration.MapperConfigs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Category, CategoryForIndexVM>().ReverseMap();

            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostForIndexVM>().ReverseMap();

            CreateMap<PostComment, PostCommentForIndexVM>().ReverseMap();

            CreateMap<User, AuthorForIndexVM>().ReverseMap();
        }
    }
}
