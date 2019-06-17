using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omdle.Account.Contracts;
using Omdle.Course.Contracts;
using Omdle.Data.Models;
using Omdle.Web.ViewModels;
using Omdle.Web.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.Controllers
{
    public class ReminderController : OmdleBaseController
    {
        private readonly IUserService _userService;
        private readonly IReminderService _reminderService;

        public ReminderController(IUserService userService, IReminderService reminderService)
        {
            _userService = userService;
            _reminderService = reminderService;
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet]
        public async Task<JsonResult> GetAllReminders([FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var model = await _reminderService.GetReminders(page, threadsPerPage);

            var result = new ReminderListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetReminderById([FromQuery] string id)
        {
            var model = await _reminderService.GetReminderById(id);

            var result = new ReminderListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetReminderByOwnerId([FromQuery] string ownerId, [FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var model = await _reminderService.GetReminderByOwnerId(ownerId, page, threadsPerPage);

            var result = new ReminderListingViewModel(model);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetIncomingRemindersByOwnerId([FromQuery] string ownerId, [FromQuery] int page, [FromQuery] int threadsPerPage = 10)
        {
            var model = await _reminderService.GetIncomingRemindersByOwnerId(ownerId, page, threadsPerPage);

            var result = new ReminderListingViewModel(model);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost] //działa
        public async Task<JsonResult> CreateReminder([FromBody] CreateReminderRequestViewModel request)
        {
            var owner = await _userService.GetUserByIdAsync(request.ownerId);
            var date = DateTime.Parse(request.date); // ISO time

            var reminder = await _reminderService.CreateReminderAsync(request.title, request.description, date, owner);
            var result = new ReminderViewModel(reminder);

            return Json(result);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPut] //działa
        public async Task<JsonResult> UpdateReminder([FromBody] UpdateReminderRequestViewModel request)
        {
            Reminder reminder = await _reminderService.GetOneReminderById(request.id.ToString());

            if (request.date != null)
            {
                reminder.Date = DateTime.Parse(request.date);
            }

            if (request.description != null)
            {
                reminder.Description = request.description;
            }

            if (request.title != null)
            {
                reminder.Title = request.title;
            }

            await _reminderService.UpdateReminder(reminder);

            return Json(reminder);
        }
        
        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete] //działa
        public async Task<JsonResult> DeleteReminder([FromBody] UpdateReminderRequestViewModel request)
        {
            Reminder reminder = await _reminderService.GetOneReminderById(request.id.ToString());

            await _reminderService.DeleteReminder(reminder);

            return Json(reminder);
        }
    }
}
