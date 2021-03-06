using AutoMapper;
using BlogManagement.Common.Common;
using BlogManagement.Common.DTOs.UserDTOs;
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
    /// Configuration for AutoMapper.
    /// </summary>
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region AuthorVM mapper configs

            CreateMap<User, AuthorVM>().ReverseMap();
            CreateMap<User, AuthorDetailVM>().ReverseMap();
            CreateMap<User, AuthorAdminIndexVM>().ReverseMap();

            #endregion
            #region CategoryVM mapper configs

            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<Category, CategoryForIndexVM>().ReverseMap();
            CreateMap<Category, CategoryCreateVM>().ReverseMap();
            CreateMap<Category, CategoryEditVM>().ReverseMap();

            

            #endregion

            #region PostVMs mapper configs

            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostCreateVM>().ReverseMap();
            CreateMap<Post, PostEditVM>().ReverseMap();
            CreateMap<Post, PostDetailVM>().ReverseMap();
            CreateMap<Post, PostForAdminIndexVM>().ReverseMap();
            CreateMap<Post, PostForIndexVM>().ReverseMap();

            #endregion

            #region PostCommentVMs mapper configs

            CreateMap<PostComment, PostCommentVM>().ReverseMap();
            CreateMap<PostComment, PostCommentCreateVM>().ReverseMap();
            CreateMap<PostComment, PostCommentDetailVM>().ReverseMap();
            CreateMap<PostComment, PostCommentEditVM>().ReverseMap();

            #endregion

            #region PostMetaVMs mapper configs

            CreateMap<PostMeta, PostMetaVM>().ReverseMap();
            CreateMap<PostMeta, PostMetaCreateVM>().ReverseMap();
            CreateMap<PostMeta, PostMetaDetailVM>().ReverseMap();
            CreateMap<PostMeta, PostMetaEditVM>().ReverseMap();

            #endregion

            #region PostRatingVMs mapper configs

            CreateMap<PostRating, PostRatingVM>().ReverseMap();
            CreateMap<PostRating, PostRatingCreateVM>().ReverseMap();
            CreateMap<PostRating, PostRatingDetailVM>().ReverseMap();

            #endregion

            #region TagVMs mapper configs

            CreateMap<Tag, TagVM>().ReverseMap();
            CreateMap<Tag, TagCreateVM>().ReverseMap();
            CreateMap<Tag, TagEditVM>().ReverseMap();

            #endregion

            #region UserDTOs

            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UserRegisterDTO>().ReverseMap();

            #endregion

            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>))
                .ConvertUsing(typeof(PaginatedListConverter<,>));

        }
    }
}
