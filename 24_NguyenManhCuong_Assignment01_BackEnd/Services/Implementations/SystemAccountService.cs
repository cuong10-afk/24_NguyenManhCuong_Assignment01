using _24_NguyenManhCuong_Assignment01_BackEnd.Models;
using _24_NguyenManhCuong_Assignment01_BackEnd.Repositories.Interfaces;
using _24_NguyenManhCuong_Assignment01_BackEnd.Services.Interfaces;

namespace _24_NguyenManhCuong_Assignment01_BackEnd.Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;

        public SystemAccountService(ISystemAccountRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<SystemAccount> GetAll() => _repository.GetAllWithDetails();

        public async Task<SystemAccount?> GetByIdAsync(short id)
            => await _repository.GetByIdAsync(id);

        public async Task<SystemAccount?> GetByEmailAsync(string email)
            => await _repository.GetByEmailAsync(email);

        public async Task<SystemAccount> CreateAsync(SystemAccount account)
        {
            await _repository.AddAsync(account);
            await _repository.SaveChangesAsync();
            return account;
        }

        public async Task<SystemAccount> UpdateAsync(SystemAccount account)
        {
            _repository.Update(account);
            await _repository.SaveChangesAsync();
            return account;
        }

        public async Task<bool> DeleteAsync(short id)
        {
            if (await _repository.HasNewsArticlesAsync(id))
                return false;

            var account = await _repository.GetByIdAsync(id);
            if (account == null) return false;

            _repository.Delete(account);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasNewsArticlesAsync(short id)
            => await _repository.HasNewsArticlesAsync(id);
    }
}
