using Omdle.Account.Models;
using System.Threading.Tasks;

namespace Omdle.Account.Contracts
{
    public interface IAuthService
    {
        Task<string> Register(RegisterViewModel model);
        Task<string> SignIn(SignInViewModel model);
        Task<string> SocialSignIn(SocialSignInViewModel model);
    }
}