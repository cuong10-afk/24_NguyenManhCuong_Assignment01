using _24_NguyenManhCuong_Assignment01_BackEnd.Models;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Repositories.Interfaces
{
    public interface INewsArticleRepository : IGenericRepository<NewsArticle>
    {
        IQueryable<NewsArticle> GetAllWithDetails();
        Task<NewsArticle?> GetByIdWithDetailsAsync(string id);
        Task<IEnumerable<NewsArticle>> GetByCreatedByIdAsync(short accountId);
        Task<IEnumerable<NewsArticle>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task UpdateTagsAsync(string newsArticleId, List<int> tagIds);
    }
}
