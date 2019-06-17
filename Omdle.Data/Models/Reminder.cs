using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omdle.Data.Models
{
    public class Reminder
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid OwnerId { get; set; }
        public virtual OmdleUser OwnerUser { get; set; }
    }
}
