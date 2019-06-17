using Omdle.Data.Models.Account;

namespace Omdle.Web.ViewModels
{
    public class UserProfileViewModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserProfileViewModel(OmdleUser user)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}