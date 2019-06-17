using Omdle.Course.Models;
using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Omdle.Course.Contracts
{
    /// <summary>Interface IReminderService</summary>
    public interface IReminderService
    {
        /// <summary>Creates the reminder asynchronous.</summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="date">The date.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>Task&lt;Data.Models.Reminder&gt;.</returns>
        Task<Data.Models.Reminder> CreateReminderAsync(string title, string description, DateTime date, OmdleUser owner);
        /// <summary>Gets the reminders.</summary>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        Task<ReminderListing> GetReminders(int skip = 0, int take = 10);
        /// <summary>Gets the reminder by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        Task<ReminderListing> GetReminderById(string id);
        /// <summary>Gets the one reminder by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;Data.Models.Reminder&gt;.</returns>
        Task<Data.Models.Reminder> GetOneReminderById(string id);
        /// <summary>Gets the reminder by owner identifier.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        Task<ReminderListing> GetReminderByOwnerId(string ownerId, int skip = 0, int take = 10);
        /// <summary>Gets the incoming reminders by owner identifier.</summary>
        /// <param name="ownerId">The owner identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <returns>Task&lt;ReminderListing&gt;.</returns>
        Task<ReminderListing> GetIncomingRemindersByOwnerId(string ownerId, int skip = 0, int take = 10);
        /// <summary>Deletes the reminder.</summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Task.</returns>
        Task DeleteReminder(Data.Models.Reminder reminder);
        /// <summary>Updates the reminder.</summary>
        /// <param name="reminder">The reminder.</param>
        /// <returns>Task.</returns>
        Task UpdateReminder(Data.Models.Reminder reminder);
    }
}
