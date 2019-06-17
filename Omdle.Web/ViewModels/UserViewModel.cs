using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(OmdleUser x)
        {
            Id = x.Id.ToString();
            Firstname = x.FirstName;
            Lastname = x.LastName;
            Email = x.Email;            
        }

        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
    }
}

