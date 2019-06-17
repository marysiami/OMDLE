using Omdle.Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omdle.Web.ViewModels
{
    public class ReminderListingViewModel
    {
        public ReminderListingViewModel(ReminderListing model)
        {
            TotalCount = model.TotalCount;
            Reminders = model.Reminders.Select(x => new ReminderViewModel(x)).ToList();
        }
        public int TotalCount { get; set; }
        public List<ReminderViewModel> Reminders { get; set; }    
    }

}
