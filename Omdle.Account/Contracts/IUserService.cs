using Omdle.Account.Models;
using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Omdle.Account.Contracts
{
    public interface IUserService
    {
        Task<OmdleUser> GetUserByIdAsync(string id);
        Task<OmdleUser> GetUserByNameAsync(string userName);
        UserListing GetUsers(int skip = 0, int take = 10);
        UserListing GetStudents(int skip = 0, int take = 10);
        UserListing GetTeachers(int skip = 0, int take = 10);
        Task CreateTeacher(OmdleUser student);
    
    }
}
