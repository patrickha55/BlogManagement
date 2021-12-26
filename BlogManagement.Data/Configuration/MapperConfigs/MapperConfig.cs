using AutoMapper;
using BlogManagement.Common.Models.AuthorVMs;
using BlogManagement.Common.Models.CategoryVMs;
using BlogManagement.Common.Models.PostCommentVMs;
using BlogManagement.Common.Models.PostMetaVMs;
using BlogManagement.Common.Models.PostRatingVMs;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Common.Models.TagVMs;
using BlogManagement.Data.Entities;

namespace BlogManagement.Data.Configuration.MapperConfigs
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region CategoryVM mapper configs

            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryForIndexVM>().ReverseMap();

            #endregion

            #region PostVMs mapper configs

            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();
            CreateMap<Post, PostForAdminIndexVM>().ReverseMap();
            CreateMap<Post, PostForIndexVM>().ReverseMap();

            #endregion

            #region PostCommentVMs mapper configs

            CreateMap<PostComment, PostCommentVM>().ReverseMap();
            CreateMap<PostComment, PostCommentForIndexVM>().ReverseMap();

            #endregion

            #region PostMetaVMs mapper configs

            CreateMap<PostMeta, PostMetaVM>().ReverseMap();

            #endregion

            #region PostRatingVMs mapper configs

            CreateMap<PostRating, PostRatingVM>().ReverseMap();

            #endregion

            #region TagVMs mapper configs

            CreateMap<Tag, TagVM>().ReverseMap();

            #endregion

            #region AuthorVM mapper configs

            CreateMap<User, AuthorVM>().ReverseMap();
            CreateMap<User, AuthorForIndexVM>().ReverseMap();

            #endregion
        }
    }
}
