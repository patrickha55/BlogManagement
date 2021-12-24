using AutoMapper;
using BlogManagement.Common.Models.PostVMs;
using BlogManagement.Data.Entities;

namespace BlogManagement.Data.Configuration.MapperConfigs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Post, PostVM>().ReverseMap();
        }
    }
}
