using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Omdle.Web.Controllers
{
    public class UserController : OmdleBaseController
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public UserController(
            IUserService userService,
            IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }

        [HttpGet]
        public async Task<JsonResult> GetUserProfile(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var result = new ViewModels.UserProfileViewModel(user);

            return Json(result);
        }

        [HttpPut]
        public async Task<ViewModels.UserProfileViewModel> UpdateProfile([FromBody]UserProfileViewModel model)
        {
            var currentUser = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            if (!currentUser.Id.ToString().Equals(model.Id))
            {
                throw new InvalidOperationException("You can't edit other user profile info.");
            }

            var user = await _userProfileService.UpdateProfile(model);

            var result = new ViewModels.UserProfileViewModel(user);

            return result;
        }



    }
}