using System.IO;
using System.Threading.Tasks;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Omdle.Data.Contracts;
using Omdle.Data.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace Omdle.Account.Services
{
    /// <summary>Class UserProfileService.
    /// Implements the <see cref="Omdle.Account.Contracts.IUserProfileService"/></summary>
    public class UserProfileService : IUserProfileService
    {
        private readonly IDataService _dataService;

        /// <summary>Initializes a new instance of the <see cref="T:Omdle.Account.Services.UserProfileService"/> class.</summary>
        /// <param name="dataService">The data service.</param>
        public UserProfileService(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>Updates the profile.</summary>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;OmdleUser&gt;.</returns>
        public async Task<OmdleUser> UpdateProfile(UserProfileViewModel model)
        {
            var user = await _dataService.GetSet<OmdleUser>().FirstOrDefaultAsync(x => x.Id.ToString() == model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _dataService.SaveDbAsync();
            return user;
        }
    }
}