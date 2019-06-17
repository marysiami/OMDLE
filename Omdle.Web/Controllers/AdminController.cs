using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omdle.Account.Contracts;
using Omdle.Web.ViewModels;
using Omdle.Web.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.Controllers
{
    public class AdminController : OmdleBaseController
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public JsonResult GetUsers([FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {          
            var model =  _userService.GetUsers(page, threadsPerPage);

            var result = new UserListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetStudents([FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var model = _userService.GetStudents(page, threadsPerPage);

            var result = new UserListingViewModel(model);

            return Json(result);
        }


        [HttpGet]
        public JsonResult GetTeachers([FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
           
            var model =  _userService.GetTeachers(page, threadsPerPage);

            var result = new UserListingViewModel(model);

            return Json(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost] 
        public async Task CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            var student = await _userService.GetUserByIdAsync(request.id);

            await _userService.CreateTeacher(student);
        }
    }
}
