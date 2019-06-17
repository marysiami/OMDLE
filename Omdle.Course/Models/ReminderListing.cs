using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Course.Models
{
    /// <summary>Class ReminderListing.</summary>
    public class ReminderListing
    {
        /// <summary>Gets or sets the reminders.</summary>
        /// <value>The reminders.</value>
        public List<Data.Models.Reminder> Reminders { get; set; }

        /// <summary>Gets or sets the total count.</summary>
        /// <value>The total count.</value>
        public int TotalCount { get; set; }
    }
}
