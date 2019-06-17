using Omdle.Account.Models;
using System.Threading.Tasks;

namespace Omdle.Account.Contracts
{
    public interface IAuthValidationService
    {
        Task ValidateRegisterViewModel(RegisterViewModel model);
        Task ValidateSignInViewModel(SignInViewModel model);
        Task ValidateSocialSignInViewModel(SocialSignInViewModel model);
    }
}
