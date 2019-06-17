using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Omdle.Data.Contracts
{
    /// <summary>Interface IDataService</summary>
    public interface IDataService
    {
        /// <summary>Gets the set.</summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>DbSet&lt;T&gt;.</returns>
        DbSet<T> GetSet<T>() where T : class;

        /// <summary>Saves the database asynchronous.</summary>
        /// <returns>Task.</returns>
        Task SaveDbAsync();
    }
}
