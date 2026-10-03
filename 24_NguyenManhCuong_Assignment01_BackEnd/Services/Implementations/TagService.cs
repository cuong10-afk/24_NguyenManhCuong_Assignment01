using _24_NguyenManhCuong_Assignment01_BackEnd.Data;
using _24_NguyenManhCuong_Assignment01_BackEnd.Models;
using _24_NguyenManhCuong_Assignment01_BackEnd.Services.Interfaces;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Services.Implementations
{
    /// <summary>
    /// TagService is registered as Singleton (Singleton Pattern).
    /// Tags are reference/lookup data that rarely change, making them ideal for singleton caching.
    /// </summary>
    public class TagService : ITagService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public TagService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IQueryable<Tag> GetAll()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FUNewsManagementContext>();
            return context.Tags.AsQueryable();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FUNewsManagementContext>();
            return await context.Tags.FindAsync(id);
        }
    }
}
