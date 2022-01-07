using AutoMapper;
using BlogManagement.Common.Common;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Data.Configuration.MapperConfigs
{
    public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>,
        PaginatedList<TDestination>> where TSource : class where TDestination : class
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

            return new PaginatedList<TDestination>(source.TotalCount, source.CurrentPage, source.PageSize, collection.ToList());
        }
    }
}
