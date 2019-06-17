using System.Threading.Tasks;
using Omdle.Account.Models;
using Omdle.Data.Models.Account;

namespace Omdle.Account.Contracts
{
    public interface IUserProfileService
    {
        Task<OmdleUser> UpdateProfile(UserProfileViewModel model);
    }
}