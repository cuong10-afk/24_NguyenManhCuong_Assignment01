using _24_NguyenManhCuong_Assignment01_BackEnd.Models;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Services.Interfaces
{
    public interface ITagService
    {
        IQueryable<Tag> GetAll();
        Task<Tag?> GetByIdAsync(int id);
    }
}
