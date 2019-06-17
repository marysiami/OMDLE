using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Omdle.Course.Contracts;
using Omdle.Course.Models;
using Omdle.Data.Contracts;
using Omdle.Data.Models;
using Omdle.Data.Models.Account;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Course.Services
{
    /// <summary>Class ReminderService.
    /// Implements the <see cref="T:Omdle.Course.Contracts.IReminderService"/></summary>
    public class ReminderService : IReminderService
    {
        /// <summary>The data service</summary>
        private readonly IDataService _dataService;
        /// <summary>The user manager</summary>
        private readonly UserManager<OmdleUser> _userManager;

        /// <summary>Initializes a new instance of the <see cref="T:Omdle.Course.Services.ReminderService"/> class.</summary>
        /// <param name="dataService">The data service.</param>
        /// <param name="userManager">The user manager.</param>
        public ReminderService(IDataService dataService, UserManager<OmdleUser> userManager)
        {
            _dataService = dataService;
            _userManager = userManager;
        }

        /// <summary>create reminder as an asynchronous operation.</summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="date">The date.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>Task&lt;Data.Models.Reminder&gt;.</returns>
        public async Task<Reminder> CreateReminderAsync(
            string title,
            string description,
            DateTime date,
            OmdleUser owner)
        {
            var reminder = new Reminder
            {
                Title = title,
                Description = description,
                Date = date,
                OwnerId = owner.Id,
            };
            await _dataService.GetSet<Reminder>().AddAsync(reminder);
            await _dataService.SaveDbAsync();

            return reminder;
        }

        /// <summary>Gets the incoming reminders by owner identifier.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        public async Task<ReminderListing> GetIncomingRemindersByOwnerId(string ownerId, int skip = 0, int take = 10)
        {
            var result = new ReminderListing();

            var query = _dataService.GetSet<Reminder>();

            var reminders = await query
            .Where(x => x.OwnerId.ToString() == ownerId && x.Date > DateTime.Now)
            .Include(x => x.OwnerUser)
            .Skip(skip * take)
            .Take(take)
            .ToListAsync();

            result.Reminders = reminders;
            result.TotalCount = reminders.Count();

            return result;
        }

        /// <summary>Gets the reminder by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        public async Task<ReminderListing> GetReminderById(string id)
        {
            var result = new ReminderListing();

            var query = _dataService.GetSet<Reminder>();

            var reminders = await query
            .Where(x => x.Id.ToString() == id)
            .Include(x => x.OwnerUser)
            .ToListAsync();

            result.Reminders = reminders;
            result.TotalCount = reminders.Count();

            return result;
        }

        /// <summary>Gets the one reminder by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Data.Models.Reminder&gt;.</returns>
        public async Task<Reminder> GetOneReminderById(string id)
        {
            var model = await _dataService.GetSet<Reminder>()
                .FirstOrDefaultAsync(x => x.Id.ToString() == id);

            return model;
        }

        /// <summary>Gets the reminder by owner identifier.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        public async Task<ReminderListing> GetReminderByOwnerId(string ownerId, int skip = 0, int take = 10)
        {
            var result = new ReminderListing();

            var query = _dataService.GetSet<Reminder>();

            var reminders = await query
            .Where(x => x.OwnerId.ToString() == ownerId)
            .Include(x => x.OwnerUser)
            .Skip(skip * take)
            .Take(take)
            .ToListAsync();

            result.Reminders = reminders;
            result.TotalCount = reminders.Count();

            return result;
        }

        /// <summary>Gets the reminders.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        public async Task<ReminderListing> GetReminders(int skip = 0, int take = 10)
        {
            var result = new ReminderListing();

            var query = _dataService.GetSet<Reminder>();

            result.TotalCount = query.Count();
            result.Reminders = await query
                .Include(x => x.OwnerUser)
                .Skip(skip * take)
                .Take(take)
                .ToListAsync();

            return result;
        }

        /// <summary>Updates the reminder.</summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Task.</returns>
        public async Task UpdateReminder(Reminder reminder)
        {
            _dataService.GetSet<Reminder>().Update(reminder);
            await _dataService.SaveDbAsync();
        }

        /// <summary>Deletes the reminder.</summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Task.</returns>
        public async Task DeleteReminder(Reminder reminder)
        {
            _dataService.GetSet<Reminder>().Remove(reminder);
            await _dataService.SaveDbAsync();
        }
    }
}
