using Omdle.Data.Models.Account;
using System.Collections.Generic;

namespace Omdle.Account.Models
{
    public class UserListing
    {
        public List<OmdleUser> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
