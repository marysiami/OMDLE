using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Omdle.Data.Contracts;

namespace Omdle.Data.Services
{
    public class DataService : IDataService
    {
        private readonly OmdleDbContext _dbContext;

        public DataService(OmdleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> GetSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public async Task SaveDbAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
