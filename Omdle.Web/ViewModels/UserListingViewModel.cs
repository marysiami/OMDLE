using Omdle.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class UserListingViewModel
    {
        public UserListingViewModel(UserListing model)
        {
            TotalCount = model.TotalCount;
            Users = model.Users.Select(x => new UserViewModel(x)).ToList();            
        }

        public int TotalCount { get; set; }
        public List<UserViewModel> Users { get; set; }       
    }
}
