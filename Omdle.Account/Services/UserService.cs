using Microsoft.AspNetCore.Identity;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Omdle.Data.Contracts;
using Omdle.Data.Models.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Account.Services
{
    /// <summary>Class UserService.
    /// Implements the <see cref="Omdle.Account.Contracts.IUserService"/></summary>
    public class UserService : IUserService
    {
        private readonly UserManager<OmdleUser> _userManager;
        private readonly IDataService _dataService;
        /// <summary>Initializes a new instance of the <see cref="T:Omdle.Account.Services.UserService"/> class.</summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="dataService">The data service.</param>
        public UserService(
            UserManager<OmdleUser> userManager, IDataService dataService)
        {
            _userManager = userManager;
            _dataService = dataService;
        }

        /// <summary>Gets the students.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>UserListing.</returns>
        public UserListing GetStudents(int skip = 0, int take = 10)
        {
            var obj = _dataService.GetSet<OmdleUser>().ToList();
            var model = obj
                .Where(x => _userManager.IsInRoleAsync(x, "Student").Result)
                .Skip(skip * take)
                .Take(take)
                .ToList();

            var list = new UserListing
            {
                Users = model,
                TotalCount = model.Count()
            };

            return list;
        }

        /// <summary>Gets the teachers.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>UserListing.</returns>
        public UserListing GetTeachers(int skip = 0, int take = 10)
        {
            var obj = _dataService.GetSet<OmdleUser>().ToList();
            var model = obj
                .Where(x => _userManager.IsInRoleAsync(x, "Teacher").Result)
                .Skip(skip * take)
                .Take(take)
                .ToList();

            var list = new UserListing
            {
                Users = model,
                TotalCount = model.Count()
            };

            return list;
        }

        /// <summary>get user by identifier as an asynchronous operation.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;OmdleUser&gt;.</returns>
        public async Task<OmdleUser> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }

        /// <summary>get user by name as an asynchronous operation.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>Task&lt;OmdleUser&gt;.</returns>
        public async Task<OmdleUser> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        /// <summary>Gets the users.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>UserListing.</returns>
        public UserListing GetUsers(int skip = 0, int take = 10)
        {
            var obj = _dataService.GetSet<OmdleUser>()
                .Skip(skip * take)
                .Take(take)
                .ToList();            

            var list = new UserListing
            {
                Users = obj,
                TotalCount = obj.Count()
            };

            return list;
        }

        /// <summary>Creates the teacher.</summary>
        /// <param name="student">The student.</param>
        /// <returns>Task.</returns>
        public async Task CreateTeacher(OmdleUser student)
        {            
            await _userManager.RemoveFromRoleAsync(student, "Student");
            await _userManager.AddToRoleAsync(student, "Teacher");
            await _dataService.SaveDbAsync();            
        }
    }
}
