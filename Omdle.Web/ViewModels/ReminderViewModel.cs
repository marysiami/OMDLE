using Omdle.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class ReminderViewModel
    {
        public ReminderViewModel(Reminder x)
        {
            Id = x.Id.ToString();
            Title = x.Title;
            Description = x.Description;
            Date = x.Date;
            OwnerId = x.OwnerId.ToString();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string OwnerId { get; set; }
    }
}
