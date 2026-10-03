using _24_NguyenManhCuong_Assignment01_BackEnd.Models;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> HasNewsArticlesAsync(short categoryId);
        IQueryable<Category> GetAllWithDetails();
    }
}
