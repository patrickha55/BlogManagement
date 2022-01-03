using BlogManagement.Data.Entities;

namespace BlogManagement.Contracts.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetAllTagIdsAndTitles();
    }
}
